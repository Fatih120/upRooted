using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectMessageFindResponse : IMessage<DirectMessageFindResponse>, IMessage, IEquatable<DirectMessageFindResponse>, IDeepCloneable<DirectMessageFindResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageFindResponse> _parser = new MessageParser<DirectMessageFindResponse>(() => new DirectMessageFindResponse());

	private UnknownFieldSet _unknownFields;

	private DirectMessageUuid id_;

	private Timestamp userLastViewedAt_;

	private UserUuid creatorUserId_;

	private static readonly FieldCodec<UserUuid> _repeated_memberUserIds_codec = FieldCodec.ForMessage(106u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> memberUserIds_ = new RepeatedField<UserUuid>();

	private MessagePacket lastMessage_;

	private static readonly FieldCodec<WebRtcUserDevicePacket> _repeated_webRtcMembers_codec = FieldCodec.ForMessage(122u, WebRtcUserDevicePacket.Parser);

	private readonly RepeatedField<WebRtcUserDevicePacket> webRtcMembers_ = new RepeatedField<WebRtcUserDevicePacket>();

	private Timestamp lastActivityAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageFindResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public DirectMessageUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp UserLastViewedAt
	{
		get
		{
			return userLastViewedAt_;
		}
		set
		{
			userLastViewedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid CreatorUserId
	{
		get
		{
			return creatorUserId_;
		}
		set
		{
			creatorUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> MemberUserIds => memberUserIds_;

	[GeneratedCode("protoc", null)]
	public MessagePacket LastMessage
	{
		get
		{
			return lastMessage_;
		}
		set
		{
			lastMessage_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcUserDevicePacket> WebRtcMembers => webRtcMembers_;

	[GeneratedCode("protoc", null)]
	public Timestamp LastActivityAt
	{
		get
		{
			return lastActivityAt_;
		}
		set
		{
			lastActivityAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageFindResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageFindResponse(DirectMessageFindResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		userLastViewedAt_ = ((other.userLastViewedAt_ != null) ? other.userLastViewedAt_.Clone() : null);
		creatorUserId_ = ((other.creatorUserId_ != null) ? other.creatorUserId_.Clone() : null);
		memberUserIds_ = other.memberUserIds_.Clone();
		lastMessage_ = ((other.lastMessage_ != null) ? other.lastMessage_.Clone() : null);
		webRtcMembers_ = other.webRtcMembers_.Clone();
		lastActivityAt_ = ((other.lastActivityAt_ != null) ? other.lastActivityAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageFindResponse Clone()
	{
		return new DirectMessageFindResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageFindResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageFindResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(UserLastViewedAt, other.UserLastViewedAt))
		{
			return false;
		}
		if (!object.Equals(CreatorUserId, other.CreatorUserId))
		{
			return false;
		}
		if (!memberUserIds_.Equals(other.memberUserIds_))
		{
			return false;
		}
		if (!object.Equals(LastMessage, other.LastMessage))
		{
			return false;
		}
		if (!webRtcMembers_.Equals(other.webRtcMembers_))
		{
			return false;
		}
		if (!object.Equals(LastActivityAt, other.LastActivityAt))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (userLastViewedAt_ != null)
		{
			num ^= UserLastViewedAt.GetHashCode();
		}
		if (creatorUserId_ != null)
		{
			num ^= CreatorUserId.GetHashCode();
		}
		num ^= memberUserIds_.GetHashCode();
		if (lastMessage_ != null)
		{
			num ^= LastMessage.GetHashCode();
		}
		num ^= webRtcMembers_.GetHashCode();
		if (lastActivityAt_ != null)
		{
			num ^= LastActivityAt.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Id);
		}
		if (userLastViewedAt_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(UserLastViewedAt);
		}
		if (creatorUserId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(CreatorUserId);
		}
		memberUserIds_.WriteTo(ref P_0, _repeated_memberUserIds_codec);
		if (lastMessage_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(LastMessage);
		}
		webRtcMembers_.WriteTo(ref P_0, _repeated_webRtcMembers_codec);
		if (lastActivityAt_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(LastActivityAt);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (userLastViewedAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserLastViewedAt);
		}
		if (creatorUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreatorUserId);
		}
		num += memberUserIds_.CalculateSize(_repeated_memberUserIds_codec);
		if (lastMessage_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LastMessage);
		}
		num += webRtcMembers_.CalculateSize(_repeated_webRtcMembers_codec);
		if (lastActivityAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(LastActivityAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageFindResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new DirectMessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.userLastViewedAt_ != null)
		{
			if (userLastViewedAt_ == null)
			{
				UserLastViewedAt = new Timestamp();
			}
			UserLastViewedAt.MergeFrom(other.UserLastViewedAt);
		}
		if (other.creatorUserId_ != null)
		{
			if (creatorUserId_ == null)
			{
				CreatorUserId = new UserUuid();
			}
			CreatorUserId.MergeFrom(other.CreatorUserId);
		}
		memberUserIds_.Add(other.memberUserIds_);
		if (other.lastMessage_ != null)
		{
			if (lastMessage_ == null)
			{
				LastMessage = new MessagePacket();
			}
			LastMessage.MergeFrom(other.LastMessage);
		}
		webRtcMembers_.Add(other.webRtcMembers_);
		if (other.lastActivityAt_ != null)
		{
			if (lastActivityAt_ == null)
			{
				LastActivityAt = new Timestamp();
			}
			LastActivityAt.MergeFrom(other.LastActivityAt);
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
				if (id_ == null)
				{
					Id = new DirectMessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				if (userLastViewedAt_ == null)
				{
					UserLastViewedAt = new Timestamp();
				}
				P_0.ReadMessage(UserLastViewedAt);
				break;
			case 98u:
				if (creatorUserId_ == null)
				{
					CreatorUserId = new UserUuid();
				}
				P_0.ReadMessage(CreatorUserId);
				break;
			case 106u:
				memberUserIds_.AddEntriesFrom(ref P_0, _repeated_memberUserIds_codec);
				break;
			case 114u:
				if (lastMessage_ == null)
				{
					LastMessage = new MessagePacket();
				}
				P_0.ReadMessage(LastMessage);
				break;
			case 122u:
				webRtcMembers_.AddEntriesFrom(ref P_0, _repeated_webRtcMembers_codec);
				break;
			case 130u:
				if (lastActivityAt_ == null)
				{
					LastActivityAt = new Timestamp();
				}
				P_0.ReadMessage(LastActivityAt);
				break;
			}
		}
	}
}
