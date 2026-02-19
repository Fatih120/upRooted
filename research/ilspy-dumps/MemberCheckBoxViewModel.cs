// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberCheckBoxViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

public class MemberCheckBoxViewModel : ViewModelBase<MemberCheckBoxViewModel>
{
	private readonly Action<RootGuid> _memberSelectedCallback;

	private readonly Action<RootGuid> _memberUnselectedCallback;

	private readonly BitmapCache _bitmapCache;

	[CompilerGenerated]
	private bool _003CIsChecked_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? setMemberCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsChecked
	{
		get
		{
			return _003CIsChecked_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsChecked_003Ek__BackingField, flag))
			{
				_003CIsChecked_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsChecked);
			}
		}
	}

	public Member Member { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Member.GlobalUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SetMemberCommand => setMemberCommand ?? (setMemberCommand = new RelayCommand(SetMember));

	public MemberCheckBoxViewModel(Member P_0, bool P_1, Action<RootGuid> P_2, Action<RootGuid> P_3, BitmapCache P_4)
		: base((IValidator<MemberCheckBoxViewModel>?)null)
	{
		Member = P_0;
		IsChecked = P_1;
		_memberSelectedCallback = P_2;
		_memberUnselectedCallback = P_3;
		_bitmapCache = P_4;
	}

	[RelayCommand]
	public void SetMember()
	{
		if (IsChecked)
		{
			_memberSelectedCallback?.Invoke(Member.GlobalUser.Id);
		}
		else
		{
			_memberUnselectedCallback?.Invoke(Member.GlobalUser.Id);
		}
	}
}

