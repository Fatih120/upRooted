using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Caching;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerImageViewModel : ViewModelBase<MediaViewerImageViewModel>
{
	private readonly Uri _uri;

	private readonly string _key;

	private readonly BitmapCache _bitmapCache;

	[CompilerGenerated]
	private Bitmap? _003CImagePreviewBitmap_003Ek__BackingField;

	public Task<BitmapWrapper?> ImageAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(_uri.ToString(), _key);

	public Bitmap? ImagePreviewBitmap
	{
		[CompilerGenerated]
		get
		{
			return _003CImagePreviewBitmap_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CImagePreviewBitmap_003Ek__BackingField = bitmap;
		}
	}

	public MediaViewerImageViewModel(Uri P_0, string P_1, Bitmap? P_2, BitmapCache P_3)
		: base((IValidator<MediaViewerImageViewModel>?)null)
	{
		_uri = P_0;
		_key = P_1;
		_bitmapCache = P_3;
		ImagePreviewBitmap = P_2;
	}
}
