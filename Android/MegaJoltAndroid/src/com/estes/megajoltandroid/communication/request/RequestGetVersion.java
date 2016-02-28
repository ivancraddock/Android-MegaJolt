package com.estes.megajoltandroid.communication.request;

import com.estes.megajoltandroid.communication.response.ResponseGetVersion;

public class RequestGetVersion extends Request {

	private static int LENGTH = 1;
	
	public RequestGetVersion()
	{
		super(LENGTH);
		RESPONSE_LENGTH = ResponseGetVersion.LENGTH;
		set(0, (byte)0x56);
	}
	
}
