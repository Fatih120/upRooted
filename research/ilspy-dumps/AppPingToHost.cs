using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Messaging.Grpc;

public sealed class AppPingToHost : IMessage<AppPingToHost>, IMessage, IEquatable<AppPingToHost>, IDeepCloneable<AppPingToHost>, IBufferMessage
{
	private static readonly MessageParser<AppPingToHost> _parser = new MessageParser<AppPingToHost>(() => new AppPingToHost());

	private UnknownFieldSet _unknownFields;

	private long sequenceNumber_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppPingToHost> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public long SequenceNumber
	{
		get
		{
			return sequenceNumber_;
		}
		set
		{
			sequenceNumber_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppPingToHost()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppPingToHost(AppPingToHost other)
		: this()
	{
		sequenceNumber_ = other.sequenceNumber_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppPingToHost Clone()
	{
		return new AppPingToHost(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppPingToHost);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppPingToHost other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SequenceNumber != other.SequenceNumber)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (SequenceNumber != 0)
		{
			num ^= SequenceNumber.GetHashCode();
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
		if (SequenceNumber != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt64(SequenceNumber);
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
		if (SequenceNumber != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(SequenceNumber);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppPingToHost other)
	{
		if (other != null)
		{
			if (other.SequenceNumber != 0)
			{
				SequenceNumber = other.SequenceNumber;
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
			if (num3 != 16)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				SequenceNumber = P_0.ReadInt64();
			}
		}
	}
}
