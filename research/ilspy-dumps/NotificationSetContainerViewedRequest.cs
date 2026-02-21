using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class NotificationSetContainerViewedRequest : IMessage<NotificationSetContainerViewedRequest>, IMessage, IEquatable<NotificationSetContainerViewedRequest>, IDeepCloneable<NotificationSetContainerViewedRequest>, IBufferMessage
{
	private static readonly MessageParser<NotificationSetContainerViewedRequest> _parser = new MessageParser<NotificationSetContainerViewedRequest>(() => new NotificationSetContainerViewedRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private RootUuid containerId_;

	private RootUuid subContainerId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationSetContainerViewedRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

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
	public RootUuid SubContainerId
	{
		get
		{
			return subContainerId_;
		}
		set
		{
			subContainerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationSetContainerViewedRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationSetContainerViewedRequest(NotificationSetContainerViewedRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		subContainerId_ = ((other.subContainerId_ != null) ? other.subContainerId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationSetContainerViewedRequest Clone()
	{
		return new NotificationSetContainerViewedRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationSetContainerViewedRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationSetContainerViewedRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(SubContainerId, other.SubContainerId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (subContainerId_ != null)
		{
			num ^= SubContainerId.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(ContainerId);
		}
		if (subContainerId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(SubContainerId);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (subContainerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SubContainerId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationSetContainerViewedRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new RootUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.subContainerId_ != null)
		{
			if (subContainerId_ == null)
			{
				SubContainerId = new RootUuid();
			}
			SubContainerId.MergeFrom(other.SubContainerId);
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 82u:
				if (containerId_ == null)
				{
					ContainerId = new RootUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 90u:
				if (subContainerId_ == null)
				{
					SubContainerId = new RootUuid();
				}
				P_0.ReadMessage(SubContainerId);
				break;
			}
		}
	}
}
