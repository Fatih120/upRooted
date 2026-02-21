using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RootApp.Core;

public sealed class RootAppVersion(string, string, Version, string, string, string, string, string) : IEquatable<RootAppVersion>
{
	[CompilerGenerated]
	private Type EqualityContract
	{
		[CompilerGenerated]
		get
		{
			return typeof(RootAppVersion);
		}
	}

	public string UserAgent { get; } = P_0;

	public string Name { get; } = P_1;

	public Version Version { get; } = P_2;

	public string Instance { get; } = P_3;

	public string RuntimeEnvironment { get; } = P_4;

	public string Branch { get; } = P_5;

	public string Commit { get; } = P_6;

	public string ServiceName { get; } = P_7;

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("RootAppVersion");
		stringBuilder.Append(" { ");
		if (PrintMembers(stringBuilder))
		{
			stringBuilder.Append(' ');
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	[CompilerGenerated]
	private bool PrintMembers(StringBuilder P_0)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		P_0.Append("UserAgent = ");
		P_0.Append((object?)UserAgent);
		P_0.Append(", Name = ");
		P_0.Append((object?)Name);
		P_0.Append(", Version = ");
		P_0.Append(Version);
		P_0.Append(", Instance = ");
		P_0.Append((object?)Instance);
		P_0.Append(", RuntimeEnvironment = ");
		P_0.Append((object?)RuntimeEnvironment);
		P_0.Append(", Branch = ");
		P_0.Append((object?)Branch);
		P_0.Append(", Commit = ");
		P_0.Append((object?)Commit);
		P_0.Append(", ServiceName = ");
		P_0.Append((object?)ServiceName);
		return true;
	}

	[CompilerGenerated]
	public static bool operator !=(RootAppVersion? P_0, RootAppVersion? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(RootAppVersion? P_0, RootAppVersion? P_1)
	{
		return (object)P_0 == P_1 || (P_0?.Equals(P_1) ?? false);
	}

	[CompilerGenerated]
	public override int GetHashCode()
	{
		return (((((((EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserAgent)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name)) * -1521134295 + EqualityComparer<System.Version>.Default.GetHashCode(Version)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Instance)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RuntimeEnvironment)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Branch)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Commit)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ServiceName);
	}

	[CompilerGenerated]
	public override bool Equals(object? P_0)
	{
		return Equals(P_0 as RootAppVersion);
	}

	[CompilerGenerated]
	public bool Equals(RootAppVersion? P_0)
	{
		return (object)this == P_0 || ((object)P_0 != null && EqualityContract == P_0.EqualityContract && EqualityComparer<string>.Default.Equals(UserAgent, P_0.UserAgent) && EqualityComparer<string>.Default.Equals(Name, P_0.Name) && EqualityComparer<System.Version>.Default.Equals(Version, P_0.Version) && EqualityComparer<string>.Default.Equals(Instance, P_0.Instance) && EqualityComparer<string>.Default.Equals(RuntimeEnvironment, P_0.RuntimeEnvironment) && EqualityComparer<string>.Default.Equals(Branch, P_0.Branch) && EqualityComparer<string>.Default.Equals(Commit, P_0.Commit) && EqualityComparer<string>.Default.Equals(ServiceName, P_0.ServiceName));
	}
}
