// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.StreamerModeBannerViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using RootApp.Client.Avalonia.Helpers.StreamerMode;

public class StreamerModeBannerViewModel : ObservableObject
{
	private readonly IStreamerModeService _streamerModeService;

	[CompilerGenerated]
	private bool _003CIsEnabled_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? disableCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsEnabled
	{
		get
		{
			return _003CIsEnabled_003Ek__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsEnabled_003Ek__BackingField, flag))
			{
				_003CIsEnabled_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsEnabled);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DisableCommand => disableCommand ?? (disableCommand = new RelayCommand(Disable));

	public StreamerModeBannerViewModel(IStreamerModeService P_0)
	{
		_streamerModeService = P_0;
		IsEnabled = _streamerModeService.IsEnabled;
		_streamerModeService.PropertyChanged += onStreamerModeServicePropertyChanged;
	}

	private void onStreamerModeServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			IsEnabled = _streamerModeService.IsEnabled;
		}
	}

	[RelayCommand]
	private void Disable()
	{
		_streamerModeService.Disable();
	}
}

