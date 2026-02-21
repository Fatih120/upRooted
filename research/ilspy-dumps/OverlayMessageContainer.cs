using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayMessageContainer : IMessageContainer
{
	public CommunityGuid? CommunityId { get; }

	public MessageContainerGuid ContainerId { get; }

	public string Name { get; }

	public DateTime? UserLastViewedAt => null;

	public bool HasActivity => false;

	public IMessageService Messages => null;

	public LocalChannelPermission LocalChannelPermission { get; }

	public OverlayMessageContainer(MessageContainerGuid P_0, CommunityGuid? P_1, string P_2)
	{
		ContainerId = P_0;
		CommunityId = P_1;
		Name = P_2;
		LocalChannelPermission = new LocalChannelPermission
		{
			ChannelCreateMessage = false,
			ChannelCreateMessageReaction = false,
			ChannelDeleteMessageOther = false,
			ChannelManagePinnedMessages = false
		};
	}

	public Task<IMessageContainerMember?> GetMemberAsync(UserGuid P_0)
	{
		return Task.FromResult<IMessageContainerMember>(null);
	}

	public void SetViewTime(DateTime P_0)
	{
	}

	public void SetLastSentMessage(Message P_0)
	{
	}

	public IEnumerable<IMessageContainerMember> FindMembers(string P_0)
	{
		return Array.Empty<IMessageContainerMember>();
	}

	public IEnumerable<Role>? FindRoles(string P_0)
	{
		return null;
	}

	public IEnumerable<Channel>? FindChannels(string P_0)
	{
		return null;
	}

	public Channel? GetChannel(RootGuid P_0)
	{
		return null;
	}

	public Role? GetRole(CommunityRoleGuid P_0)
	{
		return null;
	}
}
