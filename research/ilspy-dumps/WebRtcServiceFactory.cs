using DotNetBrowser.Js;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.Audio;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.ExperimentalFeatures;
using RootApp.Client.Avalonia.Helpers.Volume;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.Domain.Helpers.Store;

namespace RootApp.Browser.WebRtc.Services;

public class WebRtcServiceFactory(IRootSessionAccessor, DeviceServiceFactory, SoundPoolServiceFactory, ILoggerFactory, CallingServiceFactory, ISoundEffectService, PushToTalkService, ILocalDataStore, ScreenshareAudioFailedViewModelFactory, UserVolumeService, IAnalyticsService, LoopbackCaptureServiceFactory, IExperimentalFeaturesService)
{
	public WebRtcService Create(MediaRoom P_0, IJsObject P_1, WebRtcBrowser P_2, string P_3, BrowserService P_4)
	{
		return new WebRtcService(P_0, P_1, P_2, P_3, P_4, P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, P_9, P_10, P_11, P_12);
	}
}
