package com.estes.megajoltandroid.communication.request;

public class RequestWriteIgnitionConfigurationToFlash extends Request {

	private static int LENGTH = 1;
	
	public RequestWriteIgnitionConfigurationToFlash()
	{
		super(LENGTH);
		set(0, (byte)0x57);
	}
	
}
