using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using DotNetBrowser.Capture;
using DotNetBrowser.Capture.Handlers;
using DotNetBrowser.Handlers;
using FluentValidation;
using RootApp.Browser;
using RootApp.Browser.WebRtc.Models;
using RootApp.Client.Avalonia.Helpers.Audio;
using RootApp.Client.Domain.Helpers.Store;

namespace RootApp.Client.Avalonia.UI.Home.VoiceBar;

public class ScreensharePickerViewModel : ViewModelBase<ScreensharePickerViewModel>
{
	private readonly WebRtcBrowser _webRtcBrowser;

	private readonly ScreenshareViewModelFactory _screenshareViewModelFactory;

	private readonly Action<bool> _screenSelectedCallback;

	private readonly ILocalDataStore _localDataStore;

	private TaskCompletionSource<StartSessionResponse> _screenSelectionTcs;

	private bool _screenSelectedWithAudio;

	[CompilerGenerated]
	private bool <ScreenshareAudio>k__BackingField = false;

	[CompilerGenerated]
	private bool <GameMode>k__BackingField;

	[CompilerGenerated]
	private bool <ScreenshareMode>k__BackingField;

	public ObservableCollection<ScreenshareViewModel> Screens { get; } = new ObservableCollection<ScreenshareViewModel>();

	public bool CanShareAudio => LoopbackCaptureService.IsSupported;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool GameMode
	{
		get
		{
			return <GameMode>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<GameMode>k__BackingField, flag))
			{
				<GameMode>k__BackingField = flag;
				OnGameModeChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.GameMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ScreenshareMode
	{
		get
		{
			return <ScreenshareMode>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ScreenshareMode>k__BackingField, flag))
			{
				<ScreenshareMode>k__BackingField = flag;
				OnScreenshareModeChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ScreenshareMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ScreenshareAudio
	{
		get
		{
			return <ScreenshareAudio>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ScreenshareAudio>k__BackingField, flag))
			{
				<ScreenshareAudio>k__BackingField = flag;
				OnScreenshareAudioChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ScreenshareAudio);
			}
		}
	}

	public ScreensharePickerViewModel(WebRtcBrowser P_0, ScreenshareViewModelFactory P_1, Action<bool> P_2, ILocalDataStore P_3)
		: base((IValidator<ScreensharePickerViewModel>?)null)
	{
		_webRtcBrowser = P_0;
		_screenshareViewModelFactory = P_1;
		_screenSelectedCallback = P_2;
		_localDataStore = P_3;
		_screenSelectionTcs = new TaskCompletionSource<StartSessionResponse>();
		_webRtcBrowser.DotNetBrowser.Capture.StartSessionHandler = new AsyncHandler<StartSessionParameters, StartSessionResponse>(async delegate(StartSessionParameters parameters)
		{
			try
			{
				await _webRtcBrowser.WebRtc.StartScreenShareAudioCaptureAsync();
			}
			catch (Exception)
			{
			}
			foreach (Source source in parameters.Sources.Screens)
			{
				ScreenshareViewModel screenshareViewModel = _screenshareViewModelFactory.Create(source, false);
				screenshareViewModel.ScreenSelected += onScreenshareViewModelScreenSelected;
				Screens.Add(screenshareViewModel);
			}
			foreach (Source source2 in parameters.Sources.ApplicationWindows)
			{
				ScreenshareViewModel screenshareViewModel2 = _screenshareViewModelFactory.Create(source2, true);
				screenshareViewModel2.ScreenSelected += onScreenshareViewModelScreenSelected;
				Screens.Add(screenshareViewModel2);
			}
			return await _screenSelectionTcs.Task;
		});
		if (_localDataStore.TryGetGlobal(DataStoreKeys.ScreenshareAudio, out int value))
		{
			ScreenshareAudio = Convert.ToBoolean(value);
		}
		ScreenQualityMode screenQualityMode = ScreenQualityMode.GAMING;
		if (_localDataStore.TryGetGlobal(DataStoreKeys.ScreenshareQualityMode, out int num))
		{
			screenQualityMode = (ScreenQualityMode)num;
		}
		if (screenQualityMode == ScreenQualityMode.GAMING)
		{
			GameMode = true;
			ScreenshareMode = false;
		}
		else
		{
			GameMode = false;
			ScreenshareMode = true;
		}
		_webRtcBrowser.WebRtc.SetScreenShare(true, true);
	}

	private void onScreenshareViewModelScreenSelected(ScreenSelectedEventArgs args)
	{
		if (!ScreenshareAudio || !CanShareAudio)
		{
			_webRtcBrowser.WebRtc.StopScreenShareAudioCapture();
		}
		else
		{
			_screenSelectedWithAudio = true;
			if (args.IsWindow && args.WindowTitle != null)
			{
				_webRtcBrowser.WebRtc.RestartScreenShareAudioCaptureForWindowAsync(args.WindowTitle);
			}
		}
		_screenSelectionTcs.TrySetResult(StartSessionResponse.SelectSource(args.Source, AudioMode.Ignore, NotificationVisibility.Hide));
		_screenSelectedCallback(obj: true);
	}

	public override void Dispose()
	{
		base.Dispose();
		foreach (ScreenshareViewModel screen in Screens)
		{
			screen.ScreenSelected -= onScreenshareViewModelScreenSelected;
			screen.Dispose();
		}
		if (!_screenSelectedWithAudio)
		{
			_webRtcBrowser.WebRtc.StopScreenShareAudioCapture();
		}
		if (_screenSelectionTcs.TrySetResult(StartSessionResponse.Cancel()))
		{
			_webRtcBrowser.WebRtc.SetScreenShare(false, false);
		}
		_webRtcBrowser.DotNetBrowser.Capture.StartSessionHandler = null;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnGameModeChanged(bool P_0)
	{
		if (GameMode)
		{
			_webRtcBrowser.WebRtc.SetScreenQualityMode(true);
			_localDataStore.SetGlobal(DataStoreKeys.ScreenshareQualityMode, 1);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnScreenshareModeChanged(bool P_0)
	{
		if (ScreenshareMode)
		{
			_webRtcBrowser.WebRtc.SetScreenQualityMode(false);
			_localDataStore.SetGlobal(DataStoreKeys.ScreenshareQualityMode, 2);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnScreenshareAudioChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.ScreenshareAudio, Convert.ToInt32(ScreenshareAudio));
	}
}
