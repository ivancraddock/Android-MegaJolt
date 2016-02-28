package com.estes.megajoltandroid.communication.response;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;

import android.util.Log;

public class ResponseGetIgnitionConfiguration extends Response {

	public static int LENGTH = 150;
	
	public ResponseGetIgnitionConfiguration(byte[] data) {
		super(data);
	}


	public int[] getRPMBinValues()
	{
		int[] out = new int[10];
		for(int i=0;i<out.length;i++)
			out[i] = data[i+0] & 0xFF;

		return out;
	}

	public int[] getLoadBinValues()
	{
		int[] out = new int[10];
		for(int i=0;i<out.length;i++)
			out[i] = data[i+10] & 0xFF;

		return out;
	}

	public int[] getIgnitionMap()
	{
		int[] out = new int[100];

		for(int i=0;i<100;i++)
			out[i] = data[i+20] & 0xFF;

		return out;
	}

	public int getUserOutputTypes()
	{
		return data[120] & 0xFF;
	}

	public int getUserOutputModeConfigurations()
	{
		return data[121] & 0xFF;
	}

	public int getUserOutputThresholdValue(int index)
	{
		if(index<1||index>4)
		{
			Log.e("ERROR", "User Output Threshold Value must be between 1 and 4.");
			System.exit(-1);
		}
		return data[121+index] & 0xFF;
	}
	
	public int getRevLimitThresholdValue()
	{
		return data[126] & 0xFF;
	}
	
	public int getShiftLightThresholdValue()
	{
		return data[127] & 0xFF;
	}
	
	public int[] getAdvanceCorrectionBins()
	{
		int[] out = new int[10];
		for(int i=0;i<out.length;i++)
			out[i]=data[128+i] & 0xFF;
		
		return out;
	}
	
	public byte[] getAdvanceCorrectionValues()
	{
		byte[] out = new byte[10];
		for(int i=0;i<out.length;i++)
			out[i]=data[138+i];
		
		return out;
	}
	
	public int getAuxiliaryInputPeakHoldDecay()
	{
		//TODO: Verify this is little endian
		ByteBuffer bb = ByteBuffer.allocate(2);
		bb.order(ByteOrder.LITTLE_ENDIAN);
		bb.put(data[148]);
		bb.put(data[149]);
		return bb.getShort(0) & 0xFFFF;
	}
}
