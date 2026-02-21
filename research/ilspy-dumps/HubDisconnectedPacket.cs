using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace RootApp.HubServer.Client.Packets;

public sealed class HubDisconnectedPacket : InternalPacket, IEquatable<HubDisconnectedPacket>
{
	[CompilerGenerated]
	protected override Type EqualityContract
	{
		[CompilerGenerated]
		get
		{
			return typeof(HubDisconnectedPacket);
		}
	}

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("HubDisconnectedPacket");
		stringBuilder.Append(" { ");
		if (PrintMembers(stringBuilder))
		{
			stringBuilder.Append(' ');
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	[CompilerGenerated]
	protected override bool PrintMembers(StringBuilder P_0)
	{
		return base.PrintMembers(P_0);
	}

	[CompilerGenerated]
	public static bool operator !=(HubDisconnectedPacket? P_0, HubDisconnectedPacket? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(HubDisconnectedPacket? P_0, HubDisconnectedPacket? P_1)
	{
		return (object)P_0 == P_1 || (P_0?.Equals(P_1) ?? false);
	}

	[CompilerGenerated]
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	[CompilerGenerated]
	public override bool Equals(object? P_0)
	{
		return Equals(P_0 as HubDisconnectedPacket);
	}

	[CompilerGenerated]
	public sealed override bool Equals(InternalPacket? P_0)
	{
		return Equals((object?)P_0);
	}

	[CompilerGenerated]
	public bool Equals(HubDisconnectedPacket? P_0)
	{
		return (object)this == P_0 || base.Equals(P_0);
	}
}
