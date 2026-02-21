using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.App.Messaging.Grpc;

public sealed class AppRpcMessageToClient : IMessage<AppRpcMessageToClient>, IMessage, IEquatable<AppRpcMessageToClient>, IDeepCloneable<AppRpcMessageToClient>, IBufferMessage
{
	private static readonly MessageParser<AppRpcMessageToClient> _parser = new MessageParser<AppRpcMessageToClient>(() => new AppRpcMessageToClient());

	private UnknownFieldSet _unknownFields;

	private long sequenceNumber_;

	private CommunityUuid communityId_;

	private AppUuid appId_;

	private CommunityAppUuid communityAppId_;

	private static readonly FieldCodec<DeviceUuid> _repeated_deviceIds_codec = FieldCodec.ForMessage(58u, DeviceUuid.Parser);

	private readonly RepeatedField<DeviceUuid> deviceIds_ = new RepeatedField<DeviceUuid>();

	private AppRpcMessageType messageType_ = AppRpcMessageType.Unspecified;

	private ByteString message_ = ByteString.Empty;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppRpcMessageToClient> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[4];

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
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppUuid AppId
	{
		get
		{
			return appId_;
		}
		set
		{
			appId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppUuid CommunityAppId
	{
		get
		{
			return communityAppId_;
		}
		set
		{
			communityAppId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<DeviceUuid> DeviceIds => deviceIds_;

	[GeneratedCode("protoc", null)]
	public AppRpcMessageType MessageType
	{
		get
		{
			return messageType_;
		}
		set
		{
			messageType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ByteString Message
	{
		get
		{
			return message_;
		}
		set
		{
			message_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessageToClient()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessageToClient(AppRpcMessageToClient other)
		: this()
	{
		sequenceNumber_ = other.sequenceNumber_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		deviceIds_ = other.deviceIds_.Clone();
		messageType_ = other.messageType_;
		message_ = other.message_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessageToClient Clone()
	{
		return new AppRpcMessageToClient(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppRpcMessageToClient);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppRpcMessageToClient other)
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
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(AppId, other.AppId))
		{
			return false;
		}
		if (!object.Equals(CommunityAppId, other.CommunityAppId))
		{
			return false;
		}
		if (!deviceIds_.Equals(other.deviceIds_))
		{
			return false;
		}
		if (MessageType != other.MessageType)
		{
			return false;
		}
		if (Message != other.Message)
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
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (appId_ != null)
		{
			num ^= AppId.GetHashCode();
		}
		if (communityAppId_ != null)
		{
			num ^= CommunityAppId.GetHashCode();
		}
		num ^= deviceIds_.GetHashCode();
		if (MessageType != AppRpcMessageType.Unspecified)
		{
			num ^= MessageType.GetHashCode();
		}
		if (Message.Length != 0)
		{
			num ^= Message.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityId);
		}
		if (appId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppId);
		}
		if (communityAppId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(CommunityAppId);
		}
		deviceIds_.WriteTo(ref P_0, _repeated_deviceIds_codec);
		if (MessageType != AppRpcMessageType.Unspecified)
		{
			P_0.WriteRawTag(64);
			P_0.WriteEnum((int)MessageType);
		}
		if (Message.Length != 0)
		{
			P_0.WriteRawTag(74);
			P_0.WriteBytes(Message);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (appId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppId);
		}
		if (communityAppId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityAppId);
		}
		num += deviceIds_.CalculateSize(_repeated_deviceIds_codec);
		if (MessageType != AppRpcMessageType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MessageType);
		}
		if (Message.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeBytesSize(Message);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppRpcMessageToClient other)
	{
		if (other == null)
		{
			return;
		}
		if (other.SequenceNumber != 0)
		{
			SequenceNumber = other.SequenceNumber;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.appId_ != null)
		{
			if (appId_ == null)
			{
				AppId = new AppUuid();
			}
			AppId.MergeFrom(other.AppId);
		}
		if (other.communityAppId_ != null)
		{
			if (communityAppId_ == null)
			{
				CommunityAppId = new CommunityAppUuid();
			}
			CommunityAppId.MergeFrom(other.CommunityAppId);
		}
		deviceIds_.Add(other.deviceIds_);
		if (other.MessageType != AppRpcMessageType.Unspecified)
		{
			MessageType = other.MessageType;
		}
		if (other.Message.Length != 0)
		{
			Message = other.Message;
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
			case 16u:
				SequenceNumber = P_0.ReadInt64();
				break;
			case 34u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 42u:
				if (appId_ == null)
				{
					AppId = new AppUuid();
				}
				P_0.ReadMessage(AppId);
				break;
			case 50u:
				if (communityAppId_ == null)
				{
					CommunityAppId = new CommunityAppUuid();
				}
				P_0.ReadMessage(CommunityAppId);
				break;
			case 58u:
				deviceIds_.AddEntriesFrom(ref P_0, _repeated_deviceIds_codec);
				break;
			case 64u:
				MessageType = (AppRpcMessageType)P_0.ReadEnum();
				break;
			case 74u:
				Message = P_0.ReadBytes();
				break;
			}
		}
	}
}
