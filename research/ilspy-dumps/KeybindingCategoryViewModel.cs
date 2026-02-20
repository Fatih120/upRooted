// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.KeybindingCategoryViewModel
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Keybindings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class KeybindingCategoryViewModel : ViewModelBase<KeybindingCategoryViewModel>
{
	public KeybindingCategory Category { get; }

	public string CategoryName => KeybindingDisplayHelper.GetCategoryDisplayName(Category);

	public ObservableCollection<KeybindingItemViewModel> Items { get; }

	public KeybindingCategoryViewModel(KeybindingCategory P_0, IEnumerable<KeybindingItemViewModel> P_1)
		: base((IValidator<KeybindingCategoryViewModel>?)null)
	{
		Category = P_0;
		Items = new ObservableCollection<KeybindingItemViewModel>(P_1);
	}

	public override void Dispose()
	{
		base.Dispose();
		foreach (KeybindingItemViewModel item in Items)
		{
			item.Dispose();
		}
		Items.Clear();
	}
}

