using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Core.Validate;

public sealed class FieldRules : IMessage<FieldRules>, IMessage, IEquatable<FieldRules>, IDeepCloneable<FieldRules>, IBufferMessage
{
	private static readonly MessageParser<FieldRules> _parser = new MessageParser<FieldRules>(() => new FieldRules());

	private UnknownFieldSet _unknownFields;

	private bool required_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FieldRules> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ValidateReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public bool Required
	{
		get
		{
			return required_;
		}
		set
		{
			required_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FieldRules()
	{
	}

	[GeneratedCode("protoc", null)]
	public FieldRules(FieldRules other)
		: this()
	{
		required_ = other.required_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FieldRules Clone()
	{
		return new FieldRules(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FieldRules);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FieldRules other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Required != other.Required)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Required)
		{
			num ^= Required.GetHashCode();
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
		if (Required)
		{
			P_0.WriteRawTag(200, 1);
			P_0.WriteBool(Required);
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
		if (Required)
		{
			num += 3;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FieldRules other)
	{
		if (other != null)
		{
			if (other.Required)
			{
				Required = other.Required;
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 200)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				Required = P_0.ReadBool();
			}
		}
	}
}
