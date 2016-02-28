package com.estes.megajoltandroid.guages;

import java.text.DecimalFormat;
import java.util.Timer;
import java.util.TimerTask;

import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.GestureDetector;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.widget.Toast;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.WindowManager.LayoutParams;
import android.widget.TextView;

import com.estes.megajoltandroid.BaseActivity;
import com.estes.megajoltandroid.MainActivity;
import com.estes.megajoltandroid.R;
import com.estes.megajoltandroid.application.ApplicationGlobals;
import com.estes.megajoltandroid.communication.io.HandlerMessage;
import com.estes.megajoltandroid.communication.io.MegaJolt;
import com.estes.megajoltandroid.communication.request.RequestGetState;
import com.estes.megajoltandroid.communication.response.ResponseGetState;
import com.estes.megajoltandroid.ignition.IgnitionMapActivity;

/**
 * This class builds the MegaJoltGauges screen using a compilation of views.
 * 
 * @author Bryan Daul, Ivan Craddock, Troy Wellington
 * @version 1.0
 * 
 */

public class GuagesActivity extends BaseActivity {

	private static final String TAG = "GaugesActivity";
	protected GuagesActivity gaugesActivity = this;
	private GestureDetector gD;
	private ApplicationGlobals global;

	private GuagesView Gauge1;
	private GuagesView Gauge2;
	private GuagesView Gauge3;
	private IndicatorLED LED1;
	private IndicatorLED LED2;
	private IndicatorLED LED3;
	private IndicatorLED LED4;
	TextView txtViewDigital1;
	TextView txtViewDigital2;
	TextView txtViewDigital3;
	TextView txtViewShiftLight;
	TextView txtViewRevLight;
	Typeface typeFaceDigital;

	Timer timer;

	private void setGauges(SharedPreferences sp) {
		typeFaceDigital = Typeface.createFromAsset(getAssets(),
				"fonts/digital7.ttf");

		Log.d(TAG, "setScale called");
		Gauge1 = (GuagesView) (findViewById(getResources().getIdentifier(
				"@id/RPM", null, getPackageName())));
		txtViewDigital1 = (TextView) findViewById(R.id.txtViewDigital1);
		txtViewDigital1.setTypeface(typeFaceDigital);
		txtViewDigital1.setTextColor(0xff29b10b);
		txtViewDigital1.setShadowLayer(2.0f, -0.015f, -0.015f, 0x059e1710);
		txtViewDigital1.setText("0");
		Gauge1.setTitle("RPM x100");
		Gauge1.setMax(sp.getInt("gauge1_max", 100));
		Gauge1.setDegreesOfScale(sp.getInt("gauge1_scale_deg", (int) 270.0f));
		Gauge1.setTotalTicks(sp.getInt("gauge1_total_ticks", 50));
		Gauge1.setMajTicks(sp.getInt("gauge1_maj_ticks", 5));

		Gauge2 = (GuagesView) (findViewById(getResources().getIdentifier(
				"@id/Advance", null, getPackageName())));
		txtViewDigital2 = (TextView) findViewById(R.id.txtViewDigital2);
		txtViewDigital2.setTypeface(typeFaceDigital);
		txtViewDigital2.setTextColor(0xff29b10b);
		txtViewDigital2.setShadowLayer(2.0f, -0.015f, -0.015f, 0x059e1710);
		txtViewDigital2.setText("0");
		Gauge2.setTitle("Advance BTDC");
		Gauge2.setDegreesOfScale(sp.getInt("gauge2_scale_deg", (int) 120.0f));
		Gauge2.setMax(sp.getInt("gauge2_max", 60));
		Gauge2.setTotalTicks(sp.getInt("gauge2_total_ticks", 12));
		Gauge2.setMajTicks(sp.getInt("gauge2_maj_ticks", 2));

		Gauge3 = (GuagesView) (findViewById(getResources().getIdentifier(
				"@id/Load", null, getPackageName())));
		txtViewDigital3 = (TextView) findViewById(R.id.txtViewDigital3);
		txtViewDigital3.setTypeface(typeFaceDigital);
		txtViewDigital3.setTextColor(0xff29b10b);
		txtViewDigital3.setShadowLayer(2.0f, -0.015f, -0.015f, 0x059e1710);
		txtViewDigital3.setText("0");
		Gauge3.setTitle("Load KPa");
		Gauge3.setMax(sp.getInt("gauge3_max", 105));
		Gauge3.setDegreesOfScale(sp.getInt("gauge3_scale_deg", (int) 205.0f));
		Gauge3.setTotalTicks(sp.getInt("gauge3_total_ticks", 21));
		Gauge3.setMajTicks(sp.getInt("gauge3_maj_ticks", 3));

		txtViewShiftLight = (TextView) findViewById(R.id.txtViewShiftLight);
		txtViewShiftLight.setTypeface(typeFaceDigital);
		txtViewShiftLight.setTextColor(0x50FF210F);
		txtViewShiftLight.setShadowLayer(5.0f, -0.015f, -0.015f, 0x059e1710);

		txtViewRevLight = (TextView) findViewById(R.id.txtViewRevLight);
		txtViewRevLight.setTypeface(typeFaceDigital);
		txtViewRevLight.setTextColor(0x50FF210F);
		txtViewRevLight.setShadowLayer(5.0f, -0.015f, -0.015f, 0x059e1710);

		LED1 = (IndicatorLED) (findViewById(getResources().getIdentifier(
				"@id/LED1", null, getPackageName())));
		LED1.setOffColor(3540992);
		LED1.setOnColor(16711680);
		LED1.setValue(true);

		LED2 = (IndicatorLED) (findViewById(getResources().getIdentifier(
				"@id/LED2", null, getPackageName())));
		LED2.setOffColor(72704);
		LED2.setOnColor(392960);
		LED2.setValue(true);

		LED3 = (IndicatorLED) (findViewById(getResources().getIdentifier(
				"@id/LED3", null, getPackageName())));
		LED3.setOffColor(3540992);
		LED3.setOnColor(16711680);
		LED3.setValue(false);

		LED4 = (IndicatorLED) (findViewById(getResources().getIdentifier(
				"@id/LED4", null, getPackageName())));
		LED4.setOffColor(72704);
		LED4.setOnColor(392960);
		LED4.setValue(false);

		Gauge1.setHandTarget(0.0f);
		Gauge2.setHandTarget(0.0f);
		Gauge3.setHandTarget(0.0f);
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);

		Log.d(TAG, "onCreate called");

		global = (ApplicationGlobals) (this.getApplication());
		setContentView(R.layout.guages);
		getWindow().addFlags(LayoutParams.FLAG_KEEP_SCREEN_ON);

		activityHandler = new Handler() {
			@Override
			public void handleMessage(Message msg) {
				MegaJolt megaJolt = global.getMegaJolt();
				if (megaJolt == null)
					return;

				switch (msg.what) {
				case HandlerMessage.RESPONSE_GET_STATE:
					SharedPreferences prefs = PreferenceManager
							.getDefaultSharedPreferences(global);

					ResponseGetState response = ((ResponseGetState) msg.obj);

					float rpm = 0;

					try {
						int rpmTicks = (((response.getRawRPMHigh()) << 8) | response
								.getRawRPMLow());
						int cyl = prefs.getInt("global_cylinders", 4) / 2;
						if (cyl == 0)
							cyl = 2;
						rpm = (int) (60 * (1 / (float) (((float) rpmTicks / 1000000f) * cyl)));
					} catch (Exception e) {
						rpm = 0;
					}

					// Log.d("debug", rpm + "");
					Gauge1.setHandTarget(rpm / 100);
					DecimalFormat df = new DecimalFormat("0");
					String r = df.format(rpm);
					txtViewDigital1.setText(r);
					Gauge2.setHandTarget(response.getIgnitionAdvance());
					txtViewDigital2.setText(Integer.toString(response
							.getIgnitionAdvance()));
					Gauge3.setHandTarget(response.getCurrentLoadValue());
					txtViewDigital3.setText(Integer.toString(response
							.getCurrentLoadValue()));

					byte flags = (byte) response.getControllerState();
					boolean revLimit = ((flags >> 4) & 1) == 1;
					boolean shiftLimit = ((flags >> 5) & 1) == 1;

					if (shiftLimit) {
						txtViewShiftLight.setTextColor(0xffFF210F);
						txtViewShiftLight.setShadowLayer(5.0f, -0.015f,
								-0.015f, 0x509e1710);
					} else {
						txtViewShiftLight.setTextColor(0x50FF210F);
						txtViewShiftLight.setShadowLayer(5.0f, -0.015f,
								-0.015f, 0x059e1710);
					}

					if (revLimit) {
						txtViewRevLight.setTextColor(0xffFF210F);
						txtViewRevLight.setShadowLayer(5.0f, -0.015f, -0.015f,
								0x509e1710);
					} else {
						txtViewRevLight.setTextColor(0x50FF210F);
						txtViewRevLight.setShadowLayer(5.0f, -0.015f, -0.015f,
								0x059e1710);
					}

					break;
				}
			}
		};

		gD = new GestureDetector(this, new SwipeGestureDetector());
	}

	TimerTask task = new TimerTask() {
		public void run() {
			if (global.getMegaJolt() != null
					&& global.getMegaJolt().isConnected()) {
				global.getMegaJolt().write(new RequestGetState());

			}
		}
	};

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		if (gD.onTouchEvent(event)) {
			return true;
		}
		return super.onTouchEvent(event);

	}

	void onLeftSwipe() {
		Intent intent = new Intent(getBaseContext(), IgnitionMapActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);
	}

	void onRightSwipe() {
		Intent intent = new Intent(getBaseContext(), MainActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);
	}

	@Override
	public void onResume() {
		super.onResume();
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		Log.d(TAG, "onResume called");

		SharedPreferences sharedPrefs = PreferenceManager
				.getDefaultSharedPreferences(this);
		setGauges(sharedPrefs);
		int rate = Integer.valueOf(sharedPrefs
				.getString("sync_rate_key", "250"));

		if (rate < 100)
			rate = 100;

		if (PreferenceManager.getDefaultSharedPreferences(global).getInt(
				"global_cylinders", 4) == 0)
			Toast.makeText(
					this,
					"Cylinders are not set, defaulting to 4. Set the number of cylinders in the Global Settings for the gauges to work properly.",
					Toast.LENGTH_LONG).show();

		Timer old = timer;
		timer = new Timer();
		try {
			timer.scheduleAtFixedRate(task, 0, rate);
		} catch (IllegalStateException e) {
			timer = old;
			e.printStackTrace();
		}

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.guages, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		Intent intent = new Intent(getBaseContext(), GaugesPreferencesActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);

		return true;
	}

	@Override
	public void onPause() {
		super.onPause();
		Log.d(TAG, "onPause called");
		timer.cancel();
		timer.purge();
	}

	@Override
	protected void onRestart() {
		super.onRestart();
		Log.d(TAG, "onRestart called");
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
					gaugesActivity.onLeftSwipe();
				} else if (-diff > SWIPE_MIN_DISTANCE
						&& Math.abs(velocityX) > SWIPE_THRESHOLD_VELOCITY) {
					gaugesActivity.onRightSwipe();
				}
			} catch (Exception e) {
				Log.e("MainActivity", "Error on Gesture");
			}
			return false;
		}
	}

}
