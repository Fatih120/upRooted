using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.Notifications;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Packets;
using RootApp.WebApi.Shared.Permissions;

namespace RootApp.Client.CoreDomain.Models.Community;

public class Channel : ObservableObject, IMessageContainer, IChannelListItem, INotifyPropertyChanged, IDisposable
{
	private bool _disposed = false;

	[CompilerGenerated]
	private bool <PermissionsSyncedToChannelGroup>k__BackingField;

	[CompilerGenerated]
	private ChannelGroup <ChannelGroup>k__BackingField;

	[CompilerGenerated]
	private bool <IsLocked>k__BackingField;

	[CompilerGenerated]
	private string <Name>k__BackingField;

	[CompilerGenerated]
	private string <Description>k__BackingField;

	[CompilerGenerated]
	private string <IconAssetUri>k__BackingField;

	[CompilerGenerated]
	private int <Position>k__BackingField;

	[CompilerGenerated]
	private CommunityAppGuid? <CommunityAppId>k__BackingField;

	[CompilerGenerated]
	private DateTime? <LastActivityAt>k__BackingField;

	[CompilerGenerated]
	private DateTime? <UserLastViewedAt>k__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Name
	{
		get
		{
			return <Name>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<Name>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Name);
				<Name>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Name);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Description
	{
		get
		{
			return <Description>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<Description>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Description);
				<Description>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Description);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string IconAssetUri
	{
		get
		{
			return <IconAssetUri>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<IconAssetUri>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IconAssetUri);
				<IconAssetUri>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IconAssetUri);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int Position
	{
		get
		{
			return <Position>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(<Position>k__BackingField, num))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Position);
				<Position>k__BackingField = num;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Position);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public CommunityAppGuid? CommunityAppId
	{
		get
		{
			return <CommunityAppId>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<CommunityAppGuid?>.Default.Equals(<CommunityAppId>k__BackingField, communityAppGuid))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CommunityAppId);
				<CommunityAppId>k__BackingField = communityAppGuid;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CommunityAppId);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("HasActivity")]
	[NotifyPropertyChangedFor("IsCollapsed")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DateTime? LastActivityAt
	{
		get
		{
			return <LastActivityAt>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<DateTime?>.Default.Equals(<LastActivityAt>k__BackingField, dateTime))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.LastActivityAt);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.HasActivity);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsCollapsed);
				<LastActivityAt>k__BackingField = dateTime;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.LastActivityAt);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.HasActivity);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsCollapsed);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("HasActivity")]
	[NotifyPropertyChangedFor("IsCollapsed")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DateTime? UserLastViewedAt
	{
		get
		{
			return <UserLastViewedAt>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<DateTime?>.Default.Equals(<UserLastViewedAt>k__BackingField, dateTime))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.UserLastViewedAt);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.HasActivity);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsCollapsed);
				<UserLastViewedAt>k__BackingField = dateTime;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.UserLastViewedAt);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.HasActivity);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsCollapsed);
			}
		}
	}

	public string? LastViewedAtString => UserLastViewedAt?.ToLocalTime().ToLongDateString();

	public bool HasActivity
	{
		get
		{
			DateTime? lastActivityAt = LastActivityAt;
			DateTime? userLastViewedAt = UserLastViewedAt;
			return (lastActivityAt.HasValue & userLastViewedAt.HasValue) && lastActivityAt.GetValueOrDefault() > userLastViewedAt.GetValueOrDefault() && LastActivityAt.HasValue && UserLastViewedAt.HasValue && Type != ChannelType.Voice && Type != ChannelType.App;
		}
	}

	public bool IsCollapsed => (!HasActivity && Community.SelectedChannel != this && ((Type == ChannelType.Voice && !MediaRoom.HasActiveCall) || Type != ChannelType.Voice) && ChannelGroup.IsCollapsed) || ChannelGroup.IsForcedCollapsed;

	public ChannelGuid Id { get; }

	public RootGuid ChannelListItemId { get; }

	public ChannelType Type { get; }

	public bool PermissionsSyncedToChannelGroup
	{
		[CompilerGenerated]
		get
		{
			return <PermissionsSyncedToChannelGroup>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<PermissionsSyncedToChannelGroup>k__BackingField = flag;
		}
	}

	public Community Community { get; }

	public MediaRoom? MediaRoom { get; }

	public IMessageService Messages { get; } = null;

	public IDirectoryService? Directories { get; }

	public LocalChannelPermission LocalChannelPermission { get; }

	public NotificationContainer Notifications { get; }

	public CommunityGuid? CommunityId => Community.Id;

	public MessageContainerGuid ContainerId => Id;

	public ChannelGroup ChannelGroup
	{
		[CompilerGenerated]
		get
		{
			return <ChannelGroup>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<ChannelGroup>k__BackingField = channelGroup;
		}
	}

	public bool IsLocked
	{
		[CompilerGenerated]
		get
		{
			return <IsLocked>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<IsLocked>k__BackingField = flag;
		}
	}

	public Channel(ChannelResponse P_0, ChannelGroup P_1, Community P_2, int P_3, MediaRoomFactory P_4, MessageServiceFactory P_5, DirectoryServiceFactory P_6, IRootSessionAccessor P_7)
		: this(P_0.Id, P_0.Name, P_0.Description, P_0.IconAssetUri, (ChannelType)P_0.ChannelType, P_0.UseChannelGroupPermission, P_0.LastActivityAt?.ToDateTime(), P_0.UserLastViewedAt?.ToDateTime(), P_0.ChannelPermission, P_0.WebRtcMembers, P_1, P_2, P_3, P_4, P_5, P_6, P_7)
	{
	}

	public Channel(ChannelCreateResponse P_0, ChannelGroup P_1, Community P_2, int P_3, MediaRoomFactory P_4, MessageServiceFactory P_5, DirectoryServiceFactory P_6, IRootSessionAccessor P_7)
		: this(P_0.Id, P_0.Name, P_0.Description, P_0.IconAssetUri, (ChannelType)P_0.ChannelType, P_0.UseChannelGroupPermission, DateTime.UtcNow, DateTime.UtcNow, P_0.ChannelPermission, Array.Empty<WebRtcUserDevicePacket>(), P_1, P_2, P_3, P_4, P_5, P_6, P_7)
	{
		Community.PropertyChanged += onCommunityPropertyChanged;
		Notifications = P_7.Session.NotificationService.GetNotificationContainer(Id);
		SetLocalChannelPermissions(P_0.ChannelPermission);
		if (Type == ChannelType.Voice)
		{
			MediaRoom = P_4.Create(this, Array.Empty<WebRtcUserDevicePacket>());
			MediaRoom.PropertyChanged += onMediaRoomPropertyChanged;
		}
		else if (Type == ChannelType.Text)
		{
			Messages = P_5.Create(this);
			Directories = P_6.Create(this);
		}
	}

	public Channel(ChannelCreatedPacket P_0, ChannelGroup P_1, Community P_2, int P_3, MediaRoomFactory P_4, MessageServiceFactory P_5, DirectoryServiceFactory P_6, IRootSessionAccessor P_7)
		: this(P_0.Id, P_0.Name, P_0.Description, P_0.IconAssetUri, (ChannelType)P_0.ChannelType, P_0.UseChannelGroupPermission, DateTime.UtcNow, DateTime.UtcNow, P_0.ChannelPermission, P_0.WebRtcMembers, P_1, P_2, P_3, P_4, P_5, P_6, P_7)
	{
	}

	private Channel(ChannelGuid P_0, string P_1, string P_2, string P_3, ChannelType P_4, bool P_5, DateTime? P_6, DateTime? P_7, IChannelPermissions P_8, IEnumerable<WebRtcUserDevicePacket> P_9, ChannelGroup P_10, Community P_11, int P_12, MediaRoomFactory P_13, MessageServiceFactory P_14, DirectoryServiceFactory P_15, IRootSessionAccessor P_16)
	{
		LocalChannelPermission = new LocalChannelPermission();
		Id = P_0;
		ChannelListItemId = Id;
		Name = P_1;
		Description = P_2;
		IconAssetUri = P_3;
		Type = P_4;
		PermissionsSyncedToChannelGroup = P_5;
		LastActivityAt = P_6;
		UserLastViewedAt = P_7;
		Community = P_11;
		Position = P_12;
		ChannelGroup = P_10;
		listenToChannelGroupChanges();
		Community.PropertyChanged += onCommunityPropertyChanged;
		Notifications = P_16.Session.NotificationService.GetNotificationContainer(Id);
		SetLocalChannelPermissions(P_8);
		if (Type == ChannelType.Voice)
		{
			MediaRoom = P_13.Create(this, P_9);
			MediaRoom.PropertyChanged += onMediaRoomPropertyChanged;
		}
		else if (Type == ChannelType.Text)
		{
			Messages = P_14.Create(this);
			Directories = P_15.Create(this);
		}
	}

	public void Update(ChannelResponse P_0, ChannelGroup P_1, int P_2)
	{
		Name = P_0.Name;
		Description = P_0.Description;
		IconAssetUri = P_0.IconAssetUri;
		PermissionsSyncedToChannelGroup = P_0.UseChannelGroupPermission;
		LastActivityAt = P_0.LastActivityAt?.ToDateTime();
		UserLastViewedAt = P_0.UserLastViewedAt?.ToDateTime();
		Position = P_2;
		UpdateChannelGroup(P_1);
		SetLocalChannelPermissions(P_0.ChannelPermission);
		if (Type == ChannelType.Voice)
		{
			MediaRoom?.Initialize(P_0.WebRtcMembers);
		}
		else if (Type == ChannelType.Text)
		{
			Messages.ReinitializeAsync().Forget();
			Directories?.Reinitialize();
		}
	}

	public void UpdateChannelGroup(ChannelGroup P_0)
	{
		if (ChannelGroup != P_0)
		{
			stopListeningToChannelGroupChanges();
			ChannelGroup = P_0;
			listenToChannelGroupChanges();
		}
	}

	public void SetCommunityAppId(CommunityAppGuid P_0)
	{
		CommunityAppId = P_0;
	}

	private void onCommunityPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "SelectedChannel")
		{
			OnPropertyChanged("IsCollapsed");
		}
	}

	private void stopListeningToChannelGroupChanges()
	{
		ChannelGroup.PropertyChanged -= onChannelGroupPropertyChanged;
	}

	private void listenToChannelGroupChanges()
	{
		ChannelGroup.PropertyChanged += onChannelGroupPropertyChanged;
		OnPropertyChanged("IsCollapsed");
	}

	private void onChannelGroupPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		if ((propertyName == "IsCollapsed" || propertyName == "IsForcedCollapsed") ? true : false)
		{
			if (ChannelGroup.IsForcedCollapsed)
			{
				SetLock(true);
			}
			OnPropertyChanged("IsCollapsed");
		}
	}

	public void SetLocalChannelPermissions(IChannelPermissions? P_0)
	{
		P_0?.Copy(LocalChannelPermission);
	}

	public void SetViewTime(DateTime P_0)
	{
		UserLastViewedAt = P_0;
	}

	public void SetLastSentMessage(Message P_0)
	{
	}

	public async Task<IMessageContainerMember?> GetMemberAsync(UserGuid P_0)
	{
		return await Community.Members.GetMemberAsync(P_0);
	}

	public IEnumerable<IMessageContainerMember> FindMembers(string P_0)
	{
		return Community.Members.FindMembers(P_0);
	}

	public IEnumerable<Role> FindRoles(string P_0)
	{
		return Community.Roles.FindRoles(P_0);
	}

	public IEnumerable<Channel> FindChannels(string P_0)
	{
		return Community.Channels.FindChannels(P_0);
	}

	public Channel? GetChannel(RootGuid P_0)
	{
		return Community.Channels.GetChannel(P_0);
	}

	public Role? GetRole(CommunityRoleGuid P_0)
	{
		return Community.Roles.GetRole(P_0);
	}

	public bool IsInVoiceCall(UserGuid P_0)
	{
		if (MediaRoom == null)
		{
			return false;
		}
		if (MediaRoom.HasActiveCall)
		{
			return MediaRoom.GetMemberByUserId(P_0) != null;
		}
		return false;
	}

	public void SetLock(bool P_0)
	{
		IsLocked = P_0;
	}

	public void OnMessageCreatedPacketReceived(MessagePacket P_0)
	{
		if (P_0.MessageType != MessageType.System)
		{
			LastActivityAt = ((MessageGuid)P_0.Id).ToDateTime();
		}
		if (Type == ChannelType.Text)
		{
			Messages.OnMessageCreatedPacketReceived(P_0);
		}
	}

	public void OnMessageDeletedPacketReceived(MessageDeletedPacket P_0)
	{
		if (Type == ChannelType.Text)
		{
			Messages.OnMessageDeletedPacketReceived(P_0);
		}
	}

	public void OnMessageSetViewTimePacketPacketReceived(MessageSetViewTimePacket P_0)
	{
		UserLastViewedAt = P_0.UserLastViewedAt.ToDateTime();
	}

	public void OnChannelEditedPacketReceived(ChannelEditedPacket P_0)
	{
		Name = P_0.Name;
		Description = P_0.Description;
		IconAssetUri = P_0.IconAssetUri;
		PermissionsSyncedToChannelGroup = P_0.UseChannelGroupPermission;
	}

	private void onMediaRoomPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (MediaRoom == null)
		{
			return;
		}
		if (e.PropertyName == "SelfMediaMember" && MediaRoom.SelfMediaMember == null && Community.SelectedChannel == this)
		{
			Community.SelectFirstTextChannel();
		}
		if (e.PropertyName == "IsPoppedOut")
		{
			if (MediaRoom.IsPoppedOut)
			{
				if (Community.SelectedChannel == this)
				{
					Community.SelectFirstTextChannel();
				}
				return;
			}
			Channel channel = MediaRoom.MessageContainer.GetChannel(MediaRoom.MessageContainer.ContainerId);
			if (channel != null && MediaRoom.SelfMediaMember != null)
			{
				Community.SelectChannel(channel);
			}
		}
		else if (e.PropertyName == "HasActiveCall")
		{
			OnPropertyChanged("IsCollapsed");
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool P_0)
	{
		if (_disposed)
		{
			return;
		}
		if (P_0)
		{
			Messages?.Dispose();
			Directories?.Dispose();
			stopListeningToChannelGroupChanges();
			Community.PropertyChanged -= onCommunityPropertyChanged;
			if (MediaRoom != null)
			{
				MediaRoom.PropertyChanged -= onMediaRoomPropertyChanged;
				MediaRoom.Dispose();
			}
		}
		_disposed = true;
	}
}
