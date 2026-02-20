// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.AdvancedSettingsViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Core.Identifiers;

public class AdvancedSettingsViewModel : ViewModelBase<AdvancedSettingsViewModel>, IPage
{
	private readonly IDeveloperModeService _developerModeService;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ClipboardService _clipboardService;

	[CompilerGenerated]
	private bool _003CDeveloperModeEnabled_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CUserIdCopied_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyUserIdCommand;

	public string PageTitle => Resources.Advanced;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DeveloperModeEnabled
	{
		get
		{
			return _003CDeveloperModeEnabled_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CDeveloperModeEnabled_003Ek__BackingField, flag))
			{
				_003CDeveloperModeEnabled_003Ek__BackingField = flag;
				OnDeveloperModeEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DeveloperModeEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool UserIdCopied
	{
		get
		{
			return _003CUserIdCopied_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CUserIdCopied_003Ek__BackingField, flag))
			{
				_003CUserIdCopied_003Ek__BackingField = flag;
				OnUserIdCopiedChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UserIdCopied);
			}
		}
	}

	public string CopyButtonText => UserIdCopied ? Resources.Copied : Resources.Copy;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyUserIdCommand => copyUserIdCommand ?? (copyUserIdCommand = new RelayCommand(CopyUserId));

	public AdvancedSettingsViewModel(IDeveloperModeService P_0, IRootSessionAccessor P_1, ClipboardService P_2)
		: base((IValidator<AdvancedSettingsViewModel>?)null)
	{
		_developerModeService = P_0;
		_rootSessionAccessor = P_1;
		_clipboardService = P_2;
		DeveloperModeEnabled = _developerModeService.IsEnabled;
		_developerModeService.PropertyChanged += onDeveloperModeServicePropertyChanged;
	}

	private void onDeveloperModeServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			DeveloperModeEnabled = _developerModeService.IsEnabled;
		}
	}

	[RelayCommand]
	public void CopyUserId()
	{
		UserGuid? userGuid = _rootSessionAccessor.Session?.UserInfoService.SessionUser.Id;
		if (userGuid != null)
		{
			_clipboardService.CopyTextToClipboard(userGuid.Value.ToString());
			UserIdCopied = true;
			DispatcherTimer.RunOnce(delegate
			{
				UserIdCopied = false;
			}, TimeSpan.FromSeconds(2L));
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		_developerModeService.PropertyChanged -= onDeveloperModeServicePropertyChanged;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDeveloperModeEnabledChanged(bool P_0)
	{
		_developerModeService.IsEnabled = P_0;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnUserIdCopiedChanged(bool P_0)
	{
		OnPropertyChanged("CopyButtonText");
	}
}

