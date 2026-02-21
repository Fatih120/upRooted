using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class WebRtcUserDetachPacket : IPacketWebRtc, IPacket, IMessage<WebRtcUserDetachPacket>, IMessage, IEquatable<WebRtcUserDetachPacket>, IDeepCloneable<WebRtcUserDetachPacket>, IBufferMessage
{
	private static readonly MessageParser<WebRtcUserDetachPacket> _parser = new MessageParser<WebRtcUserDetachPacket>(() => new WebRtcUserDetachPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private UserUuid userId_;

	private DeviceUuid deviceId_;

	private bool isKick_;

	private static readonly FieldCodec<string> _single_audioTrackId_codec = FieldCodec.ForClassWrapper<string>(162u);

	private string audioTrackId_;

	private static readonly FieldCodec<string> _single_videoTrackId_codec = FieldCodec.ForClassWrapper<string>(170u);

	private string videoTrackId_;

	private static readonly FieldCodec<string> _single_screenTrackId_codec = FieldCodec.ForClassWrapper<string>(178u);

	private string screenTrackId_;

	private static readonly FieldCodec<string> _single_screenAudioTrackId_codec = FieldCodec.ForClassWrapper<string>(186u);

	private string screenAudioTrackId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcUserDetachPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[0];

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
	public bool IsKick
	{
		get
		{
			return isKick_;
		}
		set
		{
			isKick_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string AudioTrackId
	{
		get
		{
			return audioTrackId_;
		}
		set
		{
			audioTrackId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string VideoTrackId
	{
		get
		{
			return videoTrackId_;
		}
		set
		{
			videoTrackId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ScreenTrackId
	{
		get
		{
			return screenTrackId_;
		}
		set
		{
			screenTrackId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ScreenAudioTrackId
	{
		get
		{
			return screenAudioTrackId_;
		}
		set
		{
			screenAudioTrackId_ = value;
		}
	}

	public static implicit operator PacketContainer(WebRtcUserDetachPacket packet)
	{
		return new PacketContainer
		{
			WebRtcUserDetach = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDetachPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDetachPacket(WebRtcUserDetachPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		deviceId_ = ((other.deviceId_ != null) ? other.deviceId_.Clone() : null);
		isKick_ = other.isKick_;
		AudioTrackId = other.AudioTrackId;
		VideoTrackId = other.VideoTrackId;
		ScreenTrackId = other.ScreenTrackId;
		ScreenAudioTrackId = other.ScreenAudioTrackId;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDetachPacket Clone()
	{
		return new WebRtcUserDetachPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcUserDetachPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcUserDetachPacket other)
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
		if (IsKick != other.IsKick)
		{
			return false;
		}
		if (AudioTrackId != other.AudioTrackId)
		{
			return false;
		}
		if (VideoTrackId != other.VideoTrackId)
		{
			return false;
		}
		if (ScreenTrackId != other.ScreenTrackId)
		{
			return false;
		}
		if (ScreenAudioTrackId != other.ScreenAudioTrackId)
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
		if (IsKick)
		{
			num ^= IsKick.GetHashCode();
		}
		if (audioTrackId_ != null)
		{
			num ^= AudioTrackId.GetHashCode();
		}
		if (videoTrackId_ != null)
		{
			num ^= VideoTrackId.GetHashCode();
		}
		if (screenTrackId_ != null)
		{
			num ^= ScreenTrackId.GetHashCode();
		}
		if (screenAudioTrackId_ != null)
		{
			num ^= ScreenAudioTrackId.GetHashCode();
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
		if (IsKick)
		{
			P_0.WriteRawTag(56);
			P_0.WriteBool(IsKick);
		}
		if (audioTrackId_ != null)
		{
			_single_audioTrackId_codec.WriteTagAndValue(ref P_0, AudioTrackId);
		}
		if (videoTrackId_ != null)
		{
			_single_videoTrackId_codec.WriteTagAndValue(ref P_0, VideoTrackId);
		}
		if (screenTrackId_ != null)
		{
			_single_screenTrackId_codec.WriteTagAndValue(ref P_0, ScreenTrackId);
		}
		if (screenAudioTrackId_ != null)
		{
			_single_screenAudioTrackId_codec.WriteTagAndValue(ref P_0, ScreenAudioTrackId);
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
		if (IsKick)
		{
			num += 2;
		}
		if (audioTrackId_ != null)
		{
			num += _single_audioTrackId_codec.CalculateSizeWithTag(AudioTrackId);
		}
		if (videoTrackId_ != null)
		{
			num += _single_videoTrackId_codec.CalculateSizeWithTag(VideoTrackId);
		}
		if (screenTrackId_ != null)
		{
			num += _single_screenTrackId_codec.CalculateSizeWithTag(ScreenTrackId);
		}
		if (screenAudioTrackId_ != null)
		{
			num += _single_screenAudioTrackId_codec.CalculateSizeWithTag(ScreenAudioTrackId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcUserDetachPacket other)
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
		if (other.IsKick)
		{
			IsKick = other.IsKick;
		}
		if (other.audioTrackId_ != null && (audioTrackId_ == null || other.AudioTrackId != ""))
		{
			AudioTrackId = other.AudioTrackId;
		}
		if (other.videoTrackId_ != null && (videoTrackId_ == null || other.VideoTrackId != ""))
		{
			VideoTrackId = other.VideoTrackId;
		}
		if (other.screenTrackId_ != null && (screenTrackId_ == null || other.ScreenTrackId != ""))
		{
			ScreenTrackId = other.ScreenTrackId;
		}
		if (other.screenAudioTrackId_ != null && (screenAudioTrackId_ == null || other.ScreenAudioTrackId != ""))
		{
			ScreenAudioTrackId = other.ScreenAudioTrackId;
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
				IsKick = P_0.ReadBool();
				break;
			case 162u:
			{
				string text4 = _single_audioTrackId_codec.Read(ref P_0);
				if (audioTrackId_ == null || text4 != "")
				{
					AudioTrackId = text4;
				}
				break;
			}
			case 170u:
			{
				string text3 = _single_videoTrackId_codec.Read(ref P_0);
				if (videoTrackId_ == null || text3 != "")
				{
					VideoTrackId = text3;
				}
				break;
			}
			case 178u:
			{
				string text2 = _single_screenTrackId_codec.Read(ref P_0);
				if (screenTrackId_ == null || text2 != "")
				{
					ScreenTrackId = text2;
				}
				break;
			}
			case 186u:
			{
				string text = _single_screenAudioTrackId_codec.Read(ref P_0);
				if (screenAudioTrackId_ == null || text != "")
				{
					ScreenAudioTrackId = text;
				}
				break;
			}
			}
		}
	}
}
