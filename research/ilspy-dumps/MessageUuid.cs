using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;

namespace RootApp.Core;

public sealed class MessageUuid : IMessage<MessageUuid>, IMessage, IEquatable<MessageUuid>, IDeepCloneable<MessageUuid>, IBufferMessage, IRootUuid<MessageUuid>, IRootUuid, IComparable<MessageUuid>, ICustomDiagnosticMessage
{
	private static readonly MessageParser<MessageUuid> _parser = new MessageParser<MessageUuid>(() => new MessageUuid());

	private UnknownFieldSet _unknownFields;

	private ulong high64_;

	private ulong low64_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageUuid> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootUuidsReflection.Descriptor.MessageTypes[32];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ulong High64
	{
		get
		{
			return high64_;
		}
		set
		{
			high64_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ulong Low64
	{
		get
		{
			return low64_;
		}
		set
		{
			low64_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageUuid()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageUuid(MessageUuid other)
		: this()
	{
		high64_ = other.high64_;
		low64_ = other.low64_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageUuid Clone()
	{
		return new MessageUuid(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageUuid);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageUuid other)
	{
		if ((object)other == null)
		{
			return false;
		}
		if ((object)other == this)
		{
			return true;
		}
		if (High64 != other.High64)
		{
			return false;
		}
		if (Low64 != other.Low64)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (High64 != 0)
		{
			num ^= High64.GetHashCode();
		}
		if (Low64 != 0)
		{
			num ^= Low64.GetHashCode();
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
		if (High64 != 0)
		{
			P_0.WriteRawTag(9);
			P_0.WriteFixed64(High64);
		}
		if (Low64 != 0)
		{
			P_0.WriteRawTag(17);
			P_0.WriteFixed64(Low64);
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
		if (High64 != 0)
		{
			num += 9;
		}
		if (Low64 != 0)
		{
			num += 9;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageUuid other)
	{
		if (!(other == null))
		{
			if (other.High64 != 0)
			{
				High64 = other.High64;
			}
			if (other.Low64 != 0)
			{
				Low64 = other.Low64;
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
			case 9u:
				High64 = P_0.ReadFixed64();
				break;
			case 17u:
				Low64 = P_0.ReadFixed64();
				break;
			}
		}
	}

	public string ToDiagnosticString()
	{
		return this.ToGuid().ToString();
	}

	public bool IsValid()
	{
		return (High64 == 0L && Low64 == 0L) || (High64 == ulong.MaxValue && Low64 == ulong.MaxValue) || HasValue();
	}

	public bool HasValue()
	{
		return RootGuidInternals.ConvertToRootGuidType(High64) == RootGuidType.Message;
	}

	public int GetStableHashCode()
	{
		return (int)(Low64 & 0xFFFFFFFFu);
	}

	public static bool operator ==(MessageUuid? lhs, MessageUuid? rhs)
	{
		if ((object)lhs == null)
		{
			if ((object)rhs == null)
			{
				return true;
			}
			return false;
		}
		return lhs.Equals(rhs);
	}

	public static bool operator !=(MessageUuid? lhs, MessageUuid? rhs)
	{
		return !(lhs == rhs);
	}

	public int CompareTo(MessageUuid? other)
	{
		if ((object)other == null)
		{
			return 1;
		}
		int num = High64.CompareTo(other.High64);
		if (num != 0)
		{
			return num;
		}
		return Low64.CompareTo(other.Low64);
	}

	public static bool operator <(MessageUuid? lhs, MessageUuid? rhs)
	{
		if ((object)lhs == null)
		{
			return (object)rhs != null;
		}
		return lhs.CompareTo(rhs) < 0;
	}

	public static bool operator >(MessageUuid? lhs, MessageUuid? rhs)
	{
		if ((object)lhs == null)
		{
			return (object)rhs != null;
		}
		return lhs.CompareTo(rhs) > 0;
	}

	public static bool operator <=(MessageUuid? lhs, MessageUuid? rhs)
	{
		if ((object)lhs == null)
		{
			return (object)rhs != null;
		}
		return lhs.CompareTo(rhs) <= 0;
	}

	public static bool operator >=(MessageUuid? lhs, MessageUuid? rhs)
	{
		if ((object)lhs == null)
		{
			return (object)rhs != null;
		}
		return lhs.CompareTo(rhs) >= 0;
	}

	[return: NotNullIfNotNull("value")]
	public static implicit operator MessageGuid?(MessageUuid? value)
	{
		if ((object)value == null)
		{
			return null;
		}
		return new MessageGuid((High64: value.High64, Low64: value.Low64));
	}

	public static implicit operator Guid(MessageUuid value)
	{
		return value.ToGuid();
	}

	public static implicit operator MessageGuid(MessageUuid value)
	{
		return new MessageGuid((High64: value.High64, Low64: value.Low64));
	}

	[return: NotNullIfNotNull("value")]
	public static implicit operator RootGuid?(MessageUuid? value)
	{
		if ((object)value == null)
		{
			return null;
		}
		return new RootGuid((High64: value.High64, Low64: value.Low64));
	}

	public static implicit operator RootGuid(MessageUuid value)
	{
		return new RootGuid((High64: value.High64, Low64: value.Low64));
	}

	[return: NotNullIfNotNull("value")]
	public static implicit operator MessageUuid?(MessageGuid? value)
	{
		if (!value.HasValue)
		{
			return null;
		}
		return Create(value.Value.GetValue());
	}

	public static implicit operator MessageUuid(MessageGuid value)
	{
		return Create(value.GetValue());
	}

	public static MessageUuid Create((ulong High64, ulong Low64) t)
	{
		MessageUuid messageUuid = new MessageUuid();
		(messageUuid.High64, messageUuid.Low64) = t;
		return messageUuid;
	}
}
