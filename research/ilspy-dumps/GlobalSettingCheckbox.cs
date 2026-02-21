using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingCheckbox : IMessage<GlobalSettingCheckbox>, IMessage, IEquatable<GlobalSettingCheckbox>, IDeepCloneable<GlobalSettingCheckbox>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingCheckbox> _parser = new MessageParser<GlobalSettingCheckbox>(() => new GlobalSettingCheckbox());

	private UnknownFieldSet _unknownFields;

	private bool value_;

	private static readonly FieldCodec<bool?> _single_defaultValue_codec = FieldCodec.ForStructWrapper<bool>(50u);

	private bool? defaultValue_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingCheckbox> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[14];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public bool Value
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
	public bool? DefaultValue
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
	public GlobalSettingCheckbox()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingCheckbox(GlobalSettingCheckbox other)
		: this()
	{
		value_ = other.value_;
		DefaultValue = other.DefaultValue;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingCheckbox Clone()
	{
		return new GlobalSettingCheckbox(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingCheckbox);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingCheckbox other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Value != other.Value)
		{
			return false;
		}
		if (DefaultValue != other.DefaultValue)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Value)
		{
			num ^= Value.GetHashCode();
		}
		if (defaultValue_.HasValue)
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
		if (Value)
		{
			P_0.WriteRawTag(40);
			P_0.WriteBool(Value);
		}
		if (defaultValue_.HasValue)
		{
			_single_defaultValue_codec.WriteTagAndValue(ref P_0, DefaultValue);
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
		if (Value)
		{
			num += 2;
		}
		if (defaultValue_.HasValue)
		{
			num += _single_defaultValue_codec.CalculateSizeWithTag(DefaultValue);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingCheckbox other)
	{
		if (other != null)
		{
			if (other.Value)
			{
				Value = other.Value;
			}
			if (other.defaultValue_.HasValue && (!defaultValue_.HasValue || other.DefaultValue != false))
			{
				DefaultValue = other.DefaultValue;
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
			case 40u:
				Value = P_0.ReadBool();
				break;
			case 50u:
			{
				bool? flag = _single_defaultValue_codec.Read(ref P_0);
				if (!defaultValue_.HasValue || flag != false)
				{
					DefaultValue = flag;
				}
				break;
			}
			}
		}
	}
}
