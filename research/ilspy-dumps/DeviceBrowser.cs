using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Js;
using DotNetBrowser.Navigation;
using RootApp.Browser.WebRtc;
using RootApp.Browser.WebRtc.Services;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public class DeviceBrowser : IRootBrowser, IDisposable
{
	private readonly DeviceServiceFactory _deviceServiceFactory;

	private readonly WebRtcBundleExtractor _webRtcBundleExtractor;

	[CompilerGenerated]
	private DeviceService <DeviceService>k__BackingField;

	public RootGuid Id { get; }

	public IBrowser DotNetBrowser { get; }

	public DeviceService DeviceService
	{
		[CompilerGenerated]
		get
		{
			return <DeviceService>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<DeviceService>k__BackingField = deviceService;
		}
	} = null;

	public DeviceBrowser(RootGuid P_0, IBrowser P_1, DeviceServiceFactory P_2, WebRtcBundleExtractor P_3)
	{
		Id = P_0;
		DotNetBrowser = P_1;
		_deviceServiceFactory = P_2;
		_webRtcBundleExtractor = P_3;
	}

	public async Task InitializeAsync()
	{
		string webrtcIndexFilePath = await _webRtcBundleExtractor.GetMainFileUrlAsync();
		if ((await DotNetBrowser.Navigation.LoadUrl(webrtcIndexFilePath)).LoadResult == LoadResult.Completed)
		{
			IJsObject window = await DotNetBrowser.MainFrame.ExecuteJavaScript<IJsObject>("window", userGesture: true);
			DeviceService = _deviceServiceFactory.Create(window, false);
		}
	}

	public void Dispose()
	{
		DotNetBrowser.Dispose();
	}
}
