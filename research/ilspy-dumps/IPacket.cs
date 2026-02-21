using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public interface IPacket
{
	PacketType PacketType { get; }
}
