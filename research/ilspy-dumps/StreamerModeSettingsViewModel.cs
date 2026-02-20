// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.StreamerModeSettingsViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class StreamerModeSettingsViewModel : ViewModelBase<StreamerModeSettingsViewModel>, IPage
{
	private readonly IStreamerModeService _streamerModeService;

	[CompilerGenerated]
	private bool _003CIsEnabled_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CAutoDetect_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHidePersonalInfo_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHideInviteLinks_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CDisableNotifications_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CDisableSounds_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHideMessagePreviews_003Ek__BackingField;

	public string PageTitle => Resources.StreamerMode;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsEnabled
	{
		get
		{
			return _003CIsEnabled_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsEnabled_003Ek__BackingField, flag))
			{
				_003CIsEnabled_003Ek__BackingField = flag;
				OnIsEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool AutoDetect
	{
		get
		{
			return _003CAutoDetect_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CAutoDetect_003Ek__BackingField, flag))
			{
				_003CAutoDetect_003Ek__BackingField = flag;
				OnAutoDetectChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AutoDetect);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HidePersonalInfo
	{
		get
		{
			return _003CHidePersonalInfo_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHidePersonalInfo_003Ek__BackingField, flag))
			{
				_003CHidePersonalInfo_003Ek__BackingField = flag;
				OnHidePersonalInfoChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HidePersonalInfo);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HideInviteLinks
	{
		get
		{
			return _003CHideInviteLinks_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHideInviteLinks_003Ek__BackingField, flag))
			{
				_003CHideInviteLinks_003Ek__BackingField = flag;
				OnHideInviteLinksChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HideInviteLinks);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DisableNotifications
	{
		get
		{
			return _003CDisableNotifications_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CDisableNotifications_003Ek__BackingField, flag))
			{
				_003CDisableNotifications_003Ek__BackingField = flag;
				OnDisableNotificationsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DisableNotifications);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DisableSounds
	{
		get
		{
			return _003CDisableSounds_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CDisableSounds_003Ek__BackingField, flag))
			{
				_003CDisableSounds_003Ek__BackingField = flag;
				OnDisableSoundsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DisableSounds);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HideMessagePreviews
	{
		get
		{
			return _003CHideMessagePreviews_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHideMessagePreviews_003Ek__BackingField, flag))
			{
				_003CHideMessagePreviews_003Ek__BackingField = flag;
				OnHideMessagePreviewsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HideMessagePreviews);
			}
		}
	}

	public StreamerModeSettingsViewModel(IStreamerModeService P_0)
		: base((IValidator<StreamerModeSettingsViewModel>?)null)
	{
		_streamerModeService = P_0;
		IsEnabled = _streamerModeService.IsEnabled;
		AutoDetect = _streamerModeService.AutoDetect;
		HidePersonalInfo = _streamerModeService.HidePersonalInfo;
		HideInviteLinks = _streamerModeService.HideInviteLinks;
		DisableNotifications = _streamerModeService.DisableNotifications;
		DisableSounds = _streamerModeService.DisableSounds;
		HideMessagePreviews = _streamerModeService.HideMessagePreviews;
		_streamerModeService.PropertyChanged += onStreamerModeServicePropertyChanged;
	}

	private void onStreamerModeServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			IsEnabled = _streamerModeService.IsEnabled;
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		_streamerModeService.PropertyChanged -= onStreamerModeServicePropertyChanged;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsEnabledChanged(bool P_0)
	{
		if (P_0)
		{
			_streamerModeService.Enable();
		}
		else
		{
			_streamerModeService.Disable();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnAutoDetectChanged(bool P_0)
	{
		_streamerModeService.AutoDetect = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnHidePersonalInfoChanged(bool P_0)
	{
		_streamerModeService.HidePersonalInfo = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnHideInviteLinksChanged(bool P_0)
	{
		_streamerModeService.HideInviteLinks = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDisableNotificationsChanged(bool P_0)
	{
		_streamerModeService.DisableNotifications = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDisableSoundsChanged(bool P_0)
	{
		_streamerModeService.DisableSounds = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnHideMessagePreviewsChanged(bool P_0)
	{
		_streamerModeService.HideMessagePreviews = P_0;
	}
}

