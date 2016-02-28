package com.estes.megajoltandroid.communication.request;

public class RequestUpdateIgnitionCell extends Request {

	private static int LENGTH = 3;
	
	public RequestUpdateIgnitionCell(byte RPMLoadBin, byte ignitionAdvanceValue)
	{
		// This request likely won't be used.
		// Use RequestUpdateIgnitionConfiguration instead.
		super(LENGTH);
		set(0, (byte)0x75);
		set(1, RPMLoadBin);
		set(2, ignitionAdvanceValue);
	}
	
}
