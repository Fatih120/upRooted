// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberPickerViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using DynamicData;
using FluentValidation;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

public class MemberPickerViewModel : ViewModelBase<MemberPickerViewModel>
{
	private readonly IDisposable _cacheCleanup;

	[CompilerGenerated]
	private string _003CFilterText_003Ek__BackingField = string.Empty;

	private readonly ReadOnlyObservableCollection<MemberCheckBoxViewModel> _members;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string FilterText
	{
		get
		{
			return _003CFilterText_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CFilterText_003Ek__BackingField, text))
			{
				_003CFilterText_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FilterText);
			}
		}
	}

	public ReadOnlyObservableCollection<MemberCheckBoxViewModel> Members => _members;

	public MemberPickerViewModel(Community P_0, Action<RootGuid> P_1, Action<RootGuid> P_2, IEnumerable<RootGuid> P_3, MemberCheckBoxViewModelFactory P_4)
		: base((IValidator<MemberPickerViewModel>?)null)
	{
		MemberPickerViewModel memberPickerViewModel = this;
		IObservable<Func<Member, bool>> predicate = (from _ in Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(delegate(PropertyChangedEventHandler h)
			{
				memberPickerViewModel.PropertyChanged += h;
			}, delegate(PropertyChangedEventHandler h)
			{
				memberPickerViewModel.PropertyChanged -= h;
			})
			where _.EventArgs.PropertyName == "FilterText"
			select memberPickerViewModel.FilterText).StartWith(FilterText).Select(buildFilterPredicate);
		_cacheCleanup = P_0.Members.ConnectMembers().Filter(predicate).Transform((Member member) => P_4.Create(member, P_3.Contains(member.GlobalUser.Id), P_1, P_2))
			.ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _members)
			.DisposeMany()
			.Subscribe();
	}

	private Func<Member, bool> buildFilterPredicate(string text)
	{
		return (Member member) => string.IsNullOrEmpty(text) || member.GlobalUser.UserName.Contains(text, StringComparison.OrdinalIgnoreCase);
	}

	public override void Dispose()
	{
		_cacheCleanup.Dispose();
		base.Dispose();
	}
}

