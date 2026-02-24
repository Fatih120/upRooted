using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Core;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.Client.CoreDomain.Models.User;

public class SessionUser : GlobalUser
{
	[CompilerGenerated]
	private List<UserGuid> <BlockedUsers>k__BackingField;

	[CompilerGenerated]
	private string <Email>k__BackingField;

	[CompilerGenerated]
	private bool <IsEmailVerified>k__BackingField;

	[CompilerGenerated]
	private UserDirectMessageInviteConnection <DirectMessageInviteConnection>k__BackingField;

	[CompilerGenerated]
	private bool <DirectMessageInviteRequiresVerifiedUser>k__BackingField;

	[CompilerGenerated]
	private UserFriendshipInviteConnection <FriendshipInviteConnection>k__BackingField;

	[CompilerGenerated]
	private bool <FriendshipInviteRequiresVerifiedUser>k__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Email
	{
		get
		{
			return <Email>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<Email>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Email);
				<Email>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Email);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsEmailVerified
	{
		get
		{
			return <IsEmailVerified>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsEmailVerified>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsEmailVerified);
				<IsEmailVerified>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsEmailVerified);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public UserDirectMessageInviteConnection DirectMessageInviteConnection
	{
		get
		{
			return <DirectMessageInviteConnection>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<UserDirectMessageInviteConnection>.Default.Equals(<DirectMessageInviteConnection>k__BackingField, userDirectMessageInviteConnection))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DirectMessageInviteConnection);
				<DirectMessageInviteConnection>k__BackingField = userDirectMessageInviteConnection;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DirectMessageInviteConnection);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DirectMessageInviteRequiresVerifiedUser
	{
		get
		{
			return <DirectMessageInviteRequiresVerifiedUser>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<DirectMessageInviteRequiresVerifiedUser>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DirectMessageInviteRequiresVerifiedUser);
				<DirectMessageInviteRequiresVerifiedUser>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DirectMessageInviteRequiresVerifiedUser);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public UserFriendshipInviteConnection FriendshipInviteConnection
	{
		get
		{
			return <FriendshipInviteConnection>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<UserFriendshipInviteConnection>.Default.Equals(<FriendshipInviteConnection>k__BackingField, userFriendshipInviteConnection))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FriendshipInviteConnection);
				<FriendshipInviteConnection>k__BackingField = userFriendshipInviteConnection;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FriendshipInviteConnection);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FriendshipInviteRequiresVerifiedUser
	{
		get
		{
			return <FriendshipInviteRequiresVerifiedUser>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<FriendshipInviteRequiresVerifiedUser>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FriendshipInviteRequiresVerifiedUser);
				<FriendshipInviteRequiresVerifiedUser>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FriendshipInviteRequiresVerifiedUser);
			}
		}
	}

	public List<UserGuid> BlockedUsers
	{
		[CompilerGenerated]
		get
		{
			return <BlockedUsers>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			<BlockedUsers>k__BackingField = list;
		}
	} = new List<UserGuid>();

	public SessionUser(UserSelfResponse selfResponse)
		: base(selfResponse)
	{
		Email = selfResponse.Email;
		IsEmailVerified = selfResponse.IsEmailVerified;
		BlockedUsers.AddRange(((IEnumerable<UserUuid>)selfResponse.BlockUserIds).Select((Func<UserUuid, UserGuid>)((UserUuid u) => u)));
		SetDirectMessageInviteRequirement(selfResponse.DirectMessageInviteRequirement.Connection, selfResponse.DirectMessageInviteRequirement.IsEmailVerified);
		SetFriendshipInviteRequirement(selfResponse.FriendshipInviteRequirement.Connection, selfResponse.FriendshipInviteRequirement.IsEmailVerified);
		SetOnlineStatus(selfResponse.MaxOnlineStatus);
	}

	public void SetIsEmailVerified(bool P_0)
	{
		IsEmailVerified = P_0;
	}

	public void SetDirectMessageInviteRequirement(UserDirectMessageInviteConnection P_0, bool P_1)
	{
		DirectMessageInviteConnection = P_0;
		DirectMessageInviteRequiresVerifiedUser = P_1;
	}

	public void SetFriendshipInviteRequirement(UserFriendshipInviteConnection P_0, bool P_1)
	{
		FriendshipInviteConnection = P_0;
		FriendshipInviteRequiresVerifiedUser = P_1;
	}
}
