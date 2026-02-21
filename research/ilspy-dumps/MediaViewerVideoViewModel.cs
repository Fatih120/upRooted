using System;
using System.Runtime.CompilerServices;
using DotNetBrowser.Browser;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Zoom;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerVideoViewModel : ViewModelBase<MediaViewerVideoViewModel>
{
	private readonly Uri _uri;

	[CompilerGenerated]
	private string _003CVideoPath_003Ek__BackingField;

	public IBrowser Browser { get; }

	public ZoomService ZoomService { get; }

	public string VideoPath
	{
		[CompilerGenerated]
		get
		{
			return _003CVideoPath_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CVideoPath_003Ek__BackingField = text;
		}
	}

	public MediaViewerVideoViewModel(Uri P_0, IBrowser P_1, ZoomService P_2)
		: base((IValidator<MediaViewerVideoViewModel>?)null)
	{
		Browser = P_1;
		ZoomService = P_2;
		_uri = P_0;
		VideoPath = P_0.ToString().Split("/watch")[0];
	}
}
