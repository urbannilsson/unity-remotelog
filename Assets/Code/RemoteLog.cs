using UnityEngine;
using System.Collections;
using System;

public class RemoteLog : MonoBehaviour
{
	[SerializeField]
	private string _remoteHost = "http://192.168.1.111:9292";

	public void Awake()
	{
		Application.logMessageReceived += Application_LogMessageReceived;
	}

	private void Application_LogMessageReceived(string condition, string stackTrace, LogType type)
	{
		var url = _remoteHost;

		switch (type)
		{
			case LogType.Log:
				url += "/remotelog/log";
				break;

			case LogType.Assert:
			case LogType.Exception:
			case LogType.Error:
				url += "/remotelog/error";
				break;

			case LogType.Warning:
				url += "/remotelog/warning";
				break;
		}
	}	
}
