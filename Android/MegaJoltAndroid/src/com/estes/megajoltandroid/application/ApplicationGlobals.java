package com.estes.megajoltandroid.application;

import android.app.Activity;
import android.app.Application;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.Message;
import android.preference.PreferenceManager;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.estes.megajoltandroid.BaseActivity;
import com.estes.megajoltandroid.R;
import com.estes.megajoltandroid.GlobalSettingsPreferencesActivity.GlobalSettingsPreferencesFragment;
import com.estes.megajoltandroid.communication.io.HandlerMessage;
import com.estes.megajoltandroid.communication.io.MegaJolt;
import com.estes.megajoltandroid.communication.request.Request;
import com.estes.megajoltandroid.communication.request.RequestGetGlobalConfiguration;
import com.estes.megajoltandroid.communication.request.RequestGetIgnitionConfiguration;
import com.estes.megajoltandroid.communication.request.RequestGetState;
import com.estes.megajoltandroid.communication.response.ResponseGetGlobalConfiguration;
import com.estes.megajoltandroid.communication.response.ResponseGetIgnitionConfiguration;
import com.estes.megajoltandroid.communication.response.ResponseGetState;
import com.estes.megajoltandroid.dialog.NumberPickerPreference;

/*	
 This class will store variables which should be accessible regardless of which activity has focus.
 To set a global variable from any activity:
 ((ApplicationGlobals) this.getApplication()).setSomeVariable(x);
 or
 ApplicationGlobals.getInstance(this).setSomeVariable(x);

 To get a global variable from any activity:
 String s = ((ApplicationGlobals) this.getApplication()).getSomeVariable();
 or
 ApplicationGlobals.getInstance(this).getSomeVariable();

 To get current foreground activity: 
 Activity currentActivity = ((ApplicationGlobals)context.getApplicationContext()).getCurrentActivity();
 */

public class ApplicationGlobals extends Application {

	private MegaJolt megaJolt;
//	private int globalAdvance, globalCylinders, globalPIP, globalTrigger;
	public GlobalSettingsPreferencesFragment globalSettingsPreference;
	public static final int RESULT_SETTINGS = 1;
	private Activity mCurrentActivity = null;
	public Button buttonConnect;

	Handler handler = new Handler() {
		@Override
		public void handleMessage(Message msg) {
			MegaJolt megaJolt = getMegaJolt();
			if (megaJolt == null)
				return;

			switch (msg.what) {
			case HandlerMessage.CONNECT_FAILED:
				buttonConnect.setEnabled(true);
				displayToast("Could not connect to MegaJolt device.");
				megaJolt.disconnect();
				break;

			case HandlerMessage.CONNECT_SUCCESS:
				// Change the button text to "Disconnect" since we are now
				// connected.
				buttonConnect.setEnabled(true);
				buttonConnect.setText(getResources().getString(
						R.string.bt_button_disconnect));
				// Send an initial GetGlobalConfiguration request out to the
				// MegaJolt
				megaJolt.write(new RequestGetGlobalConfiguration());
				break;

			case HandlerMessage.RESPONSE_RECEIVED:
				byte[] data = (byte[]) msg.obj;

				String out = "";
				for (byte b : data)
					out += b + ", ";

				Request last = getMegaJolt().getLastSentRequest();
				String lastReq = last.getClass().getSimpleName();

				// Get State
				if (lastReq.equals(RequestGetState.class.getSimpleName())) {
					ResponseGetState response = new ResponseGetState(data);

					// Send the message off to the current activity (gauges
					// activity) to handle it.
					try {
						((BaseActivity) getCurrentActivity()).activityHandler
								.sendMessage(Message
										.obtain(((BaseActivity) getCurrentActivity()).activityHandler,
												HandlerMessage.RESPONSE_GET_STATE,
												response));
					} catch (NullPointerException e) {
						// not initialized yet, may happen between activity
						// switching but shouldn't cause any problems
						e.printStackTrace();
					}
				}

				// Get Global Configuration
				else if (lastReq.equals(RequestGetGlobalConfiguration.class
						.getSimpleName())) {
					ResponseGetGlobalConfiguration response = new ResponseGetGlobalConfiguration(
							data);

					setGlobalConfiguration(response);
					displayToast("Global configuration settings have been loaded from the MegaJolt.");

					Log.d("DEBUG",
							response.getCrankingAdvance() + ", "
									+ response.getNumberOfCylinders() + ", "
									+ response.getPIPNoiseFilterLevel() + ", "
									+ response.getTriggerWheelOffset());
				}

				// Get Ignition Configuration
				else if (lastReq.equals(RequestGetIgnitionConfiguration.class
						.getSimpleName())) {
					ResponseGetIgnitionConfiguration response = new ResponseGetIgnitionConfiguration(
							data);
					// Send the message off to the current activity (ignition
					// configuration screen) to handle it.
					
					setIgnitionConfiguration(response);
					
					try {
						((BaseActivity) getCurrentActivity()).activityHandler
								.sendMessage(Message
										.obtain(((BaseActivity) getCurrentActivity()).activityHandler,
												HandlerMessage.RESPONSE_IGNITION_CONFIGURATION,
												response));

					} catch (NullPointerException e) {
						// not initialized yet, may happen between activity
						// switching but shouldn't cause any problems
						e.printStackTrace();
					}
				}

				break;

			case HandlerMessage.RESPONSE_TIMEOUT:
				buttonConnect.setEnabled(true);
				buttonConnect.setText(getResources().getString(
						R.string.bt_button_connect));

				displayToast("The connection to MegaJolt was lost.");

				megaJolt.disconnect();
				break;
			}
		}
	};

	public void onCreate() {
		super.onCreate();
		if (getMegaJolt() == null)
			setMegaJolt(new MegaJolt());
		getMegaJolt().setHandler(handler);
	}

	public Activity getCurrentActivity() {
		return mCurrentActivity;
	}

	public void setCurrentActivity(Activity mCurrentActivity) {
		this.mCurrentActivity = mCurrentActivity;
	}

	public static ApplicationGlobals getInstance(Activity a) {
		return ((ApplicationGlobals) a.getApplication());
	}

	public void refreshGlobalSettingsVariables() {

	}

	public void displayToast(String s) {
		Toast.makeText(getApplicationContext(), s, Toast.LENGTH_SHORT).show();
	}

	public void setGlobalConfiguration(ResponseGetGlobalConfiguration response) {
		try {
			globalSettingsPreference.findPreference("buttonLoadGlobalSettings")
					.setEnabled(true);

		} catch (NullPointerException e) {
			// button not loaded, will already be enabled
		}


		SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
		SharedPreferences.Editor editor = prefs.edit();
		editor.putInt("global_advance", response.getCrankingAdvance());
		editor.putInt("global_cylinders", response.getNumberOfCylinders());
		editor.putInt("global_pip", response.getPIPNoiseFilterLevel());
		editor.putInt("global_trigger", response.getTriggerWheelOffset());
		editor.apply();

	}
	
	public void setIgnitionConfiguration(ResponseGetIgnitionConfiguration response) {
		SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
		SharedPreferences.Editor editor = prefs.edit();

		editor.putString("ignition_rpmbin", intArrayToString(response.getRPMBinValues()));
		editor.putString("ignition_loadbin", intArrayToString(response.getLoadBinValues()));
		editor.putString("ignition_map", intArrayToString(response.getIgnitionMap()));
		editor.putInt("ignition_revlimit", response.getRevLimitThresholdValue());
		editor.putInt("ignition_shiftlimit", response.getShiftLightThresholdValue());
		
		editor.apply();

	}

	public int getGlobalAdvance() {
		return PreferenceManager.getDefaultSharedPreferences(this).getInt("global_advance", 0);
	}

	public int getGlobalCylinders() {
		return PreferenceManager.getDefaultSharedPreferences(this).getInt("global_cylinders", 0);
	}

	public int getGlobalPIP() {
		return PreferenceManager.getDefaultSharedPreferences(this).getInt("global_pip", 0);
	}

	public int getGlobalTrigger() {
		return PreferenceManager.getDefaultSharedPreferences(this).getInt("global_trigger", 0);
	}

	public MegaJolt getMegaJolt() {
		return megaJolt;
	}

	public void setMegaJolt(MegaJolt megaJolt) {
		this.megaJolt = megaJolt;
	}

	
	public String intArrayToString(int[] a)
	{
		String out = "";
		for(int i: a)
			out+=i + ",";
		
		return out.substring(0,out.length()-1);
	}

	
	public int[] stringToIntArray(String s)
	{
		String[] vals = s.split(",");
		int[] out = new int[vals.length];
		
		for(int i=0;i<out.length;i++)
			out[i] = Integer.parseInt(vals[i]);
		
		return out;
	}
}