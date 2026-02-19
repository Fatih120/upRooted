// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class MemberViewModel : ViewModelBase<MemberViewModel>
{
	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly BitmapCache _bitmapCache;

	private bool _isMemberProfilePopupOpen;

	[CompilerGenerated]
	private readonly Community _003CCommunity_003Ek__BackingField;

	[CompilerGenerated]
	private MemberProfileViewModel? _003CMemberProfile_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsSelectedOrHighlighted_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CMenuIn_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? pointerEnteredCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? pointerExitedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? profileOpeningCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? profileClosingCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberProfileViewModel? MemberProfile
	{
		get
		{
			return _003CMemberProfile_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MemberProfileViewModel>.Default.Equals(_003CMemberProfile_003Ek__BackingField, memberProfileViewModel))
			{
				_003CMemberProfile_003Ek__BackingField = memberProfileViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MemberProfile);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsSelectedOrHighlighted
	{
		get
		{
			return _003CIsSelectedOrHighlighted_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsSelectedOrHighlighted_003Ek__BackingField, flag))
			{
				_003CIsSelectedOrHighlighted_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsSelectedOrHighlighted);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool MenuIn
	{
		get
		{
			return _003CMenuIn_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CMenuIn_003Ek__BackingField, flag))
			{
				_003CMenuIn_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MenuIn);
			}
		}
	}

	public Lazy<UserContextMenuViewModel> UserContextMenu { get; }

	public Member Member { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Member.GlobalUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PointerEnteredCommand => pointerEnteredCommand ?? (pointerEnteredCommand = new RelayCommand(PointerEntered));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PointerExitedCommand => pointerExitedCommand ?? (pointerExitedCommand = new RelayCommand(PointerExited));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ProfileOpeningCommand => profileOpeningCommand ?? (profileOpeningCommand = new RelayCommand(ProfileOpening));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ProfileClosingCommand => profileClosingCommand ?? (profileClosingCommand = new RelayCommand(ProfileClosing));

	public MemberViewModel(Member P_0, Community P_1, bool P_2, MemberProfileViewModelFactory P_3, UserContextMenuViewModelFactory P_4, BitmapCache P_5, ILoggerFactory P_6)
		: base((IValidator<MemberViewModel>?)null)
	{
		_memberProfileViewModelFactory = P_3;
		_isMemberProfilePopupOpen = false;
		_bitmapCache = P_5;
		Member = P_0;
		_003CCommunity_003Ek__BackingField = P_1;
		MenuIn = P_2;
		Member.GlobalUser.PropertyChanged += onGlobalUserPropertyChanged;
		UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_4.Create(P_0));
	}

	private void onGlobalUserPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ProfilePictureUri")
			{
				OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
			}
		});
	}

	[RelayCommand]
	public void PointerEntered()
	{
		if (!_isMemberProfilePopupOpen)
		{
			IsSelectedOrHighlighted = true;
		}
	}

	[RelayCommand]
	public void PointerExited()
	{
		if (!_isMemberProfilePopupOpen)
		{
			IsSelectedOrHighlighted = false;
		}
	}

	[RelayCommand]
	public void ProfileOpening()
	{
		MemberProfile?.Dispose();
		MemberProfile = _memberProfileViewModelFactory.Create(Member, ProfileClosing);
		_isMemberProfilePopupOpen = true;
		IsSelectedOrHighlighted = true;
	}

	[RelayCommand]
	public void ProfileClosing()
	{
		_isMemberProfilePopupOpen = false;
		IsSelectedOrHighlighted = false;
		MemberProfile?.Dispose();
		MemberProfile = null;
	}

	public override void Dispose()
	{
		base.Dispose();
		MemberProfile?.Dispose();
		Member.GlobalUser.PropertyChanged -= onGlobalUserPropertyChanged;
	}
}

