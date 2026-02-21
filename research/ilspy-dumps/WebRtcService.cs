using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Styling;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using DotNetBrowser.Js;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser.WebRtc.Bridges;
using RootApp.Browser.WebRtc.Models;
using RootApp.Client.Avalonia.Helpers.Audio;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.ExperimentalFeatures;
using RootApp.Client.Avalonia.Helpers.Volume;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Client.Domain.Helpers.Store.State.Media;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.Browser.WebRtc.Services;

public class WebRtcService : IDisposable
{
	private readonly string _webapiBaseUrl;

	private readonly ILogger<WebRtcService> _logger;

	private readonly IAnalyticsService _analyticsService;

	private CancellationTokenSource? _pttCts;

	private IJsObject _window;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly PushToTalkService _pushToTalkService;

	private readonly UserVolumeService _userVolumeService;

	private WebRtcToNativeBridge _webRtcToNativeBridge;

	private NativeToWebRtcBridge _nativeToWebRtcBridge;

	private bool _shouldQueuePackets;

	private MediaDeviceState? _mediaDeviceState;

	private readonly Queue<IPacket> _packetQueue;

	private readonly LoopbackCaptureServiceFactory _loopbackCaptureServiceFactory;

	private readonly IExperimentalFeaturesService _experimentalFeaturesService;

	private LoopbackCaptureService? _loopbackCaptureService;

	private bool _isNativeLoopbackActive;

	[CompilerGenerated]
	private MediaRoom <MediaRoom>k__BackingField;

	[CompilerGenerated]
	private DeviceService <DeviceService>k__BackingField;

	[CompilerGenerated]
	private SoundPoolService <SoundPoolService>k__BackingField;

	[CompilerGenerated]
	private CallingService <CallingService>k__BackingField;

	[CompilerGenerated]
	private WebRtcBrowser <WebRtcBrowser>k__BackingField;

	public MediaRoom MediaRoom
	{
		[CompilerGenerated]
		get
		{
			return <MediaRoom>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<MediaRoom>k__BackingField = mediaRoom;
		}
	}

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
	}

	public SoundPoolService SoundPoolService
	{
		[CompilerGenerated]
		get
		{
			return <SoundPoolService>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<SoundPoolService>k__BackingField = soundPoolService;
		}
	}

	public CallingService CallingService
	{
		[CompilerGenerated]
		get
		{
			return <CallingService>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<CallingService>k__BackingField = callingService;
		}
	}

	public WebRtcBrowser WebRtcBrowser
	{
		[CompilerGenerated]
		get
		{
			return <WebRtcBrowser>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<WebRtcBrowser>k__BackingField = webRtcBrowser;
		}
	}

	public WebRtcService(MediaRoom P_0, IJsObject P_1, WebRtcBrowser P_2, string P_3, BrowserService P_4, IRootSessionAccessor P_5, DeviceServiceFactory P_6, SoundPoolServiceFactory P_7, ILoggerFactory P_8, CallingServiceFactory P_9, ISoundEffectService P_10, PushToTalkService P_11, ILocalDataStore P_12, ScreenshareAudioFailedViewModelFactory P_13, UserVolumeService P_14, IAnalyticsService P_15, LoopbackCaptureServiceFactory P_16, IExperimentalFeaturesService P_17)
	{
		MediaRoom = P_0;
		_window = P_1;
		WebRtcBrowser = P_2;
		_webapiBaseUrl = P_3;
		_rootSessionAccessor = P_5;
		_pushToTalkService = P_11;
		_userVolumeService = P_14;
		_analyticsService = P_15;
		_loopbackCaptureServiceFactory = P_16;
		_experimentalFeaturesService = P_17;
		_logger = P_8.CreateLogger<WebRtcService>();
		_packetQueue = new Queue<IPacket>();
		CallingService = P_9.Create(P_4);
		_webRtcToNativeBridge = new WebRtcToNativeBridge(WebRtcBrowser.DotNetBrowser.MainFrame, this, P_10, P_12, P_13);
		_window.Properties["__webRtcToNative"] = _webRtcToNativeBridge.JsObject;
		_nativeToWebRtcBridge = new NativeToWebRtcBridge((IJsObject)_window.Properties["__nativeToWebRtc"]);
		_userVolumeService.SetWebRtcService(this);
		DeviceService = P_6.Create(P_1);
		SoundPoolService = P_7.Create(P_1);
		Application.Current.ActualThemeVariantChanged += onActualThemeVariantChanged;
	}

	public async Task StartCallAsync(string[]? P_0, int? P_1)
	{
		try
		{
			ThemeVariant themeVariant = null;
			await Dispatcher.UIThread.InvokeAsync(delegate
			{
				themeVariant = Application.Current.ActualThemeVariant;
			});
			MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Connecting);
			MediaRoom.WebRtcPacketReceived += onMediaRoomWebRtcPacketReceived;
			MediaRoom.PlaySoundEffectRequested += onMediaRoomPlaySoundEffectRequested;
			_shouldQueuePackets = true;
			_mediaDeviceState = await DeviceService.GetSelectedMediaDeviceStateAsync();
			await SoundPoolService.InitializeAsync();
			_window.Frame.ExecuteJavaScript<IJsPromise>("window.__setOutputDevice('" + (_mediaDeviceState.AudioOutputDeviceId ?? "default") + "')").Forget();
			List<MediaMember> activeMembers = MediaRoom.GetMembers().ToList();
			UserTileVolume[] tileVolumes = _userVolumeService.GetAllCachedTileVolumes();
			InitializeWebRtcPayload initObject = new InitializeWebRtcPayload
			{
				theme = ThemeMapper.ToWebRtcThemeString(themeVariant),
				callPlatform = "desktop",
				currentDeviceId = _rootSessionAccessor.Session.DeviceId.ToString("s"),
				permissions = new WebRtcPermission
				{
					channelVoiceTalk = MediaRoom.MessageContainer.LocalChannelPermission.ChannelVoiceTalk,
					channelVideoStreamMedia = MediaRoom.MessageContainer.LocalChannelPermission.ChannelVideoStreamMedia
				},
				currentUserId = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString("s"),
				containerId = MediaRoom.MessageContainer.ContainerId.ToString("s"),
				communityId = MediaRoom.MessageContainer.CommunityId?.ToString("s"),
				webApiBaseUrl = _webapiBaseUrl,
				videoDeviceId = "",
				audioInputDeviceId = "",
				audioOutputDeviceId = _mediaDeviceState.AudioOutputDeviceId,
				isPushToTalkMode = _pushToTalkService.IsPushToTalkEnabled,
				debugMode = false,
				logging = "state-info",
				ringingUserIds = (P_0 ?? Array.Empty<string>()),
				ringTimeoutMs = (P_1 ?? 30000),
				activeUserIds = activeMembers.Select((MediaMember member) => member.UserId.ToString("s")).ToArray(),
				inputVolume = _userVolumeService.GetGlobalInputVolume() / 100.0,
				outputVolume = _userVolumeService.GetGlobalOutputVolume() / 100.0,
				defaultGlobalOutputVolume = 1.0,
				defaultInputVolume = 1.0,
				defaultPrimaryOutputVolume = 1.0,
				defaultScreenOutputVolume = 0.5,
				tileVolumes = ((tileVolumes.Length != 0) ? tileVolumes : null),
				experimentalFlags = buildExperimentalFlags()
			};
			_nativeToWebRtcBridge.Initialize(initObject.ToJsObject(WebRtcBrowser.DotNetBrowser.MainFrame));
			DeviceService.SelectedDeviceChanged += DeviceServiceSelectedDeviceChanged;
			DeviceService.NoiseSuppressionStrengthChanged += DeviceServiceNoiseSuppressionStrengthChanged;
			DeviceService.MediaConstraintsChanged += onDeviceServiceMediaConstraintsChanged;
			_pushToTalkService.SetPushToTalkOnPressAction(OnPushToTalkPress);
			_pushToTalkService.SetPushToTalkOnReleaseAction(OnPushToTalkRelease);
			_pushToTalkService.PropertyChanged += onPushToTalkServicePropertyChanged;
			_pushToTalkService.StartListening();
			WeakReferenceMessenger.Default.Register<OpenWebRtcDevLogsMessage>(this, onOpenWebRtcDevLogsMessageReceived);
			_analyticsService.IncrementVoiceCallsJoined();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to start call");
			MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Error);
			MediaRoom.WebRtcPacketReceived -= onMediaRoomWebRtcPacketReceived;
			MediaRoom.PlaySoundEffectRequested -= onMediaRoomPlaySoundEffectRequested;
			_shouldQueuePackets = false;
			_packetQueue.Clear();
		}
	}

	public void Initialized()
	{
		SetDenoisePower((float)DeviceService.GetNoiseSuppressionStrength());
		SetMediaConstraints(DeviceService.GetEchoCancellationEnabled(), DeviceService.GetAutomaticGainControlEnabled());
	}

	private void onMediaRoomPlaySoundEffectRequested(string? soundEffectPath)
	{
		if (soundEffectPath != null)
		{
			SoundPoolService.PlayAsync(soundEffectPath).Forget();
		}
		else
		{
			SoundPoolService.StopAllAsync().Forget();
		}
	}

	private void disconnect()
	{
		try
		{
			_nativeToWebRtcBridge.Disconnect();
			MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Disconnecting);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to disconnect");
			MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Disconnected);
			WeakReferenceMessenger.Default.Send(new BrowserDisposingMessage(WebRtcBrowser.Id));
			WebRtcBrowser.DotNetBrowser.Dispose();
		}
	}

	public void SetIsVideoOn(bool P_0)
	{
		_nativeToWebRtcBridge.SetIsVideoOn(P_0);
	}

	public void SetScreenShare(bool P_0, bool P_1)
	{
		if (!P_0)
		{
			StopNativeLoopbackCapture();
		}
		_nativeToWebRtcBridge.SetIsScreenShareOn(P_0, P_1);
	}

	public Task StartScreenShareAudioCaptureAsync()
	{
		return StartNativeLoopbackCaptureAsync();
	}

	public void StopScreenShareAudioCapture()
	{
		StopNativeLoopbackCapture();
	}

	public async Task RestartScreenShareAudioCaptureForWindowAsync(string P_0)
	{
		_logger.LogInformation("Restarting audio capture for window: {WindowTitle}", P_0);
		StopNativeLoopbackCapture();
		await StartNativeLoopbackCaptureAsync(P_0);
	}

	private async Task StartNativeLoopbackCaptureAsync(string? P_0 = null)
	{
		if (_isNativeLoopbackActive)
		{
			_logger.LogDebug("Native loopback capture already active, skipping start.");
			return;
		}
		if (!LoopbackCaptureService.IsSupported)
		{
			_logger.LogInformation("Native loopback capture not supported on this OS version. Using browser-based capture.");
			return;
		}
		try
		{
			_loopbackCaptureService = _loopbackCaptureServiceFactory.Create();
			_loopbackCaptureService.AudioDataAvailable += OnLoopbackAudioDataAvailable;
			_loopbackCaptureService.CaptureStopped += OnLoopbackCaptureStopped;
			await _loopbackCaptureService.StartCaptureAsync(P_0);
			_isNativeLoopbackActive = true;
			await _nativeToWebRtcBridge.NativeLoopbackAudioStartedAsync(_loopbackCaptureService.SampleRate, _loopbackCaptureService.Channels);
			string modeDescription = (string.IsNullOrEmpty(P_0) ? "desktop (excluding app audio)" : ("window '" + P_0 + "' (process audio only)"));
			_logger.LogInformation("Native loopback capture started for {Mode}", modeDescription);
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Failed to start native loopback capture. Falling back to browser-based capture.");
			_loopbackCaptureService?.Dispose();
			_loopbackCaptureService = null;
			_isNativeLoopbackActive = false;
		}
	}

	private void StopNativeLoopbackCapture()
	{
		if (_loopbackCaptureService == null)
		{
			return;
		}
		try
		{
			_isNativeLoopbackActive = false;
			_loopbackCaptureService.AudioDataAvailable -= OnLoopbackAudioDataAvailable;
			_loopbackCaptureService.CaptureStopped -= OnLoopbackCaptureStopped;
			_loopbackCaptureService.StopCapture();
			_loopbackCaptureService.Dispose();
			_loopbackCaptureService = null;
			_nativeToWebRtcBridge.StopNativeLoopbackAudio();
			_logger.LogInformation("Native loopback capture stopped");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error stopping native loopback capture");
		}
	}

	private void OnLoopbackCaptureStopped(Exception? exception)
	{
		if (exception != null)
		{
			_logger.LogError(exception, "Native loopback capture stopped unexpectedly");
		}
		_isNativeLoopbackActive = false;
	}

	private void OnLoopbackAudioDataAvailable(byte[] audioData, int byteCount, int sampleCount, int sampleRate)
	{
		if (!_isNativeLoopbackActive)
		{
			return;
		}
		try
		{
			_nativeToWebRtcBridge.SendNativeLoopbackAudioData(audioData, byteCount);
		}
		catch (ObjectDisposedException)
		{
			_isNativeLoopbackActive = false;
		}
	}

	public void SetScreenQualityMode(bool P_0)
	{
		ScreenQualityMode screenQualityMode = (P_0 ? ScreenQualityMode.GAMING : ScreenQualityMode.PRESENTATION);
		_nativeToWebRtcBridge.SetScreenQualityMode(screenQualityMode);
	}

	public void SetMute(bool P_0)
	{
		_nativeToWebRtcBridge.SetMute(P_0);
	}

	public void SetDeafen(bool P_0)
	{
		_nativeToWebRtcBridge.SetDeafen(P_0);
	}

	public void ScreenPickerDismissed()
	{
		_nativeToWebRtcBridge.ScreenPickerDismissed();
	}

	public void SetTileVolume(string P_0, string P_1, float P_2)
	{
		_nativeToWebRtcBridge.SetTileVolume(P_0, P_1, P_2);
	}

	public void SetInputVolume(float P_0)
	{
		_nativeToWebRtcBridge.SetInputVolume(P_0);
	}

	public void SetOutputVolume(float P_0)
	{
		_nativeToWebRtcBridge.SetOutputVolume(P_0);
	}

	public void SetDenoisePower(float P_0)
	{
		_nativeToWebRtcBridge.SetDenoisePower(P_0);
	}

	public void SetMediaConstraints(bool P_0, bool P_1)
	{
	}

	public void OnPushToTalkPress()
	{
		_pttCts?.Cancel();
		_pttCts = null;
		_nativeToWebRtcBridge.SetPushToTalk(true);
	}

	public void OnPushToTalkRelease()
	{
		_pttCts?.Cancel();
		_pttCts = new CancellationTokenSource();
		CancellationToken token = _pttCts.Token;
		Dispatcher.UIThread.InvokeAsync(async delegate
		{
			try
			{
				await Task.Delay((int)_pushToTalkService.PushToTalkDelay, token);
				if (!token.IsCancellationRequested)
				{
					_nativeToWebRtcBridge.SetPushToTalk(false);
				}
			}
			catch (TaskCanceledException)
			{
			}
		}).Forget();
	}

	private void onPushToTalkServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsPushToTalkEnabled")
		{
			_nativeToWebRtcBridge.SetPushToTalkMode(_pushToTalkService.IsPushToTalkEnabled);
		}
	}

	private void DeviceServiceSelectedDeviceChanged(MediaDeviceInfo mediaDeviceInfo)
	{
		switch (mediaDeviceInfo.Kind)
		{
		case "audioinput":
			_nativeToWebRtcBridge.UpdateAudioInputDeviceId("");
			break;
		case "audiooutput":
			_nativeToWebRtcBridge.UpdateAudioOutputDeviceId(mediaDeviceInfo.DeviceId);
			_window.Frame.ExecuteJavaScript<IJsPromise>("window.__setOutputDevice('" + (mediaDeviceInfo.DeviceId ?? "default") + "')").Forget();
			break;
		case "videoinput":
			_nativeToWebRtcBridge.UpdateVideoDeviceId("");
			break;
		}
	}

	private void DeviceServiceNoiseSuppressionStrengthChanged(float power)
	{
		SetDenoisePower(power);
	}

	private void onDeviceServiceMediaConstraintsChanged(bool echoCancellation, bool autoGainControl)
	{
		SetMediaConstraints(echoCancellation, autoGainControl);
	}

	private void onActualThemeVariantChanged(object? sender, EventArgs e)
	{
		_nativeToWebRtcBridge.SetTheme(ThemeMapper.ToWebRtcThemeString(Application.Current.ActualThemeVariant));
	}

	private Dictionary<string, bool>? buildExperimentalFlags()
	{
		IReadOnlyDictionary<string, bool> flagOverrides = _experimentalFeaturesService.FlagOverrides;
		return (flagOverrides.Count > 0) ? new Dictionary<string, bool>(flagOverrides) : null;
	}

	private void launchDevTools()
	{
		WebRtcBrowser.DotNetBrowser.DevTools.Show();
	}

	private void onOpenWebRtcDevLogsMessageReceived(object recipient, OpenWebRtcDevLogsMessage message)
	{
		launchDevTools();
	}

	public void ReleasePackets()
	{
		while (_packetQueue.Count > 0)
		{
			PacketContainer packetContainer = ToPacketContainer(_packetQueue.Dequeue());
			if (packetContainer != null)
			{
				_nativeToWebRtcBridge.ReceiveRawPacketContainer(packetContainer.ToByteArray());
			}
		}
		_shouldQueuePackets = false;
	}

	private void onMediaRoomWebRtcPacketReceived(IPacket packet)
	{
		if (packet.PacketType == PacketType.ChannelWebRtcUserDetach || packet.PacketType == PacketType.DirectMessageWebRtcUserDetach)
		{
			WebRtcUserDetachPacket webRtcUserDetachPacket = (WebRtcUserDetachPacket)packet;
			if (webRtcUserDetachPacket.DeviceId == (DeviceUuid)_rootSessionAccessor.Session.DeviceId)
			{
				WeakReferenceMessenger.Default.Send(new RemoveBrowserMessage(MediaRoom.MessageContainer.ContainerId));
			}
		}
		if (_shouldQueuePackets)
		{
			_packetQueue.Enqueue(packet);
			return;
		}
		PacketContainer packetContainer = ToPacketContainer(packet);
		_nativeToWebRtcBridge.ReceiveRawPacketContainer(packetContainer.ToByteArray());
	}

	private PacketContainer? ToPacketContainer(IPacket P_0)
	{
		PacketContainer packetContainer = new PacketContainer();
		switch (P_0.PacketType)
		{
		case PacketType.DirectMessageWebRtcUserAttach:
		case PacketType.ChannelWebRtcUserAttach:
			packetContainer.WebRtcUserDevice = (WebRtcUserDevicePacket)P_0;
			break;
		case PacketType.DirectMessageWebRtcUserDetach:
		case PacketType.ChannelWebRtcUserDetach:
			packetContainer.WebRtcUserDetach = (WebRtcUserDetachPacket)P_0;
			break;
		case PacketType.DirectMessageWebRtcUserDeviceSetStatus:
		case PacketType.ChannelWebRtcUserDeviceSetStatus:
			packetContainer.WebRtcUserDeviceSetStatus = (WebRtcUserDeviceSetStatusPacket)P_0;
			break;
		case PacketType.DirectMessageWebRtcUserDeviceSetTransport:
		case PacketType.ChannelWebRtcUserDeviceSetTransport:
			packetContainer.WebRtcUserDeviceSetTransport = (WebRtcUserDeviceSetTransportPacket)P_0;
			break;
		case PacketType.DirectMessageRing:
			packetContainer.DirectMessageRing = (DirectMessageRingPacket)P_0;
			break;
		case PacketType.DirectMessageRingDeclined:
			packetContainer.DirectMessageRingDeclined = (DirectMessageRingDeclinedPacket)P_0;
			break;
		case PacketType.DirectMessageWebRtcUserDeviceSetDataChannel:
		case PacketType.ChannelWebRtcUserDeviceSetDataChannel:
			packetContainer.WebRtcUserDeviceSetDataChannel = (WebRtcUserDeviceSetDataChannelPacket)P_0;
			break;
		default:
			return null;
		}
		return packetContainer;
	}

	public void Dispose()
	{
		MediaRoom.WebRtcPacketReceived -= onMediaRoomWebRtcPacketReceived;
		MediaRoom.PlaySoundEffectRequested -= onMediaRoomPlaySoundEffectRequested;
		DeviceService.SelectedDeviceChanged -= DeviceServiceSelectedDeviceChanged;
		DeviceService.NoiseSuppressionStrengthChanged -= DeviceServiceNoiseSuppressionStrengthChanged;
		DeviceService.MediaConstraintsChanged -= onDeviceServiceMediaConstraintsChanged;
		Application.Current.ActualThemeVariantChanged -= onActualThemeVariantChanged;
		_pushToTalkService.StopListening();
		_pushToTalkService.PropertyChanged -= onPushToTalkServicePropertyChanged;
		WeakReferenceMessenger.Default.UnregisterAll(this);
		MediaRoom.ForceDetachAsync().Forget();
		_userVolumeService.SetWebRtcService(null);
		StopNativeLoopbackCapture();
		if (!_window.IsDisposed)
		{
			disconnect();
		}
		_nativeToWebRtcBridge.MarkDisposed();
		_pttCts?.Cancel();
		_pttCts?.Dispose();
		_pttCts = null;
		DeviceService.Dispose();
	}
}
