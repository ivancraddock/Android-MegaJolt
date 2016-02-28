package com.estes.megajoltandroid.guages;

import com.estes.megajoltandroid.R;
import android.annotation.SuppressLint;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.preference.Preference;
import android.preference.Preference.OnPreferenceClickListener;
import android.preference.PreferenceActivity;
import android.preference.PreferenceFragment;
import android.preference.PreferenceManager;

/**
 * This class provides access to the preference picker for the gauges and
 * updates the setting for each gauge when they are changed.
 * 
 * @author Bryan Daul, Ivan Craddock, Troy Wellington
 * @version 1.0
 * 
 */

public class GaugesPreferencesActivity extends PreferenceActivity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		getFragmentManager().beginTransaction()
				.replace(android.R.id.content, new GaugesSettingFragment())
				.commit();
	}

	@SuppressLint("ValidFragment")
	public class GaugesSettingFragment extends PreferenceFragment {

		@Override
		public void onCreate(Bundle savedInstanceState) {
			super.onCreate(savedInstanceState);
			addPreferencesFromResource(R.xml.gaugespreferences);

			findPreference("buttonGauge1Reset").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {

							SharedPreferences sp = PreferenceManager
									.getDefaultSharedPreferences(getActivity());
							SharedPreferences.Editor ed = sp.edit();
							ed.putInt("gauge1_max", 100);
							ed.putInt("gauge1_scale_deg", (int) 270.0f);
							ed.putInt("gauge1_total_ticks", 50);
							ed.putInt("gauge1_maj_ticks", 5);
							ed.apply();
							return false;
						}
					});

			findPreference("buttonGauge2Reset").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {

							SharedPreferences sp = PreferenceManager
									.getDefaultSharedPreferences(getActivity());
							SharedPreferences.Editor ed = sp.edit();
							ed.putInt("gauge2_max", 60);
							ed.putInt("gauge2_scale_deg", (int) 120.0f);
							ed.putInt("gauge2_total_ticks", 30);
							ed.putInt("gauge2_maj_ticks", 5);
							ed.apply();
							return false;
						}
					});

			findPreference("buttonGauge3Reset").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {

							SharedPreferences sp = PreferenceManager
									.getDefaultSharedPreferences(getActivity());
							SharedPreferences.Editor ed = sp.edit();
							ed.putInt("gauge3_max", 105);
							ed.putInt("gauge3_scale_deg", (int) 210.0f);
							ed.putInt("gauge3_total_ticks", 21);
							ed.putInt("gauge3_maj_ticks", 3);
							ed.apply();
							return false;
						}
					});

		}

		@Override
		public void onResume() {
			super.onResume();
		}

		@Override
		public void onPause() {
			super.onPause();
		}

	}

}
