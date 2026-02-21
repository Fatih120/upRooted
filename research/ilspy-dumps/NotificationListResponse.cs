using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class NotificationListResponse : IMessage<NotificationListResponse>, IMessage, IEquatable<NotificationListResponse>, IDeepCloneable<NotificationListResponse>, IBufferMessage
{
	private static readonly MessageParser<NotificationListResponse> _parser = new MessageParser<NotificationListResponse>(() => new NotificationListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<NotificationPacket> _repeated_notifications_codec = FieldCodec.ForMessage(82u, NotificationPacket.Parser);

	private readonly RepeatedField<NotificationPacket> notifications_ = new RepeatedField<NotificationPacket>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<NotificationPacket> Notifications => notifications_;

	[GeneratedCode("protoc", null)]
	public NotificationListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationListResponse(NotificationListResponse other)
		: this()
	{
		notifications_ = other.notifications_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationListResponse Clone()
	{
		return new NotificationListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!notifications_.Equals(other.notifications_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= notifications_.GetHashCode();
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
		notifications_.WriteTo(ref P_0, _repeated_notifications_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += notifications_.CalculateSize(_repeated_notifications_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationListResponse other)
	{
		if (other != null)
		{
			notifications_.Add(other.notifications_);
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
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				notifications_.AddEntriesFrom(ref P_0, _repeated_notifications_codec);
			}
		}
	}
}
