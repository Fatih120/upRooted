using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Js;
using DotNetBrowser.Navigation;
using RootApp.Browser.WebRtc;
using RootApp.Browser.WebRtc.Services;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public sealed class WebRtcBrowser : IRootBrowser, IDisposable
{
	private readonly WebRtcServiceFactory _webRtcServiceFactory;

	private readonly WebRtcBundleExtractor _webRtcBundleExtractor;

	private readonly NoiseSuppressionBundleExtractor _noiseSuppressionBundleExtractor;

	private MediaRoom _mediaRoom;

	private readonly BrowserService _browserService;

	private readonly string _baseWebapiUrl;

	[CompilerGenerated]
	private WebRtcService <WebRtc>k__BackingField;

	public RootGuid Id { get; }

	public IBrowser DotNetBrowser { get; }

	public WebRtcService WebRtc
	{
		[CompilerGenerated]
		get
		{
			return <WebRtc>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<WebRtc>k__BackingField = webRtcService;
		}
	} = null;

	public WebRtcBrowser(RootGuid P_0, MediaRoom P_1, IBrowser P_2, string P_3, BrowserService P_4, WebRtcServiceFactory P_5, WebRtcBundleExtractor P_6, NoiseSuppressionBundleExtractor P_7)
	{
		Id = P_0;
		DotNetBrowser = P_2;
		_baseWebapiUrl = P_3.TrimEnd('/');
		_mediaRoom = P_1;
		_browserService = P_4;
		_webRtcServiceFactory = P_5;
		_webRtcBundleExtractor = P_6;
		_noiseSuppressionBundleExtractor = P_7;
	}

	public async Task InitializeAsync()
	{
		await _noiseSuppressionBundleExtractor.EnsureExtractedAsync();
		string webrtcIndexFilePath = await _webRtcBundleExtractor.GetMainFileUrlAsync();
		if ((await DotNetBrowser.Navigation.LoadUrl(webrtcIndexFilePath)).LoadResult == LoadResult.Completed)
		{
			await DotNetBrowser.MainFrame.ExecuteJavaScript("\r\n                document.addEventListener('dragover', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n                document.addEventListener('drop', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n            ");
			IJsObject window = await DotNetBrowser.MainFrame.ExecuteJavaScript<IJsObject>("window", userGesture: true);
			WebRtc = _webRtcServiceFactory.Create(_mediaRoom, window, this, _baseWebapiUrl, _browserService);
			return;
		}
		throw new Exception("Failed to load WebRTC browser.");
	}

	public void Dispose()
	{
		WebRtc.Dispose();
	}
}
