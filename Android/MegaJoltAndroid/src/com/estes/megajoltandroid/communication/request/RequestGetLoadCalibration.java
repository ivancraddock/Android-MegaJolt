package com.estes.megajoltandroid.communication.request;

import com.estes.megajoltandroid.communication.response.ResponseGetLoadCalibration;

public class RequestGetLoadCalibration extends Request {

	private static int LENGTH = 1;
	
	public RequestGetLoadCalibration()
	{
		super(LENGTH);
		RESPONSE_LENGTH = ResponseGetLoadCalibration.LENGTH;
		set(0, (byte)0x6C);
	}
	
}
