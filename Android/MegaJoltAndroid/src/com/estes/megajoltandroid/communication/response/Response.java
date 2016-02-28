package com.estes.megajoltandroid.communication.response;

public abstract class Response {

	protected byte data[] = new byte[256];
	
	public Response(byte[] data)
	{
		this.data = data;
	}
	
}
