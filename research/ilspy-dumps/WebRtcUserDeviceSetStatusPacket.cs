using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class WebRtcUserDeviceSetStatusPacket : IPacketWebRtc, IPacket, IMessage<WebRtcUserDeviceSetStatusPacket>, IMessage, IEquatable<WebRtcUserDeviceSetStatusPacket>, IDeepCloneable<WebRtcUserDeviceSetStatusPacket>, IBufferMessage
{
	private static readonly MessageParser<WebRtcUserDeviceSetStatusPacket> _parser = new MessageParser<WebRtcUserDeviceSetStatusPacket>(() => new WebRtcUserDeviceSetStatusPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private UserUuid userId_;

	private DeviceUuid deviceId_;

	private bool isMuted_;

	private bool isAdminMuted_;

	private bool isDeafened_;

	private bool isAdminDeafened_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcUserDeviceSetStatusPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PacketType PacketType
	{
		get
		{
			return packetType_;
		}
		set
		{
			packetType_ = value;
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
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DeviceUuid DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsMuted
	{
		get
		{
			return isMuted_;
		}
		set
		{
			isMuted_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsAdminMuted
	{
		get
		{
			return isAdminMuted_;
		}
		set
		{
			isAdminMuted_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsDeafened
	{
		get
		{
			return isDeafened_;
		}
		set
		{
			isDeafened_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsAdminDeafened
	{
		get
		{
			return isAdminDeafened_;
		}
		set
		{
			isAdminDeafened_ = value;
		}
	}

	public static implicit operator PacketContainer(WebRtcUserDeviceSetStatusPacket packet)
	{
		return new PacketContainer
		{
			WebRtcUserDeviceSetStatus = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDeviceSetStatusPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDeviceSetStatusPacket(WebRtcUserDeviceSetStatusPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		deviceId_ = ((other.deviceId_ != null) ? other.deviceId_.Clone() : null);
		isMuted_ = other.isMuted_;
		isAdminMuted_ = other.isAdminMuted_;
		isDeafened_ = other.isDeafened_;
		isAdminDeafened_ = other.isAdminDeafened_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDeviceSetStatusPacket Clone()
	{
		return new WebRtcUserDeviceSetStatusPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcUserDeviceSetStatusPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcUserDeviceSetStatusPacket other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PacketType != other.PacketType)
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (!object.Equals(DeviceId, other.DeviceId))
		{
			return false;
		}
		if (IsMuted != other.IsMuted)
		{
			return false;
		}
		if (IsAdminMuted != other.IsAdminMuted)
		{
			return false;
		}
		if (IsDeafened != other.IsDeafened)
		{
			return false;
		}
		if (IsAdminDeafened != other.IsAdminDeafened)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (PacketType != PacketType.Unspecified)
		{
			num ^= PacketType.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (deviceId_ != null)
		{
			num ^= DeviceId.GetHashCode();
		}
		if (IsMuted)
		{
			num ^= IsMuted.GetHashCode();
		}
		if (IsAdminMuted)
		{
			num ^= IsAdminMuted.GetHashCode();
		}
		if (IsDeafened)
		{
			num ^= IsDeafened.GetHashCode();
		}
		if (IsAdminDeafened)
		{
			num ^= IsAdminDeafened.GetHashCode();
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
		if (PacketType != PacketType.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)PacketType);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CommunityId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(ContainerId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(UserId);
		}
		if (deviceId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(DeviceId);
		}
		if (IsMuted)
		{
			P_0.WriteRawTag(56);
			P_0.WriteBool(IsMuted);
		}
		if (IsAdminMuted)
		{
			P_0.WriteRawTag(64);
			P_0.WriteBool(IsAdminMuted);
		}
		if (IsDeafened)
		{
			P_0.WriteRawTag(72);
			P_0.WriteBool(IsDeafened);
		}
		if (IsAdminDeafened)
		{
			P_0.WriteRawTag(80);
			P_0.WriteBool(IsAdminDeafened);
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
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (deviceId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DeviceId);
		}
		if (IsMuted)
		{
			num += 2;
		}
		if (IsAdminMuted)
		{
			num += 2;
		}
		if (IsDeafened)
		{
			num += 2;
		}
		if (IsAdminDeafened)
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
	public void MergeFrom(WebRtcUserDeviceSetStatusPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.deviceId_ != null)
		{
			if (deviceId_ == null)
			{
				DeviceId = new DeviceUuid();
			}
			DeviceId.MergeFrom(other.DeviceId);
		}
		if (other.IsMuted)
		{
			IsMuted = other.IsMuted;
		}
		if (other.IsAdminMuted)
		{
			IsAdminMuted = other.IsAdminMuted;
		}
		if (other.IsDeafened)
		{
			IsDeafened = other.IsDeafened;
		}
		if (other.IsAdminDeafened)
		{
			IsAdminDeafened = other.IsAdminDeafened;
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
			case 8u:
				PacketType = (PacketType)P_0.ReadEnum();
				break;
			case 26u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 34u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 42u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 50u:
				if (deviceId_ == null)
				{
					DeviceId = new DeviceUuid();
				}
				P_0.ReadMessage(DeviceId);
				break;
			case 56u:
				IsMuted = P_0.ReadBool();
				break;
			case 64u:
				IsAdminMuted = P_0.ReadBool();
				break;
			case 72u:
				IsDeafened = P_0.ReadBool();
				break;
			case 80u:
				IsAdminDeafened = P_0.ReadBool();
				break;
			}
		}
	}
}
