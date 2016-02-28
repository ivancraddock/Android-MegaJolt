package com.estes.megajoltandroid.communication.response;

public class ResponseGetVersion extends Response {

	public static int LENGTH = 3;
	
	public ResponseGetVersion(byte[] data) {
		super(data);
	}
	
	public byte getMajorVersion()
	{
		return data[0];
	}
	
	public byte getMinorVersion()
	{
		return data[1];
	}
	
	public byte getBugfixVersion()
	{
		return data[2];
	}
}
