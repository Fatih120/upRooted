using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using RootApp.Core.Converters;
using RootApp.Core.Enums;

namespace RootApp.Core.Identifiers;

[TypeConverter(typeof(RootGuidConverter<RootGuid>))]
[JsonConverter(typeof(JsonRootGuidConverter<RootGuid>))]
public readonly struct RootGuid : IRootGuid<RootGuid>, IRootGuid, IComparable, IComparable<RootGuid>, IEquatable<RootGuid>, IFormattable, IParsable<RootGuid>
{
	public static readonly RootGuid Empty = new RootGuid(0uL, 0uL);

	public static readonly RootGuid Full = new RootGuid(ulong.MaxValue, ulong.MaxValue);

	[CompilerGenerated]
	private readonly ulong _003CHigh64_003Ek__BackingField;

	[CompilerGenerated]
	private readonly ulong _003CLow64_003Ek__BackingField;

	public ulong High64
	{
		[CompilerGenerated]
		get
		{
			return _003CHigh64_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CHigh64_003Ek__BackingField = num;
		}
	}

	public ulong Low64
	{
		[CompilerGenerated]
		get
		{
			return _003CLow64_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CLow64_003Ek__BackingField = num;
		}
	}

	public RootGuid()
	{
		High64 = 0uL;
		Low64 = 0uL;
	}

	public RootGuid(ulong P_0, ulong P_1)
	{
		High64 = P_0;
		Low64 = P_1;
	}

	public RootGuid((ulong High64, ulong Low64) P_0)
		: this(P_0.High64, P_0.Low64)
	{
	}

	public RootGuid(ReadOnlySpan<byte> P_0)
		: this(RootGuidInternals.FromReadOnlySpan(P_0))
	{
	}

	public RootGuid(DateTime P_0)
		: this(RootGuidInternals.FromDateTime(P_0))
	{
	}

	public (ulong High64, ulong Low64) GetValue()
	{
		return (High64: High64, Low64: Low64);
	}

	public int CompareTo(object? P_0)
	{
		return RootGuidInternals.CompareTo(this, P_0);
	}

	public int CompareTo(RootGuid P_0)
	{
		return RootGuidInternals.CompareToGuid(this, P_0);
	}

	public bool Equals(RootGuid P_0)
	{
		return P_0.High64 == High64 && P_0.Low64 == Low64;
	}

	public string ToString(string? P_0, IFormatProvider? P_1)
	{
		return this.ToStringFormat(P_0);
	}

	public byte[] ToByteArray()
	{
		byte[] array = new byte[16];
		this.ToBytes(array);
		return array;
	}

	public static RootGuid Parse(string P_0, IFormatProvider? P_1)
	{
		return Parse<RootGuid>(P_0.AsSpan());
	}

	public static bool TryParse(string? P_0, IFormatProvider? P_1, out RootGuid P_2)
	{
		return TryParse<RootGuid>(P_0, out P_2);
	}

	public static T Parse<T>(string? input) where T : struct, IRootGuid<T>
	{
		if (!TryParse<T>(input, out var result))
		{
			throw new ArgumentException("Invalid format: " + input, "input");
		}
		return result;
	}

	public static bool TryParse<T>(string? P_0, out T P_1) where T : struct, IRootGuid<T>
	{
		if (P_0 == null)
		{
			P_1 = T.Create((High64: 0uL, Low64: 0uL));
			return false;
		}
		return TryParse<T>(P_0.AsSpan(), out P_1);
	}

	public static T Parse<T>(ReadOnlySpan<char> P_0) where T : struct, IRootGuid<T>
	{
		if (!TryParse<T>(P_0, out var result))
		{
			throw new ArgumentException($"Invalid format: {P_0}", "input");
		}
		return result;
	}

	public static bool TryParse<T>(ReadOnlySpan<char> P_0, out T P_1) where T : struct, IRootGuid<T>
	{
		(ulong, ulong) tuple;
		bool result = RootGuidInternals.TryParse(P_0, out tuple);
		P_1 = T.Create(tuple);
		return result;
	}

	public override string ToString()
	{
		return RootGuidInternals.ToString(this);
	}

	public override int GetHashCode()
	{
		return GetStableHashCode();
	}

	public int GetStableHashCode()
	{
		return (int)(Low64 & 0xFFFFFFFFu);
	}

	public override bool Equals(object? P_0)
	{
		if (!(P_0 is RootGuid rootGuid) || 1 == 0)
		{
			return false;
		}
		return Equals(rootGuid);
	}

	[return: NotNullIfNotNull("rootGuid")]
	public static implicit operator string?(RootGuid? P_0)
	{
		return P_0?.ToString();
	}

	public static RootGuid operator ^(RootGuid P_0, RootGuid P_1)
	{
		return new RootGuid(P_0.High64 ^ P_1.High64, P_0.Low64 ^ P_1.Low64);
	}

	public static bool operator ==(RootGuid? P_0, RootGuid? P_1)
	{
		if (!P_0.HasValue && !P_1.HasValue)
		{
			return true;
		}
		if (!P_0.HasValue || !P_1.HasValue)
		{
			return false;
		}
		return P_0.Value.Equals(P_1.Value);
	}

	public static bool operator !=(RootGuid? P_0, RootGuid? P_1)
	{
		return !(P_0 == P_1);
	}

	public static bool operator >(RootGuid P_0, RootGuid P_1)
	{
		return P_0.ToDateTime() > P_1.ToDateTime();
	}

	public static bool operator <(RootGuid P_0, RootGuid P_1)
	{
		return P_0.ToDateTime() < P_1.ToDateTime();
	}

	public static bool operator >=(RootGuid P_0, RootGuid P_1)
	{
		return P_0.ToDateTime() >= P_1.ToDateTime();
	}

	public static bool operator <=(RootGuid P_0, RootGuid P_1)
	{
		return P_0.ToDateTime() <= P_1.ToDateTime();
	}

	public static implicit operator Guid(RootGuid P_0)
	{
		return P_0.ToGuid();
	}

	public static implicit operator RootGuid(Guid P_0)
	{
		return new RootGuid(RootGuidInternals.ConvertFromGuid(P_0));
	}

	public static implicit operator byte[](RootGuid P_0)
	{
		return P_0.ToByteArray();
	}

	public static implicit operator byte[]?(RootGuid? P_0)
	{
		return null;
	}

	public static implicit operator DateTime(RootGuid P_0)
	{
		return P_0.ToDateTime();
	}

	public static implicit operator DateTimeOffset(RootGuid P_0)
	{
		return P_0.ToDateTimeOffset();
	}

	public static implicit operator RootGuidType(RootGuid P_0)
	{
		return P_0.ToRootGuidType();
	}

	public static RootGuid Create(ReadOnlySpan<byte> P_0)
	{
		return new RootGuid(P_0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static RootGuid Create((ulong High64, ulong Low64) P_0)
	{
		return new RootGuid(P_0);
	}

	public static RootGuid Create(Guid P_0)
	{
		return new RootGuid(RootGuidInternals.ConvertFromGuid(P_0));
	}
}
