package com.estes.megajoltandroid.communication.request;

public class RequestUpdateGlobalConfiguration extends Request {

	private static int LENGTH = 65;
	
	public RequestUpdateGlobalConfiguration(byte numberOfCylinders, byte PIPNoiseFilterLevel, byte crankingAdvance, byte triggerWheelOffset)
	{
		super(LENGTH);
		set(0, (byte)0x47);
		set(1, numberOfCylinders);
		set(2, PIPNoiseFilterLevel);
		set(3, crankingAdvance);
		set(4, triggerWheelOffset);
		for(int i=5;i<LENGTH;i++)
			data[i]=(byte)0x0;
	}
	
}
