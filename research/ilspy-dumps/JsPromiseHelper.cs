using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;

namespace RootApp.Browser.Models;

public static class JsPromiseHelper
{
	private static int _counter;

	public static IJsPromise FromAsync(IFrame P_0, Func<Task<IJsObject>> P_1)
	{
		Task<IJsObject> value = P_1();
		string text = $"__jph_{Interlocked.Increment(ref _counter)}";
		IJsObject result = P_0.ExecuteJavaScript<IJsObject>("window").Result;
		result.Properties[text] = value;
		try
		{
			return P_0.ExecuteJavaScript<IJsPromise>("window[\"" + text + "\"].ToJsPromise()").Result;
		}
		finally
		{
			P_0.ExecuteJavaScript("delete window[\"" + text + "\"]");
		}
	}

	public static IJsPromise FromResult(IFrame P_0, IJsObject P_1)
	{
		string text = $"__jph_{Interlocked.Increment(ref _counter)}";
		IJsObject result = P_0.ExecuteJavaScript<IJsObject>("window").Result;
		result.Properties[text] = P_1;
		try
		{
			return P_0.ExecuteJavaScript<IJsPromise>("Promise.resolve(window[\"" + text + "\"])").Result;
		}
		finally
		{
			P_0.ExecuteJavaScript("delete window[\"" + text + "\"]");
		}
	}
}
