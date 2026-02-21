using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class NotificationCountResponse : IMessage<NotificationCountResponse>, IMessage, IEquatable<NotificationCountResponse>, IDeepCloneable<NotificationCountResponse>, IBufferMessage
{
	private static readonly MessageParser<NotificationCountResponse> _parser = new MessageParser<NotificationCountResponse>(() => new NotificationCountResponse());

	private UnknownFieldSet _unknownFields;

	private RootUuid containerId_;

	private int count_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationCountResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootUuid ContainerId
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
	public int Count
	{
		get
		{
			return count_;
		}
		set
		{
			count_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationCountResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationCountResponse(NotificationCountResponse other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		count_ = other.count_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationCountResponse Clone()
	{
		return new NotificationCountResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationCountResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationCountResponse other)
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
		if (Count != other.Count)
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
		if (Count != 0)
		{
			num ^= Count.GetHashCode();
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
		if (Count != 0)
		{
			P_0.WriteRawTag(88);
			P_0.WriteInt32(Count);
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
		if (Count != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Count);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationCountResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new RootUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.Count != 0)
		{
			Count = other.Count;
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
					ContainerId = new RootUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 88u:
				Count = P_0.ReadInt32();
				break;
			}
		}
	}
}
