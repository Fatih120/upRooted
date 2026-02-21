using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public interface IPacketWebRtc : IPacket
{
	CommunityUuid? CommunityId { get; }
}
