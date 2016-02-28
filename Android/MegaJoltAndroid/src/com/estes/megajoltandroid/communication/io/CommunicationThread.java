package com.estes.megajoltandroid.communication.io;

import java.io.IOException;
import java.util.Timer;
import java.util.TimerTask;
import java.util.UUID;

import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;

import com.estes.megajoltandroid.communication.request.Request;

public class CommunicationThread extends Thread {
	private BluetoothSocket socket;
	private static final UUID sppUUID = UUID
			.fromString("00001101-0000-1000-8000-00805F9B34FB");

	protected boolean connected = false;
	private int getResponse = 0;
	private Handler handler;
	public Handler mHandler;

	protected class ReadTask extends TimerTask {
		@Override
		public void run() {
			if (getResponse > 0)
				readData(getResponse);
		}
	}

	@Override
	public void run() {
		Looper.prepare();

		mHandler = new Handler() {
			public void handleMessage(Message msg) {
				if (msg.what == HandlerMessage.CONNECT) {
					try {
						BluetoothSocket tmp = null;
						try {
							tmp = ((BluetoothDevice) msg.obj)
									.createInsecureRfcommSocketToServiceRecord(sppUUID);
						} catch (IOException e) {
							Log.d("ERROR", "Socket could not be created.");
							e.printStackTrace();
						}
						socket = tmp;
						socket.connect();
						connected = true;
						Log.d("connected", "connected");
						notifyHandler(HandlerMessage.CONNECT_SUCCESS);
					} catch (IOException connectException) {
						notifyHandler(HandlerMessage.CONNECT_FAILED);
						Log.d("ERROR", "Could not connect to socket.");
						try {
							socket.close();
						} catch (IOException closeException) {
							Log.d("ERROR", "Could not close socket.");
						}
					}
				} else if (msg.what == HandlerMessage.DISCONNECT) {
					try {
						socket.close();
					} catch (Exception e) {
						e.printStackTrace();
					}
					connected = false;
				} else if (msg.what == HandlerMessage.REQUEST_SEND) {
					if (getResponse > 0)
						Log.d("debug",
								"attempt to send request while waiting for response");
					try {
						if (((Request) msg.obj).RESPONSE_LENGTH > 0)
							getResponse = ((Request) msg.obj).RESPONSE_LENGTH;
						socket.getOutputStream().write(
								((Request) msg.obj).getBytes());
					} catch (IOException e) {
						Log.e("ERROR", "Unable to write to socket");
						notifyHandler(HandlerMessage.RESPONSE_TIMEOUT);
					}

				}
			}
		};

		(new Timer()).schedule(new ReadTask(), 100, 100);

		Looper.loop();
	}

	public boolean isConnected() {
		return connected;
	}

	private void readData(int len) {
		//Log.d("debug", "read " + len);
		if (!connected)
			return;
		byte[] buffer = new byte[len];
		try {
			int read = 0;

			while (read < len) {
				read += socket.getInputStream().read(buffer, read, len - read);
				//Log.d("debug", "read " + read + " / " + len);
			}

			notifyHandler(HandlerMessage.RESPONSE_RECEIVED, buffer);
			getResponse = 0;
		} catch (IOException e) {
			Log.e("ERROR", "Unable to read from socket");
			// TODO: add a toast
			// "There was a problem communicating with the MegaJolt device."

		}
	}

	public void notifyHandler(int what) {
		if (this.handler != null)
			this.handler.sendEmptyMessage(what);
	}

	public void notifyHandler(int what, Object obj) {
		if (this.handler != null)
			this.handler.sendMessage(Message.obtain(this.handler, what, obj));
	}

	public void setHandler(Handler h) {
		this.handler = h;
	}
}
