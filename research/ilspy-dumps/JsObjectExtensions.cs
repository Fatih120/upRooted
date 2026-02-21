using DotNetBrowser.Js;

namespace RootApp.Browser.Models;

public static class JsObjectExtensions
{
	public static JsPromise AsPromise(this IJsObject P_0)
	{
		return JsPromise.AsPromise(P_0);
	}
}
