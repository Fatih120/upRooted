using System;
using System.Buffers.Binary;

namespace RootApp.Core;

public static class RootUuidExtensionMethods
{
	public static Guid ToGuid<T>(this IRootUuid<T> P_0) where T : IRootUuid<T>
	{
		Span<byte> span = stackalloc byte[16];
		BinaryPrimitives.WriteUInt32LittleEndian(span.Slice(0, 4), (uint)(P_0.High64 >> 32));
		BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(4, 2), (ushort)((P_0.High64 >> 16) & 0xFFFF));
		BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(6, 2), (ushort)(P_0.High64 & 0xFFFF));
		BinaryPrimitives.WriteUInt64BigEndian(span.Slice(8, 8), P_0.Low64);
		return new Guid(span);
	}
}
