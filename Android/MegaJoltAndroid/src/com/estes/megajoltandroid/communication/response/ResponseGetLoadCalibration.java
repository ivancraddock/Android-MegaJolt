package com.estes.megajoltandroid.communication.response;

public class ResponseGetLoadCalibration extends Response {

	public static int LENGTH = 256;
	
	public ResponseGetLoadCalibration(byte[] data) {
		super(data);
	}
	
	// TODO: Decode this once we understand the encoding of it
	// Temporary method:
	public byte[] getData()
	{
		return data;
	}
}
