using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DotNetBrowser.Browser;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using RootApp.Assets;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Client.Domain.Helpers.Images.Caching;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerViewModel : ViewModelBase<MediaViewerViewModel>, IBlocksEscapeKey
{
	private readonly LinkHelper _linkHelper;

	private readonly BitmapCache _bitmapCache;

	private readonly IFileDownloader _fileDownloader;

	private readonly ClipboardService _clipboardService;

	private readonly MediaViewerImageViewModelFactory _mediaViewerImageViewModelFactory;

	private readonly MediaViewerGifViewModelFactory _mediaViewerGifViewModelFactory;

	private readonly MediaViewerVideoViewModelFactory _mediaViewerVideoViewModelFactory;

	private readonly ILogger<MediaViewerViewModel> _logger;

	private readonly IBrowser? _browser;

	[CompilerGenerated]
	private bool <CanCopyToClipboard>k__BackingField;

	[CompilerGenerated]
	private bool <CanDownload>k__BackingField;

	[CompilerGenerated]
	private bool <ShouldStretch>k__BackingField;

	[CompilerGenerated]
	private long <DownloadPercentage>k__BackingField;

	[CompilerGenerated]
	private bool <IsDownloading>k__BackingField;

	[CompilerGenerated]
	private bool <DownloadComplete>k__BackingField;

	[CompilerGenerated]
	private IViewModelBase? <MediaViewerContent>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<IStorageProvider>? saveImageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? openContainerFolderCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? copyImageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyLinkCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? openLinkCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public long DownloadPercentage
	{
		get
		{
			return <DownloadPercentage>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<long>.Default.Equals(<DownloadPercentage>k__BackingField, num))
			{
				<DownloadPercentage>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DownloadPercentage);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsDownloading
	{
		get
		{
			return <IsDownloading>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsDownloading>k__BackingField, flag))
			{
				<IsDownloading>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsDownloading);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DownloadComplete
	{
		get
		{
			return <DownloadComplete>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<DownloadComplete>k__BackingField, flag))
			{
				<DownloadComplete>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DownloadComplete);
			}
		}
	}

	public GlobalUser GlobalUser { get; }

	public Attachment Attachment { get; }

	public bool IsGif => Attachment.FileType == FileType.Gif;

	public bool IsVideo => Attachment.FileType == FileType.Video;

	public bool CanCopyToClipboard
	{
		[CompilerGenerated]
		get
		{
			return <CanCopyToClipboard>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<CanCopyToClipboard>k__BackingField = flag;
		}
	}

	public bool CanDownload
	{
		[CompilerGenerated]
		get
		{
			return <CanDownload>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<CanDownload>k__BackingField = flag;
		}
	}

	public bool ShouldStretch
	{
		[CompilerGenerated]
		get
		{
			return <ShouldStretch>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<ShouldStretch>k__BackingField = flag;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? MediaViewerContent
	{
		get
		{
			return <MediaViewerContent>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(<MediaViewerContent>k__BackingField, viewModelBase))
			{
				<MediaViewerContent>k__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MediaViewerContent);
			}
		}
	}

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(GlobalUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<IStorageProvider> SaveImageCommand => saveImageCommand ?? (saveImageCommand = new AsyncRelayCommand<IStorageProvider>(SaveImageAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand OpenContainerFolderCommand => openContainerFolderCommand ?? (openContainerFolderCommand = new RelayCommand(OpenContainerFolder));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CopyImageCommand => copyImageCommand ?? (copyImageCommand = new AsyncRelayCommand(CopyImageAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyLinkCommand => copyLinkCommand ?? (copyLinkCommand = new RelayCommand(CopyLink));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand OpenLinkCommand => openLinkCommand ?? (openLinkCommand = new RelayCommand(OpenLink));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public MediaViewerViewModel(GlobalUser P_0, Attachment P_1, BitmapCache P_2, IFileDownloader P_3, LinkHelper P_4, ClipboardService P_5, MediaViewerImageViewModelFactory P_6, MediaViewerGifViewModelFactory P_7, MediaViewerVideoViewModelFactory P_8, ILogger<MediaViewerViewModel> P_9, IBrowser? P_10 = null)
		: base((IValidator<MediaViewerViewModel>?)null)
	{
		_linkHelper = P_4;
		_clipboardService = P_5;
		_mediaViewerImageViewModelFactory = P_6;
		_mediaViewerGifViewModelFactory = P_7;
		_mediaViewerVideoViewModelFactory = P_8;
		_logger = P_9;
		_browser = P_10;
		_bitmapCache = P_2;
		_fileDownloader = P_3;
		GlobalUser = P_0;
		Attachment = P_1;
		CanCopyToClipboard = !IsGif && !IsVideo;
		CanDownload = !IsVideo;
		loadContentAsync().Forget();
	}

	private async Task loadContentAsync()
	{
		switch (Attachment.FileType)
		{
		case FileType.Image:
			if (!Attachment.AssetLinkWrapper.ImageWrapper.ImagePreviewData.IsEmpty)
			{
				AssetImageLink assetLink = Attachment.AssetLinkWrapper.ImageWrapper.GetAssetLink(AssetLinkSize.Large);
				try
				{
					ReadOnlyMemory<byte> previewData = Attachment.AssetLinkWrapper.ImageWrapper.ImagePreviewData;
					if (previewData.IsEmpty)
					{
						break;
					}
					int width = assetLink.WidthEstimate;
					int height = assetLink.HeightEstimate;
					if (width <= 0 || height <= 0)
					{
						LogInvalidImageDimensions(_logger, Attachment.AssetLinkWrapper.AssetUrl, width, height);
						break;
					}
					byte[] previewBytes = previewData.ToArray();
					PixelSize targetSize = new PixelSize(width, height);
					Bitmap previewBitmap = await Task.Run(delegate
					{
						using MemoryStream memoryStream = new MemoryStream(previewBytes);
						using Bitmap bitmap = new Bitmap(memoryStream);
						return bitmap.CreateScaledBitmap(targetSize, BitmapInterpolationMode.LowQuality);
					});
					MediaViewerContent = _mediaViewerImageViewModelFactory.Create(Attachment.AssetLinkWrapper.GetFullUri(), $"{Attachment.AssetLinkWrapper.AssetUrl}:{3}", previewBitmap);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					LogUnableToLoadPreview(_logger, ex2, Attachment.AssetLinkWrapper.AssetUrl);
				}
			}
			else
			{
				MediaViewerContent = _mediaViewerImageViewModelFactory.Create(Attachment.AssetLinkWrapper.GetFullUri(), $"{Attachment.AssetLinkWrapper.AssetUrl}:{3}", null);
			}
			break;
		case FileType.Gif:
			MediaViewerContent = _mediaViewerGifViewModelFactory.Create(Attachment);
			break;
		case FileType.Video:
			if (_browser != null)
			{
				ShouldStretch = true;
				MediaViewerVideoViewModelFactory mediaViewerVideoViewModelFactory = _mediaViewerVideoViewModelFactory;
				MediaViewerContent = mediaViewerVideoViewModelFactory.Create(await Attachment.AssetLinkWrapper.GetPreviewUriAsync(), _browser);
			}
			break;
		case FileType.Spreadsheet:
		case FileType.PDF:
			break;
		}
	}

	[RelayCommand]
	public async Task SaveImageAsync(IStorageProvider storageProvider)
	{
		try
		{
			IsDownloading = true;
			DownloadComplete = false;
			Uri uri = Attachment.AssetLinkWrapper.GetDownloadUri();
			DownloadResult result = await _fileDownloader.GetOrDownloadFileAsync(uri.ToString(), uri, new Progress<DownloadProgress>(delegate(DownloadProgress s)
			{
				double num = (double)s.DownloadedBytes / (double)Attachment.Length * 100.0;
				DownloadPercentage = (long)num;
			}));
			if (result.IsValid)
			{
				await _fileDownloader.CopyFileToLocalDestinationAsync(result, Attachment.Filename);
				DownloadPercentage = 100L;
				await Task.Delay(200);
				IsDownloading = false;
				DownloadComplete = true;
			}
		}
		catch
		{
			IsDownloading = false;
			DownloadComplete = false;
		}
	}

	[RelayCommand]
	public void OpenContainerFolder()
	{
		_linkHelper.OpenDownloadsFolder();
	}

	[RelayCommand]
	public async Task CopyImageAsync()
	{
		BitmapWrapper bitmapWrapper = await _bitmapCache.GetBitmapAsync(Attachment.AssetLinkWrapper.GetFullUri().ToString());
		if (bitmapWrapper != null)
		{
			_clipboardService.CopyImageToClipboard(bitmapWrapper.Bitmap);
		}
	}

	[RelayCommand]
	public void CopyLink()
	{
		_clipboardService.CopyTextToClipboard(Attachment.AssetLinkWrapper.GetFullUri().ToString());
	}

	[RelayCommand]
	public void OpenLink()
	{
		_linkHelper.OpenLink(Attachment.AssetLinkWrapper.GetFullUri().ToString());
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	public override void Dispose()
	{
		base.Dispose();
		MediaViewerContent?.Dispose();
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Unable to load preview for {AssetId}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogUnableToLoadPreview(ILogger P_0, Exception P_1, string P_2)
	{
		if (P_0.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Unable to load preview for {AssetId}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("AssetId", P_2);
			P_0.Log(LogLevel.Warning, new EventId(118408303, "LogUnableToLoadPreview"), threadLocalState, P_1, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(27, 1, invariantCulture);
				handler.AppendLiteral("Unable to load preview for ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Invalid image dimensions for {AssetId}: {Width}x{Height}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogInvalidImageDimensions(ILogger P_0, string P_1, int P_2, int P_3)
	{
		if (P_0.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "Invalid image dimensions for {AssetId}: {Width}x{Height}");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("AssetId", P_1);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Width", P_2);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Height", P_3);
			P_0.Log(LogLevel.Warning, new EventId(603299804, "LogInvalidImageDimensions"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[2].Value ?? "(null)";
				object value = s.TagArray[1].Value;
				object value2 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(32, 3, invariantCulture);
				handler.AppendLiteral("Invalid image dimensions for ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral("x");
				handler.AppendFormatted<object>(value2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
