using DotNetBrowser.Js;
using Microsoft.VisualStudio.Threading;
using RootApp.App.Messaging.Grpc;

namespace RootApp.Browser.RootApps.Bridges;

public class NativeToAppPrivateBridge
{
	private readonly IJsObject _hook;

	public NativeToAppPrivateBridge(IJsObject P_0)
	{
		_hook = P_0;
	}

	public void SetTheme(IJsObject P_0)
	{
		_hook.InvokeAsync("setTheme", P_0).Forget();
	}

	public void Receive(byte[] P_0, AppRpcMessageType P_1)
	{
		_hook.InvokeAsync("receive", P_0, P_1).Forget();
	}
}
