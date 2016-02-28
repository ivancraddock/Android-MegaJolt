package com.estes.megajoltandroid.communication.request;

import com.estes.megajoltandroid.communication.response.ResponseGetGlobalConfiguration;
import com.estes.megajoltandroid.communication.response.ResponseGetIgnitionConfiguration;

public class RequestGetIgnitionConfiguration extends Request {

	private static int LENGTH = 1;

	public RequestGetIgnitionConfiguration()
	{
		super(LENGTH);
		RESPONSE_LENGTH = ResponseGetIgnitionConfiguration.LENGTH;
		set(0, (byte)0x43);
	}

}
