using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class HttpServer
{
	private int _port;
	private HttpListener _listener;
	private bool _running;

	public HttpServer(int port)
	{
		_port = port;
	}

	public static HttpServer StartNew(int port)
	{
		var server = new HttpServer(port);
		server.Start();

		return server;
	}

	public void Start()
	{
		_listener = new HttpListener();
		_listener.Prefixes.Add(string.Format("http://*:{0}/", _port));
		_listener.Start();

		_running = true;

		var thread = new Thread(HandleRequests);
		thread.Start();
	}

	public void Stop()
	{
		_running = false;
		if (_listener != null)
		{
			_listener.Close();
		}
	}

	private void HandleRequests()
	{
		while (_running)
		{
			HttpListenerContext context = _listener.GetContext();

			var thread = new Thread(HandleRequest);
			thread.Start(context);
		}
	}

	private void HandleRequest(object obj)
	{
		var context = obj as HttpListenerContext;
		var request = context.Request;

		switch (request.Url.LocalPath)
		{
			case "/remotelog/log":
				HandleDebugLog(request);
				break;

			case "/remotelog/warning":
				HandleDebugWarning(request);
				break;

			case "/remotelog/error":
				HandleDebugError(request);
				break;
		}

		// Send 0 response
		context.Response.ContentLength64 = 0;
		context.Response.OutputStream.Close();
	}

	private void HandleDebugLog(HttpListenerRequest request)
	{
		using (var reader = new StreamReader(request.InputStream))
		{
			var str = reader.ReadToEnd();
			Debug.Log(str);
		}
    }

	private void HandleDebugWarning(HttpListenerRequest request)
	{
		using (var reader = new StreamReader(request.InputStream))
		{
			var str = reader.ReadToEnd();
			Debug.LogWarning(str);
		}
	}

	private void HandleDebugError(HttpListenerRequest request)
	{
		using (var reader = new StreamReader(request.InputStream))
		{
			var str = reader.ReadToEnd();
			Debug.LogError(str);
		}
	}
}

