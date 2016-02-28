package com.estes.megajoltandroid.communication.io;

public class HandlerMessage {
	public static final int
		CONNECT_SUCCESS = 0,
		CONNECT_FAILED = 1,
		RESPONSE_RECEIVED = 2,
		RESPONSE_TIMEOUT = 3,
		CONNECT = 4,
		DISCONNECT = 5,
		REQUEST_SEND  = 6,
		RESPONSE_IGNITION_CONFIGURATION = 7,
		RESPONSE_GET_STATE = 8;
	;
}
