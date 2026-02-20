// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.KeybindingItemViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Keybinds;
using RootApp.Client.Avalonia.Helpers.Keybindings;
using RootApp.Client.Avalonia.Helpers.LowLevelHook.KeyBinds;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class KeybindingItemViewModel : ViewModelBase<KeybindingItemViewModel>
{
	private readonly KeybindingDefinition _definition;

	private readonly KeybindingService _keybindingService;

	private readonly KeybindConflictConfirmationViewModelFactory _conflictConfirmationViewModelFactory;

	[CompilerGenerated]
	private string _003CCurrentGestureDisplay_003Ek__BackingField = string.Empty;

	[CompilerGenerated]
	private bool _003CIsModified_003Ek__BackingField;

	public string DisplayName => KeybindingDisplayHelper.GetActionDisplayName(_definition.Action);

	public string Description => _definition.Description;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CurrentGestureDisplay
	{
		get
		{
			return _003CCurrentGestureDisplay_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CCurrentGestureDisplay_003Ek__BackingField, text))
			{
				_003CCurrentGestureDisplay_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CurrentGestureDisplay);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsModified
	{
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsModified_003Ek__BackingField, flag))
			{
				_003CIsModified_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsModified);
			}
		}
	}

	public KeybindEditorViewModel KeybindViewModel { get; }

	public KeybindingItemViewModel(KeybindingDefinition P_0, KeybindingService P_1, KeybindEditorViewModelFactory P_2, KeybindConflictConfirmationViewModelFactory P_3)
		: base((IValidator<KeybindingItemViewModel>?)null)
	{
		_definition = P_0;
		_keybindingService = P_1;
		_conflictConfirmationViewModelFactory = P_3;
		CurrentGestureDisplay = P_0.CurrentGesture.ToString();
		IsModified = P_0.IsModified;
		KeybindViewModel = P_2.Create(P_0.CurrentGesture, P_0.IsGlobalHook);
		KeybindViewModel.KeybindChanged += OnKeybindChanged;
		_keybindingService.KeybindingChanged += OnExternalKeybindingChanged;
	}

	private void OnExternalKeybindingChanged(KeybindingAction action, KeyGesture gesture)
	{
		if (action == _definition.Action)
		{
			CurrentGestureDisplay = gesture.ToString();
			KeybindViewModel.SelectedGesture = CurrentGestureDisplay;
			IsModified = _definition.IsModified;
		}
	}

	private void OnKeybindChanged(KeyGesture newGesture)
	{
		if (newGesture.Keys == null || newGesture.Keys.Count == 0)
		{
			ApplyKeybind(newGesture);
			return;
		}
		KeybindingDefinition keybindingDefinition = _keybindingService.FindConflict(newGesture, _definition.Action);
		if (keybindingDefinition != null)
		{
			KeybindConflictConfirmationViewModel keybindConflictConfirmationViewModel = _conflictConfirmationViewModelFactory.Create(_definition.Action, newGesture, keybindingDefinition, delegate(bool confirmed)
			{
				if (confirmed)
				{
					CurrentGestureDisplay = newGesture.ToString();
					IsModified = _definition.IsModified;
				}
				else
				{
					KeybindViewModel.SelectedGesture = _definition.CurrentGesture.ToString();
				}
			});
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(keybindConflictConfirmationViewModel));
		}
		else
		{
			ApplyKeybind(newGesture);
		}
	}

	private void ApplyKeybind(KeyGesture P_0)
	{
		_keybindingService.SetKeybinding(_definition.Action, P_0);
		CurrentGestureDisplay = P_0.ToString();
		IsModified = _definition.IsModified;
	}

	public override void Dispose()
	{
		base.Dispose();
		KeybindViewModel.KeybindChanged -= OnKeybindChanged;
		_keybindingService.KeybindingChanged -= OnExternalKeybindingChanged;
	}
}

