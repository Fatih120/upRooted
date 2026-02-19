// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.SelectableViewModelBase
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;

public abstract class SelectableViewModelBase : ViewModelBase<SelectableViewModelBase>
{
	[CompilerGenerated]
	private bool _003CIsSelected_003Ek__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsSelected
	{
		get
		{
			return _003CIsSelected_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsSelected_003Ek__BackingField, flag))
			{
				_003CIsSelected_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsSelected);
			}
		}
	}

	public abstract string? SelectedText { get; }

	protected SelectableViewModelBase()
		: base((IValidator<SelectableViewModelBase>?)null)
	{
	}
}

