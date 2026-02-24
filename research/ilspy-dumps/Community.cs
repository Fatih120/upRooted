using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.CoreDomain.Repositories;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Helpers;
using RootApp.WebApi.Shared.Packets;
using RootApp.WebApi.Shared.Permissions;

namespace RootApp.Client.CoreDomain.Models.Community;

public class Community : ObservableObject, IDisposable
{
	private bool _disposed = false;

	private readonly ILogger<Community> _logger;

	private readonly RoleServiceFactory _roleServiceFactory;

	private readonly MemberServiceFactory _memberServiceFactory;

	private readonly MemberManagementServiceFactory _memberManagementServiceFactory;

	private readonly ChannelServiceFactory _channelServiceFactory;

	private readonly ActionLogServiceFactory _actionLogServiceFactory;

	private readonly CommunitySearchServiceFactory _communitySearchServiceFactory;

	private readonly CommunityAppServiceFactory _communityAppServiceFactory;

	private readonly ICommunityRepository _communityRepository;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IGlobalUserCacheService _globalUserCacheService;

	[CompilerGenerated]
	private bool <IsFullyLoaded>k__BackingField;

	[CompilerGenerated]
	private IRoleService? <Roles>k__BackingField;

	[CompilerGenerated]
	private IMemberManagementService? <MemberManagement>k__BackingField;

	[CompilerGenerated]
	private ICommunityLogService? <CommunityLogs>k__BackingField;

	[CompilerGenerated]
	private ICommunitySearchService? <Search>k__BackingField;

	[CompilerGenerated]
	private ICommunityAppService? <Apps>k__BackingField;

	[CompilerGenerated]
	private string <Name>k__BackingField;

	[CompilerGenerated]
	private string <PictureUrl>k__BackingField;

	[CompilerGenerated]
	private string <PictureHex>k__BackingField;

	[CompilerGenerated]
	private ChannelGuid? <DefaultChannelId>k__BackingField;

	[CompilerGenerated]
	private Channel? <SelectedChannel>k__BackingField;

	[CompilerGenerated]
	private int <AttachedUserCount>k__BackingField;

	[CompilerGenerated]
	private string <PrimaryRoleName>k__BackingField;

	[CompilerGenerated]
	private bool <IsFavorite>k__BackingField;

	[CompilerGenerated]
	private bool <VerifiedMemberAccessEnabled>k__BackingField;

	[CompilerGenerated]
	private CommunityJoinThrottle? <JoinThrottle>k__BackingField;

	[CompilerGenerated]
	private RootGuid <OwnerUserId>k__BackingField;

	[CompilerGenerated]
	private IMemberService? <Members>k__BackingField;

	[CompilerGenerated]
	private IChannelService? <Channels>k__BackingField;

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
	public string PictureUrl
	{
		get
		{
			return <PictureUrl>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<PictureUrl>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PictureUrl);
				<PictureUrl>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PictureUrl);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string PictureHex
	{
		get
		{
			return <PictureHex>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<PictureHex>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PictureHex);
				<PictureHex>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PictureHex);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ChannelGuid? DefaultChannelId
	{
		get
		{
			return <DefaultChannelId>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<ChannelGuid?>.Default.Equals(<DefaultChannelId>k__BackingField, channelGuid))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DefaultChannelId);
				<DefaultChannelId>k__BackingField = channelGuid;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DefaultChannelId);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Channel? SelectedChannel
	{
		get
		{
			return <SelectedChannel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<Channel>.Default.Equals(<SelectedChannel>k__BackingField, channel))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedChannel);
				<SelectedChannel>k__BackingField = channel;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedChannel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int AttachedUserCount
	{
		get
		{
			return <AttachedUserCount>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(<AttachedUserCount>k__BackingField, num))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.AttachedUserCount);
				<AttachedUserCount>k__BackingField = num;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.AttachedUserCount);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string PrimaryRoleName
	{
		get
		{
			return <PrimaryRoleName>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<PrimaryRoleName>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PrimaryRoleName);
				<PrimaryRoleName>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PrimaryRoleName);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsFavorite
	{
		get
		{
			return <IsFavorite>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsFavorite>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsFavorite);
				<IsFavorite>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsFavorite);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool VerifiedMemberAccessEnabled
	{
		get
		{
			return <VerifiedMemberAccessEnabled>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<VerifiedMemberAccessEnabled>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.VerifiedMemberAccessEnabled);
				<VerifiedMemberAccessEnabled>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.VerifiedMemberAccessEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public CommunityJoinThrottle? JoinThrottle
	{
		get
		{
			return <JoinThrottle>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<CommunityJoinThrottle>.Default.Equals(<JoinThrottle>k__BackingField, communityJoinThrottle))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.JoinThrottle);
				<JoinThrottle>k__BackingField = communityJoinThrottle;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.JoinThrottle);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("IsOwner")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RootGuid OwnerUserId
	{
		get
		{
			return <OwnerUserId>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<RootGuid>.Default.Equals(<OwnerUserId>k__BackingField, rootGuid))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.OwnerUserId);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsOwner);
				<OwnerUserId>k__BackingField = rootGuid;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.OwnerUserId);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsOwner);
			}
		}
	}

	public bool IsFullyLoaded
	{
		[CompilerGenerated]
		get
		{
			return <IsFullyLoaded>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<IsFullyLoaded>k__BackingField = flag;
		}
	}

	public CommunityGuid Id { get; }

	public string IdString => Id.ToString();

	public bool IsOwner => OwnerUserId == _rootSessionAccessor.Session?.UserInfoService.SessionUser.Id;

	public bool IsVerified { get; }

	public LocalCommunityPermission LocalCommunityPermission { get; }

	public IRoleService? Roles
	{
		[CompilerGenerated]
		get
		{
			return <Roles>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Roles>k__BackingField = roleService;
		}
	}

	public IMemberManagementService? MemberManagement
	{
		[CompilerGenerated]
		get
		{
			return <MemberManagement>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<MemberManagement>k__BackingField = memberManagementService;
		}
	}

	public ICommunityLogService? CommunityLogs
	{
		[CompilerGenerated]
		get
		{
			return <CommunityLogs>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<CommunityLogs>k__BackingField = communityLogService;
		}
	}

	public ICommunitySearchService? Search
	{
		[CompilerGenerated]
		get
		{
			return <Search>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Search>k__BackingField = communitySearchService;
		}
	}

	public ICommunityAppService? Apps
	{
		[CompilerGenerated]
		get
		{
			return <Apps>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Apps>k__BackingField = communityAppService;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IMemberService? Members
	{
		get
		{
			return <Members>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IMemberService>.Default.Equals(<Members>k__BackingField, memberService))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Members);
				<Members>k__BackingField = memberService;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Members);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IChannelService? Channels
	{
		get
		{
			return <Channels>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<IChannelService>.Default.Equals(<Channels>k__BackingField, channelService))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Channels);
				<Channels>k__BackingField = channelService;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Channels);
			}
		}
	}

	public Community(CommunityGuid id, string name, string pictureUrl, string pictureHex, RootGuid ownerId, int attachedUserCount, string primaryRoleName, bool isFavorite, RoleServiceFactory roleServiceFactory, MemberServiceFactory memberServiceFactory, MemberManagementServiceFactory memberManagementServiceFactory, ChannelServiceFactory channelServiceFactory, ActionLogServiceFactory actionLogServiceFactory, CommunitySearchServiceFactory communitySearchServiceFactory, CommunityAppServiceFactory communityAppServiceFactory, ICommunityRepository communityRepository, IRootSessionAccessor rootSessionAccessor, IGlobalUserCacheService globalUserCacheService, IVerifiedCommunityService verifiedCommunityService, ILoggerFactory loggerFactory)
	{
		IsFullyLoaded = false;
		LocalCommunityPermission = new LocalCommunityPermission();
		Id = id;
		Name = name;
		PictureUrl = pictureUrl;
		PictureHex = pictureHex;
		AttachedUserCount = attachedUserCount;
		PrimaryRoleName = primaryRoleName;
		IsFavorite = isFavorite;
		OwnerUserId = ownerId;
		IsVerified = verifiedCommunityService.IsVerified(id);
		_roleServiceFactory = roleServiceFactory;
		_memberServiceFactory = memberServiceFactory;
		_memberManagementServiceFactory = memberManagementServiceFactory;
		_channelServiceFactory = channelServiceFactory;
		_actionLogServiceFactory = actionLogServiceFactory;
		_communitySearchServiceFactory = communitySearchServiceFactory;
		_communityAppServiceFactory = communityAppServiceFactory;
		_communityRepository = communityRepository;
		_rootSessionAccessor = rootSessionAccessor;
		_globalUserCacheService = globalUserCacheService;
		_logger = loggerFactory.CreateLogger<Community>();
	}

	public void UpdateFromCommunityMemberResponse(CommunityMemberResponse P_0)
	{
		AttachedUserCount = P_0.ActiveCount;
		PrimaryRoleName = P_0.PrimaryCommunityRoleName;
		IsFavorite = P_0.IsFavorite;
	}

	public void UpdateFromCommunityExtendedResponse(CommunityGetExtendedResponse P_0)
	{
		FullyLoad(P_0, true);
	}

	public void FullyLoad(CommunityGetExtendedResponse P_0, bool P_1 = false)
	{
		IsFullyLoaded = true;
		Name = P_0.Name;
		PictureUrl = P_0.PictureAssetUri;
		PictureHex = P_0.PictureHex;
		OwnerUserId = P_0.OwnerUserId;
		DefaultChannelId = P_0.DefaultChannelId;
		VerifiedMemberAccessEnabled = P_0.RejectUnverifiedEmail;
		JoinThrottle = P_0.JoinThrottle;
		List<UserGuid> list = ((IEnumerable<CommunityMemberShort>)P_0.CommunityMembers).Select((Func<CommunityMemberShort, UserGuid>)((CommunityMemberShort cu) => cu.UserId)).ToList();
		_globalUserCacheService.CreateShellUsers(list);
		SetLocalCommunityPermissions(P_0.CommunityPermission);
		if (Roles == null)
		{
			Roles = _roleServiceFactory.Create(P_0, this);
		}
		else
		{
			Roles.Initialize(P_0);
		}
		if (Members == null)
		{
			Members = _memberServiceFactory.Create(P_0, this);
		}
		else
		{
			Members.Initialize(P_0);
		}
		if (MemberManagement == null)
		{
			MemberManagement = _memberManagementServiceFactory.Create(this);
		}
		if (Channels == null)
		{
			Channels = _channelServiceFactory.Create(P_0, this);
		}
		else
		{
			Channels.Initialize(P_0);
		}
		if (CommunityLogs == null)
		{
			CommunityLogs = _actionLogServiceFactory.Create(this);
		}
		if (Search == null)
		{
			Search = _communitySearchServiceFactory.Create(this);
		}
		if (Apps == null)
		{
			Apps = _communityAppServiceFactory.Create(P_0, this);
		}
		else
		{
			Apps.Initialize(P_0);
		}
		attachAsync();
		if (!P_1)
		{
			SelectFirstTextChannel();
			downloadUsersAndRefreshStatusesAsync(list, true).Forget();
		}
		else
		{
			downloadUsersAndRefreshStatusesAsync(list, false).Forget();
		}
	}

	private async Task downloadUsersAndRefreshStatusesAsync(List<UserGuid> P_0, bool P_1)
	{
		Members?.SuppressStatusUpdates();
		try
		{
			await _globalUserCacheService.GetUsersByIdsAsync(P_0, P_1);
		}
		finally
		{
			Members?.ResumeStatusUpdates();
		}
	}

	public void FullyUnload()
	{
		IsFullyLoaded = false;
		cleanup();
		detachAsync();
	}

	public void UpdateCommunityOwner(UserGuid P_0)
	{
		OwnerUserId = P_0;
	}

	public async Task EditCommunityAsync(CommunityEditRequest P_0)
	{
		try
		{
			updateCommunityFromEditResponse(await _communityRepository.EditCommunityAsync(P_0));
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to edit community");
			throw;
		}
	}

	public async Task SetFavoriteAsync(bool P_0)
	{
		try
		{
			CommunityMemberSetFavoriteRequest request = new CommunityMemberSetFavoriteRequest
			{
				CommunityId = Id,
				IsFavorite = P_0
			};
			await _communityRepository.SetFavoriteAsync(request);
			IsFavorite = P_0;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to set favorite for community");
			throw;
		}
	}

	private async Task attachAsync()
	{
		try
		{
			CommunityAttachRequest request = new CommunityAttachRequest
			{
				CommunityId = Id
			};
			await _communityRepository.AttachAsync(request);
			Members?.SetAttached(_rootSessionAccessor.Session.UserInfoService.SessionUser.Id, true);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to attach to community");
		}
	}

	private async Task detachAsync()
	{
		try
		{
			CommunityDetachRequest request = new CommunityDetachRequest
			{
				CommunityId = Id
			};
			await _communityRepository.DetachAsync(request);
			Members?.SetAttached(_rootSessionAccessor.Session.UserInfoService.SessionUser.Id, false);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to detach from community");
		}
	}

	public void HandlePacket(IPacket P_0)
	{
		if (P_0.PacketType == PacketType.CommunityEditedExternal)
		{
			processCommunityEditedPacket((CommunityPacket)P_0);
		}
		else if (P_0.PacketType == PacketType.CommunityMemberEditedExternal)
		{
			processCommunityMemberExternalEditedPacket((CommunityMemberEditedExternalPacket)P_0);
		}
		else if (P_0.PacketType == PacketType.CommunityPermissionUpdate)
		{
			processCommunityPermissionUpdatePacket((CommunityPermissionUpdatePacket)P_0);
		}
		else if (P_0.PacketType.IsChannelGroupPacket() || P_0.PacketType.IsChannelPacket() || P_0.PacketType.IsChannelFilePacket() || P_0.PacketType.IsChannelDirectoryPacket() || P_0.PacketType.IsChannelMessagePacket() || P_0.PacketType.IsChannelWebRtcPacket())
		{
			Channels?.HandlePacket(P_0);
		}
		else if (P_0.PacketType.IsCommunityMemberPacket() || P_0.PacketType.IsCommunityInfoPacket() || P_0.PacketType.IsCommunityMemberRolePacket())
		{
			Members?.HandlePacket(P_0);
		}
		else if (P_0.PacketType.IsRolePacket())
		{
			Roles?.HandlePacket(P_0);
		}
		else if (P_0.PacketType.IsCommunityAppPacket())
		{
			Apps?.HandlePacket(P_0);
		}
	}

	private void processCommunityEditedPacket(CommunityPacket P_0)
	{
		updateCommunityFromPacket(P_0);
	}

	private void processCommunityMemberExternalEditedPacket(CommunityMemberEditedExternalPacket P_0)
	{
		IsFavorite = P_0.IsFavorite;
	}

	private void processCommunityPermissionUpdatePacket(CommunityPermissionUpdatePacket P_0)
	{
		if (P_0.CommunityPermission != null)
		{
			SetLocalCommunityPermissions(P_0.CommunityPermission);
		}
		foreach (ChannelGroupCreatedPacket item in P_0.ChannelGroupsCreated)
		{
			HandlePacket(item);
		}
		foreach (ChannelCreatedPacket item2 in P_0.ChannelsCreated)
		{
			HandlePacket(item2);
		}
		foreach (ChannelGroupEditedPacket item3 in P_0.ChannelGroupsEdited)
		{
			HandlePacket(item3);
		}
		foreach (ChannelEditedPacket item4 in P_0.ChannelsEdited)
		{
			HandlePacket(item4);
		}
		foreach (ChannelDeletedPacket item5 in P_0.ChannelsDeleted)
		{
			HandlePacket(item5);
		}
		foreach (ChannelGroupDeletedPacket item6 in P_0.ChannelGroupsDeleted)
		{
			HandlePacket(item6);
		}
		foreach (ChannelGroupMovedPacket item7 in P_0.ChannelGroupsMoved)
		{
			HandlePacket(item7);
		}
		foreach (ChannelMovedPacket item8 in P_0.ChannelsMoved)
		{
			HandlePacket(item8);
		}
	}

	private void updateCommunityFromPacket(CommunityPacket P_0)
	{
		UpdateCommunityOwner(P_0.OwnerUserId);
		Name = P_0.Name;
		PictureHex = P_0.PictureHex;
		PictureUrl = P_0.PictureAssetUri;
		DefaultChannelId = P_0.DefaultChannelId;
		VerifiedMemberAccessEnabled = P_0.RejectUnverifiedEmail;
		JoinThrottle = P_0.JoinThrottle;
	}

	private void updateCommunityFromEditResponse(CommunityEditResponse P_0)
	{
		Name = P_0.Name;
		PictureHex = P_0.PictureHex;
		PictureUrl = P_0.PictureAssetUri;
		DefaultChannelId = P_0.DefaultChannelId;
		VerifiedMemberAccessEnabled = P_0.RejectUnverifiedEmail;
		JoinThrottle = P_0.JoinThrottle;
	}

	public void SelectFirstTextChannel()
	{
		if (Channels != null)
		{
			Channel firstTextChannel = Channels.GetFirstTextChannel();
			if (firstTextChannel != null)
			{
				SelectChannel(firstTextChannel);
			}
		}
	}

	public void SelectChannel(Channel P_0)
	{
		SelectedChannel = P_0;
	}

	public void SetLocalCommunityPermissions(ICommunityPermissions P_0)
	{
		P_0.Copy(LocalCommunityPermission);
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
				cleanup();
			}
			_disposed = true;
		}
	}

	private void cleanup()
	{
		Roles?.Dispose();
		Roles = null;
		Members?.Dispose();
		Members = null;
		Channels?.Dispose();
		Channels = null;
	}
}
