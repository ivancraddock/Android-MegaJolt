package com.bryan.megajoltsimulator;

import java.io.IOException;
import java.util.UUID;

import com.bryan.megajoltsimulator.R;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothServerSocket;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends Activity {

	private BluetoothServerSocket mmServerSocket;
	private boolean isRunning = false;
	private SimServer server;
	private Thread thread;
	private MainActivity mainActivity;
	private int sent, received = 0;
	private LogQueue log = new LogQueue();

	Handler handler = new Handler() {
		@Override
		public void handleMessage(Message msg) {
			switch (msg.what) {
			case SimMessage.CONNECTION_MADE:
				Toast.makeText(mainActivity,
						"A device has connected to the simulator.",
						Toast.LENGTH_LONG).show();
				break;
			case SimMessage.SERVER_STOPPED:
				Toast.makeText(mainActivity, "Server stopped.",
						Toast.LENGTH_SHORT).show();
				break;
			case SimMessage.MESSAGE_RECEIVED:
				received++;
				log.add("Received: " + (String) msg.obj);
				updateText();
				break;

			case SimMessage.MESSAGE_SENT:
				sent++;
				log.add("Sent: " + (String) msg.obj);
				updateText();
				break;
			}
		}
	};

	@Override
	public void onDestroy() {
		super.onDestroy();
		if (isRunning) {
			endServer();
		}
	}

	protected void updateText() {
		((TextView) findViewById(R.id.textView1)).setText("");
		((TextView) findViewById(R.id.textView2)).setText("Received: "
				+ received + "\nSent: " + sent);
		((TextView) findViewById(R.id.TextView01)).setText(log
				.getListAsString());
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		mainActivity = this;
		setupButtonConnect();

	}

	private void setupButtonConnect() {
		final Button buttonConnect = (Button) findViewById(R.id.button1);

		buttonConnect.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (!isRunning) {
					startServer();
					buttonConnect.setText("Stop simulator server");
				} else {
					endServer();
					buttonConnect.setText("Start simulator server");
				}
			}
		});
	}

	public void startServer() {
		sent = received = 0;
		try {
			mmServerSocket = BluetoothAdapter
					.getDefaultAdapter()
					.listenUsingRfcommWithServiceRecord(
							"test",
							UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"));
		} catch (IOException e) {
		}
		server = new SimServer(mmServerSocket);
		server.setHandler(handler);
		thread = new Thread(server);
		thread.start();
		isRunning = true;
	}

	public void endServer() {
		if (isRunning) {
			try {
				mmServerSocket.close();
			} catch (Exception e) {
			}
			server.stop();
			try {
				thread.join();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			isRunning = false;
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

}
