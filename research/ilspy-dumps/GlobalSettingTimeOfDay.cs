using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingTimeOfDay : IMessage<GlobalSettingTimeOfDay>, IMessage, IEquatable<GlobalSettingTimeOfDay>, IDeepCloneable<GlobalSettingTimeOfDay>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingTimeOfDay> _parser = new MessageParser<GlobalSettingTimeOfDay>(() => new GlobalSettingTimeOfDay());

	private UnknownFieldSet _unknownFields;

	private int hours_;

	private int minutes_;

	private int seconds_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingTimeOfDay> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[10];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public int Hours
	{
		get
		{
			return hours_;
		}
		set
		{
			hours_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Minutes
	{
		get
		{
			return minutes_;
		}
		set
		{
			minutes_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Seconds
	{
		get
		{
			return seconds_;
		}
		set
		{
			seconds_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimeOfDay()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimeOfDay(GlobalSettingTimeOfDay other)
		: this()
	{
		hours_ = other.hours_;
		minutes_ = other.minutes_;
		seconds_ = other.seconds_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimeOfDay Clone()
	{
		return new GlobalSettingTimeOfDay(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingTimeOfDay);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingTimeOfDay other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Hours != other.Hours)
		{
			return false;
		}
		if (Minutes != other.Minutes)
		{
			return false;
		}
		if (Seconds != other.Seconds)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Hours != 0)
		{
			num ^= Hours.GetHashCode();
		}
		if (Minutes != 0)
		{
			num ^= Minutes.GetHashCode();
		}
		if (Seconds != 0)
		{
			num ^= Seconds.GetHashCode();
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
		if (Hours != 0)
		{
			P_0.WriteRawTag(8);
			P_0.WriteInt32(Hours);
		}
		if (Minutes != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt32(Minutes);
		}
		if (Seconds != 0)
		{
			P_0.WriteRawTag(24);
			P_0.WriteInt32(Seconds);
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
		if (Hours != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Hours);
		}
		if (Minutes != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Minutes);
		}
		if (Seconds != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Seconds);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingTimeOfDay other)
	{
		if (other != null)
		{
			if (other.Hours != 0)
			{
				Hours = other.Hours;
			}
			if (other.Minutes != 0)
			{
				Minutes = other.Minutes;
			}
			if (other.Seconds != 0)
			{
				Seconds = other.Seconds;
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
			case 8u:
				Hours = P_0.ReadInt32();
				break;
			case 16u:
				Minutes = P_0.ReadInt32();
				break;
			case 24u:
				Seconds = P_0.ReadInt32();
				break;
			}
		}
	}
}
