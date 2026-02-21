using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadJoinThrottle : IMessage<CommunityLogPayloadJoinThrottle>, IMessage, IEquatable<CommunityLogPayloadJoinThrottle>, IDeepCloneable<CommunityLogPayloadJoinThrottle>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadJoinThrottle> _parser = new MessageParser<CommunityLogPayloadJoinThrottle>(() => new CommunityLogPayloadJoinThrottle());

	private UnknownFieldSet _unknownFields;

	private int refillCount_;

	private int windowInMinutes_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadJoinThrottle> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[17];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public int RefillCount
	{
		get
		{
			return refillCount_;
		}
		set
		{
			refillCount_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int WindowInMinutes
	{
		get
		{
			return windowInMinutes_;
		}
		set
		{
			windowInMinutes_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadJoinThrottle()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadJoinThrottle(CommunityLogPayloadJoinThrottle other)
		: this()
	{
		refillCount_ = other.refillCount_;
		windowInMinutes_ = other.windowInMinutes_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadJoinThrottle Clone()
	{
		return new CommunityLogPayloadJoinThrottle(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadJoinThrottle);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadJoinThrottle other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (RefillCount != other.RefillCount)
		{
			return false;
		}
		if (WindowInMinutes != other.WindowInMinutes)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (RefillCount != 0)
		{
			num ^= RefillCount.GetHashCode();
		}
		if (WindowInMinutes != 0)
		{
			num ^= WindowInMinutes.GetHashCode();
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
		if (RefillCount != 0)
		{
			P_0.WriteRawTag(40);
			P_0.WriteInt32(RefillCount);
		}
		if (WindowInMinutes != 0)
		{
			P_0.WriteRawTag(48);
			P_0.WriteInt32(WindowInMinutes);
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
		if (RefillCount != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(RefillCount);
		}
		if (WindowInMinutes != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(WindowInMinutes);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadJoinThrottle other)
	{
		if (other != null)
		{
			if (other.RefillCount != 0)
			{
				RefillCount = other.RefillCount;
			}
			if (other.WindowInMinutes != 0)
			{
				WindowInMinutes = other.WindowInMinutes;
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
				RefillCount = P_0.ReadInt32();
				break;
			case 48u:
				WindowInMinutes = P_0.ReadInt32();
				break;
			}
		}
	}
}
