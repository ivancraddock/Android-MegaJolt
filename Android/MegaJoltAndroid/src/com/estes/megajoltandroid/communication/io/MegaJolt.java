package com.estes.megajoltandroid.communication.io;

import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.ConcurrentLinkedQueue;

import android.app.Application;
import android.bluetooth.BluetoothDevice;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import com.estes.megajoltandroid.communication.request.Request;
import com.estes.megajoltandroid.communication.request.RequestGetGlobalConfiguration;
import com.estes.megajoltandroid.communication.request.RequestGetIgnitionConfiguration;
import com.estes.megajoltandroid.communication.request.RequestGetState;

public class MegaJolt extends Application {
	protected CommunicationThread thread;
	private Request lastSentRequest;
	private ConcurrentLinkedQueue<Request> writeQueue = new ConcurrentLinkedQueue<Request>();

	public MegaJolt() {
		this.thread = new CommunicationThread();
		this.thread.start();
		(new Timer()).scheduleAtFixedRate(new CheckReady(), 10, 10);
	}

	protected class CheckReady extends TimerTask {
		@Override
		public void run() {
			if (lastSentRequest != null)
				return;
			Request r = writeQueue.poll();

			if (r == null)
				return;

			 Log.d("WRITE", "Writing request to socket");
			thread.mHandler.sendMessage(Message.obtain(thread.mHandler,
					HandlerMessage.REQUEST_SEND, r));

			// set lastSentRequest if this request should expect a response
			lastSentRequest = 
				(r instanceof RequestGetState
				|| r instanceof RequestGetGlobalConfiguration
				|| r instanceof RequestGetIgnitionConfiguration) ? r
					: null;
			
			//Log.d("debug", r.getClass().getSimpleName());
		}
	}

	public void setHandler(Handler h) {
		this.thread.setHandler(h);
	}

	public void connect(BluetoothDevice device) {
		this.thread.mHandler.sendMessage(Message.obtain(this.thread.mHandler,
				HandlerMessage.CONNECT, device));
	}

	public void disconnect() {
		lastSentRequest = null;
		this.thread.mHandler.sendEmptyMessage(HandlerMessage.DISCONNECT);
	}

	public boolean isConnected() {
		return thread.isConnected();
	}

	@Override
	public void onCreate() {
		// reinitialize variable
	}

	public void write(Request data) {
		if(!isConnected())
			return;
		writeQueue.add(data);
	}

	public Request getLastSentRequest() {
		Request r = lastSentRequest;
		
		lastSentRequest = null;
		//Log.d("debug", "1" + r.getClass().getSimpleName());
		return r;
	}

}
