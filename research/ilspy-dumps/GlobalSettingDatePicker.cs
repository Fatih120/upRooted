using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingDatePicker : IMessage<GlobalSettingDatePicker>, IMessage, IEquatable<GlobalSettingDatePicker>, IDeepCloneable<GlobalSettingDatePicker>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingDatePicker> _parser = new MessageParser<GlobalSettingDatePicker>(() => new GlobalSettingDatePicker());

	private UnknownFieldSet _unknownFields;

	private GlobalSettingDate value_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingDatePicker> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[9];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public GlobalSettingDate Value
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
	public GlobalSettingDatePicker()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingDatePicker(GlobalSettingDatePicker other)
		: this()
	{
		value_ = ((other.value_ != null) ? other.value_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingDatePicker Clone()
	{
		return new GlobalSettingDatePicker(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingDatePicker);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingDatePicker other)
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Value);
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingDatePicker other)
	{
		if (other == null)
		{
			return;
		}
		if (other.value_ != null)
		{
			if (value_ == null)
			{
				Value = new GlobalSettingDate();
			}
			Value.MergeFrom(other.Value);
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (value_ == null)
			{
				Value = new GlobalSettingDate();
			}
			P_0.ReadMessage(Value);
		}
	}
}
