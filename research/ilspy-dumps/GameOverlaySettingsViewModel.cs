// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.GameOverlaySettingsViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.Domain.Helpers.Store;

public class GameOverlaySettingsViewModel : ViewModelBase<GameOverlaySettingsViewModel>, IPage
{
	private readonly ILocalDataStore _localDataStore;

	private readonly OverlayService _overlayService;

	[CompilerGenerated]
	private int _003COverlayOpacity_003Ek__BackingField = 100;

	[CompilerGenerated]
	private int _003COverlayScale_003Ek__BackingField = 100;

	[CompilerGenerated]
	private bool _003CShowAvatars_003Ek__BackingField = true;

	[CompilerGenerated]
	private bool _003CShowNames_003Ek__BackingField = true;

	[CompilerGenerated]
	private bool _003COverlayMasterEnabled_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CVoiceOverlayEnabled_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CChatOverlayEnabled_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003COnlyShowSpeaking_003Ek__BackingField;

	public string PageTitle => Resources.GameOverlay;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool OverlayMasterEnabled
	{
		get
		{
			return _003COverlayMasterEnabled_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003COverlayMasterEnabled_003Ek__BackingField, flag))
			{
				_003COverlayMasterEnabled_003Ek__BackingField = flag;
				OnOverlayMasterEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OverlayMasterEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool VoiceOverlayEnabled
	{
		get
		{
			return _003CVoiceOverlayEnabled_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CVoiceOverlayEnabled_003Ek__BackingField, flag))
			{
				_003CVoiceOverlayEnabled_003Ek__BackingField = flag;
				OnVoiceOverlayEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.VoiceOverlayEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ChatOverlayEnabled
	{
		get
		{
			return _003CChatOverlayEnabled_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CChatOverlayEnabled_003Ek__BackingField, flag))
			{
				_003CChatOverlayEnabled_003Ek__BackingField = flag;
				OnChatOverlayEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChatOverlayEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int OverlayOpacity
	{
		get
		{
			return _003COverlayOpacity_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(_003COverlayOpacity_003Ek__BackingField, num))
			{
				_003COverlayOpacity_003Ek__BackingField = num;
				OnOverlayOpacityChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OverlayOpacity);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int OverlayScale
	{
		get
		{
			return _003COverlayScale_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(_003COverlayScale_003Ek__BackingField, num))
			{
				_003COverlayScale_003Ek__BackingField = num;
				OnOverlayScaleChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OverlayScale);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowAvatars
	{
		get
		{
			return _003CShowAvatars_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CShowAvatars_003Ek__BackingField, flag))
			{
				_003CShowAvatars_003Ek__BackingField = flag;
				OnShowAvatarsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowAvatars);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowNames
	{
		get
		{
			return _003CShowNames_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CShowNames_003Ek__BackingField, flag))
			{
				_003CShowNames_003Ek__BackingField = flag;
				OnShowNamesChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowNames);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool OnlyShowSpeaking
	{
		get
		{
			return _003COnlyShowSpeaking_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003COnlyShowSpeaking_003Ek__BackingField, flag))
			{
				_003COnlyShowSpeaking_003Ek__BackingField = flag;
				OnOnlyShowSpeakingChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OnlyShowSpeaking);
			}
		}
	}

	public GameOverlaySettingsViewModel(ILocalDataStore P_0, OverlayService P_1)
		: base((IValidator<GameOverlaySettingsViewModel>?)null)
	{
		_localDataStore = P_0;
		_overlayService = P_1;
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.OverlayMasterEnabled, out int value))
		{
			value = 1;
		}
		OverlayMasterEnabled = Convert.ToBoolean(value);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.GameOverlayEnabled, out int value2))
		{
			value2 = 1;
		}
		VoiceOverlayEnabled = Convert.ToBoolean(value2);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.ChatOverlayEnabled, out int value3))
		{
			value3 = 0;
		}
		ChatOverlayEnabled = Convert.ToBoolean(value3);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.GameOverlayOpacity, out int num))
		{
			num = 100;
		}
		OverlayOpacity = Math.Clamp(num, 0, 100);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.GameOverlayScale, out int num2))
		{
			num2 = 100;
		}
		OverlayScale = Math.Clamp(num2, 50, 150);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.GameOverlayShowAvatars, out int value4))
		{
			value4 = 1;
		}
		ShowAvatars = Convert.ToBoolean(value4);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.GameOverlayShowNames, out int value5))
		{
			value5 = 1;
		}
		ShowNames = Convert.ToBoolean(value5);
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.GameOverlayOnlyShowSpeaking, out int value6))
		{
			value6 = 0;
		}
		OnlyShowSpeaking = Convert.ToBoolean(value6);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOverlayMasterEnabledChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.OverlayMasterEnabled, Convert.ToInt32(P_0));
		_overlayService.IsMasterEnabled = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnVoiceOverlayEnabledChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.GameOverlayEnabled, Convert.ToInt32(P_0));
		_overlayService.IsVoiceOverlayEnabled = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnChatOverlayEnabledChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.ChatOverlayEnabled, Convert.ToInt32(P_0));
		_overlayService.IsChatOverlayEnabled = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOverlayOpacityChanged(int P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.GameOverlayOpacity, P_0);
		_overlayService.Opacity = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOverlayScaleChanged(int P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.GameOverlayScale, P_0);
		_overlayService.Scale = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnShowAvatarsChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.GameOverlayShowAvatars, Convert.ToInt32(P_0));
		_overlayService.ShowAvatars = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnShowNamesChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.GameOverlayShowNames, Convert.ToInt32(P_0));
		_overlayService.ShowNames = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOnlyShowSpeakingChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.GameOverlayOnlyShowSpeaking, Convert.ToInt32(P_0));
		_overlayService.OnlyShowSpeaking = P_0;
	}
}

