using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.Domain.Helpers.Images.Caching;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerGifViewModelFactory
{
	private static readonly MemoryCacheOptions _memoryCacheOptions = new MemoryCacheOptions();

	private readonly IHostApplicationLifetime _applicationLifetime;

	private readonly IFileDownloader _fileDownloader;

	private readonly ILogger<MediaViewerGifViewModel> _logger;

	private readonly IMemoryCache _memoryCache;

	public MediaViewerGifViewModelFactory(IFileDownloader P_0, IHostApplicationLifetime P_1, ILoggerFactory P_2)
	{
		_applicationLifetime = P_1;
		_fileDownloader = P_0;
		_logger = P_2.CreateLogger<MediaViewerGifViewModel>();
		_memoryCache = new MemoryCache(_memoryCacheOptions, P_2);
		base._002Ector();
	}

	public MediaViewerGifViewModel Create(Attachment P_0)
	{
		return new MediaViewerGifViewModel(P_0, _fileDownloader, _applicationLifetime, _memoryCache, _logger);
	}
}
