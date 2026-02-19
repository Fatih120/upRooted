// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberGroupViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class MemberGroupViewModel : ViewModelBase<MemberGroupViewModel>
{
	[CompilerGenerated]
	private bool _003CShouldRender_003Ek__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShouldRender
	{
		get
		{
			return _003CShouldRender_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CShouldRender_003Ek__BackingField, flag))
			{
				_003CShouldRender_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShouldRender);
			}
		}
	}

	public MemberGroup MemberGroup { get; }

	public MemberGroupViewModel(MemberGroup P_0, bool P_1)
		: base((IValidator<MemberGroupViewModel>?)null)
	{
		MemberGroup = P_0;
		ShouldRender = P_1;
	}
}

