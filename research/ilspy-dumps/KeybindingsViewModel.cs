// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.KeybindingsViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Keybinds;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Keybindings;
using RootApp.Client.Avalonia.Helpers.LowLevelHook.KeyBinds;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class KeybindingsViewModel : ViewModelBase<KeybindingsViewModel>, IPage
{
	private readonly KeybindingService _keybindingService;

	private readonly KeybindEditorViewModelFactory _keybindEditorViewModelFactory;

	private readonly KeybindConflictConfirmationViewModelFactory _conflictConfirmationViewModelFactory;

	private readonly ResetAllKeybindingsConfirmationViewModelFactory _resetAllConfirmationViewModelFactory;

	[CompilerGenerated]
	private bool _003CAnyModified_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? resetAllToDefaultsCommand;

	public string PageTitle => Resources.Keybindings;

	public bool AreGlobalHooksAvailable => _keybindingService.AreGlobalHooksAvailable;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool AnyModified
	{
		get
		{
			return _003CAnyModified_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CAnyModified_003Ek__BackingField, flag))
			{
				_003CAnyModified_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AnyModified);
			}
		}
	}

	public ObservableCollection<KeybindingCategoryViewModel> Categories { get; } = new ObservableCollection<KeybindingCategoryViewModel>();

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ResetAllToDefaultsCommand => resetAllToDefaultsCommand ?? (resetAllToDefaultsCommand = new RelayCommand(ResetAllToDefaults));

	public KeybindingsViewModel(KeybindingService P_0, KeybindEditorViewModelFactory P_1, KeybindConflictConfirmationViewModelFactory P_2, ResetAllKeybindingsConfirmationViewModelFactory P_3)
		: base((IValidator<KeybindingsViewModel>?)null)
	{
		_keybindingService = P_0;
		_keybindEditorViewModelFactory = P_1;
		_conflictConfirmationViewModelFactory = P_2;
		_resetAllConfirmationViewModelFactory = P_3;
		_keybindingService.KeybindingChanged += OnKeybindingChanged;
		LoadCategories();
	}

	private void LoadCategories()
	{
		Categories.Clear();
		KeybindingCategory[] values = Enum.GetValues<KeybindingCategory>();
		foreach (KeybindingCategory keybindingCategory in values)
		{
			IReadOnlyCollection<KeybindingDefinition> definitionsByCategory = _keybindingService.GetDefinitionsByCategory(keybindingCategory);
			if (definitionsByCategory.Count == 0)
			{
				continue;
			}
			List<KeybindingDefinition> list = definitionsByCategory.Where((KeybindingDefinition d) => !d.IsGlobalHook || AreGlobalHooksAvailable).ToList();
			if (list.Count != 0)
			{
				KeybindingCategoryViewModel keybindingCategoryViewModel = new KeybindingCategoryViewModel(keybindingCategory, list.Select((KeybindingDefinition d) => new KeybindingItemViewModel(d, _keybindingService, _keybindEditorViewModelFactory, _conflictConfirmationViewModelFactory)).ToList());
				Categories.Add(keybindingCategoryViewModel);
			}
		}
		UpdateAnyModified();
	}

	private void OnKeybindingChanged(KeybindingAction action, KeyGesture gesture)
	{
		UpdateAnyModified();
	}

	private void UpdateAnyModified()
	{
		AnyModified = _keybindingService.AnyModified();
	}

	[RelayCommand]
	public void ResetAllToDefaults()
	{
		ResetAllKeybindingsConfirmationViewModel resetAllKeybindingsConfirmationViewModel = _resetAllConfirmationViewModelFactory.Create(delegate(bool confirmed)
		{
			if (confirmed)
			{
				_keybindingService.ResetAllToDefaults();
				LoadCategories();
			}
		});
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(resetAllKeybindingsConfirmationViewModel));
	}

	public override void Dispose()
	{
		base.Dispose();
		_keybindingService.KeybindingChanged -= OnKeybindingChanged;
		foreach (KeybindingCategoryViewModel category in Categories)
		{
			category.Dispose();
		}
		Categories.Clear();
	}
}

