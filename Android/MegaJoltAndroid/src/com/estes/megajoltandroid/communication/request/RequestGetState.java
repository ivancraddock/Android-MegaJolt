package com.estes.megajoltandroid.communication.request;

import com.estes.megajoltandroid.communication.response.ResponseGetState;

public class RequestGetState extends Request {
	
	private static int LENGTH = 1;
	
	public RequestGetState()
	{
		super(LENGTH);
		RESPONSE_LENGTH = ResponseGetState.LENGTH;
		set(0, (byte)0x53);
	}
	
}
