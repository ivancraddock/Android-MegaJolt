package com.estes.megajoltandroid.communication.response;


public class ResponseGetGlobalConfiguration extends Response {

	public static int LENGTH = 64;

	public ResponseGetGlobalConfiguration(byte[] data) {
		super(data);
	}

	public int getNumberOfCylinders()
	{
		return data[0] & 0xFF;
	}
	
	public int getPIPNoiseFilterLevel()
	{
		return data[1] & 0xFF;
	}
	
	public int getCrankingAdvance()
	{
		return data[2] & 0xFF;
	}
	
	public byte getTriggerWheelOffset()
	{
		return data[3];
	}
	
	// byte indices 4-63 are "reserved for future use"

}
