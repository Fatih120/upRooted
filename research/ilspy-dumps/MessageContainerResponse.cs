using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class MessageContainerResponse : IMessage<MessageContainerResponse>, IMessage, IEquatable<MessageContainerResponse>, IDeepCloneable<MessageContainerResponse>, IBufferMessage
{
	private static readonly MessageParser<MessageContainerResponse> _parser = new MessageParser<MessageContainerResponse>(() => new MessageContainerResponse());

	private UnknownFieldSet _unknownFields;

	private MessageContainerUuid containerId_;

	private static readonly FieldCodec<MessagePacket> _repeated_messages_codec = FieldCodec.ForMessage(90u, MessagePacket.Parser);

	private readonly RepeatedField<MessagePacket> messages_ = new RepeatedField<MessagePacket>();

	private int oldCount_;

	private int newCount_;

	private MessageReferenceMaps referenceMaps_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageContainerResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[14];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageContainerUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessagePacket> Messages => messages_;

	[GeneratedCode("protoc", null)]
	public int OldCount
	{
		get
		{
			return oldCount_;
		}
		set
		{
			oldCount_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int NewCount
	{
		get
		{
			return newCount_;
		}
		set
		{
			newCount_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMaps ReferenceMaps
	{
		get
		{
			return referenceMaps_;
		}
		set
		{
			referenceMaps_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageContainerResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageContainerResponse(MessageContainerResponse other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		messages_ = other.messages_.Clone();
		oldCount_ = other.oldCount_;
		newCount_ = other.newCount_;
		referenceMaps_ = ((other.referenceMaps_ != null) ? other.referenceMaps_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageContainerResponse Clone()
	{
		return new MessageContainerResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageContainerResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageContainerResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!messages_.Equals(other.messages_))
		{
			return false;
		}
		if (OldCount != other.OldCount)
		{
			return false;
		}
		if (NewCount != other.NewCount)
		{
			return false;
		}
		if (!object.Equals(ReferenceMaps, other.ReferenceMaps))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		num ^= messages_.GetHashCode();
		if (OldCount != 0)
		{
			num ^= OldCount.GetHashCode();
		}
		if (NewCount != 0)
		{
			num ^= NewCount.GetHashCode();
		}
		if (referenceMaps_ != null)
		{
			num ^= ReferenceMaps.GetHashCode();
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
		if (containerId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(ContainerId);
		}
		messages_.WriteTo(ref P_0, _repeated_messages_codec);
		if (OldCount != 0)
		{
			P_0.WriteRawTag(96);
			P_0.WriteInt32(OldCount);
		}
		if (NewCount != 0)
		{
			P_0.WriteRawTag(104);
			P_0.WriteInt32(NewCount);
		}
		if (referenceMaps_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(ReferenceMaps);
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
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		num += messages_.CalculateSize(_repeated_messages_codec);
		if (OldCount != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(OldCount);
		}
		if (NewCount != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(NewCount);
		}
		if (referenceMaps_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ReferenceMaps);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageContainerResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		messages_.Add(other.messages_);
		if (other.OldCount != 0)
		{
			OldCount = other.OldCount;
		}
		if (other.NewCount != 0)
		{
			NewCount = other.NewCount;
		}
		if (other.referenceMaps_ != null)
		{
			if (referenceMaps_ == null)
			{
				ReferenceMaps = new MessageReferenceMaps();
			}
			ReferenceMaps.MergeFrom(other.ReferenceMaps);
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 82u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 90u:
				messages_.AddEntriesFrom(ref P_0, _repeated_messages_codec);
				break;
			case 96u:
				OldCount = P_0.ReadInt32();
				break;
			case 104u:
				NewCount = P_0.ReadInt32();
				break;
			case 114u:
				if (referenceMaps_ == null)
				{
					ReferenceMaps = new MessageReferenceMaps();
				}
				P_0.ReadMessage(ReferenceMaps);
				break;
			}
		}
	}
}
