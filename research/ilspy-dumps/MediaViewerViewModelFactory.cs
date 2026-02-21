using DotNetBrowser.Browser;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Client.Domain.Helpers.Images.Caching;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public sealed class MediaViewerViewModelFactory
{
	private readonly BitmapCache _bitmapCache;

	private readonly IFileDownloader _fileDownloader;

	private readonly LinkHelper _linkHelper;

	private readonly ClipboardService _clipboardService;

	private readonly MediaViewerImageViewModelFactory _mediaViewerImageViewModelFactory;

	private readonly MediaViewerGifViewModelFactory _mediaViewerGifViewModelFactory;

	private readonly MediaViewerVideoViewModelFactory _mediaViewerVideoViewModelFactory;

	private readonly ILogger<MediaViewerViewModel> _logger;

	public MediaViewerViewModelFactory(BitmapCache P_0, IFileDownloader P_1, LinkHelper P_2, ClipboardService P_3, MediaViewerImageViewModelFactory P_4, MediaViewerGifViewModelFactory P_5, MediaViewerVideoViewModelFactory P_6, ILoggerFactory P_7)
	{
		_bitmapCache = P_0;
		_fileDownloader = P_1;
		_linkHelper = P_2;
		_clipboardService = P_3;
		_mediaViewerImageViewModelFactory = P_4;
		_mediaViewerGifViewModelFactory = P_5;
		_mediaViewerVideoViewModelFactory = P_6;
		_logger = P_7.CreateLogger<MediaViewerViewModel>();
		base._002Ector();
	}

	public MediaViewerViewModel Create(GlobalUser P_0, Attachment P_1, IBrowser? P_2 = null)
	{
		return new MediaViewerViewModel(P_0, P_1, _bitmapCache, _fileDownloader, _linkHelper, _clipboardService, _mediaViewerImageViewModelFactory, _mediaViewerGifViewModelFactory, _mediaViewerVideoViewModelFactory, _logger, P_2);
	}
}
