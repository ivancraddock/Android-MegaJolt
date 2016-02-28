package com.estes.megajoltandroid.ignition;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.pm.ActivityInfo;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.text.Editable;
import android.util.Log;
import android.view.GestureDetector;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.NumberPicker.OnValueChangeListener;
import android.widget.Toast;

import com.estes.megajoltandroid.BaseActivity;
import com.estes.megajoltandroid.MainActivity;
import com.estes.megajoltandroid.R;
import com.estes.megajoltandroid.application.ApplicationGlobals;
import com.estes.megajoltandroid.communication.io.HandlerMessage;
import com.estes.megajoltandroid.communication.io.MegaJolt;
import com.estes.megajoltandroid.communication.request.RequestGetIgnitionConfiguration;
import com.estes.megajoltandroid.communication.response.ResponseGetIgnitionConfiguration;
import com.estes.megajoltandroid.guages.GuagesActivity;

public class IgnitionMapActivity extends BaseActivity {

	static final String LOAD_BOOL = "load_bool";
	static final String RPM_BINS = "rpm_bins";
	static final String LOAD_BINS = "load_bins";
	static final String RESUME_BINS = "resume_bins";
	static final String FILE_STRING = "file_string";
	static final String PREF_BINS = "pref_bins";

	static boolean normalMode = true;
	final int binMin = 0, binMax = 59, loadMin = 5, loadMax = 220, loadInc = 5,
			rpmMin = 100, rpmMax = 9000, rpmInc = 100;

	EditText[] BinArray = new EditText[100];
	EditText[] RpmArray = new EditText[10];
	EditText[] LoadArray = new EditText[10];

	int[] loadValues = { 0, 15, 30, 45, 60, 75, 90, 105, 120, 135 };
	int[] RpmValues = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

	final int[] defaultValues = { 8, 9, 11, 9, 15, 23, 26, 32, 32, 30, 8, 9,
			11, 9, 15, 23, 26, 32, 32, 29, 8, 9, 12, 12, 15, 23, 26, 32, 31,
			28, 8, 9, 13, 13, 17, 22, 26, 32, 29, 27, 8, 9, 13, 14, 17, 22, 26,
			32, 28, 26, 8, 9, 14, 15, 17, 22, 26, 32, 28, 25, 8, 9, 14, 18, 20,
			22, 26, 32, 28, 24, 8, 9, 15, 18, 20, 21, 26, 31, 27, 23, 8, 9, 15,
			18, 20, 20, 26, 31, 26, 22, 8, 9, 15, 18, 20, 20, 25, 32, 26, 20 };

	AlertDialog.Builder alert;
	IgnitionMapActivity mainActivity = this;
	final Context context = this;
	private ApplicationGlobals global;
	String filestring;
	private GestureDetector gD;

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);

		alert = new AlertDialog.Builder(IgnitionMapActivity.this);
		getWindow().setSoftInputMode(
				WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
		setContentView(R.layout.ignitionmap_main);
		global = (ApplicationGlobals) (mainActivity.getApplication());
		LoadPreferences();
		initRpm();
		initLoad();
		initMultiButton();

		activityHandler = new Handler() {
			@Override
			public void handleMessage(Message msg) {
				MegaJolt megaJolt = global.getMegaJolt();
				if (megaJolt == null)
					return;

				switch (msg.what) {
				case HandlerMessage.RESPONSE_IGNITION_CONFIGURATION:
					ResponseGetIgnitionConfiguration response = ((ResponseGetIgnitionConfiguration) msg.obj);
					for (int i = 0; i < 100; i++) {
						if (i < 10) {
							RpmArray[i].setText(100
									* response.getRPMBinValues()[i] + "");
							LoadArray[i].setText(response.getLoadBinValues()[i]
									+ "");
						}
						int v = response.getIgnitionMap()[i];

						BinArray[i].setText(v + "");
						BinArray[i].setBackgroundColor(colorAdvance(v));
					}
					break;
				}
			}
		};

		gD = new GestureDetector(this, new SwipeGestureDetector());

	}

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		if (gD.onTouchEvent(event)) {
			return true;
		}
		return super.onTouchEvent(event);
	}

	void onLeftSwipe() {
		Intent intent = new Intent(getBaseContext(), MainActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);
	}

	void onRightSwipe() {
		Intent intent = new Intent(getBaseContext(), GuagesActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);
	}

	private class SwipeGestureDetector extends SimpleOnGestureListener {

		private static final int SWIPE_MIN_DISTANCE = 120;
		private static final int SWIPE_MAX_OFF_PATH = 200;
		private static final int SWIPE_THRESHOLD_VELOCITY = 200;

		public SwipeGestureDetector() {

		}

		@Override
		public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX,
				float velocityY) {
			try {
				float diffAbs = Math.abs(e1.getY() - e2.getY());
				float diff = e1.getX() - e2.getX();
				if (diffAbs > SWIPE_MAX_OFF_PATH)
					return false;

				// Left swipe
				if (diff > SWIPE_MIN_DISTANCE
						&& Math.abs(velocityX) > SWIPE_THRESHOLD_VELOCITY) {
					((IgnitionMapActivity) mainActivity).onLeftSwipe();
				} else if (-diff > SWIPE_MIN_DISTANCE
						&& Math.abs(velocityX) > SWIPE_THRESHOLD_VELOCITY) {
					((IgnitionMapActivity) mainActivity).onRightSwipe();
				}
			} catch (Exception e) {
				Log.e("MainActivity", "Error on Gesture");
			}
			return false;
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.ignition, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		SharedPreferences sharedPreferences = getSharedPreferences(FILE_STRING,
				MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPreferences.edit();
		editor.putString(RESUME_BINS, arrayToString(BinArray));
		editor.putString(PREF_BINS, parseMap());

		editor.commit();

		Intent intent = new Intent(getBaseContext(), IgnitionSettingsPreferencesActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);
		return true;
	}

	public NumberPicker initBinPicker(int curr) {
		final NumberPicker np = new NumberPicker(mainActivity);
		np.setMinValue(binMin);
		np.setMaxValue(binMax);
		np.setWrapSelectorWheel(true);
		np.setValue(curr);
		np.setOnLongPressUpdateInterval(100);
		np.setOnValueChangedListener(new OnValueChangeListener() {
			@Override
			public void onValueChange(NumberPicker picker, int oldVal,
					int newVal) {
				newVal = picker.getValue();
			}
		});
		return np;
	}

	public String parseMap() {
		String result = "mapBins=";
		int BinIndex = 0;
		for (int i = 0; i < LoadArray.length - 1; i++) {
			result = result + LoadArray[i].getText().toString() + ",";
		}
		result = result + LoadArray[9].getText().toString() + "\nrpmBins=";
		for (int j = 0; j < RpmArray.length; j++) {
			result = result
					+ (Integer.parseInt(RpmArray[j].getText().toString()) / 100)
					+ ",";
		}
		result = result
				+ (Integer.parseInt(RpmArray[9].getText().toString()) / 100);
		for (int k = 0; k < 10; k++) {
			result = result + "\nadvance" + k + "=";
			for (int m = 0; m < 9; m++) {
				result = result + BinArray[BinIndex].getText().toString() + ",";
				BinIndex++;
			}
			if (BinIndex < 100) {
				result = result + BinArray[BinIndex].getText().toString();
				BinIndex++;
			}
		}
		result = result + "\ncorrectionBins=0,0,0,0,0,0,0,0,0,0"
				+ "\ncorrectionValues=1,2,3,4,5,6,7,8,9,10"
				+ "\ncorrectionPeakHold=265" + "\nuserOutType0=1"
				+ "\nuserOutMode0=0" + "\nuserOutValue0=50"
				+ "\nuserOutType1=1" + "\nuserOutMode1=0"
				+ "\nuserOutValue1=60" + "\nuserOutType2=1"
				+ "\nuserOutMode2=0" + "\nuserOutValue2=10"
				+ "\nuserOutType3=1" + "\nuserOutMode3=0"
				+ "\nuserOutValue3=10";
		return result;
	}

	public NumberPicker initLoadPicker(int curr) {
		final NumberPicker np = new NumberPicker(mainActivity);
		np.setMinValue(1);
		np.setMaxValue((loadMax - loadMin) / loadInc);
		np.setWrapSelectorWheel(true);
		np.setValue(curr);
		np.setOnLongPressUpdateInterval(100);
		String[] temp = new String[(loadMax - loadMin) / loadInc];
		for (int i = 0; i < ((loadMax - loadMin) / loadInc); i++) {
			temp[i] = Integer.toString(loadMin + i * loadInc);
		}
		np.setDisplayedValues(temp);
		np.setOnValueChangedListener(new OnValueChangeListener() {
			@Override
			public void onValueChange(NumberPicker picker, int oldVal,
					int newVal) {
				newVal = picker.getValue();
			}
		});
		return np;
	}

	public NumberPicker initRPMPicker(int curr) {
		final NumberPicker np = new NumberPicker(mainActivity);
		np.setMinValue(1);
		np.setMaxValue((rpmMax - rpmMin) / rpmInc);
		np.setWrapSelectorWheel(true);
		np.setValue(curr);
		np.setOnLongPressUpdateInterval(100);
		String[] temp = new String[(rpmMax - rpmMin) / rpmInc];
		for (int i = 0; i < ((rpmMax - rpmMin) / rpmInc); i++) {
			temp[i] = Integer.toString(rpmMin + i * rpmInc);
		}
		np.setDisplayedValues(temp);
		np.setOnValueChangedListener(new OnValueChangeListener() {
			@Override
			public void onValueChange(NumberPicker picker, int oldVal,
					int newVal) {
				newVal = picker.getValue();

			}
		});
		return np;
	}

	public void initMultiButton() {
		int MultiID = getResources().getIdentifier("@id/MultiButton", null,
				getPackageName());
		Button MultiButton = (Button) findViewById(MultiID);
		MultiButton.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View arg0) {
				if (normalMode == true) {
					Log.d("MULTIBUTTON", "normalmode==true");
					Toast.makeText(getApplicationContext(),
							"Multi Select Engaged", Toast.LENGTH_SHORT).show();
				} else {
					Log.d("MULTIBUTTON", "normalmode==false");

					AlertDialog.Builder alert = new AlertDialog.Builder(context);
					alert.setTitle("Adjust Bin Value");
					final NumberPicker np = initBinPicker(0);
					alert.setView(np);
					alert.setMessage(
							"Setting will be changed for multiple bins")
							.setCancelable(false)
							.setPositiveButton("Yes",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {
											updateBin(np.getValue());
											dialog.cancel();

										}
									})
							.setNegativeButton("Cancel",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {
											dialog.cancel();
										}
									});
					alert.show();
				}
				normalMode = !normalMode;
			}
		});
	}

	public int[] strListToArray(String prime) {
		String[] lineTokens = prime.split(",");
		int[] result = new int[lineTokens.length];
		for (int i = 0; i < result.length; i++)
			result[i] = Integer.parseInt(lineTokens[i].trim());
		return result;
	}

	private int getRedValue(int i) {
		return Math.min(255, 111 + (i * 21));
	}

	private int getGreenValue(int i) {
		int temp = Math.max(0, i - 18);
		return Math.min(255, temp * 21);
	}

	private int getBlueValue(int i) {
		if (i < 8)
			return 255;
		if (i > 19 && i < 33)
			return 0;
		if (i < 20)
			return Math.max(0, 255 - ((i - 8) * 21));
		return Math.min(255, (i - 32) * 11);
	}

	private int colorAdvance(int i) {
		return Color.rgb(getRedValue(i), getGreenValue(i), getBlueValue(i));
	}

	public void updateBin(int value) {
		for (int i = 0; i < 100; i++) {
			ColorDrawable buttonColor = (ColorDrawable) BinArray[i]
					.getBackground();
			if (buttonColor.getColor() == (Color.CYAN)) {
				BinArray[i].setText(Integer.toString(value));
				BinArray[i].setBackgroundColor(colorAdvance(value));
			}
		}

	}

	public void initBin(int runstate) {
		for (int i = 0; i < 100; i++) {
			String tempString = "@id/Bin" + (i + 1) + "";
			int tempID = getResources().getIdentifier(tempString, null,
					getPackageName());
			BinArray[i] = (EditText) findViewById(tempID);
			switch (runstate) {
			case 3:
				BinArray[i].setBackgroundColor(colorAdvance(Integer
						.parseInt(BinArray[i].getText().toString())));
				break;
			default:
				break;
			}

			BinArray[i].setOnTouchListener(new OnTouchNumberListener(
					BinArray[i]) {
				@Override
				public boolean onTouch(View arg0, MotionEvent arg1) {

					if (arg1.getAction() != MotionEvent.ACTION_UP)
						return true;
					if (normalMode == false) {
						((EditText) getView()).setBackgroundColor(Color.CYAN);
						return true;
					}
					AlertDialog.Builder alert = new AlertDialog.Builder(context);

					Editable edCurr = ((EditText) getView()).getText();

					int npCurr = Integer.parseInt(edCurr.toString());

					alert.setTitle("Adjust Bin Value");
					final NumberPicker np = initBinPicker(npCurr);

					alert.setView(np)
							.setCancelable(false)
							.setPositiveButton("Yes",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {

											((EditText) getView())
													.setBackgroundColor(colorAdvance(np
															.getValue()));
											((EditText) getView()).setText(np
													.getValue() + "");
										}
									})
							.setNegativeButton("No",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {

											EditText e = ((EditText) getView());
											e.setBackgroundColor(colorAdvance(Integer
													.parseInt(e.getText()
															.toString())));
											dialog.cancel();
										}
									});

					if (arg1.getAction() == MotionEvent.ACTION_UP)
						((EditText) getView()).setBackgroundColor(Color.GREEN);
					alert.show();
					return true;
				}
			});
		}
	}

	public void initRpm() {
		for (int i = 0; i < 10; i++) {
			String tempString = "@id/Rpm" + (i + 1) + "";
			int tempID = getResources().getIdentifier(tempString, null,
					getPackageName());
			RpmArray[i] = (EditText) findViewById(tempID);
			RpmArray[i].setText(RpmValues[i] * 100 + "");
			RpmArray[i].setBackgroundColor(Color.YELLOW);

			RpmArray[i].setOnTouchListener(new OnTouchNumberListener(
					RpmArray[i]) {

				@Override
				public boolean onTouch(View arg0, MotionEvent arg1) {
					if (arg1.getAction() != MotionEvent.ACTION_UP)
						return true;

					AlertDialog.Builder alert = new AlertDialog.Builder(context);
					Editable edCurr = ((EditText) getView()).getText();
					int npCurr = Integer.parseInt(edCurr.toString());
					alert.setTitle("Adjust RPM Value");
					final NumberPicker np = initRPMPicker(npCurr / 100);

					alert.setView(np);

					alert.setMessage("Select RPM setting")
							.setCancelable(false)
							.setPositiveButton("Yes",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {
											((EditText) getView()).setText(np
													.getValue() * 100 + "");
										}
									})
							.setNegativeButton("No",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {

											dialog.cancel();
										}
									});
					if (arg1.getAction() == MotionEvent.ACTION_UP)
						alert.show();
					return true;
				}
			});
		}
	}

	private void CalibrateAxis() {
		for (int i = 0; i < 10; i++) {
			RpmValues[i] = Integer.parseInt(RpmArray[i].getText().toString());
			loadValues[i] = Integer.parseInt(LoadArray[i].getText().toString());
		}
	}

	private void SavePreferences() {
		CalibrateAxis();
		initBin(-1);
		SharedPreferences sharedPreferences = getSharedPreferences(FILE_STRING,
				MODE_PRIVATE);
		SharedPreferences.Editor editor = sharedPreferences.edit();
		editor.putString(RESUME_BINS, arrayToString(BinArray));
		editor.putString(PREF_BINS, parseMap());
		editor.putString(RPM_BINS, arrayToString(RpmArray));
		editor.putString(LOAD_BINS, arrayToString(LoadArray));

		editor.commit(); // I missed to save the data to preference here,.
	}

	private void MetaLoad() {
		Log.d("METHODTEST", "METALOAD");

		SharedPreferences sharedPreferences = getSharedPreferences(FILE_STRING,
				MODE_PRIVATE);
		boolean prefMap = sharedPreferences.getBoolean(LOAD_BOOL, false);
		SharedPreferences.Editor editor = sharedPreferences.edit();
		if (prefMap == true) {
			LoadPreferences();
			initRpm();
			initLoad();
		}
		editor.putBoolean(LOAD_BOOL, false);
		editor.commit();
	}

	private void LoadPreferences() {
		initBin(-1);
		SharedPreferences sharedPreferences = getSharedPreferences(FILE_STRING,
				MODE_PRIVATE);
		String state = sharedPreferences.getString(RESUME_BINS, null);

		boolean prefMap = sharedPreferences.getBoolean(LOAD_BOOL, false);
		Log.d("METHODTEST", "LOADPREF");

		if (prefMap == true) {
			loadValues = strListToArray(sharedPreferences.getString(LOAD_BINS,
					null));
			RpmValues = strListToArray(sharedPreferences.getString(RPM_BINS,
					null));

			Log.d("RPM_TEST", sharedPreferences.getString(LOAD_BINS, null));
			int[] tempArray = strListToArray(state);
			Log.d("STATETEST", state);

			setBinFromArray(tempArray);

		} else {
			if (state != null) {
				int[] tempArray = strListToArray(state);
				setBinFromArray(tempArray);
			} else {
				setBinFromArray(defaultValues);
			}
		}
	}

	private void setBinFromArray(int[] prime) {
		for (int i = 0; i < 100; i++) {
			BinArray[i].setText(Integer.toString(prime[i]));
			BinArray[i].setBackgroundColor(colorAdvance(prime[i]));
		}
	}

	@Override
	public void onBackPressed() {

		super.onBackPressed();
	}

	public String arrayToString(EditText[] et) {
		String out = "";
		for (EditText e : et)
			out = out + e.getText().toString() + ",";
		return out.substring(0, out.length() - 1);
	}

	protected void onPause() {
		super.onPause();
		SavePreferences();
		SharedPreferences settings = getSharedPreferences(RESUME_BINS, 0);
		SharedPreferences.Editor editor = settings.edit();
		// Necessary to clear first if we save preferences onPause.
		editor.clear();

		editor.putString(RESUME_BINS, arrayToString(BinArray));
		editor.putString(PREF_BINS, parseMap());

		editor.commit();
	}

	@Override
	public void onResume() {
		super.onResume();
		Log.d("TEST", "ONRESUME");

		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		MetaLoad();
		initBin(3);
//		if (global.getMegaJolt() != null && global.getMegaJolt().isConnected()) {
//			Bundle extras = getIntent().getExtras();
//
//			if (extras != null
//					&& extras.containsKey("megajoltandroid.downloadMap")
//					&& extras.getBoolean("megajoltandroid.downloadMap")) {
//
//				AlertDialog.Builder alert = new AlertDialog.Builder(this);
//				alert.setTitle("Download");
//				alert.setMessage(
//						"Download ignition configuration from MegaJolt?")
//						.setCancelable(false)
//						.setPositiveButton("Yes",
//								new DialogInterface.OnClickListener() {
//									public void onClick(DialogInterface dialog,
//											int id) {
//										downloadMap();
//									}
//								})
//						.setNegativeButton("No",
//								new DialogInterface.OnClickListener() {
//									public void onClick(DialogInterface dialog,
//											int id) {
//										dialog.cancel();
//									}
//								});
//				alert.show();
//				getIntent().removeExtra("megajoltandroid.downloadMap");
//			}
//		}

		if (global.getMegaJolt() != null && global.getMegaJolt().isConnected()) {
			SharedPreferences sp = context.getSharedPreferences("flags",
					MODE_PRIVATE);

			if (sp.getBoolean("forceDownloadMap", false)) {
				Editor edit = sp.edit();
				edit.clear();
				edit.putBoolean("forceDownloadMap", false);
				edit.commit();
				downloadMap();
			}
		}
	}

	protected void downloadMap() {
		Toast.makeText(mainActivity, "Downloading ignition map...",
				Toast.LENGTH_SHORT).show();
		global.getMegaJolt().write(new RequestGetIgnitionConfiguration());
	}

	public void initLoad() {
		for (int i = 0; i < 10; i++) {
			String tempString = "@id/Load" + (i + 1) + "";
			int tempID = getResources().getIdentifier(tempString, null,
					getPackageName());
			LoadArray[i] = (EditText) findViewById(tempID);

			LoadArray[i].setText(loadValues[i] + "");

			LoadArray[i].setBackgroundColor(Color.LTGRAY);

			LoadArray[i].setOnTouchListener(new OnTouchNumberListener(
					LoadArray[i]) {

				@Override
				public boolean onTouch(View arg0, MotionEvent arg1) {
					if (arg1.getAction() != MotionEvent.ACTION_UP)
						return true;

					AlertDialog.Builder alert = new AlertDialog.Builder(context);
					Editable edCurr = ((EditText) getView()).getText();
					int npCurr = Integer.parseInt(edCurr.toString());

					alert.setTitle("Adjust Engine Load");
					final NumberPicker np = initLoadPicker(npCurr / 5);
					alert.setView(np);

					alert.setMessage("Select Load Value")
							.setCancelable(false)
							.setPositiveButton("Yes",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {

											((EditText) getView()).setText(np
													.getValue() * 5 + "");

										}
									})
							.setNegativeButton("No",
									new DialogInterface.OnClickListener() {
										public void onClick(
												DialogInterface dialog, int id) {

											dialog.cancel();
										}
									});

					if (arg1.getAction() == MotionEvent.ACTION_UP)
						alert.show();
					return true;
				}
			});
		}
	}
}