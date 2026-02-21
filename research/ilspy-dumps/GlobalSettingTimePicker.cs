using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingTimePicker : IMessage<GlobalSettingTimePicker>, IMessage, IEquatable<GlobalSettingTimePicker>, IDeepCloneable<GlobalSettingTimePicker>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingTimePicker> _parser = new MessageParser<GlobalSettingTimePicker>(() => new GlobalSettingTimePicker());

	private UnknownFieldSet _unknownFields;

	private GlobalSettingTimeOfDay value_;

	private GlobalSettingTimeOfDay defaultValue_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingTimePicker> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[11];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimeOfDay Value
	{
		get
		{
			return value_;
		}
		set
		{
			value_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimeOfDay DefaultValue
	{
		get
		{
			return defaultValue_;
		}
		set
		{
			defaultValue_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimePicker()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimePicker(GlobalSettingTimePicker other)
		: this()
	{
		value_ = ((other.value_ != null) ? other.value_.Clone() : null);
		defaultValue_ = ((other.defaultValue_ != null) ? other.defaultValue_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimePicker Clone()
	{
		return new GlobalSettingTimePicker(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingTimePicker);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingTimePicker other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Value, other.Value))
		{
			return false;
		}
		if (!object.Equals(DefaultValue, other.DefaultValue))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (value_ != null)
		{
			num ^= Value.GetHashCode();
		}
		if (defaultValue_ != null)
		{
			num ^= DefaultValue.GetHashCode();
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
		if (value_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Value);
		}
		if (defaultValue_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(DefaultValue);
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
		if (value_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Value);
		}
		if (defaultValue_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DefaultValue);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingTimePicker other)
	{
		if (other == null)
		{
			return;
		}
		if (other.value_ != null)
		{
			if (value_ == null)
			{
				Value = new GlobalSettingTimeOfDay();
			}
			Value.MergeFrom(other.Value);
		}
		if (other.defaultValue_ != null)
		{
			if (defaultValue_ == null)
			{
				DefaultValue = new GlobalSettingTimeOfDay();
			}
			DefaultValue.MergeFrom(other.DefaultValue);
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			case 42u:
				if (value_ == null)
				{
					Value = new GlobalSettingTimeOfDay();
				}
				P_0.ReadMessage(Value);
				break;
			case 50u:
				if (defaultValue_ == null)
				{
					DefaultValue = new GlobalSettingTimeOfDay();
				}
				P_0.ReadMessage(DefaultValue);
				break;
			}
		}
	}
}
