// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChangeThemeViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class ChangeThemeViewModel : ViewModelBase<ChangeThemeViewModel>, IPage
{
	private readonly ThemeService _themeService;

	[CompilerGenerated]
	private ThemeVariant _003CTheme_003Ek__BackingField;

	public string PageTitle => Resources.Theme;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ThemeVariant Theme
	{
		get
		{
			return _003CTheme_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<ThemeVariant>.Default.Equals(_003CTheme_003Ek__BackingField, themeVariant))
			{
				_003CTheme_003Ek__BackingField = themeVariant;
				OnThemeChanged(themeVariant);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Theme);
			}
		}
	}

	public ChangeThemeViewModel(ThemeService P_0)
		: base((IValidator<ChangeThemeViewModel>?)null)
	{
		_themeService = P_0;
		Theme = Application.Current.RequestedThemeVariant ?? ThemeVariant.Default;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnThemeChanged(ThemeVariant P_0)
	{
		if (!(Application.Current.RequestedThemeVariant == Theme))
		{
			_themeService.SetTheme(Theme, true);
		}
	}
}
