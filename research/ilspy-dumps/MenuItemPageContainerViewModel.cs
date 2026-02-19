// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.Settings.MenuItemPageContainerViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Navigation;

public class MenuItemPageContainerViewModel : ViewModelBase<MenuItemPageContainerViewModel>
{
	[CompilerGenerated]
	private Action<MenuItemPageContainerViewModel>? m_MenuItemSelected;

	[CompilerGenerated]
	private bool _003CIsHeaderItem_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CForceInitialLoad_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CShowUpdateIndicator_003Ek__BackingField;

	public Navigator Navigator { get; }

	public string MenuTitle { get; }

	public bool IsHeaderItem
	{
		[CompilerGenerated]
		get
		{
			return _003CIsHeaderItem_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsHeaderItem_003Ek__BackingField = flag;
		}
	}

	public bool ForceInitialLoad
	{
		[CompilerGenerated]
		get
		{
			return _003CForceInitialLoad_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CForceInitialLoad_003Ek__BackingField = flag;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowUpdateIndicator
	{
		get
		{
			return _003CShowUpdateIndicator_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CShowUpdateIndicator_003Ek__BackingField, flag))
			{
				_003CShowUpdateIndicator_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowUpdateIndicator);
			}
		}
	}

	public event Action<MenuItemPageContainerViewModel>? MenuItemSelected
	{
		[CompilerGenerated]
		add
		{
			Action<MenuItemPageContainerViewModel> action = this.m_MenuItemSelected;
			Action<MenuItemPageContainerViewModel> action2;
			do
			{
				action2 = action;
				Action<MenuItemPageContainerViewModel> action3 = (Action<MenuItemPageContainerViewModel>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_MenuItemSelected, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<MenuItemPageContainerViewModel> action = this.m_MenuItemSelected;
			Action<MenuItemPageContainerViewModel> action2;
			do
			{
				action2 = action;
				Action<MenuItemPageContainerViewModel> action3 = (Action<MenuItemPageContainerViewModel>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_MenuItemSelected, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public MenuItemPageContainerViewModel(string P_0, bool P_1 = false)
		: base((IValidator<MenuItemPageContainerViewModel>?)null)
	{
		MenuTitle = P_0;
		IsHeaderItem = P_1;
		Navigator = new Navigator();
	}

	public void SelectMenuItem()
	{
		this.MenuItemSelected?.Invoke(this);
	}

	public void SetForceInitialLoad()
	{
		ForceInitialLoad = true;
	}

	public override void Dispose()
	{
		Navigator.FirstViewModel?.Dispose();
		base.Dispose();
	}
}

