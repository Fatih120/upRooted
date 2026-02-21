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
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Media;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelMediaMemberViewModel : ViewModelBase<ChannelMediaMemberViewModel>
{
	private readonly Channel _channel;

	private readonly BitmapCache _bitmapCache;

	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly CallPopoutService _callPopoutService;

	[CompilerGenerated]
	private Member? <CommunityMember>k__BackingField;

	[CompilerGenerated]
	private MemberProfileViewModel? <MemberProfile>k__BackingField;

	[CompilerGenerated]
	private bool <IsPopupOpen>k__BackingField;

	[CompilerGenerated]
	private MediaMember <Member>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? profileOpeningCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? profileClosingCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? focusMediaCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberProfileViewModel? MemberProfile
	{
		get
		{
			return <MemberProfile>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<MemberProfileViewModel>.Default.Equals(<MemberProfile>k__BackingField, memberProfileViewModel))
			{
				<MemberProfile>k__BackingField = memberProfileViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MemberProfile);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsPopupOpen
	{
		get
		{
			return <IsPopupOpen>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsPopupOpen>k__BackingField, flag))
			{
				<IsPopupOpen>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsPopupOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MediaMember Member
	{
		get
		{
			return <Member>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<MediaMember>.Default.Equals(<Member>k__BackingField, mediaMember))
			{
				<Member>k__BackingField = mediaMember;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Member);
			}
		}
	}

	public Lazy<UserContextMenuViewModel> UserContextMenu { get; }

	public Member? CommunityMember
	{
		[CompilerGenerated]
		get
		{
			return <CommunityMember>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			<CommunityMember>k__BackingField = member;
		}
	}

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Member.GlobalUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ProfileOpeningCommand => profileOpeningCommand ?? (profileOpeningCommand = new RelayCommand(ProfileOpening));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ProfileClosingCommand => profileClosingCommand ?? (profileClosingCommand = new RelayCommand(ProfileClosing));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand FocusMediaCommand => focusMediaCommand ?? (focusMediaCommand = new RelayCommand(FocusMedia));

	public ChannelMediaMemberViewModel(MediaMember P_0, Channel P_1, BitmapCache P_2, UserContextMenuViewModelFactory P_3, MemberProfileViewModelFactory P_4, CallPopoutService P_5)
		: base((IValidator<ChannelMediaMemberViewModel>?)null)
	{
		ChannelMediaMemberViewModel channelMediaMemberViewModel = this;
		Member = P_0;
		_channel = P_1;
		_bitmapCache = P_2;
		_memberProfileViewModelFactory = P_4;
		_callPopoutService = P_5;
		Member.GlobalUser.PropertyChanged += onGlobalUserPropertyChanged;
		Member communityMember = _channel.Community.Members?.GetMemberFromCache(Member.GlobalUser.Id);
		if (communityMember != null)
		{
			UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_3.Create(communityMember));
			CommunityMember = communityMember;
		}
		else
		{
			UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_3.Create(channelMediaMemberViewModel.Member.GlobalUser));
		}
	}

	[RelayCommand]
	public void ProfileOpening()
	{
		MemberProfile?.Dispose();
		MemberProfile = _memberProfileViewModelFactory.Create(Member, delegate
		{
			IsPopupOpen = false;
		});
		IsPopupOpen = true;
	}

	[RelayCommand]
	public void ProfileClosing()
	{
		IsPopupOpen = false;
		MemberProfile?.Dispose();
		MemberProfile = null;
	}

	[RelayCommand]
	public void FocusMedia()
	{
		MediaRoom? mediaRoom = _channel.MediaRoom;
		if (mediaRoom != null && mediaRoom.HasMediaView() && _channel.MediaRoom?.SelfMediaMember != null)
		{
			MediaRoom? mediaRoom2 = _channel.MediaRoom;
			if (mediaRoom2 != null && !mediaRoom2.IsPoppedOut)
			{
				_channel.Community.SelectChannel(_channel);
			}
			else
			{
				_callPopoutService.FocusPopoutWindow();
			}
		}
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

	public override void Dispose()
	{
		Member.GlobalUser.PropertyChanged -= onGlobalUserPropertyChanged;
		MemberProfile?.Dispose();
		UserContextMenu.Value.Dispose();
		base.Dispose();
	}
}
