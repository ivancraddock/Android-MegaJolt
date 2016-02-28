package com.estes.megajoltandroid.communication.request;

import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class RequestUpdateIgnitionConfiguration extends Request {
	
	private static int LENGTH = 151;
	
	public RequestUpdateIgnitionConfiguration(byte[] RPMBinValues, byte[] loadBinValues, byte[] ignitionMap, byte userOutputTypes, byte userOutputModeConfigurations, byte[] userOutputThresholdValues, byte revLimitThresholdValue, byte shiftLightThresholdValue, byte[] advanceCorrectionBins, byte[] advanceCorrectionValues, short auxiliaryInputPeakHoldDecay)
	{
		super(LENGTH);
		set(0, (byte)0x55);
		set(1, RPMBinValues);
		set(11, loadBinValues);
		set(21, ignitionMap);
		set(121, userOutputTypes);
		set(122, userOutputModeConfigurations);
		set(123, userOutputThresholdValues[0]);
		set(124, userOutputThresholdValues[1]);
		set(125, userOutputThresholdValues[2]);
		set(126, userOutputThresholdValues[3]);
		set(127, revLimitThresholdValue);
		set(128, shiftLightThresholdValue);
		set(129, advanceCorrectionBins);
		set(139, advanceCorrectionValues);
		
		ByteBuffer bb = ByteBuffer.allocate(2);
		bb.order(ByteOrder.LITTLE_ENDIAN);
		bb.putShort(auxiliaryInputPeakHoldDecay);

		set(149, bb.get(0));
		set(150, bb.get(1));
	}
	
}
