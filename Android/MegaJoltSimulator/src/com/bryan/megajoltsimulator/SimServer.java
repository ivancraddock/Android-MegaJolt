package com.bryan.megajoltsimulator;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Arrays;

import android.bluetooth.BluetoothServerSocket;
import android.bluetooth.BluetoothSocket;
import android.os.Handler;
import android.util.Log;

public class SimServer implements Runnable {

	private BluetoothServerSocket mmServerSocket;
	private boolean ended = false;
	private Handler handler;

	public SimServer(BluetoothServerSocket socket) {
		this.mmServerSocket = socket;
	}

	@Override
	public void run() {
		BluetoothSocket socket = null;
		while (!ended) {

			// Listen for a new connection
			try {
				socket = mmServerSocket.accept();
				if (this.handler != null)
					this.handler.sendEmptyMessage(SimMessage.CONNECTION_MADE);
			} catch (IOException e) {
				e.printStackTrace();
				return;
			}

			while (!ended) {// Manage a connection after one is accepted
				if (socket != null) {
					try {
						manageConnectedSocket(socket);
					} catch (IOException e) {
						break;
					} catch (InterruptedException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
				try {
					Thread.sleep(100);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}
	}

	public void stop() {
		ended = true;
	}

	private void manageConnectedSocket(BluetoothSocket socket)
			throws IOException, InterruptedException {
		byte[] buffer;
		int messageID;
		while ((messageID = socket.getInputStream().read()) != -1) {
			if (ended)
				return;
			switch (messageID) {
			case 0x53: //  GetState
				buffer = readBytesDelayed(0, socket);
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_RECEIVED, "RequestGetState"));

				short rpm = (short) (Math.sin((System.currentTimeMillis()%6280)/1000f)*5000 + 5000);
				
				byte load = (byte) (Math.sin((System.currentTimeMillis()%6280)/1000f)*55 + 55);
				byte adv = (byte) (Math.sin((System.currentTimeMillis()%6280)/1000f)*30 + 30);
				
				byte high = (byte)(rpm/256);
				byte low = (byte)(rpm%256);
				
				Log.d("TEST", (System.currentTimeMillis()%6280)/1000f + " " + rpm + " " + load + " " + adv + " " + high + " " + low);
				
				socket.getOutputStream().write(
						new byte[] { adv, high,
								low, 0x35, load,
								0x21, 0x00, 0x00, 0x00 });
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_SENT, "ResponseGetState"));
				break;
			case 0x67: // GetGlobalConfiguration
				buffer = readBytesDelayed(0, socket);
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_RECEIVED,
						"RequestGetGlobalConfiguration"));

				socket.getOutputStream().write(
						new byte[] { (byte) Global.globalCylinders,
								(byte) Global.globalPIP,
								(byte) Global.globalAdvance,
								(byte) Global.globalTrigger, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
								0x00 });

				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_SENT,
						"ResponseGetGlobalConfiguration"));

				break;
			case 0x47: // UpdateGlobalConfiguration
				buffer = readBytesDelayed(64, socket);// untested
				Global.globalCylinders = buffer[0];
				Global.globalPIP = buffer[1];
				Global.globalAdvance = buffer[2];
				Global.globalTrigger = buffer[3];
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_RECEIVED,
						"RequestUpdateGlobalConfiguration"));
				break;

			case 0x43: // GetIgnitionConfiguration
				buffer = readBytesDelayed(0, socket);
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_RECEIVED,
						"RequestGetIgnitionConfiguration"));
				
				ByteBuffer b = ByteBuffer.allocate(20);
				b.put(Global.rpm);
				b.put(Global.load);
				
				socket.getOutputStream().write(b.array());
				Thread.sleep(500);
				
				b = ByteBuffer.allocate(108);
				b.put(Global.ignition);
				b.put((byte)0);
				b.put((byte)0);
				b.put((byte)0);
				b.put((byte)0);
				b.put((byte)0);
				b.put((byte)0);
				b.put((byte)0);
				b.put((byte)0);
				
				socket.getOutputStream().write(b.array());
				Thread.sleep(500);
				
				b = ByteBuffer.allocate(22);
				b.put(new byte[10]);
				b.put(new byte[10]);
				b.put((byte)0);
				b.put((byte)0);
				
				socket.getOutputStream().write(b.array());
	
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_SENT,
						"ResponseGetIgnitionConfiguration"));

				break;

			case 0x55: // UpdateIgnitionConfiguration
				buffer = readBytesDelayed(150, socket);
				this.handler.sendMessage(this.handler.obtainMessage(
						SimMessage.MESSAGE_RECEIVED,
						"RequestUpdateIgnitionConfiguration"));
				
				UpdateIgnitionConfiguration req = new UpdateIgnitionConfiguration(buffer);
				
				Global.ignition = req.getIgnitionMap();
				Global.rpm = req.getRPMBinValues();
				Global.load = req.getLoadBinValues();
				
				Log.d("out", Arrays.toString(Global.ignition));
				Log.d("out", Arrays.toString(Global.rpm));
				Log.d("out", Arrays.toString(Global.load));
				
				break;
			}
		}
	}

	private byte[] readBytesDelayed(int i, BluetoothSocket socket)
			throws IOException, InterruptedException {

		byte[] out = new byte[i];
		int d = 0;
		// read chunks of 32 bytes and delay 150ms between
		for (d = 0; d < i / 32; d++) {
			Log.d("TEST", "sending 32..");
			byte[] buffer = new byte[32];
			socket.getInputStream().read(buffer);
			for (int j = 0; j < buffer.length; j++)
				out[j + 32 * d] = buffer[j];
			Log.d("TEST", "sleep..");
			Thread.sleep(150);
			Log.d("TEST", "up..");
		}
		Log.d("TEST", "sending remaining..");
		// read remaining bytes not in a full 32byte chunk
		if (i % 32 != 0) {
			byte[] buffer = new byte[i % 32];
			socket.getInputStream().read(buffer);
			for (int j = 0; j < buffer.length; j++)
				out[j + 32 * d] = buffer[j];
		}
		Log.d("TEST", "done");
		return out;
	}

	// final protected static char[] hexArray = { '0', '1', '2', '3', '4', '5',
	// '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
	//
	// public static String bytesToHex(byte[] bytes) {
	// char[] hexChars = new char[bytes.length * 2];
	// int v;
	// for (int j = 0; j < bytes.length; j++) {
	// v = bytes[j] & 0xFF;
	// hexChars[j * 2] = hexArray[v >>> 4];
	// hexChars[j * 2 + 1] = hexArray[v & 0x0F];
	// }
	// return new String(hexChars);
	// }

	public void setHandler(Handler handler) {
		this.handler = handler;

	}

}
