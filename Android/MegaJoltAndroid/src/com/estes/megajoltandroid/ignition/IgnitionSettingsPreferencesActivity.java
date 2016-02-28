package com.estes.megajoltandroid.ignition;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.OutputStreamWriter;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.os.Environment;
import android.preference.Preference;
import android.preference.Preference.OnPreferenceClickListener;
import android.preference.PreferenceActivity;
import android.preference.PreferenceFragment;
import android.util.Log;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.Toast;
import android.widget.NumberPicker.OnValueChangeListener;

import com.estes.megajoltandroid.application.ApplicationGlobals;
import com.estes.megajoltandroid.communication.request.RequestGetIgnitionConfiguration;
import com.estes.megajoltandroid.communication.request.RequestUpdateIgnitionConfiguration;
import com.estes.megajoltandroid.guages.GuagesActivity;

import com.estes.megajoltandroid.R;

public class IgnitionSettingsPreferencesActivity extends PreferenceActivity {
	static final String LOAD_BOOL = "load_bool";
	static final String RPM_BINS = "rpm_bins";
	static final String LOAD_BINS = "load_bins";
	static final String RESUME_BINS = "resume_bins";
	static final String FILE_STRING = "file_string";
	static final String PREF_BINS = "pref_bins";
	final int rpmMax = 8000, rpmMin = 100, rpmInc = 100;
	static int rev = 0;
	static int shift = 0;
	private ApplicationGlobals global;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		global = (ApplicationGlobals) (this.getApplication());
		getFragmentManager()
				.beginTransaction()
				.replace(android.R.id.content,
						new IgnitionSettingsPreferencesFragment()).commit();
	}

	@SuppressLint("ValidFragment")
	public class IgnitionSettingsPreferencesFragment extends PreferenceFragment {

		Activity activity = getActivity();

		@Override
		public void onCreate(Bundle savedInstanceState) {
			super.onCreate(savedInstanceState);
			addPreferencesFromResource(R.xml.ignitionsettings);
			activity = getActivity();

			findPreference("rpm_thresh_max").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {
						@Override
						public boolean onPreferenceClick(Preference preference) {
							AlertDialog.Builder alert = new AlertDialog.Builder(
									activity);
							alert.setTitle("Adjust Threshold for Rev Limit");
							final NumberPicker np = initBinPicker(rev);

							alert.setView(np)
									.setCancelable(false)
									.setPositiveButton(
											"Yes",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													rev = np.getValue();
												}
											})
									.setNegativeButton(
											"No",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													dialog.cancel();
												}
											});
							alert.show();
							return false;
						}
					});
			findPreference("downloadMap").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							SharedPreferences sp = getBaseContext()
									.getSharedPreferences("flags", MODE_PRIVATE);
							Editor edit = sp.edit();
							edit.clear();
							edit.putBoolean("forceDownloadMap", true);
							edit.commit();

							Intent intent = new Intent(getBaseContext(),
									IgnitionMapActivity.class);
							intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

							startActivity(intent);

							return false;
						}
					});
			findPreference("shift_current").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							if (shift > 0) {
								String message = "The Current Shift Light Threshold is "
										+ (shift * 100) + " RPM";
								Toast.makeText(activity, message,
										Toast.LENGTH_SHORT).show();
							} else {
								Toast.makeText(
										activity,
										"Error: No Shift Light Threshold saved",
										Toast.LENGTH_SHORT).show();

							}
							return false;
						}
					});
			findPreference("rpm_current").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							if (rev > 0) {
								String message = "The Current Rev Limit Threshold is "
										+ (rev * 100) + " RPM";
								Toast.makeText(activity, message,
										Toast.LENGTH_SHORT).show();
							} else {
								Toast.makeText(activity,
										"Error: No Rev Limit Threshold saved",
										Toast.LENGTH_SHORT).show();

							}
							return false;
						}
					});

			findPreference("uploadMap").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							AlertDialog.Builder alert = new AlertDialog.Builder(
									activity);
							alert.setTitle("Upload");
							alert.setMessage(
									"Upload ignition configuration to MegaJolt?")
									.setCancelable(false)
									.setPositiveButton(
											"Yes",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {

													SharedPreferences sharedPreferences = getSharedPreferences(
															FILE_STRING,
															MODE_PRIVATE);

													int loadValues[] = strListToArray(sharedPreferences
															.getString(
																	LOAD_BINS,
																	null));
													int rpmValues[] = strListToArray(sharedPreferences
															.getString(
																	RPM_BINS,
																	null));
													int binValues[] = strListToArray(sharedPreferences
															.getString(
																	RESUME_BINS,
																	null));

													byte[] rpmOut = new byte[10], loadOut = new byte[10], ignitionOut = new byte[100];
													for (int i = 0; i < 100; i++) {

														if (i < 10) {
															rpmOut[i] = (byte) (Integer
																	.valueOf(rpmValues[i]) / 100);
															loadOut[i] = (byte) (Integer
																	.valueOf(loadValues[i]) / 1);
														}
														ignitionOut[i] = (byte) (Integer
																.valueOf(binValues[i]) / 1);
													}
													global.getMegaJolt()
															.write(new RequestUpdateIgnitionConfiguration(
																	rpmOut,
																	loadOut,
																	ignitionOut,
																	(byte) 0,
																	(byte) 0,
																	new byte[4],
																	(byte) 0,
																	(byte) 0,
																	new byte[10],
																	new byte[10],
																	(byte) 0));
													Toast.makeText(
															activity,
															"Ignition configuration sent to MegaJolt.",
															Toast.LENGTH_SHORT)
															.show();
												}
											})
									.setNegativeButton(
											"No",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													dialog.cancel();
												}
											});
							alert.show();
							return false;
						}
					});
			findPreference("sdLoad").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							AlertDialog.Builder alert = new AlertDialog.Builder(
									activity);
							alert.setTitle("Load Ignition Map from SD Card?")
									.setCancelable(false)
									.setPositiveButton(
											"Yes",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {

													AlertDialog.Builder alert = new AlertDialog.Builder(
															activity);
													String dirName = Environment
															.getExternalStorageDirectory()
															.toString();

													File mfile = new File(
															dirName + "/MJLJ");
													File[] list = mfile
															.listFiles();
													final String[] fileNames = new String[list.length];
													for (int i = 0; i < list.length; i++) {
														Log.d("SD_FILE_TEST",
																list[i].toString());
														fileNames[i] = list[i]
																.toString();
													}

													if (list.length > 0) {
														final NumberPicker input = new NumberPicker(
																activity);
														input.setMinValue(1);
														input.setMaxValue(fileNames.length);
														input.setWrapSelectorWheel(true);
														input.setOnLongPressUpdateInterval(100);

														input.setDisplayedValues(fileNames);
														input.setOnValueChangedListener(new OnValueChangeListener() {
															@Override
															public void onValueChange(
																	NumberPicker picker,
																	int oldVal,
																	int newVal) {
																newVal = picker
																		.getValue();

															}
														});

														alert.setTitle("Select Map to Load");
														alert.setView(input);

														alert.setPositiveButton(
																"Ok",
																new DialogInterface.OnClickListener() {
																	public void onClick(
																			DialogInterface dialog,
																			int whichButton) {
																		SharedPreferences sharedPreferences = getSharedPreferences(
																				FILE_STRING,
																				MODE_PRIVATE);
																		SharedPreferences.Editor editor = sharedPreferences
																				.edit();

																		FileInputStream fis;
																		try {
																			Log.d("SD_NAME_TEST",
																					fileNames[input
																							.getValue() - 1]
																							+ "");
																			String parsedDir[] = fileNames[input
																					.getValue() - 1]
																					.split("/");
																			String directory = "";

																			for (int i = 0; i < parsedDir.length - 1; i++) {
																				directory = directory
																						+ parsedDir[i]
																						+ "/";
																			}
																			Log.d("DIRECTORY_TEST",
																					directory);

																			File completeFile = new File(
																					directory,
																					parsedDir[parsedDir.length - 1]);
																			fis = new FileInputStream(
																					completeFile);

																			byte[] buffer = new byte[4096];
																			fis.read(buffer);

																			String[] lines = (new String(
																					buffer))
																					.trim()
																					.split("\n");
																			for (int j = 0; j < lines.length; j++) {
																				Log.d("FILE_CONTENT_TEST",
																						lines[j]);
																			}
																			int tempValues[] = new int[100];

																			for (String line : lines) {
																				if (line.startsWith("#")
																						|| !line.contains("="))
																					continue;

																				String[] data = line
																						.split("=");

																				if (data[0]
																						.equals("mapBins")) {
																					editor.putString(
																							LOAD_BINS,
																							data[1]);
																				} else if (data[0]
																						.equals("rpmBins")) {
																					editor.putString(
																							RPM_BINS,
																							data[1]);
																				} else if (data[0]
																						.startsWith("advance")) {
																					Log.d("debug",
																							"d:"
																									+ data[1]);
																					int[] result = strListToArray(data[1]);
																					int index = Integer
																							.parseInt(data[0]
																									.substring(7));
																					for (int i = 0; i < 10; i++) {
																						Log.d("Cells",
																								Integer.toString(index));

																						tempValues[index
																								* 10
																								+ i] = result[i];
																					}

																				}

																			}
																			String binString = intArrayToString(tempValues);
																			Log.d("Cells",
																					binString);

																			editor.putString(
																					RESUME_BINS,
																					binString);
																			editor.putBoolean(
																					LOAD_BOOL,
																					true);
																			editor.commit(); // I
																								// missed
																								// to
																								// save
																								// the
																								// data
																								// to
																								// preference
																								// here,.

																		} catch (FileNotFoundException e) {
																			e.printStackTrace();
																		} catch (IOException e) {
																			e.printStackTrace();
																		}

																	}

																});

														alert.setNegativeButton(
																"Cancel",
																new DialogInterface.OnClickListener() {
																	public void onClick(
																			DialogInterface dialog,
																			int whichButton) {
																	}
																});

														alert.show();
													}

													else {
														Toast.makeText(
																getApplicationContext(),
																"Error: No Saved Maps",
																Toast.LENGTH_SHORT)
																.show();

													}

												}
											})
									.setNegativeButton(
											"No",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													dialog.cancel();
												}
											});
							alert.show();
							return false;
						}

					});

			findPreference("shift_thresh_max").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							AlertDialog.Builder alert = new AlertDialog.Builder(
									activity);
							alert.setTitle("Adjust Threshold for Shift Light");
							final NumberPicker np = initBinPicker(shift);

							alert.setView(np)
									.setCancelable(false)
									.setPositiveButton(
											"Yes",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													shift = np.getValue();
												}
											})
									.setNegativeButton(
											"No",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													dialog.cancel();
												}
											});
							alert.show();
							Log.d("shiftValue", shift + "");
							return false;

						}
					});
			findPreference("loadMap").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {

						@Override
						public boolean onPreferenceClick(Preference preference) {
							AlertDialog.Builder alert = new AlertDialog.Builder(
									activity);
							alert.setTitle("Load Ignition Map from Memory?")
									.setCancelable(false)
									.setPositiveButton(
											"Yes",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {

													AlertDialog.Builder alert = new AlertDialog.Builder(
															activity);
													final String[] files = fileList();

													if (files.length > 0) {
														final NumberPicker input = new NumberPicker(
																activity);
														input.setMinValue(1);
														input.setMaxValue(files.length);
														input.setWrapSelectorWheel(true);
														input.setOnLongPressUpdateInterval(100);

														input.setDisplayedValues(files);
														input.setOnValueChangedListener(new OnValueChangeListener() {
															@Override
															public void onValueChange(
																	NumberPicker picker,
																	int oldVal,
																	int newVal) {
																newVal = picker
																		.getValue();

															}
														});

														alert.setTitle("Select Map to Load");
														alert.setView(input);

														alert.setPositiveButton(
																"Ok",
																new DialogInterface.OnClickListener() {
																	public void onClick(
																			DialogInterface dialog,
																			int whichButton) {
																		SharedPreferences sharedPreferences = getSharedPreferences(
																				FILE_STRING,
																				MODE_PRIVATE);
																		SharedPreferences.Editor editor = sharedPreferences
																				.edit();

																		FileInputStream fis;
																		try {
																			fis = openFileInput(files[input
																					.getValue() - 1]);
																			byte[] buffer = new byte[4096];
																			fis.read(buffer);
																			String[] lines = (new String(
																					buffer))
																					.trim()
																					.split("\n");
																			int tempValues[] = new int[100];
																			for (String line : lines) {
																				if (line.startsWith("#")
																						|| !line.contains("="))
																					continue;
																				String[] data = line
																						.split("=");

																				if (data[0]
																						.equals("mapBins")) {
																					editor.putString(
																							LOAD_BINS,
																							data[1]);
																				} else if (data[0]
																						.equals("rpmBins")) {
																					editor.putString(
																							RPM_BINS,
																							data[1]);
																				} else if (data[0]
																						.startsWith("advance")) {
																					int[] result = strListToArray(data[1]);
																					int index = Integer
																							.parseInt(data[0]
																									.substring(7));
																					for (int i = 0; i < 10; i++) {
																						Log.d("Cells",
																								Integer.toString(index));

																						tempValues[index
																								* 10
																								+ i] = result[i];
																					}
																				}
																			}
																			String binString = intArrayToString(tempValues);
																			Log.d("Cells",
																					binString);

																			editor.putString(
																					RESUME_BINS,
																					binString);
																			editor.putBoolean(
																					LOAD_BOOL,
																					true);
																			editor.commit();

																		} catch (FileNotFoundException e) {
																			e.printStackTrace();
																		} catch (IOException e) {
																			e.printStackTrace();
																		}
																	}
																});
														alert.setNegativeButton(
																"Cancel",
																new DialogInterface.OnClickListener() {
																	public void onClick(
																			DialogInterface dialog,
																			int whichButton) {
																	}
																});
														alert.show();
													} else {
														Toast.makeText(
																getApplicationContext(),
																"Error: No Saved Maps",
																Toast.LENGTH_SHORT)
																.show();
													}
												}
											})
									.setNegativeButton(
											"No",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													dialog.cancel();
												}
											});
							alert.show();
							return false;
						}
					});

			findPreference("saveMap").setOnPreferenceClickListener(
					new OnPreferenceClickListener() {
						@Override
						public boolean onPreferenceClick(Preference preference) {
							AlertDialog.Builder alert = new AlertDialog.Builder(
									activity);
							alert.setTitle("Save Map");
							alert.setMessage(
									"Save to New File or Overwrite Existing Map")
									.setCancelable(false)
									.setPositiveButton(
											"Overwrite",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													final String[] files = fileList();
													if (files.length > 0) {

														AlertDialog.Builder alert = new AlertDialog.Builder(
																activity);
														final NumberPicker input = new NumberPicker(
																activity);
														input.setMinValue(1);
														input.setMaxValue(files.length);
														input.setWrapSelectorWheel(true);
														input.setOnLongPressUpdateInterval(100);
														input.setDisplayedValues(files);
														input.setOnValueChangedListener(new OnValueChangeListener() {
															@Override
															public void onValueChange(
																	NumberPicker picker,
																	int oldVal,
																	int newVal) {
																newVal = picker
																		.getValue();
															}
														});
														alert.setTitle("Select Map File to Overwrite");
														alert.setView(input);

														alert.setPositiveButton(
																"Ok",
																new DialogInterface.OnClickListener() {
																	public void onClick(
																			DialogInterface dialog,
																			int whichButton) {
																		SharedPreferences sharedPreferences = getSharedPreferences(
																				FILE_STRING,
																				MODE_PRIVATE);
																		String state = sharedPreferences
																				.getString(
																						PREF_BINS,
																						null);
																		writeToFile(
																				files[input
																						.getValue() - 1],
																				state);
																	}
																});
														alert.setNegativeButton(
																"Cancel",
																new DialogInterface.OnClickListener() {
																	public void onClick(
																			DialogInterface dialog,
																			int whichButton) {
																	}
																});
														alert.show();
													} else {
														Toast.makeText(
																getApplicationContext(),
																"Error: No Saved Maps",
																Toast.LENGTH_SHORT)
																.show();
													}
												}
											})
									.setNeutralButton(
											"New File",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													AlertDialog.Builder alert = new AlertDialog.Builder(
															activity);
													final EditText input = new EditText(
															activity);
													alert.setTitle("Enter Filename");
													alert.setView(input);
													alert.setPositiveButton(
															"Ok",
															new DialogInterface.OnClickListener() {
																public void onClick(
																		DialogInterface dialog,
																		int whichButton) {
																	String filename = input
																			.getText()
																			.toString()
																			+ ".mjlj";
																	SharedPreferences sharedPreferences = getSharedPreferences(
																			FILE_STRING,
																			MODE_PRIVATE);
																	String state = sharedPreferences
																			.getString(
																					PREF_BINS,
																					null);
																	state = state
																			+ "\nshiftLight="
																			+ shift
																			+ "\nrevLimit="
																			+ rev;
																	writeToFile(
																			filename,
																			state);
																}
															});
													alert.setNegativeButton(
															"Cancel",
															new DialogInterface.OnClickListener() {
																public void onClick(
																		DialogInterface dialog,
																		int whichButton) {
																}
															});
													alert.show();
												}
											})
									.setNegativeButton(
											"Cancel",
											new DialogInterface.OnClickListener() {
												public void onClick(
														DialogInterface dialog,
														int id) {
													dialog.cancel();
												}
											});
							alert.show();
							return false;
						}
					});
		}

		@Override
		public void onResume() {
			super.onResume();
			setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		}

		private void writeToFile(String filename, String data) {
			try {
				OutputStreamWriter outputStreamWriter = new OutputStreamWriter(
						openFileOutput(filename, Context.MODE_PRIVATE));
				outputStreamWriter.write(data);
				outputStreamWriter.close();
			} catch (IOException e) {
				Log.e("Exception", "File write failed: " + e.toString());
			}
		}

		public int[] strListToArray(String prime) {
			String[] lineTokens = prime.split(",");
			int[] result = new int[lineTokens.length];
			for (int i = 0; i < result.length; i++)
				result[i] = Integer.parseInt(lineTokens[i].trim());
			return result;
		}

		public String intArrayToString(int[] et) {
			String out = "";
			for (int e : et) {
				out = out + e + ",";
			}
			return out.substring(0, out.length());
		}

		@Override
		public void onPause() {
			super.onPause();
		}

		public NumberPicker initBinPicker(int curr) {
			final NumberPicker np = new NumberPicker(activity);
			np.setMinValue(0);
			np.setMaxValue(100);
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

	}

}