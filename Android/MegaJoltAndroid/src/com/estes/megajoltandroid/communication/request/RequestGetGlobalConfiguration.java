package com.estes.megajoltandroid.communication.request;

import com.estes.megajoltandroid.communication.response.ResponseGetGlobalConfiguration;

public class RequestGetGlobalConfiguration extends Request {
	
	private static int LENGTH = 1;
	
	public RequestGetGlobalConfiguration()
	{
		super(LENGTH);
		RESPONSE_LENGTH = ResponseGetGlobalConfiguration.LENGTH;
		set(0, (byte)0x67);
	}
	
}
