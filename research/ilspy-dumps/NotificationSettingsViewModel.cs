// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.NotificationSettingsViewModel
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
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.Domain.Helpers.Store;

public class NotificationSettingsViewModel : ViewModelBase<NotificationSettingsViewModel>, IPage
{
	private readonly ILocalDataStore _localDataStore;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private bool _003CTaskbarAttention_003Ek__BackingField = true;

	[CompilerGenerated]
	private bool _003CDesktopNotifications_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CNotificationSounds_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CUseNativeNotifications_003Ek__BackingField;

	public string PageTitle => Resources.Notifications;

	public bool ShouldShowTaskbarAttention => true;

	public bool ShouldShowUseNativeNotifications => 0 == 0;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DesktopNotifications
	{
		get
		{
			return _003CDesktopNotifications_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CDesktopNotifications_003Ek__BackingField, flag))
			{
				_003CDesktopNotifications_003Ek__BackingField = flag;
				OnDesktopNotificationsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DesktopNotifications);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool NotificationSounds
	{
		get
		{
			return _003CNotificationSounds_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CNotificationSounds_003Ek__BackingField, flag))
			{
				_003CNotificationSounds_003Ek__BackingField = flag;
				OnNotificationSoundsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.NotificationSounds);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool TaskbarAttention
	{
		get
		{
			return _003CTaskbarAttention_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CTaskbarAttention_003Ek__BackingField, flag))
			{
				_003CTaskbarAttention_003Ek__BackingField = flag;
				OnTaskbarAttentionChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.TaskbarAttention);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool UseNativeNotifications
	{
		get
		{
			return _003CUseNativeNotifications_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CUseNativeNotifications_003Ek__BackingField, flag))
			{
				_003CUseNativeNotifications_003Ek__BackingField = flag;
				OnUseNativeNotificationsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UseNativeNotifications);
			}
		}
	}

	public NotificationSettingsViewModel(ILocalDataStore P_0, IRootSessionAccessor P_1)
		: base((IValidator<NotificationSettingsViewModel>?)null)
	{
		_localDataStore = P_0;
		_rootSessionAccessor = P_1;
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "DesktopNotifications";
		if (!localDataStore.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray281, out int value))
		{
			value = 1;
		}
		DesktopNotifications = Convert.ToBoolean(value);
		ILocalDataStore localDataStore2 = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray282 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray282[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray282[1] = "NotificationSounds";
		if (!localDataStore2.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray282, out value))
		{
			value = 1;
		}
		NotificationSounds = Convert.ToBoolean(value);
		bool flag = true;
		ILocalDataStore localDataStore3 = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray283 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray283[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray283[1] = "TaskbarAttention";
		if (!localDataStore3.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray283, out value))
		{
			value = 1;
		}
		TaskbarAttention = Convert.ToBoolean(value);
		ILocalDataStore localDataStore4 = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray284 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray284[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray284[1] = "UseNativeNotifications";
		if (!localDataStore4.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray284, out value))
		{
			_ = 0;
			value = 0;
		}
		UseNativeNotifications = Convert.ToBoolean(value);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDesktopNotificationsChanged(bool P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "DesktopNotifications";
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(DesktopNotifications));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnNotificationSoundsChanged(bool P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "NotificationSounds";
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(NotificationSounds));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnTaskbarAttentionChanged(bool P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "TaskbarAttention";
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(TaskbarAttention));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnUseNativeNotificationsChanged(bool P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "UseNativeNotifications";
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(UseNativeNotifications));
	}
}

