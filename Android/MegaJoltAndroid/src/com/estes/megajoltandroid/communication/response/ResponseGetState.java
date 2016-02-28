package com.estes.megajoltandroid.communication.response;

public class ResponseGetState extends Response {
	
	public static int LENGTH = 9;
	
	public ResponseGetState(byte[] data)
	{
		super(data);
	}
	public int getIgnitionAdvance()
	{
		return data[0] & 0xFF;
	}
	public int getRawRPMHigh()
	{
		return data[1] & 0xFF;
	}
	public int getRawRPMLow()
	{
		return data[2] & 0xFF;
	}
	public int getCurrentRPMLoadBin()
	{
		return data[3] & 0xFF;
	}
	public int getCurrentLoadValue()
	{
		return data[4] & 0xFF;
	}
	public int getControllerState()
	{
		return data[5] & 0xFF;
	}
	public int getAuxiliaryInputValue()
	{
		return data[6] & 0xFF;
	}
	public int getAdvanceCorrectionBin()
	{
		return data[7] & 0xFF;
	}
	public byte getAdvanceCorrectionDegrees()
	{
		return data[8];
	}
}
