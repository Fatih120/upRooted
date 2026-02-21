using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingDate : IMessage<GlobalSettingDate>, IMessage, IEquatable<GlobalSettingDate>, IDeepCloneable<GlobalSettingDate>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingDate> _parser = new MessageParser<GlobalSettingDate>(() => new GlobalSettingDate());

	private UnknownFieldSet _unknownFields;

	private int year_;

	private int month_;

	private int day_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingDate> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public int Year
	{
		get
		{
			return year_;
		}
		set
		{
			year_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Month
	{
		get
		{
			return month_;
		}
		set
		{
			month_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Day
	{
		get
		{
			return day_;
		}
		set
		{
			day_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingDate()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingDate(GlobalSettingDate other)
		: this()
	{
		year_ = other.year_;
		month_ = other.month_;
		day_ = other.day_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingDate Clone()
	{
		return new GlobalSettingDate(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingDate);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingDate other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Year != other.Year)
		{
			return false;
		}
		if (Month != other.Month)
		{
			return false;
		}
		if (Day != other.Day)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Year != 0)
		{
			num ^= Year.GetHashCode();
		}
		if (Month != 0)
		{
			num ^= Month.GetHashCode();
		}
		if (Day != 0)
		{
			num ^= Day.GetHashCode();
		}
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext P_0)
	{
		if (Year != 0)
		{
			P_0.WriteRawTag(32);
			P_0.WriteInt32(Year);
		}
		if (Month != 0)
		{
			P_0.WriteRawTag(40);
			P_0.WriteInt32(Month);
		}
		if (Day != 0)
		{
			P_0.WriteRawTag(48);
			P_0.WriteInt32(Day);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (Year != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Year);
		}
		if (Month != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Month);
		}
		if (Day != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Day);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingDate other)
	{
		if (other != null)
		{
			if (other.Year != 0)
			{
				Year = other.Year;
			}
			if (other.Month != 0)
			{
				Month = other.Month;
			}
			if (other.Day != 0)
			{
				Day = other.Day;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext P_0)
	{
		uint num;
		while ((num = P_0.ReadTag()) != 0 && (num & 7) != 4)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 32u:
				Year = P_0.ReadInt32();
				break;
			case 40u:
				Month = P_0.ReadInt32();
				break;
			case 48u:
				Day = P_0.ReadInt32();
				break;
			}
		}
	}
}
