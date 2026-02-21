using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class FriendshipInviteRespondRequest : IMessage<FriendshipInviteRespondRequest>, IMessage, IEquatable<FriendshipInviteRespondRequest>, IDeepCloneable<FriendshipInviteRespondRequest>, IBufferMessage
{
	private static readonly MessageParser<FriendshipInviteRespondRequest> _parser = new MessageParser<FriendshipInviteRespondRequest>(() => new FriendshipInviteRespondRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private NotificationUuid notificationId_;

	private UserUuid friendUserId_;

	private bool isFriendshipAccepted_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipInviteRespondRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[8];

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
	public NotificationUuid NotificationId
	{
		get
		{
			return notificationId_;
		}
		set
		{
			notificationId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid FriendUserId
	{
		get
		{
			return friendUserId_;
		}
		set
		{
			friendUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsFriendshipAccepted
	{
		get
		{
			return isFriendshipAccepted_;
		}
		set
		{
			isFriendshipAccepted_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipInviteRespondRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipInviteRespondRequest(FriendshipInviteRespondRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		notificationId_ = ((other.notificationId_ != null) ? other.notificationId_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		isFriendshipAccepted_ = other.isFriendshipAccepted_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipInviteRespondRequest Clone()
	{
		return new FriendshipInviteRespondRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipInviteRespondRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipInviteRespondRequest other)
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
		if (!object.Equals(NotificationId, other.NotificationId))
		{
			return false;
		}
		if (!object.Equals(FriendUserId, other.FriendUserId))
		{
			return false;
		}
		if (IsFriendshipAccepted != other.IsFriendshipAccepted)
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
		if (notificationId_ != null)
		{
			num ^= NotificationId.GetHashCode();
		}
		if (friendUserId_ != null)
		{
			num ^= FriendUserId.GetHashCode();
		}
		if (IsFriendshipAccepted)
		{
			num ^= IsFriendshipAccepted.GetHashCode();
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
		if (notificationId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(NotificationId);
		}
		if (friendUserId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(FriendUserId);
		}
		if (IsFriendshipAccepted)
		{
			P_0.WriteRawTag(96);
			P_0.WriteBool(IsFriendshipAccepted);
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
		if (notificationId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(NotificationId);
		}
		if (friendUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendUserId);
		}
		if (IsFriendshipAccepted)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipInviteRespondRequest other)
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
		if (other.notificationId_ != null)
		{
			if (notificationId_ == null)
			{
				NotificationId = new NotificationUuid();
			}
			NotificationId.MergeFrom(other.NotificationId);
		}
		if (other.friendUserId_ != null)
		{
			if (friendUserId_ == null)
			{
				FriendUserId = new UserUuid();
			}
			FriendUserId.MergeFrom(other.FriendUserId);
		}
		if (other.IsFriendshipAccepted)
		{
			IsFriendshipAccepted = other.IsFriendshipAccepted;
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
				if (notificationId_ == null)
				{
					NotificationId = new NotificationUuid();
				}
				P_0.ReadMessage(NotificationId);
				break;
			case 90u:
				if (friendUserId_ == null)
				{
					FriendUserId = new UserUuid();
				}
				P_0.ReadMessage(FriendUserId);
				break;
			case 96u:
				IsFriendshipAccepted = P_0.ReadBool();
				break;
			}
		}
	}
}
