package com.estes.megajoltandroid.guages;

import com.estes.megajoltandroid.R;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapShader;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.LightingColorFilter;
import android.graphics.LinearGradient;
import android.graphics.Matrix;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.RadialGradient;
import android.graphics.RectF;
import android.graphics.Shader;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Handler;
import android.os.Parcelable;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;

/**
 * This class draws the user indicator lights to be placed by GuagesActivity on
 * the screen
 * 
 * @author Bryan Daul, Ivan Craddock, Troy Wellington
 * @version 1.0
 * 
 */
public class IndicatorLED extends View {
	private static int DEFAULT_TITLE_COLOR = -1349230327;
	private static boolean DEFAULT_VALUE = false;
	private Paint facePaint;
	private RectF faceRect;
	private Bitmap faceTexture;
	
	@SuppressWarnings("unused")
	private Handler handler;
	
	private int offColor = 5242880;
	private int onColor = 16711680;
	private Paint rimCirclePaint;
	private Paint rimPaint;
	private RectF rimRect;
	private Paint rimShadowPaint;
	private String title = "LED";
	private int titleColor = DEFAULT_TITLE_COLOR;
	private Paint titlePaint;
	private Path titlePath;
	private boolean value = DEFAULT_VALUE;
	private static final String TAG = IndicatorLED.class.getSimpleName();

	public IndicatorLED(Context paramContext) {
		super(paramContext);
		init();
	}

	public IndicatorLED(Context paramContext, AttributeSet paramAttributeSet) {
		super(paramContext, paramAttributeSet);
		init();
	}

	public IndicatorLED(Context paramContext, AttributeSet paramAttributeSet,
			int paramInt) {
		super(paramContext, paramAttributeSet, paramInt);
		init();
	}

	private int chooseDimension(int paramInt1, int paramInt2) {
		if ((paramInt1 == -2147483648) || (paramInt1 == 1073741824))
			return paramInt2;
		return getPreferredSize();
	}

	private void drawFace(Canvas paramCanvas) {

		int i;
		if (this.value)
			i = this.onColor;
		else
			i = this.offColor;

		LightingColorFilter localLightingColorFilter = new LightingColorFilter(
				100, i);
		this.facePaint.setColorFilter(localLightingColorFilter);
		paramCanvas.drawOval(this.faceRect, this.facePaint);

		int colorFilter = -5197648;
		String hex = "#" + Integer.toHexString(colorFilter);
		Log.d(TAG, hex);
		return;
	}

	private void drawRim(Canvas paramCanvas) {
		paramCanvas.drawOval(this.rimRect, this.rimCirclePaint);
		paramCanvas.drawOval(this.faceRect, this.rimShadowPaint);
	}

	private int getPreferredSize() {
		return 50;
	}

	private void init() {
		this.handler = new Handler();
		initDrawingTools();
	}

	private void initDrawingTools() {
		this.rimRect = new RectF(0.07F, 0.07F, 0.93F, 0.93F);
		this.rimPaint = new Paint();
		this.rimPaint.setFlags(1);
		this.rimPaint.setShader(new LinearGradient(0.4F, 0.0F, 0.6F, 1.0F,
				Color.rgb(240, 245, 240), Color.rgb(48, 49, 48),
				Shader.TileMode.CLAMP));
		this.rimCirclePaint = new Paint();
		this.rimCirclePaint.setAntiAlias(true);
		this.rimCirclePaint.setStyle(Paint.Style.STROKE);
		this.rimCirclePaint.setColor(Color.argb(79, 51, 54, 51));
		this.rimCirclePaint.setStrokeWidth(0.005F);
		this.faceRect = new RectF();
		this.faceRect.set(0.1F + this.rimRect.left, 0.1F + this.rimRect.top,
				this.rimRect.right - 0.1F, this.rimRect.bottom - 0.1F);
		this.faceTexture = BitmapFactory.decodeResource(getContext()
				.getResources(), R.drawable.indicator);
		BitmapShader localBitmapShader = new BitmapShader(this.faceTexture,
				Shader.TileMode.MIRROR, Shader.TileMode.MIRROR);
		Matrix localMatrix = new Matrix();
		this.facePaint = new Paint();
		this.facePaint.setFilterBitmap(true);
		localMatrix.setScale(1.0F / this.faceTexture.getWidth(),
				1.0F / this.faceTexture.getHeight());
		localBitmapShader.setLocalMatrix(localMatrix);
		this.facePaint.setStyle(Paint.Style.FILL);
		this.facePaint.setShader(localBitmapShader);
		this.rimShadowPaint = new Paint();
		Paint localPaint = this.rimShadowPaint;
		float f = this.faceRect.width() / 2.0F;
		int[] arrayOfInt = new int[3];
		arrayOfInt[1] = 1280;
		arrayOfInt[2] = 1342178560;
		localPaint.setShader(new RadialGradient(0.5F, 0.5F, f, arrayOfInt,
				new float[] { 0.96F, 0.96F, 0.99F }, Shader.TileMode.MIRROR));
		this.rimShadowPaint.setStyle(Paint.Style.FILL);
		this.titlePaint = new Paint();
		this.titlePaint.setColor(this.titleColor);
		this.titlePaint.setAntiAlias(true);
		this.titlePaint.setTypeface(Typeface.DEFAULT_BOLD);
		this.titlePaint.setTextAlign(Paint.Align.CENTER);
		this.titlePaint.setTextSize(0.1F);
		this.titlePaint.setTextScaleX(0.8F);
		this.titlePath = new Path();
		this.titlePath.moveTo(0.3F, 0.85F);
		this.titlePath.lineTo(0.7F, 0.85F);
	}

	public int getOffColor() {
		return this.offColor;
	}

	public int getOnColor() {
		return this.onColor;
	}

	public int getSegmentOffColor() {
		return this.offColor;
	}

	public int getSegmentOnColor() {
		return this.onColor;
	}

	public String getTitle() {
		return this.title;
	}

	public int getTitleColor() {
		return this.titleColor;
	}

	public boolean getValue() {
		return this.value;
	}

	protected void onDraw(Canvas paramCanvas) {
		float f = getWidth();
		paramCanvas.save(1);
		paramCanvas.scale(f, f);
		drawRim(paramCanvas);
		drawFace(paramCanvas);
		paramCanvas.restore();
	}

	protected void onMeasure(int paramInt1, int paramInt2) {
		int i = View.MeasureSpec.getMode(paramInt1);
		int j = View.MeasureSpec.getSize(paramInt1);
		int k = View.MeasureSpec.getMode(paramInt2);
		int m = View.MeasureSpec.getSize(paramInt2);
		int n = Math.min(chooseDimension(i, j), chooseDimension(k, m));
		setMeasuredDimension(n, n);
	}

	protected void onRestoreInstanceState(Parcelable paramParcelable) {
		super.onRestoreInstanceState(((Bundle) paramParcelable)
				.getParcelable("superState"));
	}

	protected Parcelable onSaveInstanceState() {
		Parcelable localParcelable = super.onSaveInstanceState();
		Bundle localBundle = new Bundle();
		localBundle.putParcelable("superState", localParcelable);
		return localBundle;
	}

	protected void onSizeChanged(int paramInt1, int paramInt2, int paramInt3,
			int paramInt4) {
	}

	public void setOffColor(int paramInt) {
		this.offColor = paramInt;
		invalidate();
	}

	public void setOnColor(int paramInt) {
		this.onColor = paramInt;
	}

	public void setSegmentOffColor(int paramInt) {
		this.offColor = paramInt;
	}

	public void setSegmentOnColor(int paramInt) {
		this.onColor = paramInt;
		invalidate();
	}

	public void setTitle(String paramString) {
		this.title = paramString;
	}

	public void setTitleColor(int paramInt) {
		this.titleColor = paramInt;
		this.titlePaint.setColor(this.titleColor);
		invalidate();
	}

	public void setValue(boolean paramBoolean) {
		this.value = paramBoolean;
	}
}
