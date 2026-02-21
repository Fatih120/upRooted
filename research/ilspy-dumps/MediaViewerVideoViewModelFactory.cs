using System;
using DotNetBrowser.Browser;
using RootApp.Client.Avalonia.Helpers.Zoom;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerVideoViewModelFactory(ZoomService)
{
	public MediaViewerVideoViewModel Create(Uri P_0, IBrowser P_1)
	{
		return new MediaViewerVideoViewModel(P_0, P_1, P_0);
	}
}
