package com.estes.megajoltandroid;

import java.util.ArrayList;
import java.util.Set;

import android.annotation.SuppressLint;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.os.Bundle;
import android.preference.ListPreference;
import android.preference.Preference;
import android.preference.Preference.OnPreferenceClickListener;
import android.preference.PreferenceActivity;
import android.preference.PreferenceFragment;
import android.util.Log;
import android.view.MenuItem;

public class MainPreferencesActivity extends PreferenceActivity {

	@Override
	protected void onCreate(final Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		getFragmentManager().beginTransaction()
				.replace(android.R.id.content, new MainPreferencesFragment())
				.commit();
	}

	@SuppressLint("ValidFragment")
	public class MainPreferencesFragment extends PreferenceFragment {
		@Override
		public void onCreate(Bundle savedInstanceState) {
			super.onCreate(savedInstanceState);
			addPreferencesFromResource(R.xml.preferences);
			final ListPreference listPreference = (ListPreference) findPreference("bluetooth_device");

			populateBluetoothDevices(listPreference);

			listPreference
					.setOnPreferenceClickListener(new OnPreferenceClickListener() {
						@Override
						public boolean onPreferenceClick(Preference preference) {
							populateBluetoothDevices(listPreference);
							return false;
						}
					});
		}
	}

	protected static void populateBluetoothDevices(ListPreference lp) {
		ArrayList<CharSequence> a = new ArrayList<CharSequence>();
		
		BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
		if (mBluetoothAdapter == null) {
		    // TODO: Device does not support Bluetooth
			return;
		}
		Set<BluetoothDevice> pairedDevices = mBluetoothAdapter.getBondedDevices();
		// If there are paired devices
		if (pairedDevices.size() > 0) {
		    // Loop through paired devices
		    for (BluetoothDevice device : pairedDevices) {
		    	a.add(device.getName() + "\n" + device.getAddress());
		    }
		}
		CharSequence[] t = new CharSequence[a.size()];
		for(int i=0;i<t.length;i++)
			t[i] = a.get(i);

		CharSequence[] entries = t;
		CharSequence[] entryValues = t;
		lp.setEntries(entries);
		lp.setDefaultValue("1");
		lp.setEntryValues(entryValues);
	}
}