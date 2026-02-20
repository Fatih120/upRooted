// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.WindowsSettingsViewModel
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
using RootApp.Client.Domain.Helpers.Store;

public class WindowsSettingsViewModel : ViewModelBase<WindowsSettingsViewModel>, IPage
{
	private readonly ILocalDataStore _localDataStore;

	[CompilerGenerated]
	private bool _003CCloseToTray_003Ek__BackingField;

	public string PageTitle => Resources.Windows;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool CloseToTray
	{
		get
		{
			return _003CCloseToTray_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CCloseToTray_003Ek__BackingField, flag))
			{
				_003CCloseToTray_003Ek__BackingField = flag;
				OnCloseToTrayChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CloseToTray);
			}
		}
	}

	public WindowsSettingsViewModel(ILocalDataStore P_0)
		: base((IValidator<WindowsSettingsViewModel>?)null)
	{
		_localDataStore = P_0;
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.CloseToTray, out int value))
		{
			value = 1;
		}
		CloseToTray = Convert.ToBoolean(value);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnCloseToTrayChanged(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.CloseToTray, Convert.ToInt32(CloseToTray));
	}
}

