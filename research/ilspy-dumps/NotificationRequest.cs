using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class NotificationRequest : IMessage<NotificationRequest>, IMessage, IEquatable<NotificationRequest>, IDeepCloneable<NotificationRequest>, IBufferMessage
{
	private static readonly MessageParser<NotificationRequest> _parser = new MessageParser<NotificationRequest>(() => new NotificationRequest());

	private UnknownFieldSet _unknownFields;

	private RootUuid before_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootUuid Before
	{
		get
		{
			return before_;
		}
		set
		{
			before_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationRequest(NotificationRequest other)
		: this()
	{
		before_ = ((other.before_ != null) ? other.before_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationRequest Clone()
	{
		return new NotificationRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Before, other.Before))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (before_ != null)
		{
			num ^= Before.GetHashCode();
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
		if (before_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Before);
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
		if (before_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Before);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.before_ != null)
		{
			if (before_ == null)
			{
				Before = new RootUuid();
			}
			Before.MergeFrom(other.Before);
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
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (before_ == null)
			{
				Before = new RootUuid();
			}
			P_0.ReadMessage(Before);
		}
	}
}
