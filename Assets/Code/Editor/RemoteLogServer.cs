using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class RemoteLogServer
{
	private static HttpServer s_server;

	static RemoteLogServer()
	{
		EditorApplication.update += Update;
	}

	private static void Update()
	{
		Start();

		EditorApplication.update -= Update;
	}

	[MenuItem("RemoteLog/Start")]
	public static void Start()
	{
		Stop();

		s_server = HttpServer.StartNew(9292);
		
		Debug.Log("RemoteLogServer started.");
	}

	[MenuItem("RemoteLog/Stop")]
	public static void Stop()
	{
		if (s_server != null)
		{
			s_server.Stop();
			s_server = null;

			Debug.Log("RemoteLogServer stopped.");
		}
	}
}
