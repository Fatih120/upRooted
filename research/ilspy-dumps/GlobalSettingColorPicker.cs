using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingColorPicker : IMessage<GlobalSettingColorPicker>, IMessage, IEquatable<GlobalSettingColorPicker>, IDeepCloneable<GlobalSettingColorPicker>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingColorPicker> _parser = new MessageParser<GlobalSettingColorPicker>(() => new GlobalSettingColorPicker());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<string> _single_value_codec = FieldCodec.ForClassWrapper<string>(42u);

	private string value_;

	private static readonly FieldCodec<string> _single_defaultValue_codec = FieldCodec.ForClassWrapper<string>(50u);

	private string defaultValue_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingColorPicker> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[12];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Value
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
	public string DefaultValue
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
	public GlobalSettingColorPicker()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingColorPicker(GlobalSettingColorPicker other)
		: this()
	{
		Value = other.Value;
		DefaultValue = other.DefaultValue;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingColorPicker Clone()
	{
		return new GlobalSettingColorPicker(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingColorPicker);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingColorPicker other)
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
			_single_value_codec.WriteTagAndValue(ref P_0, Value);
		}
		if (defaultValue_ != null)
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
		if (value_ != null)
		{
			num += _single_value_codec.CalculateSizeWithTag(Value);
		}
		if (defaultValue_ != null)
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
	public void MergeFrom(GlobalSettingColorPicker other)
	{
		if (other != null)
		{
			if (other.value_ != null && (value_ == null || other.Value != ""))
			{
				Value = other.Value;
			}
			if (other.defaultValue_ != null && (defaultValue_ == null || other.DefaultValue != ""))
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
			case 42u:
			{
				string text2 = _single_value_codec.Read(ref P_0);
				if (value_ == null || text2 != "")
				{
					Value = text2;
				}
				break;
			}
			case 50u:
			{
				string text = _single_defaultValue_codec.Read(ref P_0);
				if (defaultValue_ == null || text != "")
				{
					DefaultValue = text;
				}
				break;
			}
			}
		}
	}
}
