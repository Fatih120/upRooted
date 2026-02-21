using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace RootApp.WebApi.Client;

public class UploadContext(FileInfo, IProgress<int>? = null) : IEquatable<UploadContext>
{
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[CompilerGenerated]
		get
		{
			return typeof(UploadContext);
		}
	}

	public FileInfo File { get; } = P_0;

	public IProgress<int>? PercentComplete { get; } = P_1;

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("UploadContext");
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
		P_0.Append("File = ");
		P_0.Append(File);
		P_0.Append(", PercentComplete = ");
		P_0.Append(PercentComplete);
		return true;
	}

	[CompilerGenerated]
	public static bool operator !=(UploadContext? P_0, UploadContext? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(UploadContext? P_0, UploadContext? P_1)
	{
		return (object)P_0 == P_1 || (P_0?.Equals(P_1) ?? false);
	}

	[CompilerGenerated]
	public override int GetHashCode()
	{
		return (EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<FileInfo>.Default.GetHashCode(File)) * -1521134295 + EqualityComparer<IProgress<int>>.Default.GetHashCode(PercentComplete);
	}

	[CompilerGenerated]
	public override bool Equals(object? P_0)
	{
		return Equals(P_0 as UploadContext);
	}

	[CompilerGenerated]
	public virtual bool Equals(UploadContext? P_0)
	{
		return (object)this == P_0 || ((object)P_0 != null && EqualityContract == P_0.EqualityContract && EqualityComparer<FileInfo>.Default.Equals(File, P_0.File) && EqualityComparer<IProgress<int>>.Default.Equals(PercentComplete, P_0.PercentComplete));
	}
}
