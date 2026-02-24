using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.Client.CoreDomain.Models.User;

public class GlobalUser : ObservableObject, IDisposable
{
	[CompilerGenerated]
	private UserGuid <Id>k__BackingField;

	private bool _disposed;

	[CompilerGenerated]
	private string <ProfilePictureUri>k__BackingField;

	[CompilerGenerated]
	private string <UserName>k__BackingField;

	[CompilerGenerated]
	private UserOnlineStatus <OnlineStatus>k__BackingField;

	[CompilerGenerated]
	private bool <IsFriend>k__BackingField;

	[CompilerGenerated]
	private bool <IsBlocked>k__BackingField;

	public UserGuid Id
	{
		[CompilerGenerated]
		get
		{
			return <Id>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Id>k__BackingField = userGuid;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ProfilePictureUri
	{
		get
		{
			return <ProfilePictureUri>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<ProfilePictureUri>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ProfilePictureUri);
				<ProfilePictureUri>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ProfilePictureUri);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string UserName
	{
		get
		{
			return <UserName>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<UserName>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.UserName);
				<UserName>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.UserName);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public UserOnlineStatus OnlineStatus
	{
		get
		{
			return <OnlineStatus>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<UserOnlineStatus>.Default.Equals(<OnlineStatus>k__BackingField, userOnlineStatus))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.OnlineStatus);
				<OnlineStatus>k__BackingField = userOnlineStatus;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.OnlineStatus);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsFriend
	{
		get
		{
			return <IsFriend>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsFriend>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsFriend);
				<IsFriend>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsFriend);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsBlocked
	{
		get
		{
			return <IsBlocked>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsBlocked>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsBlocked);
				<IsBlocked>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsBlocked);
			}
		}
	}

	public ObservableCollection<UserBadge> Badges { get; } = new ObservableCollection<UserBadge>();

	public UserBadge? PrimaryBadge => Badges.FirstOrDefault();

	public GlobalUser(UserGuid P_0)
	{
		Id = P_0;
		ProfilePictureUri = string.Empty;
		UserName = string.Empty;
		Badges.CollectionChanged += onBadgesCollectionChanged;
	}

	public GlobalUser(UserResponse P_0)
	{
		Id = P_0.UserId;
		ProfilePictureUri = P_0.ProfilePictureAssetUri;
		UserName = P_0.Username;
		SetOnlineStatus(P_0.OnlineStatus);
		SetBadges(P_0.Badges);
		Badges.CollectionChanged += onBadgesCollectionChanged;
	}

	protected GlobalUser(UserSelfResponse P_0)
	{
		Id = P_0.UserId;
		ProfilePictureUri = P_0.ProfilePictureAssetUri;
		UserName = P_0.Username;
		SetBadges(P_0.UserBadges);
		Badges.CollectionChanged += onBadgesCollectionChanged;
	}

	public void UpdateFrom(GlobalUser P_0)
	{
		Id = P_0.Id;
		ProfilePictureUri = P_0.ProfilePictureUri;
		UserName = P_0.UserName;
		SetOnlineStatus(P_0.OnlineStatus);
		SetBadges(P_0.Badges);
	}

	public void SetOnlineStatus(UserOnlineStatus P_0)
	{
		OnlineStatus = P_0;
	}

	public void SetFriendStatus(bool P_0)
	{
		IsFriend = P_0;
	}

	public void SetUsername(string P_0)
	{
		UserName = P_0;
	}

	public void SetProfilePictureUrl(string P_0)
	{
		ProfilePictureUri = P_0;
	}

	public void SetBlockedStatus(bool P_0)
	{
		IsBlocked = P_0;
	}

	public void SetBadges(IEnumerable<UserBadge>? P_0)
	{
		Badges.Clear();
		if (P_0 == null)
		{
			return;
		}
		foreach (UserBadge item in P_0)
		{
			Badges.Add(item);
		}
		onBadgesCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
	}

	private void onBadgesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		OnPropertyChanged("Badges");
		OnPropertyChanged("PrimaryBadge");
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool P_0)
	{
		if (!_disposed)
		{
			if (P_0)
			{
				Badges.CollectionChanged -= onBadgesCollectionChanged;
			}
			_disposed = true;
		}
	}
}
