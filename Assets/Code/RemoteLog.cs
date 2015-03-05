using UnityEngine;
using System.Collections;
using System;

public class RemoteLog : MonoBehaviour
{
	[SerializeField]
	private string _remoteHost = "http://192.168.101.70:9292";

	private HttpClient _httpClient = new HttpClient();

	public void Awake()
	{
#if UNITY_ANDROID || UNITY_IOS
		Application.logMessageReceived += Application_LogMessageReceived;
#endif
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

		_httpClient.PostString(url, string.Format("[{0}] - {1}", SystemInfo.deviceModel, condition));
	}	
}
