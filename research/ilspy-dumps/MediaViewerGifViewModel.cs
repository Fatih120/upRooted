using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using RootApp.Assets;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.Domain.Helpers.Images.Caching;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerGifViewModel : ViewModelBase<MediaViewerGifViewModel>
{
	private static readonly TimeSpan? _slidingExpiration = TimeSpan.FromMinutes(20L);

	private static readonly Lock _cacheLock = new Lock();

	private readonly IHostApplicationLifetime _applicationLifetime;

	private readonly IFileDownloader _fileDownloader;

	private readonly Attachment _gifAttachment;

	private readonly ILogger<MediaViewerGifViewModel> _logger;

	private readonly IMemoryCache _memoryCache;

	private Task? _loadingTask;

	[CompilerGenerated]
	private double _003CPlaceholderWidth_003Ek__BackingField;

	[CompilerGenerated]
	private double _003CPlaceholderHeight_003Ek__BackingField;

	[CompilerGenerated]
	private object? _003CImageData_003Ek__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PlaceholderWidth
	{
		get
		{
			return _003CPlaceholderWidth_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CPlaceholderWidth_003Ek__BackingField, num))
			{
				_003CPlaceholderWidth_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PlaceholderWidth);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PlaceholderHeight
	{
		get
		{
			return _003CPlaceholderHeight_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CPlaceholderHeight_003Ek__BackingField, num))
			{
				_003CPlaceholderHeight_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PlaceholderHeight);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public object? ImageData
	{
		get
		{
			return _003CImageData_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<object>.Default.Equals(_003CImageData_003Ek__BackingField, obj))
			{
				_003CImageData_003Ek__BackingField = obj;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ImageData);
			}
		}
	}

	public MediaViewerGifViewModel(Attachment P_0, IFileDownloader P_1, IHostApplicationLifetime P_2, IMemoryCache P_3, ILogger<MediaViewerGifViewModel> P_4)
		: base((IValidator<MediaViewerGifViewModel>?)null)
	{
		_gifAttachment = P_0;
		_fileDownloader = P_1;
		_applicationLifetime = P_2;
		_memoryCache = P_3;
		_logger = P_4;
		_loadingTask = GetAssetDataAsync();
	}

	public async Task<ReadOnlyMemory<byte>> GetAssetDataAsync()
	{
		AssetImageLink assetLink = _gifAttachment.AssetLinkWrapper.ImageWrapper.GetAssetLink(AssetLinkSize.Medium);
		int origW = assetLink.WidthEstimate;
		int origH = assetLink.HeightEstimate;
		setPreviewSize(origW, origH);
		var (data, downloadResult) = await GetDataAsync(assetLink, origW, origH);
		if ((downloadResult?.Width.HasValue ?? false) && downloadResult.Height.HasValue)
		{
			setPreviewSize(downloadResult.Width.Value, downloadResult.Height.Value);
		}
		if (data.Length != 0)
		{
			ImageData = data;
		}
		return data;
	}

	private async Task<(byte[] data, DownloadResult? downloadResult)> GetDataAsync(AssetImageLink P_0, int P_1, int P_2)
	{
		AwaitExtensions.ConfiguredTaskYieldAwaiter awaiter = Task.Yield().ConfigureAwait(continueOnCapturedContext: false).GetAwaiter();
		_ = awaiter.IsCompleted;
		await awaiter;
		AwaitExtensions.ConfiguredTaskYieldAwaiter configuredTaskYieldAwaiter = default(AwaitExtensions.ConfiguredTaskYieldAwaiter);
		awaiter = configuredTaskYieldAwaiter;
		int num = -1;
		awaiter.GetResult();
		string key = $"{_gifAttachment.AssetLinkWrapper.AssetUrl}:{2}";
		CancellationToken cancellationToken = _applicationLifetime.ApplicationStopping;
		Lock.Scope scope = _cacheLock.EnterScope();
		Task<(byte[] data, DownloadResult? downloadResult)> dataTask;
		try
		{
			dataTask = _memoryCache.GetOrCreate(key, (ICacheEntry _) => Task.Run(() => ReadDataAsync(P_0, key, cancellationToken), cancellationToken), new MemoryCacheEntryOptions
			{
				Size = 4 * P_1 * P_2,
				SlidingExpiration = _slidingExpiration
			});
		}
		finally
		{
			if (num < 0)
			{
				scope.Dispose();
			}
		}
		if (dataTask != null)
		{
			return await dataTask.ConfigureAwait(continueOnCapturedContext: false);
		}
		LogCacheEntryFailed(_gifAttachment.AssetLinkWrapper.AssetUrl);
		return (data: Array.Empty<byte>(), downloadResult: null);
	}

	private async Task<(byte[] data, DownloadResult? downloadResult)> ReadDataAsync(AssetImageLink P_0, string P_1, CancellationToken P_2)
	{
		try
		{
			DownloadResult downloadResult = await _fileDownloader.GetOrDownloadFileAsync(P_1, new Uri(P_0.Url), null, false, P_2);
			if (!downloadResult.IsValid)
			{
				LogDownloadFailed(_gifAttachment.AssetLinkWrapper.AssetUrl);
				return (data: Array.Empty<byte>(), downloadResult: null);
			}
			return (data: await File.ReadAllBytesAsync(downloadResult.Path, P_2), downloadResult: downloadResult);
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			LogCacheError(ex2, _gifAttachment.AssetLinkWrapper.AssetUrl);
		}
		return (data: Array.Empty<byte>(), downloadResult: null);
	}

	private void setPreviewSize(double P_0, double P_1)
	{
		PlaceholderWidth = P_0;
		PlaceholderHeight = P_1;
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Failed to download gif {AssetId}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogDownloadFailed(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Failed to download gif {AssetId}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("AssetId", P_0);
			_logger.Log(LogLevel.Warning, new EventId(654635564, "LogDownloadFailed"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(23, 1, invariantCulture);
				handler.AppendLiteral("Failed to download gif ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Error caching gif {AssetId}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogCacheError(Exception P_0, string P_1)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Error caching gif {AssetId}");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("AssetId", P_1);
		_logger.Log(LogLevel.Error, new EventId(990254533, "LogCacheError"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object obj = s.TagArray[0].Value ?? "(null)";
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(18, 1, invariantCulture);
			handler.AppendLiteral("Error caching gif ");
			handler.AppendFormatted<object>(obj);
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Failed to get or create cache entry for gif {AssetId}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogCacheEntryFailed(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Failed to get or create cache entry for gif {AssetId}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("AssetId", P_0);
			_logger.Log(LogLevel.Warning, new EventId(794539974, "LogCacheEntryFailed"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(44, 1, invariantCulture);
				handler.AppendLiteral("Failed to get or create cache entry for gif ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
