using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RootApp.HubServer.Client.Packets;

public sealed class HubConnectedPacket(Uri) : InternalPacket(), IEquatable<HubConnectedPacket>
{
	[CompilerGenerated]
	protected override Type EqualityContract
	{
		[CompilerGenerated]
		get
		{
			return typeof(HubConnectedPacket);
		}
	}

	public Uri HubUrl { get; } = P_0;

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("HubConnectedPacket");
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
		if (base.PrintMembers(P_0))
		{
			P_0.Append(", ");
		}
		P_0.Append("HubUrl = ");
		P_0.Append(HubUrl);
		return true;
	}

	[CompilerGenerated]
	public static bool operator !=(HubConnectedPacket? P_0, HubConnectedPacket? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(HubConnectedPacket? P_0, HubConnectedPacket? P_1)
	{
		return (object)P_0 == P_1 || (P_0?.Equals(P_1) ?? false);
	}

	[CompilerGenerated]
	public override int GetHashCode()
	{
		return base.GetHashCode() * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(HubUrl);
	}

	[CompilerGenerated]
	public override bool Equals(object? P_0)
	{
		return Equals(P_0 as HubConnectedPacket);
	}

	[CompilerGenerated]
	public sealed override bool Equals(InternalPacket? P_0)
	{
		return Equals((object?)P_0);
	}

	[CompilerGenerated]
	public bool Equals(HubConnectedPacket? P_0)
	{
		return (object)this == P_0 || (base.Equals(P_0) && EqualityComparer<Uri>.Default.Equals(HubUrl, P_0.HubUrl));
	}
}
