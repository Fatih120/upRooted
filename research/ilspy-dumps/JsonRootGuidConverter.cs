using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RootApp.Core.Identifiers;

namespace RootApp.Core.Converters;

public class JsonRootGuidConverter<T> : JsonConverter<T> where T : struct, IRootGuid<T>
{
	public override T Read(ref Utf8JsonReader P_0, Type P_1, JsonSerializerOptions P_2)
	{
		if (P_0.TokenType != JsonTokenType.String)
		{
			throw new FormatException("RootGuid: Wrong Type");
		}
		return ReadRootGuid(ref P_0);
	}

	private static T ReadRootGuid(ref Utf8JsonReader P_0)
	{
		if (!P_0.HasValueSequence)
		{
			return ConvertGuid(P_0.ValueSpan);
		}
		ReadOnlySequence<byte> valueSequence = P_0.ValueSequence;
		if (valueSequence.Length <= 64)
		{
			Span<byte> span = stackalloc byte[(int)valueSequence.Length];
			P_0.ValueSequence.CopyTo(span);
			return ConvertGuid(span);
		}
		SequenceReader<byte> sequenceReader = new SequenceReader<byte>(valueSequence);
		sequenceReader.AdvancePastAny(32, 9, 13, 10);
		ReadOnlySequence<byte> unreadSequence = sequenceReader.UnreadSequence;
		if (unreadSequence.Length > 1024)
		{
			throw new FormatException("RootGuid: bad decode");
		}
		return ConvertGuid(BuffersExtensions.ToArray(in unreadSequence));
		static T ConvertGuid(ReadOnlySpan<byte> readOnlySpan)
		{
			if (!Utf8Parser.TryParse(readOnlySpan, out Guid guid, out int _, '\0'))
			{
				if (!RootGuid.TryParse(Encoding.UTF8.GetString(readOnlySpan), null, out var rootGuid))
				{
					throw new FormatException("Guid: bad decode");
				}
				return T.Create((High64: rootGuid.High64, Low64: rootGuid.Low64));
			}
			return T.Create(guid);
		}
	}

	public override void Write(Utf8JsonWriter P_0, T P_1, JsonSerializerOptions P_2)
	{
		P_0.WriteStringValue(P_1.ToGuid());
	}

	public override T ReadAsPropertyName(ref Utf8JsonReader P_0, Type P_1, JsonSerializerOptions P_2)
	{
		return ReadRootGuid(ref P_0);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter P_0, T P_1, JsonSerializerOptions P_2)
	{
		Span<byte> span = stackalloc byte[64];
		if (!Utf8Formatter.TryFormat(P_1.ToGuid(), span, out var num))
		{
			throw new FormatException("Unable to format guid");
		}
		span = span.Slice(0, num);
		P_0.WritePropertyName(span);
	}
}
