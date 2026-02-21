using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingSelectOption : IMessage<GlobalSettingSelectOption>, IMessage, IEquatable<GlobalSettingSelectOption>, IDeepCloneable<GlobalSettingSelectOption>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingSelectOption> _parser = new MessageParser<GlobalSettingSelectOption>(() => new GlobalSettingSelectOption());

	private UnknownFieldSet _unknownFields;

	private string key_ = "";

	private string value_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingSelectOption> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[16];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Key
	{
		get
		{
			return key_;
		}
		set
		{
			key_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Value
	{
		get
		{
			return value_;
		}
		set
		{
			value_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelectOption()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelectOption(GlobalSettingSelectOption other)
		: this()
	{
		key_ = other.key_;
		value_ = other.value_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelectOption Clone()
	{
		return new GlobalSettingSelectOption(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingSelectOption);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingSelectOption other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Key != other.Key)
		{
			return false;
		}
		if (Value != other.Value)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Key.Length != 0)
		{
			num ^= Key.GetHashCode();
		}
		if (Value.Length != 0)
		{
			num ^= Value.GetHashCode();
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
		if (Key.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Key);
		}
		if (Value.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Value);
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
		if (Key.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Key);
		}
		if (Value.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Value);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingSelectOption other)
	{
		if (other != null)
		{
			if (other.Key.Length != 0)
			{
				Key = other.Key;
			}
			if (other.Value.Length != 0)
			{
				Value = other.Value;
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
				Key = P_0.ReadString();
				break;
			case 50u:
				Value = P_0.ReadString();
				break;
			}
		}
	}
}
