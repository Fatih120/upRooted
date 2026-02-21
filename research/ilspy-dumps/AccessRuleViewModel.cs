using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Grpc;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.Client.Avalonia.UI.Community.Channels.Permissions;

public class AccessRuleViewModel : ViewModelBase<AccessRuleViewModel>
{
	private readonly Action _navigateBackAction;

	private readonly CommunityGuid _communityId;

	private AccessRuleResponse? _initialAccessRuleResponse;

	private RoleOrMemberGuid _roleOrMemberId;

	private ChannelOrChannelGroupGuid? _channelOrChannelGroupId;

	[CompilerGenerated]
	private bool? <FullControlChannel>k__BackingField;

	[CompilerGenerated]
	private bool? <SendMessage>k__BackingField;

	[CompilerGenerated]
	private bool? <AttachFiles>k__BackingField;

	[CompilerGenerated]
	private bool? <UseGlobalMentions>k__BackingField;

	[CompilerGenerated]
	private bool? <ViewHistory>k__BackingField;

	[CompilerGenerated]
	private bool? <AddReactions>k__BackingField;

	[CompilerGenerated]
	private bool? <DeleteMessages>k__BackingField;

	[CompilerGenerated]
	private bool? <ManagePinnedMessages>k__BackingField;

	[CompilerGenerated]
	private bool? <ViewFiles>k__BackingField;

	[CompilerGenerated]
	private bool? <UploadFiles>k__BackingField;

	[CompilerGenerated]
	private bool? <ManageFiles>k__BackingField;

	[CompilerGenerated]
	private bool? <StreamAudio>k__BackingField;

	[CompilerGenerated]
	private bool? <StreamVideo>k__BackingField;

	[CompilerGenerated]
	private bool? <MuteMicrophones>k__BackingField;

	[CompilerGenerated]
	private bool? <MuteSpeakers>k__BackingField;

	[CompilerGenerated]
	private bool? <KickVoiceMember>k__BackingField;

	[CompilerGenerated]
	private bool <IsAppChannel>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? navigateBackCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? FullControlChannel
	{
		get
		{
			return <FullControlChannel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<FullControlChannel>k__BackingField, flag))
			{
				<FullControlChannel>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FullControlChannel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? SendMessage
	{
		get
		{
			return <SendMessage>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<SendMessage>k__BackingField, flag))
			{
				<SendMessage>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SendMessage);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? AttachFiles
	{
		get
		{
			return <AttachFiles>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<AttachFiles>k__BackingField, flag))
			{
				<AttachFiles>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AttachFiles);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? UseGlobalMentions
	{
		get
		{
			return <UseGlobalMentions>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<UseGlobalMentions>k__BackingField, flag))
			{
				<UseGlobalMentions>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UseGlobalMentions);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? ViewHistory
	{
		get
		{
			return <ViewHistory>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<ViewHistory>k__BackingField, flag))
			{
				<ViewHistory>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ViewHistory);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? AddReactions
	{
		get
		{
			return <AddReactions>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<AddReactions>k__BackingField, flag))
			{
				<AddReactions>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AddReactions);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? DeleteMessages
	{
		get
		{
			return <DeleteMessages>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<DeleteMessages>k__BackingField, flag))
			{
				<DeleteMessages>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DeleteMessages);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? ManagePinnedMessages
	{
		get
		{
			return <ManagePinnedMessages>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<ManagePinnedMessages>k__BackingField, flag))
			{
				<ManagePinnedMessages>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ManagePinnedMessages);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? ViewFiles
	{
		get
		{
			return <ViewFiles>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<ViewFiles>k__BackingField, flag))
			{
				<ViewFiles>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ViewFiles);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? UploadFiles
	{
		get
		{
			return <UploadFiles>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<UploadFiles>k__BackingField, flag))
			{
				<UploadFiles>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UploadFiles);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? ManageFiles
	{
		get
		{
			return <ManageFiles>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<ManageFiles>k__BackingField, flag))
			{
				<ManageFiles>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ManageFiles);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? StreamAudio
	{
		get
		{
			return <StreamAudio>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<StreamAudio>k__BackingField, flag))
			{
				<StreamAudio>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.StreamAudio);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? StreamVideo
	{
		get
		{
			return <StreamVideo>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<StreamVideo>k__BackingField, flag))
			{
				<StreamVideo>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.StreamVideo);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? MuteMicrophones
	{
		get
		{
			return <MuteMicrophones>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<MuteMicrophones>k__BackingField, flag))
			{
				<MuteMicrophones>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MuteMicrophones);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? MuteSpeakers
	{
		get
		{
			return <MuteSpeakers>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<MuteSpeakers>k__BackingField, flag))
			{
				<MuteSpeakers>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MuteSpeakers);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? KickVoiceMember
	{
		get
		{
			return <KickVoiceMember>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<KickVoiceMember>k__BackingField, flag))
			{
				<KickVoiceMember>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.KickVoiceMember);
			}
		}
	}

	public string Name { get; }

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsAppChannel
	{
		get
		{
			return <IsAppChannel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsAppChannel>k__BackingField, flag))
			{
				<IsAppChannel>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsAppChannel);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand NavigateBackCommand => navigateBackCommand ?? (navigateBackCommand = new RelayCommand(NavigateBack));

	public AccessRuleViewModel(RoleOrMemberGuid P_0, string P_1, RootApp.Client.CoreDomain.Models.Community.Community P_2, Action P_3, bool P_4 = false)
		: base((IValidator<AccessRuleViewModel>?)null)
	{
		Name = P_1;
		IsAppChannel = P_4;
		_navigateBackAction = P_3;
		_roleOrMemberId = P_0;
		_communityId = P_2.Id;
		if ((RootGuidType)_roleOrMemberId == RootGuidType.Person)
		{
			SendMessage = true;
			AttachFiles = true;
			UseGlobalMentions = true;
			AddReactions = true;
			StreamAudio = true;
			StreamVideo = true;
			ViewFiles = true;
			ViewHistory = true;
		}
	}

	public void UpdateFromAccessRuleResponse(AccessRuleResponse P_0)
	{
		_initialAccessRuleResponse = P_0;
		_roleOrMemberId = P_0.RoleOrMemberId;
		_channelOrChannelGroupId = P_0.ChannelOrChannelGroupId;
		FullControlChannel = P_0.Overlay.ChannelFullControl;
		SendMessage = P_0.Overlay.ChannelCreateMessage;
		AttachFiles = P_0.Overlay.ChannelCreateMessageAttachment;
		UseGlobalMentions = P_0.Overlay.ChannelCreateMessageMention;
		ViewHistory = P_0.Overlay.ChannelViewMessageHistory;
		AddReactions = P_0.Overlay.ChannelCreateMessageReaction;
		DeleteMessages = P_0.Overlay.ChannelDeleteMessageOther;
		ManagePinnedMessages = P_0.Overlay.ChannelManagePinnedMessages;
		ViewFiles = P_0.Overlay.ChannelViewFile;
		UploadFiles = P_0.Overlay.ChannelCreateFile;
		ManageFiles = P_0.Overlay.ChannelManageFiles;
		StreamAudio = P_0.Overlay.ChannelVoiceTalk;
		StreamVideo = P_0.Overlay.ChannelVideoStreamMedia;
		MuteMicrophones = P_0.Overlay.ChannelVoiceMuteOther;
		MuteSpeakers = P_0.Overlay.ChannelVoiceDeafenOther;
		KickVoiceMember = P_0.Overlay.ChannelVoiceKick;
	}

	public AccessRuleCreateRoleOrMemberRequest GetAccessRuleCreateRoleOrMemberRequest()
	{
		AccessRuleCreateRoleOrMemberRequest accessRuleCreateRoleOrMemberRequest = new AccessRuleCreateRoleOrMemberRequest();
		accessRuleCreateRoleOrMemberRequest.Overlay = buildOverlay();
		accessRuleCreateRoleOrMemberRequest.RoleOrMemberId = _roleOrMemberId;
		return accessRuleCreateRoleOrMemberRequest;
	}

	public AccessRuleUpdateRequest GetAccessRuleUpdateRequest()
	{
		AccessRuleUpdateRequest accessRuleUpdateRequest = new AccessRuleUpdateRequest();
		if (_initialAccessRuleResponse != null && containsDif())
		{
			accessRuleUpdateRequest.Edits.Add(getAccessRuleEditRequest());
		}
		else if (_initialAccessRuleResponse == null)
		{
			accessRuleUpdateRequest.Creates.Add(getAccessRuleCreateRequest());
		}
		return accessRuleUpdateRequest;
	}

	private AccessRuleEditRequest getAccessRuleEditRequest()
	{
		AccessRuleEditRequest accessRuleEditRequest = new AccessRuleEditRequest();
		accessRuleEditRequest.Overlay = buildOverlay();
		accessRuleEditRequest.CommunityId = _communityId;
		accessRuleEditRequest.RoleOrMemberId = _roleOrMemberId;
		accessRuleEditRequest.ChannelOrChannelGroupId = _channelOrChannelGroupId;
		return accessRuleEditRequest;
	}

	private AccessRuleCreateRequest getAccessRuleCreateRequest()
	{
		AccessRuleCreateRequest accessRuleCreateRequest = new AccessRuleCreateRequest();
		accessRuleCreateRequest.Overlay = buildOverlay();
		accessRuleCreateRequest.CommunityId = _communityId;
		accessRuleCreateRequest.RoleOrMemberId = _roleOrMemberId;
		accessRuleCreateRequest.ChannelOrChannelGroupId = _channelOrChannelGroupId;
		return accessRuleCreateRequest;
	}

	private ChannelOverlayPermission buildOverlay()
	{
		return new ChannelOverlayPermission
		{
			ChannelFullControl = FullControlChannel,
			ChannelCreateMessage = SendMessage,
			ChannelCreateMessageAttachment = AttachFiles,
			ChannelCreateMessageMention = UseGlobalMentions,
			ChannelViewMessageHistory = ViewHistory,
			ChannelCreateMessageReaction = AddReactions,
			ChannelDeleteMessageOther = DeleteMessages,
			ChannelManagePinnedMessages = ManagePinnedMessages,
			ChannelViewFile = ViewFiles,
			ChannelCreateFile = UploadFiles,
			ChannelManageFiles = ManageFiles,
			ChannelVoiceTalk = StreamAudio,
			ChannelVideoStreamMedia = StreamVideo,
			ChannelVoiceMuteOther = MuteMicrophones,
			ChannelVoiceDeafenOther = MuteSpeakers,
			ChannelVoiceKick = KickVoiceMember
		};
	}

	private bool containsDif()
	{
		if (_initialAccessRuleResponse == null)
		{
			return true;
		}
		return FullControlChannel != _initialAccessRuleResponse.Overlay.ChannelFullControl || SendMessage != _initialAccessRuleResponse.Overlay.ChannelCreateMessage || AttachFiles != _initialAccessRuleResponse.Overlay.ChannelCreateMessageAttachment || UseGlobalMentions != _initialAccessRuleResponse.Overlay.ChannelCreateMessageMention || ViewHistory != _initialAccessRuleResponse.Overlay.ChannelViewMessageHistory || AddReactions != _initialAccessRuleResponse.Overlay.ChannelCreateMessageReaction || DeleteMessages != _initialAccessRuleResponse.Overlay.ChannelDeleteMessageOther || ManagePinnedMessages != _initialAccessRuleResponse.Overlay.ChannelManagePinnedMessages || ViewFiles != _initialAccessRuleResponse.Overlay.ChannelViewFile || UploadFiles != _initialAccessRuleResponse.Overlay.ChannelCreateFile || ManageFiles != _initialAccessRuleResponse.Overlay.ChannelManageFiles || StreamAudio != _initialAccessRuleResponse.Overlay.ChannelVoiceTalk || StreamVideo != _initialAccessRuleResponse.Overlay.ChannelVideoStreamMedia || MuteMicrophones != _initialAccessRuleResponse.Overlay.ChannelVoiceMuteOther || MuteSpeakers != _initialAccessRuleResponse.Overlay.ChannelVoiceDeafenOther || KickVoiceMember != _initialAccessRuleResponse.Overlay.ChannelVoiceKick;
	}

	[RelayCommand]
	public void NavigateBack()
	{
		_navigateBackAction();
	}
}
