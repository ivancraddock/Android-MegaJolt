package com.estes.megajoltandroid.dialog;

import android.content.Context;
import android.content.DialogInterface;
import android.content.res.TypedArray;
import android.preference.DialogPreference;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;
import android.widget.NumberPicker;

import com.estes.megajoltandroid.R;

public class NumberPickerPreference extends DialogPreference {

	NumberPicker picker;
	Integer minValue, maxValue;
	String[] strValues;

	public NumberPickerPreference(Context context, AttributeSet attrs) {
		super(context, attrs);

		TypedArray a = context.obtainStyledAttributes(attrs,
				R.styleable.NumberPicker);
		String temp = a.getString(R.styleable.NumberPicker_strValues);
		if (temp == null || temp.equals("")) {
			minValue = a.getInteger(R.styleable.NumberPicker_minValue, 0);
			maxValue = a.getInteger(R.styleable.NumberPicker_maxValue, 0);
		} else {
			strValues = temp.split(",");
		}
	}

	@Override
	protected void onBindDialogView(View view) {
		super.onBindDialogView(view);
		this.picker = (NumberPicker) view.findViewById(R.id.pref_num_picker);
		// TODO this should be an XML parameter:
		if (strValues != null) {
			picker.setMinValue(0);
			picker.setMaxValue(strValues.length-1);
			picker.setDisplayedValues(strValues);
			picker.setValue(0);
			for(int i=0;i<strValues.length;i++)
				if(Integer.parseInt(strValues[i]) == (getPersistedInt(Integer.parseInt(strValues[0]))))
				{
					picker.setValue(i);
					break;
				}
		} else {
			picker.setMinValue(minValue);
			picker.setMaxValue(maxValue);
			picker.setValue(getPersistedInt(minValue));
		}
	}

	@Override
	public void onClick(DialogInterface dialog, int which) {
		super.onClick(dialog, which);
		if (which == DialogInterface.BUTTON_POSITIVE) {
			int saveValue = picker.getValue();
			if(strValues!=null)
				saveValue = Integer.parseInt(strValues[picker.getValue()]);

		Log.d("TEST2", saveValue + "");
			persistInt(saveValue);
			
			callChangeListener(picker.getValue());
		}
	}

	public void setNewValue(int value)
	{
		persistInt(value);
		callChangeListener(value);
//		callChangeListener(picker.getValue());
	}

	@Override
	protected Object onGetDefaultValue(TypedArray a, int index) {
		return a.getInt(index, 1);
	}
}