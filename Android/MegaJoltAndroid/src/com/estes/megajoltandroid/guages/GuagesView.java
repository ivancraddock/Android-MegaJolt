package com.estes.megajoltandroid.guages;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapShader;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.LinearGradient;
import android.graphics.Matrix;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.RadialGradient;
import android.graphics.RectF;
import android.graphics.Shader;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Parcelable;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;

import com.estes.megajoltandroid.R;

/**
 * This class constructs the layout of each gauge.
 * @author Bryan Daul, Ivan Craddock, Troy Wellington
 * @version 1.0
 * 
 */
public class GuagesView extends View {

	private static final String TAG = GuagesView.class.getSimpleName();

	// drawing tools
	private RectF rimRect;
	private Paint rimPaint;
	private Paint rimCirclePaint;

	private RectF faceRect;
	private Bitmap faceTexture;
	private Paint facePaint;
	private Paint rimShadowPaint;

	private Paint scalePaint;
	private RectF scaleRect;

	private Paint titlePaint;
	private Path titlePath;

	private Paint handPaint;
	private Path handPath;
	private Paint handScrewPaint;
	private Path handScrewPath;

	private Paint backgroundPaint;
	// end drawing tools

	private Bitmap background; // holds the cached static part

	// initial scale configuration
	private float totalDegrees = 200.0f;
	private int majTicks = 5;
	private int minValue = 0;
	private int maxValue = 100;
	private int totalTicks = (maxValue / 2) + 1;
	private float degreesPerNick = totalDegrees / 10;
	
	@SuppressWarnings("unused")
	private int centerDegree = totalTicks * (1 / 2); // the one in the top center (12 o'clock)

	// hand dynamics -- all are angular expressed in RPMs
	private boolean handInitialized = false;
	private float handPosition = 0;// current hand position;
	private float handTarget = 0;// desired position of hand (current input value);
	private float handVelocity = 0.0f;
	private float handAcceleration = 0.0f;
	private long lastHandMoveTime = -1L;

	// Gauge Title
	private String title = "";

	public GuagesView(Context context) {
		super(context);
		init();
	}

	public GuagesView(Context context, AttributeSet attrs) {
		super(context, attrs);
		init();
	}

	public GuagesView(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		init();
	}

	@Override
	protected void onAttachedToWindow() {
		super.onAttachedToWindow();
	}

	@Override
	protected void onDetachedFromWindow() {
		super.onDetachedFromWindow();
	}

	@Override
	protected void onRestoreInstanceState(Parcelable state) {
		Bundle bundle = (Bundle) state;
		Parcelable superState = bundle.getParcelable("superState");
		super.onRestoreInstanceState(superState);

		handInitialized = bundle.getBoolean("handInitialized");
		handPosition = bundle.getFloat("handPosition");
		handTarget = bundle.getFloat("handTarget");
		handVelocity = bundle.getFloat("handVelocity");
		handAcceleration = bundle.getFloat("handAcceleration");
		lastHandMoveTime = bundle.getLong("lastHandMoveTime");
	}

	@Override
	protected Parcelable onSaveInstanceState() {
		Parcelable superState = super.onSaveInstanceState();

		Bundle state = new Bundle();
		state.putParcelable("superState", superState);
		state.putBoolean("handInitialized", handInitialized);
		state.putFloat("handPosition", handPosition);
		state.putFloat("handTarget", handTarget);
		state.putFloat("handVelocity", handVelocity);
		state.putFloat("handAcceleration", handAcceleration);
		state.putLong("lastHandMoveTime", lastHandMoveTime);
		return state;
	}

	private void init() {
		initDrawingTools();
	}

	void setTitle(String titleIn) {
		this.title = titleIn;
	}

	private void initDrawingTools() {
		rimRect = new RectF(0.1f, 0.1f, 0.9f, 0.9f);

		rimPaint = new Paint();
		rimPaint.setFlags(Paint.ANTI_ALIAS_FLAG);
		rimPaint.setShader(new LinearGradient(0.40f, 0.0f, 0.60f, 1.0f, Color
				.rgb(0xf0, 0xf5, 0xf0), Color.rgb(0x30, 0x31, 0x30),
				Shader.TileMode.CLAMP));

		rimCirclePaint = new Paint();
		rimCirclePaint.setAntiAlias(true);
		rimCirclePaint.setStyle(Paint.Style.STROKE);
		rimCirclePaint.setColor(Color.argb(0x4f, 0x33, 0x36, 0x33));
		rimCirclePaint.setStrokeWidth(0.005f);

		float rimSize = 0.02f;
		faceRect = new RectF();
		faceRect.set(rimRect.left + rimSize, rimRect.top + rimSize,
				rimRect.right - rimSize, rimRect.bottom - rimSize);

		faceTexture = BitmapFactory.decodeResource(getContext().getResources(),
				R.drawable.bg_texture);
		BitmapShader paperShader = new BitmapShader(faceTexture,
				Shader.TileMode.MIRROR, Shader.TileMode.MIRROR);
		Matrix paperMatrix = new Matrix();
		facePaint = new Paint();
		facePaint.setFilterBitmap(true);
		paperMatrix.setScale(1.0f / faceTexture.getWidth(),
				1.0f / faceTexture.getHeight());
		paperShader.setLocalMatrix(paperMatrix);
		facePaint.setStyle(Paint.Style.FILL);
		facePaint.setShader(paperShader);

		rimShadowPaint = new Paint();
		rimShadowPaint.setShader(new RadialGradient(0.5f, 0.5f, faceRect
				.width() / 2.0f,
				new int[] { 0x00000000, 0x0029b10b, 0x5029b10b }, new float[] {
						0.96f, 0.96f, 0.99f }, Shader.TileMode.MIRROR));
		rimShadowPaint.setStyle(Paint.Style.FILL);

		scalePaint = new Paint();
		scalePaint.setStyle(Paint.Style.STROKE);
		scalePaint.setColor(0xff29b10b);
		scalePaint.setStrokeWidth(0.005f);
		scalePaint.setAntiAlias(true);

		scalePaint.setTextSize(0.05f);
		scalePaint.setTypeface(Typeface.SERIF);
		scalePaint.setTextScaleX(1f);
		scalePaint.setTextAlign(Paint.Align.CENTER);

		float scalePosition = 0.10f;
		scaleRect = new RectF();
		scaleRect.set(faceRect.left + scalePosition, faceRect.top
				+ scalePosition, faceRect.right - scalePosition,
				faceRect.bottom - scalePosition);

		titlePaint = new Paint();
		titlePaint.setColor(0xff29b10b);
		titlePaint.setAntiAlias(true);
		titlePaint.setTypeface(Typeface.DEFAULT_BOLD);
		titlePaint.setTextAlign(Paint.Align.CENTER);
		titlePaint.setTextSize(0.07f);
		titlePaint.setTextScaleX(0.8f);

		titlePath = new Path();
		titlePath.addArc(new RectF(0.24f, 0.24f, 0.76f, 0.76f), -180.0f,
				-180.0f);

		handPaint = new Paint();
		handPaint.setAntiAlias(true);
		handPaint.setColor(0xff9e1710);
		handPaint.setShadowLayer(0.01f, -0.005f, -0.005f, 0x7f9e1710);
		handPaint.setStyle(Paint.Style.FILL);

		handPath = new Path();
		handPath.moveTo(-245, -75);
		handPath.lineTo(0, -7);
		handPath.lineTo(5, -75);
		handPath.lineTo(2, 140);
		handPath.lineTo(-2, 140);
		handPath.lineTo(-5, -75);
		handPath.addCircle(0f, 0f, 15f, Path.Direction.CW);

		handScrewPaint = new Paint();
		handScrewPaint.setAntiAlias(true);
		handScrewPaint.setColor(0xff000000);
		handScrewPaint.setStyle(Paint.Style.FILL);

		handScrewPath = new Path();
		handScrewPath.addCircle(0, 0, 8, Path.Direction.CW);

		backgroundPaint = new Paint();
		backgroundPaint.setFilterBitmap(true);
	}

	@Override
	protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
		// Log.d(TAG, "Width spec: " + MeasureSpec.toString(widthMeasureSpec));
		// Log.d(TAG, "Height spec: " + MeasureSpec.toString(heightMeasureSpec));

		int widthMode = MeasureSpec.getMode(widthMeasureSpec);
		int widthSize = MeasureSpec.getSize(widthMeasureSpec);

		int heightMode = MeasureSpec.getMode(heightMeasureSpec);
		int heightSize = MeasureSpec.getSize(heightMeasureSpec);

		int chosenWidth = chooseDimension(widthMode, widthSize);
		int chosenHeight = chooseDimension(heightMode, heightSize);

		int chosenDimension = Math.min(chosenWidth, chosenHeight);

		setMeasuredDimension(chosenDimension, chosenDimension);
	}

	private int chooseDimension(int mode, int size) {
		if (mode == MeasureSpec.AT_MOST || mode == MeasureSpec.EXACTLY) {
			return size;
		} else {
			return getPreferredSize();
		}
	}

	private int getPreferredSize() {
		return 300;
	}

	private void drawRim(Canvas canvas) {
		canvas.drawOval(rimRect, rimPaint);
		canvas.drawOval(rimRect, rimCirclePaint);
	}

	private void drawFace(Canvas canvas) {
		canvas.drawOval(faceRect, facePaint);
		canvas.drawOval(faceRect, rimCirclePaint);
		canvas.drawOval(faceRect, rimShadowPaint);
	}

	private void drawScale(Canvas canvas) {

		canvas.drawOval(scaleRect, scalePaint);

		canvas.save(Canvas.MATRIX_SAVE_FLAG);
		canvas.rotate(totalDegrees / -2, 0.5f, 0.5f);
		for (int i = 0; i < totalTicks; ++i) {
			float y1 = scaleRect.top;
			float y2 = y1 - 0.020f;

			canvas.drawLine(0.5f, y1, 0.5f, y2, scalePaint);

			if (i % majTicks == 0) {
				int value = i;

				if (value >= minValue && value <= maxValue) {
					value = value * (maxValue / (totalTicks - 1));
					String valueString = Integer.toString(value);
					canvas.drawLine(0.5f, y1, 0.5f, y2 - 0.01f, scalePaint);
					canvas.drawText(valueString, 0.5f, y2 - 0.015f, scalePaint);
				}
			}

			canvas.rotate(degreesPerNick, 0.5f, 0.5f);
		}
		canvas.restore();

	}

	private void drawTitle(Canvas canvas) {
		canvas.drawTextOnPath(title, titlePath, 0.0f, 0.0f, titlePaint);
	}

	private float degreeToAngle(float degree) {
		return ((totalDegrees / -2) + (degree * (totalDegrees / maxValue)));
	}

	private void drawHand(Canvas canvas) {
		if (handInitialized) {
			canvas.save(Canvas.MATRIX_SAVE_FLAG);

			canvas.translate(canvas.getClipBounds().centerX(), canvas
					.getClipBounds().centerY());

			float handAngle = degreeToAngle(handPosition) - 180;

			canvas.save();
			canvas.rotate(handAngle);
			canvas.drawPath(handPath, handPaint);
			canvas.restore();

			canvas.drawPath(handScrewPath, handScrewPaint);
			canvas.restore();

		}
	}

	private void drawBackground(Canvas canvas) {
		if (background == null) {
			Log.w(TAG, "Background not created");
		} else {
			canvas.drawBitmap(background, 0, 0, backgroundPaint);
		}
	}

	@Override
	protected void onDraw(Canvas canvas) {
		if (background == null) {
			background = Bitmap.createBitmap(getWidth(), getHeight(),
					Bitmap.Config.ARGB_8888);
			Canvas backgroundCanvas = new Canvas(background);
			float scale = getWidth();
			backgroundCanvas.scale(scale, scale);
			drawRim(backgroundCanvas);
			drawFace(backgroundCanvas);
			drawScale(backgroundCanvas);
			drawTitle(backgroundCanvas);
		}

		drawBackground(canvas);

		drawHand(canvas);

		if (handNeedsToMove()) {
			moveHand();
		}
	}

	@Override
	protected void onSizeChanged(int w, int h, int oldw, int oldh) {
		// Log.d(TAG, "Size changed to " + w + "x" + h);
		regenerateBackground();
		setHandPath(w, h);
	}

	private void regenerateBackground() {
		if (background == null)
			return;
		if (background != null) {
			background.recycle();
		}

		background = Bitmap.createBitmap(getWidth(), getHeight(),
				Bitmap.Config.ARGB_8888);
		Canvas backgroundCanvas = new Canvas(background);
		float scale = getWidth();
		backgroundCanvas.scale(scale, scale);

		drawRim(backgroundCanvas);
		drawFace(backgroundCanvas);
		drawScale(backgroundCanvas);
		drawTitle(backgroundCanvas);
	}

	private boolean handNeedsToMove() {
		return Math.abs(handPosition - handTarget) > 0.01f;
	}

	private void moveHand() {
		if (!handNeedsToMove()) {
			return;
		}

		if (lastHandMoveTime != -1L) {
			long currentTime = System.currentTimeMillis();
			float delta = (currentTime - lastHandMoveTime) / 1500.0f;

			float direction = Math.signum(handVelocity);

			if (Math.abs(handVelocity) < 90.0f) {
				handAcceleration = 5.0f * (handTarget - handPosition);
			} else {
				handAcceleration = 0.0f;
			}

			handPosition += handVelocity * delta;
			handVelocity += handAcceleration * delta;

			if ((handTarget - handPosition) * direction < 0.01f * direction) {
				handPosition = handTarget;
				handVelocity = 0.0f;
				handAcceleration = 0.0f;
				lastHandMoveTime = -1L;
			} else {
				lastHandMoveTime = System.currentTimeMillis();
			}

			invalidate();
		} else {
			lastHandMoveTime = System.currentTimeMillis();
			moveHand();
		}
	}

	// Checks raw value against min/max scale settings
	// Sets hand target
	public void setHandTarget(float rpm) {
		if (rpm < minValue) {
			rpm = minValue;
		} else if (rpm > maxValue) {
			rpm = maxValue;
		}
		handTarget = rpm;
		handInitialized = true;
		moveHand();
	}

	public void setHandPath(int w, int h) {
		handPath.reset();
		handPath.moveTo((float) (w * .015) * -1, (float) -(h * .17));// bottom
																		// right
		handPath.lineTo(0, (float) -(h * .18));// bottom center
		handPath.lineTo((float) (w * .015), (float) -(h * .17));// bottom left
		handPath.lineTo((float) (w * .005), (float) (h * .35));// top left
		handPath.lineTo((float) (w * .005) * -1, (float) (h * .35));// top right
		handPath.lineTo((float) (w * .01) * -1, (float) -(h * .17));// completion
		handPath.addCircle(0f, 0f, (float) (w * .045), Path.Direction.CW);

		handScrewPath = new Path();
		handScrewPath.addCircle(0, 0, (float) (w * .02), Path.Direction.CW);

	}

	public void setMax(int max) {
		this.maxValue = max;
		setScale();

	}

	public void setDegreesOfScale(float degs) {
		this.totalDegrees = degs;
		setScale();

	}

	public void setTotalTicks(int tTicks) {
		this.totalTicks = tTicks + 1;
		setScale();

	}

	public void setMajTicks(int mTicks) {
		this.majTicks = mTicks;
		setScale();

	}

	public void setScale() {

		degreesPerNick = totalDegrees / totalTicks;
		centerDegree = totalTicks * (1 / 2);

		regenerateBackground();

	}

}
