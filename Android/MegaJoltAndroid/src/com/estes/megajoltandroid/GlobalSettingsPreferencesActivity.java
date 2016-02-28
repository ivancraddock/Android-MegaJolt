package com.estes.megajoltandroid;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.Preference;
import android.preference.Preference.OnPreferenceClickListener;
import android.preference.PreferenceActivity;
import android.preference.PreferenceFragment;
import android.widget.Toast;

import com.estes.megajoltandroid.application.ApplicationGlobals;
import com.estes.megajoltandroid.communication.request.RequestGetGlobalConfiguration;
import com.estes.megajoltandroid.communication.request.RequestUpdateGlobalConfiguration;

public class GlobalSettingsPreferencesActivity extends PreferenceActivity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		getFragmentManager()
				.beginTransaction()
				.replace(android.R.id.content,
						new GlobalSettingsPreferencesFragment()).commit();
	}

	@SuppressLint("ValidFragment")
	public class GlobalSettingsPreferencesFragment extends PreferenceFragment {

		Activity activity;

		@Override
		public void onCreate(Bundle savedInstanceState) {
			super.onCreate(savedInstanceState);
			addPreferencesFromResource(R.xml.globalsettings);
			activity = getActivity();
			ApplicationGlobals
			.getInstance(activity).globalSettingsPreference = this;

			findPreference("buttonLoadGlobalSettings")
					.setOnPreferenceClickListener(
							new OnPreferenceClickListener() {
								@Override
								public boolean onPreferenceClick(
										Preference preference) {
									findPreference("buttonLoadGlobalSettings").setEnabled(false);
									ApplicationGlobals
											.getInstance(activity)
											.getMegaJolt()
											.write(new RequestGetGlobalConfiguration());

									return false;
								}
							});
			
			findPreference("buttonSaveGlobalSettings")
			.setOnPreferenceClickListener(
					new OnPreferenceClickListener() {
						@Override
						public boolean onPreferenceClick(
								Preference preference) {

							byte cyl, adv, pip, tri;
							SharedPreferences sp = getPreferenceManager().getSharedPreferences();
							cyl=(byte) sp.getInt("global_cylinders", 4);
							adv=(byte) sp.getInt("global_advance", 1);
							pip=(byte) sp.getInt("global_pip", 1);
							tri=(byte) sp.getInt("global_trigger", 1);
							
							ApplicationGlobals
									.getInstance(activity)
									.getMegaJolt()
									.write(new RequestUpdateGlobalConfiguration(cyl, pip, adv, tri));
							
							Toast.makeText(activity,
									"Global configuration settings have been sent to the MegaJolt.",
									Toast.LENGTH_SHORT).show();
							
							return false;
						}
					});
		}
	}
}