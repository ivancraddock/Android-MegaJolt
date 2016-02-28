package com.bryan.megajoltsimulator;

import java.util.LinkedList;

public class LogQueue extends LinkedList<String> {

	@Override
	public boolean add(String s) {
		super.add(s);
		while (super.size() >= 50)
			super.remove();
		return true;
	}

	public String getListAsString() {
		String s = "";
		for (String t : this)
			s = t + "\n" + s;
		return s;
	}
}