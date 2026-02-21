using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Core.Validate;

public sealed class StringRules : IMessage<StringRules>, IMessage, IEquatable<StringRules>, IDeepCloneable<StringRules>, IBufferMessage
{
	private static readonly MessageParser<StringRules> _parser = new MessageParser<StringRules>(() => new StringRules());

	private UnknownFieldSet _unknownFields;

	private ulong minLen_;

	private ulong maxLen_;

	private string pattern_ = "";

	private string custom_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<StringRules> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ValidateReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ulong MinLen
	{
		get
		{
			return minLen_;
		}
		set
		{
			minLen_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ulong MaxLen
	{
		get
		{
			return maxLen_;
		}
		set
		{
			maxLen_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Pattern
	{
		get
		{
			return pattern_;
		}
		set
		{
			pattern_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Custom
	{
		get
		{
			return custom_;
		}
		set
		{
			custom_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public StringRules()
	{
	}

	[GeneratedCode("protoc", null)]
	public StringRules(StringRules other)
		: this()
	{
		minLen_ = other.minLen_;
		maxLen_ = other.maxLen_;
		pattern_ = other.pattern_;
		custom_ = other.custom_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public StringRules Clone()
	{
		return new StringRules(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as StringRules);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(StringRules other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MinLen != other.MinLen)
		{
			return false;
		}
		if (MaxLen != other.MaxLen)
		{
			return false;
		}
		if (Pattern != other.Pattern)
		{
			return false;
		}
		if (Custom != other.Custom)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (MinLen != 0)
		{
			num ^= MinLen.GetHashCode();
		}
		if (MaxLen != 0)
		{
			num ^= MaxLen.GetHashCode();
		}
		if (Pattern.Length != 0)
		{
			num ^= Pattern.GetHashCode();
		}
		if (Custom.Length != 0)
		{
			num ^= Custom.GetHashCode();
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
		if (MinLen != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteUInt64(MinLen);
		}
		if (MaxLen != 0)
		{
			P_0.WriteRawTag(24);
			P_0.WriteUInt64(MaxLen);
		}
		if (Pattern.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(Pattern);
		}
		if (Custom.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Custom);
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
		if (MinLen != 0)
		{
			num += 1 + CodedOutputStream.ComputeUInt64Size(MinLen);
		}
		if (MaxLen != 0)
		{
			num += 1 + CodedOutputStream.ComputeUInt64Size(MaxLen);
		}
		if (Pattern.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Pattern);
		}
		if (Custom.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Custom);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(StringRules other)
	{
		if (other != null)
		{
			if (other.MinLen != 0)
			{
				MinLen = other.MinLen;
			}
			if (other.MaxLen != 0)
			{
				MaxLen = other.MaxLen;
			}
			if (other.Pattern.Length != 0)
			{
				Pattern = other.Pattern;
			}
			if (other.Custom.Length != 0)
			{
				Custom = other.Custom;
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
			case 16u:
				MinLen = P_0.ReadUInt64();
				break;
			case 24u:
				MaxLen = P_0.ReadUInt64();
				break;
			case 34u:
				Pattern = P_0.ReadString();
				break;
			case 42u:
				Custom = P_0.ReadString();
				break;
			}
		}
	}
}
