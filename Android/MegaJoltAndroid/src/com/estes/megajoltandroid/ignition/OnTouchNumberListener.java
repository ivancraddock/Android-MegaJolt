package com.estes.megajoltandroid.ignition;
import android.view.MotionEvent;
import android.view.View;


public class OnTouchNumberListener implements android.view.View.OnTouchListener {

	private View parent;
	
	public OnTouchNumberListener(View parent)
	{
		super();
		this.parent = parent;
	}
	
	public View getView()
	{
		return this.parent;
	}
	
	@Override
	public boolean onTouch(View v, MotionEvent event) {
		
		return false;
	}

}
