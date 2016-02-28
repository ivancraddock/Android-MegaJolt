package com.estes.megajoltandroid;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.view.Menu;
import android.view.MenuItem;

import com.estes.megajoltandroid.application.ApplicationGlobals;

public class BaseActivity extends Activity {

	protected static final int RESULT_SETTINGS = 1;
	protected static final int RESULT_GLOBAL_SETTINGS = 1;
	protected ApplicationGlobals global;
	public Handler activityHandler;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		global = (ApplicationGlobals) (this.getApplicationContext());
	}

	@Override
	protected void onResume() {
		super.onResume();
		global.setCurrentActivity(this);
	}

	@Override
	protected void onPause() {
		clearReferences();
		super.onPause();
	}

	@Override
	protected void onDestroy() {
		clearReferences();
		super.onDestroy();
	}

	private void clearReferences() {
		Activity currActivity = global.getCurrentActivity();
		if (currActivity != null && currActivity.equals(this))
			global.setCurrentActivity(null);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		Intent i;
		switch (item.getItemId()) {
		case R.id.action_settings:
			i = new Intent(getBaseContext(), MainPreferencesActivity.class);
			i.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
			startActivityForResult(i, RESULT_SETTINGS);
			break;
		case R.id.global_settings:
			i = new Intent(getBaseContext(), GlobalSettingsPreferencesActivity.class);
			i.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
			startActivityForResult(i, RESULT_GLOBAL_SETTINGS);
			break;
		}

		return true;
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);

	}

}