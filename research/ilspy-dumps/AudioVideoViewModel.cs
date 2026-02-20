// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.AudioVideoViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using DotNetBrowser.Js;
using DynamicData;
using FluentValidation;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser;
using RootApp.Browser.WebRtc.Models;
using RootApp.Browser.WebRtc.Services;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Keybinds.Global;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.LowLevelHook.KeyBinds;
using RootApp.Client.Avalonia.Helpers.Volume;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using SharpHook.Native;

public class AudioVideoViewModel : ViewModelBase<AudioVideoViewModel>, IPage
{
	private readonly BrowserService _browserService;

	private readonly PushToTalkService _pushToTalkService;

	private readonly UserVolumeService _userVolumeService;

	private DeviceBrowser? _deviceBrowser;

	private DeviceService? _deviceService;

	[CompilerGenerated]
	private double _003CInputVolume_003Ek__BackingField = 100.0;

	[CompilerGenerated]
	private double _003COutputVolume_003Ek__BackingField = 100.0;

	[CompilerGenerated]
	private MediaDeviceInfo? _003CSelectedAudioInputDevice_003Ek__BackingField;

	[CompilerGenerated]
	private MediaDeviceInfo? _003CSelectedAudioOutputDevice_003Ek__BackingField;

	[CompilerGenerated]
	private MediaDeviceInfo? _003CSelectedVideoInputDevice_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsInPushToTalkMode_003Ek__BackingField;

	[CompilerGenerated]
	private KeyGesture _003CPushToTalkKeybind_003Ek__BackingField;

	[CompilerGenerated]
	private double _003CPushToTalkDelay_003Ek__BackingField;

	[CompilerGenerated]
	private double _003CNoiseSuppressionStrength_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CEchoCancellationEnabled_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CAutomaticGainControlEnabled_003Ek__BackingField;

	public bool ShouldShowPushToTalk => UioHook.IsAxApiEnabled(true);

	public string PageTitle => Resources.AudioAndVideo;

	public ObservableCollection<MediaDeviceInfo> AudioInputDevices { get; } = new ObservableCollection<MediaDeviceInfo>();

	public ObservableCollection<MediaDeviceInfo> AudioOutputDevices { get; } = new ObservableCollection<MediaDeviceInfo>();

	public ObservableCollection<MediaDeviceInfo> VideoInputDevices { get; } = new ObservableCollection<MediaDeviceInfo>();

	public GlobalHookKeybindViewModel PushToTalkGlobalHookKeybindViewModel { get; }

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MediaDeviceInfo? SelectedAudioInputDevice
	{
		get
		{
			return _003CSelectedAudioInputDevice_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MediaDeviceInfo>.Default.Equals(_003CSelectedAudioInputDevice_003Ek__BackingField, mediaDeviceInfo))
			{
				MediaDeviceInfo mediaDeviceInfo2 = _003CSelectedAudioInputDevice_003Ek__BackingField;
				_003CSelectedAudioInputDevice_003Ek__BackingField = mediaDeviceInfo;
				OnSelectedAudioInputDeviceChanged(mediaDeviceInfo2, mediaDeviceInfo);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedAudioInputDevice);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MediaDeviceInfo? SelectedAudioOutputDevice
	{
		get
		{
			return _003CSelectedAudioOutputDevice_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MediaDeviceInfo>.Default.Equals(_003CSelectedAudioOutputDevice_003Ek__BackingField, mediaDeviceInfo))
			{
				MediaDeviceInfo mediaDeviceInfo2 = _003CSelectedAudioOutputDevice_003Ek__BackingField;
				_003CSelectedAudioOutputDevice_003Ek__BackingField = mediaDeviceInfo;
				OnSelectedAudioOutputDeviceChanged(mediaDeviceInfo2, mediaDeviceInfo);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedAudioOutputDevice);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MediaDeviceInfo? SelectedVideoInputDevice
	{
		get
		{
			return _003CSelectedVideoInputDevice_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MediaDeviceInfo>.Default.Equals(_003CSelectedVideoInputDevice_003Ek__BackingField, mediaDeviceInfo))
			{
				MediaDeviceInfo mediaDeviceInfo2 = _003CSelectedVideoInputDevice_003Ek__BackingField;
				_003CSelectedVideoInputDevice_003Ek__BackingField = mediaDeviceInfo;
				OnSelectedVideoInputDeviceChanged(mediaDeviceInfo2, mediaDeviceInfo);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedVideoInputDevice);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsInPushToTalkMode
	{
		get
		{
			return _003CIsInPushToTalkMode_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsInPushToTalkMode_003Ek__BackingField, flag))
			{
				_003CIsInPushToTalkMode_003Ek__BackingField = flag;
				OnIsInPushToTalkModeChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsInPushToTalkMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public KeyGesture PushToTalkKeybind
	{
		get
		{
			return _003CPushToTalkKeybind_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<KeyGesture>.Default.Equals(_003CPushToTalkKeybind_003Ek__BackingField, keyGesture))
			{
				_003CPushToTalkKeybind_003Ek__BackingField = keyGesture;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PushToTalkKeybind);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PushToTalkDelay
	{
		get
		{
			return _003CPushToTalkDelay_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CPushToTalkDelay_003Ek__BackingField, num))
			{
				_003CPushToTalkDelay_003Ek__BackingField = num;
				OnPushToTalkDelayChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PushToTalkDelay);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double NoiseSuppressionStrength
	{
		get
		{
			return _003CNoiseSuppressionStrength_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CNoiseSuppressionStrength_003Ek__BackingField, num))
			{
				_003CNoiseSuppressionStrength_003Ek__BackingField = num;
				OnNoiseSuppressionStrengthChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.NoiseSuppressionStrength);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool EchoCancellationEnabled
	{
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CEchoCancellationEnabled_003Ek__BackingField, flag))
			{
				_003CEchoCancellationEnabled_003Ek__BackingField = flag;
				OnEchoCancellationEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.EchoCancellationEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool AutomaticGainControlEnabled
	{
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CAutomaticGainControlEnabled_003Ek__BackingField, flag))
			{
				_003CAutomaticGainControlEnabled_003Ek__BackingField = flag;
				OnAutomaticGainControlEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AutomaticGainControlEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double InputVolume
	{
		get
		{
			return _003CInputVolume_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CInputVolume_003Ek__BackingField, num))
			{
				_003CInputVolume_003Ek__BackingField = num;
				OnInputVolumeChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.InputVolume);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double OutputVolume
	{
		get
		{
			return _003COutputVolume_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003COutputVolume_003Ek__BackingField, num))
			{
				_003COutputVolume_003Ek__BackingField = num;
				OnOutputVolumeChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OutputVolume);
			}
		}
	}

	public double NoiseSuppressionStrengthUI
	{
		get
		{
			return NoiseSuppressionStrength * 100.0;
		}
		set
		{
			NoiseSuppressionStrength = num / 100.0;
		}
	}

	public AudioVideoViewModel(BrowserService P_0, GlobalHookKeybindViewModelFactory P_1, PushToTalkService P_2, UserVolumeService P_3)
		: base((IValidator<AudioVideoViewModel>?)null)
	{
		_browserService = P_0;
		_pushToTalkService = P_2;
		_userVolumeService = P_3;
		IsInPushToTalkMode = _pushToTalkService.IsPushToTalkEnabled;
		PushToTalkKeybind = _pushToTalkService.PushToTalkKeybind;
		PushToTalkDelay = _pushToTalkService.PushToTalkDelay;
		PushToTalkGlobalHookKeybindViewModel = P_1.Create(PushToTalkKeybind);
		InputVolume = _userVolumeService.GetGlobalInputVolume();
		OutputVolume = _userVolumeService.GetGlobalOutputVolume();
		Task.Run((Func<Task?>)initializeAsync).Forget();
		PushToTalkGlobalHookKeybindViewModel.KeybindChanged += onPushToTalkGlobalHookKeybindViewModelKeybindChanged;
	}

	private async Task initializeAsync()
	{
		try
		{
			WebRtcBrowser webRtcBrowser = _browserService.GetFirstWebRtcBrowser();
			if (webRtcBrowser != null)
			{
				_deviceService = webRtcBrowser.WebRtc.DeviceService;
			}
			else
			{
				_deviceBrowser = await _browserService.CreateDeviceBrowserAsync();
				if (_deviceBrowser != null)
				{
					_deviceService = _deviceBrowser.DeviceService;
				}
			}
			if (_deviceService != null)
			{
				AudioInputDevices.Add(_deviceService.GetAudioInputDevices());
				if (AudioInputDevices.Any())
				{
					SelectedAudioInputDevice = AudioInputDevices.First();
				}
				IReadOnlyList<IJsObject> rawAudioOutputDevices = await _deviceService.GetRawAudioOutputDevicesAsync();
				AudioOutputDevices.Add(_deviceService.GetAudioOutputDevices(rawAudioOutputDevices));
				if (AudioOutputDevices.Any())
				{
					SelectedAudioOutputDevice = AudioOutputDevices.First();
				}
				VideoInputDevices.Add(_deviceService.GetVideoInputDevices());
				if (VideoInputDevices.Any())
				{
					SelectedVideoInputDevice = VideoInputDevices.First();
				}
				NoiseSuppressionStrength = _deviceService.GetNoiseSuppressionStrength();
				EchoCancellationEnabled = _deviceService.GetEchoCancellationEnabled();
				AutomaticGainControlEnabled = _deviceService.GetAutomaticGainControlEnabled();
			}
		}
		catch
		{
		}
	}

	private void onPushToTalkGlobalHookKeybindViewModelKeybindChanged(KeyGesture keybind)
	{
		_pushToTalkService.SetPushToTalkKeybind(keybind);
	}

	public override void Dispose()
	{
		base.Dispose();
		if (_deviceBrowser != null)
		{
			_browserService.RemoveBrowser(_deviceBrowser.Id);
		}
		PushToTalkGlobalHookKeybindViewModel.KeybindChanged -= onPushToTalkGlobalHookKeybindViewModelKeybindChanged;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedAudioInputDeviceChanged(MediaDeviceInfo? P_0, MediaDeviceInfo? P_1)
	{
		if (P_0 != null && P_1 != null)
		{
			_deviceService?.SelectAudioInputDevice(P_1);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedAudioOutputDeviceChanged(MediaDeviceInfo? P_0, MediaDeviceInfo? P_1)
	{
		if (P_0 != null && P_1 != null)
		{
			_deviceService?.SelectAudioOutputDevice(P_1);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedVideoInputDeviceChanged(MediaDeviceInfo? P_0, MediaDeviceInfo? P_1)
	{
		if (P_0 != null && P_1 != null)
		{
			_deviceService?.SelectVideoInputDevice(P_1);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsInPushToTalkModeChanged(bool P_0)
	{
		_pushToTalkService.SetPushToTalkMode(IsInPushToTalkMode);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnPushToTalkDelayChanged(double P_0)
	{
		_pushToTalkService.SetPushToTalkDelay(PushToTalkDelay);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnNoiseSuppressionStrengthChanged(double P_0)
	{
		_deviceService?.SetNoiseSuppressionStrength(P_0);
		OnPropertyChanged("NoiseSuppressionStrengthUI");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnEchoCancellationEnabledChanged(bool P_0)
	{
		_deviceService?.SetEchoCancellationEnabled(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnAutomaticGainControlEnabledChanged(bool P_0)
	{
		_deviceService?.SetAutomaticGainControlEnabled(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnInputVolumeChanged(double P_0)
	{
		_userVolumeService.SetGlobalInputVolume(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOutputVolumeChanged(double P_0)
	{
		_userVolumeService.SetGlobalOutputVolume(P_0);
	}
}

