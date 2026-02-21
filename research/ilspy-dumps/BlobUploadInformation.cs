using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RootApp.WebApi.Shared.Blob;

public class BlobUploadInformation(string, long, ReadOnlyMemory<byte>, ReadOnlyMemory<byte>, DateTimeOffset? = null) : IEquatable<BlobUploadInformation>
{
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[CompilerGenerated]
		get
		{
			return typeof(BlobUploadInformation);
		}
	}

	public string FileName { get; } = P_0;

	public long Length { get; } = P_1;

	public ReadOnlyMemory<byte> Md5Bytes { get; } = P_2;

	public ReadOnlyMemory<byte> Sha256Bytes { get; } = P_3;

	public DateTimeOffset? ModificationTime { get; } = P_4;

	public static async ValueTask<BlobUploadInformation> CreateAsync(string P_0, Stream P_1, DateTimeOffset? P_2, CancellationToken P_3)
	{
		using SHA256 sha256 = SHA256.Create();
		using MD5 md5 = MD5.Create();
		CryptoStream csSha = new CryptoStream(P_1, sha256, CryptoStreamMode.Read, true);
		ConfiguredAsyncDisposable I_0 = csSha.ConfigureAwait(false);
		try
		{
			CryptoStream csMd5 = new CryptoStream(csSha, md5, CryptoStreamMode.Read, true);
			ConfiguredAsyncDisposable I_3 = csMd5.ConfigureAwait(false);
			try
			{
				using IMemoryOwner<byte> buffer = MemoryPool<byte>.Shared.Rent(131072);
				while (0 < await csMd5.ReadAsync(buffer.Memory, P_3).ConfigureAwait(false))
				{
				}
			}
			finally
			{
				IAsyncDisposable asyncDisposable2 = I_3 as IAsyncDisposable;
				if (asyncDisposable2 != null)
				{
					await asyncDisposable2.DisposeAsync();
				}
			}
		}
		finally
		{
			IAsyncDisposable asyncDisposable = I_0 as IAsyncDisposable;
			if (asyncDisposable != null)
			{
				await asyncDisposable.DisposeAsync();
			}
		}
		if (md5.Hash == null || sha256.Hash == null)
		{
			throw new InvalidOperationException("Unable to compute file hash");
		}
		return new BlobUploadInformation(P_0, P_1.Length, md5.Hash, sha256.Hash, P_2);
	}

	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("BlobUploadInformation");
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
		P_0.Append("FileName = ");
		P_0.Append((object?)FileName);
		P_0.Append(", Length = ");
		P_0.Append(Length.ToString());
		P_0.Append(", Md5Bytes = ");
		P_0.Append(Md5Bytes.ToString());
		P_0.Append(", Sha256Bytes = ");
		P_0.Append(Sha256Bytes.ToString());
		P_0.Append(", ModificationTime = ");
		P_0.Append(ModificationTime.ToString());
		return true;
	}

	[CompilerGenerated]
	public static bool operator !=(BlobUploadInformation? P_0, BlobUploadInformation? P_1)
	{
		return !(P_0 == P_1);
	}

	[CompilerGenerated]
	public static bool operator ==(BlobUploadInformation? P_0, BlobUploadInformation? P_1)
	{
		return (object)P_0 == P_1 || (P_0?.Equals(P_1) ?? false);
	}

	[CompilerGenerated]
	public override int GetHashCode()
	{
		return ((((EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FileName)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(Length)) * -1521134295 + EqualityComparer<ReadOnlyMemory<byte>>.Default.GetHashCode(Md5Bytes)) * -1521134295 + EqualityComparer<ReadOnlyMemory<byte>>.Default.GetHashCode(Sha256Bytes)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(ModificationTime);
	}

	[CompilerGenerated]
	public override bool Equals(object? P_0)
	{
		return Equals(P_0 as BlobUploadInformation);
	}

	[CompilerGenerated]
	public virtual bool Equals(BlobUploadInformation? P_0)
	{
		return (object)this == P_0 || ((object)P_0 != null && EqualityContract == P_0.EqualityContract && EqualityComparer<string>.Default.Equals(FileName, P_0.FileName) && EqualityComparer<long>.Default.Equals(Length, P_0.Length) && EqualityComparer<ReadOnlyMemory<byte>>.Default.Equals(Md5Bytes, P_0.Md5Bytes) && EqualityComparer<ReadOnlyMemory<byte>>.Default.Equals(Sha256Bytes, P_0.Sha256Bytes) && EqualityComparer<DateTimeOffset?>.Default.Equals(ModificationTime, P_0.ModificationTime));
	}
}
