using System;
using Avalonia.Media.Imaging;
using RootApp.Client.Avalonia.Helpers.Caching;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerImageViewModelFactory(BitmapCache)
{
	public MediaViewerImageViewModel Create(Uri P_0, string P_1, Bitmap? P_2)
	{
		return new MediaViewerImageViewModel(P_0, P_1, P_2, P_0);
	}
}
