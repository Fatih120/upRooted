using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Messaging.Grpc;

public sealed class AppPacketToHost : IMessage<AppPacketToHost>, IMessage, IEquatable<AppPacketToHost>, IDeepCloneable<AppPacketToHost>, IBufferMessage
{
	private static readonly MessageParser<AppPacketToHost> _parser = new MessageParser<AppPacketToHost>(() => new AppPacketToHost());

	private UnknownFieldSet _unknownFields;

	private ByteString packet_ = ByteString.Empty;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppPacketToHost> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ByteString Packet
	{
		get
		{
			return packet_;
		}
		set
		{
			packet_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public AppPacketToHost()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppPacketToHost(AppPacketToHost other)
		: this()
	{
		packet_ = other.packet_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppPacketToHost Clone()
	{
		return new AppPacketToHost(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppPacketToHost);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppPacketToHost other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Packet != other.Packet)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Packet.Length != 0)
		{
			num ^= Packet.GetHashCode();
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
		if (Packet.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteBytes(Packet);
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
		if (Packet.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeBytesSize(Packet);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppPacketToHost other)
	{
		if (other != null)
		{
			if (other.Packet.Length != 0)
			{
				Packet = other.Packet;
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
			if (num3 != 10)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				Packet = P_0.ReadBytes();
			}
		}
	}
}
