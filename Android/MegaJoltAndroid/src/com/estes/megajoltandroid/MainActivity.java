package com.estes.megajoltandroid;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.ComponentName;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.GestureDetector;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.Menu;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.estes.megajoltandroid.application.ApplicationGlobals;
import com.estes.megajoltandroid.communication.io.HandlerMessage;
import com.estes.megajoltandroid.communication.io.MegaJolt;
import com.estes.megajoltandroid.communication.response.ResponseGetState;
import com.estes.megajoltandroid.guages.GuagesActivity;
import com.estes.megajoltandroid.ignition.IgnitionMapActivity;

public class MainActivity extends BaseActivity {

	protected final Activity mainActivity = this;
	private GestureDetector gD;
	protected SharedPreferences sp;

	@SuppressWarnings("deprecation")
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		PreferenceManager.setDefaultValues(this,R.xml.gaugespreferences,false);

		global.buttonConnect = ((Button) findViewById(R.id.buttonConnect));
		setupButtonConnect();
		
		activityHandler = new Handler() {
			@Override
			public void handleMessage(Message msg) {
				MegaJolt megaJolt = global.getMegaJolt();
				if (megaJolt == null)
					return;

				switch (msg.what) {
				
				}
			}
		};
		
		gD = new GestureDetector(this, new SwipeGestureDetector());
		// showUserSettings();
	}
	
	@Override
	protected void onResume()
	{
		super.onResume();
		sp = getSharedPreferences("flags", MODE_PRIVATE);
		if(global.getMegaJolt()!=null && global.getMegaJolt().isConnected())
			((Button) findViewById(R.id.buttonConnect)).setText(getResources().getString(
					R.string.bt_button_disconnect));
		
		else
			((Button) findViewById(R.id.buttonConnect)).setText(getResources().getString(
					R.string.bt_button_connect));
	}

	private void setupButtonConnect() {
		global.buttonConnect.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				if (global.getMegaJolt().isConnected()) {
					// already connected, so disconnect
										
					global.getMegaJolt().disconnect();
					global.buttonConnect.setText(getResources().getString(
							R.string.bt_button_connect));
					
					Editor edit = sp.edit();
					edit.clear();
					edit.putBoolean("forceDownloadMap", false);
					edit.commit();
					
				} else {
					global.buttonConnect.setEnabled(false);
					// connect to selected paired device
					
					
					String[] selectedBluetooth = null;
					selectedBluetooth = PreferenceManager
							.getDefaultSharedPreferences(mainActivity)
							.getString("bluetooth_device", "").split("\n");

					if (selectedBluetooth.length != 2) {
						Toast.makeText(
								mainActivity,
								"You must first select a Bluetooth device in the settings menu.",
								Toast.LENGTH_SHORT).show();
						global.buttonConnect.setEnabled(true);
						return;
					}

					String btName = selectedBluetooth[0];
					String btMACAddress = selectedBluetooth[1];
					Log.d("CONNECT", btName + " - " + btMACAddress);

					BluetoothDevice device = BluetoothAdapter
							.getDefaultAdapter().getRemoteDevice(btMACAddress);

					global.getMegaJolt().connect(device);
					
					Editor edit = sp.edit();
					edit.clear();
					edit.putBoolean("forceDownloadMap", true);
					edit.commit();
					
					
				}
			}
		});
	}

	/** Called when Gauges Button is clicked */
	public void openGuages(View view) {

		Intent intent = new Intent(getBaseContext(), GuagesActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);

	}

	public void openIgnitionMap(View view) {

		Intent intent = new Intent(getBaseContext(), IgnitionMapActivity.class);
		intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(intent);

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	
	@Override
	public boolean onTouchEvent(MotionEvent event){
		if(gD.onTouchEvent(event)){
			return true;
		}
		return super.onTouchEvent(event);
		
	}
	
	void onLeftSwipe(){
		openGuages(null);
	}
	
	void onRightSwipe(){
		openIgnitionMap(null);
	}
	
	private class SwipeGestureDetector extends SimpleOnGestureListener {
		
		private static final int SWIPE_MIN_DISTANCE = 120;
		private static final int SWIPE_MAX_OFF_PATH = 200;
		private static final int SWIPE_THRESHOLD_VELOCITY = 200;
		
		public SwipeGestureDetector() {
		}

		@Override
		public boolean onFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY) {
			try{
				float diffAbs = Math.abs(e1.getY()- e2.getY());
				float diff = e1.getX()- e2.getX();
				
				if(diffAbs>SWIPE_MAX_OFF_PATH)
					return false;
				
				//Left swipe
				if(diff>SWIPE_MIN_DISTANCE && Math.abs(velocityX)>SWIPE_THRESHOLD_VELOCITY){
					((MainActivity)mainActivity).onLeftSwipe();
				}
				else if(-diff>SWIPE_MIN_DISTANCE && Math.abs(velocityX)> SWIPE_THRESHOLD_VELOCITY){
					((MainActivity)mainActivity).onRightSwipe();
				}
			}catch(Exception e){
				Log.e("MainActivity","Error on Gesture");
			}
			return false;
		}

	}

	// @Override
	// protected void onActivityResult(int requestCode, int resultCode, Intent
	// data) {
	// super.onActivityResult(requestCode, resultCode, data);
	//
	// switch (requestCode) {
	// case RESULT_SETTINGS:
	// showUserSettings();
	// break;
	// }
	// }

	// protected void showUserSettings() {
	// SharedPreferences sharedPrefs = PreferenceManager
	// .getDefaultSharedPreferences(this);
	//
	// TextView settingsTextView = (TextView) findViewById(R.id.textView1);
	// settingsTextView
	// .setText(sharedPrefs.getString("sync_rate_key", "NULL"));
	// }
}


