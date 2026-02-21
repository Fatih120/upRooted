using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Client.Shared;

public record ClientHubPacket(IPacket Packet, PacketContainer? PacketContainer);
