using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

public class HttpClient
{
	public void PostString(string url, string str)
	{
		var client = new WebClient();
		client.UploadDataCompleted += (o, e) =>
		{
			var c = e.UserState as WebClient;
			if (c != null)
			{
				c.Dispose();
			}
		};

		var data = Encoding.UTF8.GetBytes(str);
		client.UploadDataAsync(new Uri(url), "POST", data, client);
	}
}

