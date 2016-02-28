package com.estes.megajoltandroid.guages;

import com.estes.megajoltandroid.R;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapShader;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.LightingColorFilter;
import android.graphics.Matrix;
import android.graphics.Paint;
import android.graphics.Shader;
import android.graphics.RectF;
import android.os.Bundle;
import android.os.Handler;
import android.os.Parcelable;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;

public class ShiftLight extends View {

	private static final String TAG = "ShiftLight";
	private Paint shiftLightPaint;
	private Bitmap shiftLight;
	private Matrix shiftLightMatrix;
	private float shiftLightScale;
	private Handler handler;
	private int offColor = 5242880;
	private int onColor = 16711680;
	private int current;

	public ShiftLight(Context context) {
		super(context);
		init();
	}

	public ShiftLight(Context context, AttributeSet attrs) {
		super(context, attrs);
		init();
	}

	public ShiftLight(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		init();
	}

	private int chooseDimension(int paramInt1, int paramInt2) {
		if ((paramInt1 == -2147483648) || (paramInt1 == 1073741824))
			return paramInt2;
		return getPreferredSize();
	}

	private int getPreferredSize() {
		return 50;
	}

	private void init() {
		this.handler = new Handler();
		initDrawingTools();

	}

	private void initDrawingTools() {

		shiftLightPaint = new Paint();
		shiftLightPaint.setFilterBitmap(true);
		shiftLight = BitmapFactory.decodeResource(getContext().getResources(),
				R.drawable.shift_light);
		shiftLightMatrix = new Matrix();
//		shiftLightScale = (1.0f / shiftLight.getWidth()) * 0.5f;
//		shiftLightMatrix.setScale(shiftLightScale, shiftLightScale);

	}

	private void drawShiftLight(Canvas canvas) {

		canvas.save(Canvas.MATRIX_SAVE_FLAG);
		float scale = (float) getWidth();
		canvas.scale(scale, scale);
//		canvas.translate(0.5f - shiftLight.getWidth() * shiftLightScale / 2.0f, 0.5f - shiftLight.getHeight() * shiftLightScale / 2.0f);

		LightingColorFilter logoFilter = new LightingColorFilter(Color.DKGRAY,
				current);
		shiftLightPaint.setColorFilter(logoFilter);

		canvas.drawBitmap(shiftLight, shiftLightMatrix, shiftLightPaint);
		canvas.restore();
	}
	
//	  protected void onMeasure(int paramInt1, int paramInt2)
//	  {
//	    int i = View.MeasureSpec.getMode(paramInt1);
//	    int j = View.MeasureSpec.getSize(paramInt1);
//	    int k = View.MeasureSpec.getMode(paramInt2);
//	    int m = View.MeasureSpec.getSize(paramInt2);
//	    int n = Math.min(chooseDimension(i, j), chooseDimension(k, m));
//	    setMeasuredDimension(n, n);
//	  }
	  
	  protected void onRestoreInstanceState(Parcelable paramParcelable)
	  {
	    super.onRestoreInstanceState(((Bundle)paramParcelable).getParcelable("superState"));
	  }

	  protected Parcelable onSaveInstanceState()
	  {
	    Parcelable localParcelable = super.onSaveInstanceState();
	    Bundle localBundle = new Bundle();
	    localBundle.putParcelable("superState", localParcelable);
	    return localBundle;
	  }

	  protected void onSizeChanged(int paramInt1, int paramInt2, int paramInt3, int paramInt4)
	  {
	  }

	@Override
	protected void onDraw(Canvas canvas) {
		
		
//	    float f = getWidth();
//	    canvas.save(1);
//	    canvas.scale(f, f);
	    drawShiftLight(canvas);
//	    canvas.restore();
	}

	public int getOffColor() {
		return this.offColor;
	}

	public int getOnColor() {
		return this.onColor;
	}

	public void setOffColor(int paramInt) {
		this.offColor = paramInt;
	}

	public void setOnColor(int paramInt) {
		this.onColor = paramInt;
	}

	public void setValue(boolean b) {
		if (b)
			current = onColor;
		else
			current = offColor;

	}
}
