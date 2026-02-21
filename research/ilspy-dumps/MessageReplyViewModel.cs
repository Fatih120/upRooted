using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Messages;

public class MessageReplyViewModel : ViewModelBase<MessageReplyViewModel>
{
	private readonly IMessageContainer _messageContainer;

	private readonly BitmapCache _bitmapCache;

	private readonly CommunityOpenerService _communityOpenerService;

	private readonly DirectMessageOpenerService _directMessageOpenerService;

	[CompilerGenerated]
	private IMessageContainerMember? <MessageContainerMember>k__BackingField;

	[CompilerGenerated]
	private Member? <CommunityMember>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? jumpToMessageCommand;

	public MessageReply MessageReply { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => getProfilePicAsync();

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IMessageContainerMember? MessageContainerMember
	{
		get
		{
			return <MessageContainerMember>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IMessageContainerMember>.Default.Equals(<MessageContainerMember>k__BackingField, messageContainerMember))
			{
				<MessageContainerMember>k__BackingField = messageContainerMember;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MessageContainerMember);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Member? CommunityMember
	{
		get
		{
			return <CommunityMember>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<Member>.Default.Equals(<CommunityMember>k__BackingField, member))
			{
				<CommunityMember>k__BackingField = member;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityMember);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand JumpToMessageCommand => jumpToMessageCommand ?? (jumpToMessageCommand = new RelayCommand(JumpToMessage));

	public MessageReplyViewModel(MessageReply P_0, IMessageContainer P_1, BitmapCache P_2, CommunityOpenerService P_3, DirectMessageOpenerService P_4)
		: base((IValidator<MessageReplyViewModel>?)null)
	{
		MessageReply = P_0;
		_messageContainer = P_1;
		_bitmapCache = P_2;
		_communityOpenerService = P_3;
		_directMessageOpenerService = P_4;
	}

	[RelayCommand]
	public void JumpToMessage()
	{
		if (MessageReply.MessageContent != null)
		{
			if (_messageContainer.CommunityId != null)
			{
				_communityOpenerService.OpenCommunity(_messageContainer.CommunityId.Value, _messageContainer.ContainerId, MessageReply.Id);
			}
			else
			{
				_directMessageOpenerService.OpenDirectMessage(((DirectMessageGuid?)_messageContainer.ContainerId).Value, MessageReply.Id);
			}
		}
	}

	public async Task<BitmapWrapper?> getProfilePicAsync()
	{
		if (MessageReply.UserId != null)
		{
			MessageContainerMember = await _messageContainer.GetMemberAsync(MessageReply.UserId.Value);
			IMessageContainerMember messageContainerMember = MessageContainerMember;
			if (messageContainerMember is Member member)
			{
				CommunityMember = member;
			}
			if (MessageContainerMember != null)
			{
				return await _bitmapCache.GetBitmapAsync(MessageContainerMember.GlobalUser.ProfilePictureUri, null, 120);
			}
		}
		return null;
	}
}
