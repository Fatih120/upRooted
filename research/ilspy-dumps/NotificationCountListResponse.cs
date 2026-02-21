using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class NotificationCountListResponse : IMessage<NotificationCountListResponse>, IMessage, IEquatable<NotificationCountListResponse>, IDeepCloneable<NotificationCountListResponse>, IBufferMessage
{
	private static readonly MessageParser<NotificationCountListResponse> _parser = new MessageParser<NotificationCountListResponse>(() => new NotificationCountListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<NotificationCountResponse> _repeated_notificationCounts_codec = FieldCodec.ForMessage(82u, NotificationCountResponse.Parser);

	private readonly RepeatedField<NotificationCountResponse> notificationCounts_ = new RepeatedField<NotificationCountResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationCountListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<NotificationCountResponse> NotificationCounts => notificationCounts_;

	[GeneratedCode("protoc", null)]
	public NotificationCountListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationCountListResponse(NotificationCountListResponse other)
		: this()
	{
		notificationCounts_ = other.notificationCounts_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationCountListResponse Clone()
	{
		return new NotificationCountListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationCountListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationCountListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!notificationCounts_.Equals(other.notificationCounts_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= notificationCounts_.GetHashCode();
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
		notificationCounts_.WriteTo(ref P_0, _repeated_notificationCounts_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += notificationCounts_.CalculateSize(_repeated_notificationCounts_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationCountListResponse other)
	{
		if (other != null)
		{
			notificationCounts_.Add(other.notificationCounts_);
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
				notificationCounts_.AddEntriesFrom(ref P_0, _repeated_notificationCounts_codec);
			}
		}
	}
}
