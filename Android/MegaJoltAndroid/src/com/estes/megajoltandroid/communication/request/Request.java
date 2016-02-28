package com.estes.megajoltandroid.communication.request;

public abstract class Request {

	protected byte[] data;
	public int RESPONSE_LENGTH = 0;
	
	public Request(int length)
	{
		data = new byte[length];
	}
	
	public byte[] getBytes()
	{
		return data;
	}
	
	protected void set(int position, byte value)
	{
		data[position] = value;
	}
	
	protected void set(int position, byte[] part)
	{
		for(int i=0;i<part.length;i++)
			data[position+i] = part[i];
	}
	
	protected void set(int position, byte[][] part)
	{
		//Row-major order
		int counter = 0;
		for(byte[] b: part)
			for(byte v: b)
				data[position + (counter++)] = v;
	}
}
