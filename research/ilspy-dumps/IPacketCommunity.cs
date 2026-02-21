using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public interface IPacketCommunity : IPacket
{
	CommunityUuid CommunityId { get; }
}
