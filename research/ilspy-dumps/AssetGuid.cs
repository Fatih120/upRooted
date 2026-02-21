using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using RootApp.Core.Converters;
using RootApp.Core.Enums;

namespace RootApp.Core.Identifiers;

[TypeConverter(typeof(RootGuidConverter<AssetGuid>))]
[JsonConverter(typeof(JsonRootGuidConverter<AssetGuid>))]
public readonly struct AssetGuid : IRootGuid<AssetGuid>, IRootGuid, IComparable, IComparable<AssetGuid>, IEquatable<AssetGuid>, IFormattable, IParsable<AssetGuid>
{
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

	public AssetGuid()
		: this(RootGuidInternals.FromRootGuidType(RootGuidType.Asset))
	{
	}

	public AssetGuid(ulong P_0, ulong P_1)
	{
		High64 = P_0;
		Low64 = P_1;
	}

	public AssetGuid((ulong High64, ulong Low64) P_0)
		: this(P_0.High64, P_0.Low64)
	{
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public (ulong High64, ulong Low64) GetValue()
	{
		return (High64: High64, Low64: Low64);
	}

	public bool HasValue()
	{
		return RootGuidInternals.ConvertToRootGuidType(High64) == RootGuidType.Asset;
	}

	public int CompareTo(object? P_0)
	{
		return RootGuidInternals.CompareTo(this, P_0);
	}

	public int CompareTo(AssetGuid P_0)
	{
		return RootGuidInternals.CompareToGuid(this, P_0);
	}

	public bool Equals(AssetGuid P_0)
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

	[return: NotNullIfNotNull("value")]
	public static implicit operator RootGuid?(AssetGuid? P_0)
	{
		if (!P_0.HasValue)
		{
			return null;
		}
		return new RootGuid(P_0.Value.GetValue());
	}

	public static implicit operator RootGuid(AssetGuid P_0)
	{
		return new RootGuid(P_0.GetValue());
	}

	[return: NotNullIfNotNull("value")]
	public static explicit operator AssetGuid?(RootGuid? P_0)
	{
		if (!P_0.HasValue)
		{
			return null;
		}
		return new AssetGuid(P_0.Value.GetValue());
	}

	public static explicit operator AssetGuid(RootGuid P_0)
	{
		return new AssetGuid(P_0.GetValue());
	}

	public static AssetGuid Parse(string P_0, IFormatProvider? P_1)
	{
		return RootGuid.Parse<AssetGuid>(P_0.AsSpan());
	}

	public static bool TryParse(string? P_0, IFormatProvider? P_1, out AssetGuid P_2)
	{
		return RootGuid.TryParse<AssetGuid>(P_0, out P_2);
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
		if (!(P_0 is AssetGuid assetGuid) || 1 == 0)
		{
			return false;
		}
		return High64 == assetGuid.High64 && Low64 == assetGuid.Low64;
	}

	[return: NotNullIfNotNull("guid")]
	public static implicit operator string?(AssetGuid? P_0)
	{
		return P_0?.ToString();
	}

	public static AssetGuid operator ^(AssetGuid P_0, AssetGuid P_1)
	{
		return new AssetGuid(P_0.High64 ^ P_1.High64, P_0.Low64 ^ P_1.Low64);
	}

	public static bool operator ==(AssetGuid? P_0, AssetGuid? P_1)
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

	public static bool operator !=(AssetGuid? P_0, AssetGuid? P_1)
	{
		return !(P_0 == P_1);
	}

	public static bool operator >(AssetGuid P_0, AssetGuid P_1)
	{
		return P_0.ToDateTime() > P_1.ToDateTime();
	}

	public static bool operator <(AssetGuid P_0, AssetGuid P_1)
	{
		return P_0.ToDateTime() < P_1.ToDateTime();
	}

	public static bool operator >=(AssetGuid P_0, AssetGuid P_1)
	{
		return P_0.ToDateTime() >= P_1.ToDateTime();
	}

	public static bool operator <=(AssetGuid P_0, AssetGuid P_1)
	{
		return P_0.ToDateTime() <= P_1.ToDateTime();
	}

	public static implicit operator Guid(AssetGuid P_0)
	{
		return P_0.ToGuid();
	}

	public static implicit operator AssetGuid(Guid P_0)
	{
		return new AssetGuid(RootGuidInternals.ConvertFromGuid(P_0));
	}

	public static implicit operator byte[](AssetGuid P_0)
	{
		return P_0.ToByteArray();
	}

	public static implicit operator byte[]?(AssetGuid? P_0)
	{
		return null;
	}

	public static implicit operator DateTime(AssetGuid P_0)
	{
		return P_0.ToDateTime();
	}

	public static implicit operator DateTimeOffset(AssetGuid P_0)
	{
		return P_0.ToDateTimeOffset();
	}

	public static implicit operator RootGuidType(AssetGuid P_0)
	{
		return P_0.ToRootGuidType();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static AssetGuid Create((ulong High64, ulong Low64) P_0)
	{
		return new AssetGuid(P_0);
	}

	public static AssetGuid Create(Guid P_0)
	{
		return new AssetGuid(RootGuidInternals.ConvertFromGuid(P_0));
	}
}
