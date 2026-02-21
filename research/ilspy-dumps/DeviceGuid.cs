using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using RootApp.Core.Converters;
using RootApp.Core.Enums;

namespace RootApp.Core.Identifiers;

[TypeConverter(typeof(RootGuidConverter<DeviceGuid>))]
[JsonConverter(typeof(JsonRootGuidConverter<DeviceGuid>))]
public readonly struct DeviceGuid : IRootGuid<DeviceGuid>, IRootGuid, IComparable, IComparable<DeviceGuid>, IEquatable<DeviceGuid>, IFormattable, IParsable<DeviceGuid>
{
	public static readonly DeviceGuid Empty = new DeviceGuid(0uL, 0uL);

	public static readonly DeviceGuid Full = new DeviceGuid(ulong.MaxValue, ulong.MaxValue);

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

	public DeviceGuid(ulong P_0, ulong P_1)
	{
		High64 = P_0;
		Low64 = P_1;
	}

	public DeviceGuid((ulong High64, ulong Low64) P_0)
		: this(P_0.High64, P_0.Low64)
	{
	}

	public DeviceGuid(ReadOnlySpan<byte> P_0)
		: this(RootGuidInternals.FromReadOnlySpan(P_0))
	{
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public (ulong High64, ulong Low64) GetValue()
	{
		return (High64: High64, Low64: Low64);
	}

	public int CompareTo(object? P_0)
	{
		return RootGuidInternals.CompareTo(this, P_0);
	}

	public int CompareTo(DeviceGuid P_0)
	{
		return RootGuidInternals.CompareToGuid(this, P_0);
	}

	public bool Equals(DeviceGuid P_0)
	{
		return P_0.High64 == High64 && P_0.Low64 == Low64;
	}

	public string ToString(string? P_0, IFormatProvider? P_1)
	{
		return this.ToStringFormat(P_0);
	}

	public string ToString(string? P_0)
	{
		return this.ToStringFormat(P_0);
	}

	public byte[] ToByteArray()
	{
		byte[] array = new byte[16];
		this.ToBytes(array);
		return array;
	}

	[return: NotNullIfNotNull("value")]
	public static implicit operator RootGuid?(DeviceGuid? P_0)
	{
		if (!P_0.HasValue)
		{
			return null;
		}
		return new RootGuid(P_0.Value.GetValue());
	}

	public static implicit operator RootGuid(DeviceGuid P_0)
	{
		return new RootGuid(P_0.GetValue());
	}

	[return: NotNullIfNotNull("value")]
	public static explicit operator DeviceGuid?(RootGuid? P_0)
	{
		if (!P_0.HasValue)
		{
			return null;
		}
		return new DeviceGuid(P_0.Value.GetValue());
	}

	public static explicit operator DeviceGuid(RootGuid P_0)
	{
		return new DeviceGuid(P_0.GetValue());
	}

	public static DeviceGuid Parse(string P_0, IFormatProvider? P_1)
	{
		return RootGuid.Parse<DeviceGuid>(P_0.AsSpan());
	}

	public static bool TryParse(string? P_0, IFormatProvider? P_1, out DeviceGuid P_2)
	{
		return RootGuid.TryParse<DeviceGuid>(P_0, out P_2);
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
		if (!(P_0 is DeviceGuid deviceGuid) || 1 == 0)
		{
			return false;
		}
		return High64 == deviceGuid.High64 && Low64 == deviceGuid.Low64;
	}

	[return: NotNullIfNotNull("guid")]
	public static implicit operator string?(DeviceGuid? P_0)
	{
		return P_0?.ToString();
	}

	public static DeviceGuid operator ^(DeviceGuid P_0, DeviceGuid P_1)
	{
		return new DeviceGuid(P_0.High64 ^ P_1.High64, P_0.Low64 ^ P_1.Low64);
	}

	public static bool operator ==(DeviceGuid? P_0, DeviceGuid? P_1)
	{
		if (!P_0.HasValue && !P_1.HasValue)
		{
			return true;
		}
		if (!P_0.HasValue || !P_1.HasValue)
		{
			return false;
		}
		return P_0.Value.High64 == P_1.Value.High64 && P_0.Value.Low64 == P_1.Value.Low64;
	}

	public static bool operator !=(DeviceGuid? P_0, DeviceGuid? P_1)
	{
		return !(P_0 == P_1);
	}

	public static bool operator >(DeviceGuid P_0, DeviceGuid P_1)
	{
		return P_0.ToDateTime() > P_1.ToDateTime();
	}

	public static bool operator <(DeviceGuid P_0, DeviceGuid P_1)
	{
		return P_0.ToDateTime() < P_1.ToDateTime();
	}

	public static bool operator >=(DeviceGuid P_0, DeviceGuid P_1)
	{
		return P_0.ToDateTime() >= P_1.ToDateTime();
	}

	public static bool operator <=(DeviceGuid P_0, DeviceGuid P_1)
	{
		return P_0.ToDateTime() <= P_1.ToDateTime();
	}

	public static implicit operator Guid(DeviceGuid P_0)
	{
		return P_0.ToGuid();
	}

	public static implicit operator DeviceGuid(Guid P_0)
	{
		return new DeviceGuid(RootGuidInternals.ConvertFromGuid(P_0));
	}

	public static implicit operator byte[](DeviceGuid P_0)
	{
		return P_0.ToByteArray();
	}

	public static implicit operator byte[]?(DeviceGuid? P_0)
	{
		return null;
	}

	public static implicit operator DateTime(DeviceGuid P_0)
	{
		return P_0.ToDateTime();
	}

	public static implicit operator DateTimeOffset(DeviceGuid P_0)
	{
		return P_0.ToDateTimeOffset();
	}

	public static implicit operator RootGuidType(DeviceGuid P_0)
	{
		return P_0.ToRootGuidType();
	}

	public static DeviceGuid Create(ReadOnlySpan<byte> P_0)
	{
		return new DeviceGuid(P_0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DeviceGuid Create((ulong High64, ulong Low64) P_0)
	{
		return new DeviceGuid(P_0);
	}

	public static DeviceGuid Create(Guid P_0)
	{
		return new DeviceGuid(RootGuidInternals.ConvertFromGuid(P_0));
	}
}
