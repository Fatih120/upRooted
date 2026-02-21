using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.HubServer.Client.Packets;

public abstract class InternalPacket : IPacket, IEquatable<InternalPacket>
{
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[CompilerGenerated]
		get
		{
			return typeof(InternalPacket);
		}
	}

	public PacketType PacketType => PacketType.Unspecified;

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("InternalPacket");
		stringBuilder.Append(" { ");
		if (PrintMembers(stringBuilder))
		{
			stringBuilder.Append(' ');
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder P_0)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		P_0.Append("PacketType = ");
		P_0.Append(PacketType.ToString());
		return true;
	}

	[CompilerGenerated]
	public static bool operator !=(InternalPacket? P_0, InternalPacket? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(InternalPacket? P_0, InternalPacket? P_1)
	{
		return (object)P_0 == P_1 || (P_0?.Equals(P_1) ?? false);
	}

	[CompilerGenerated]
	public override int GetHashCode()
	{
		return EqualityComparer<Type>.Default.GetHashCode(EqualityContract);
	}

	[CompilerGenerated]
	public override bool Equals(object? P_0)
	{
		return Equals(P_0 as InternalPacket);
	}

	[CompilerGenerated]
	public virtual bool Equals(InternalPacket? P_0)
	{
		return (object)this == P_0 || ((object)P_0 != null && EqualityContract == P_0.EqualityContract);
	}
}
