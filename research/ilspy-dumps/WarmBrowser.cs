using System;
using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using Microsoft.VisualStudio.Threading;

namespace RootApp.Browser;

public sealed class WarmBrowser : IDisposable
{
	public IBrowser DotNetBrowser { get; }

	public IJsObject Window { get; }

	public IFrame IFrameExecutionEnvironment { get; }

	public WarmBrowser(IBrowser P_0, IJsObject P_1, IFrame P_2)
	{
		DotNetBrowser = P_0;
		Window = P_1;
		IFrameExecutionEnvironment = P_2;
	}

	public void Dispose()
	{
		Task.Run(delegate
		{
			try
			{
				DotNetBrowser.Dispose();
			}
			catch
			{
			}
		}).Forget();
	}
}
