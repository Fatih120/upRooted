using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcUserInfoResponse : IMessage<WebRtcUserInfoResponse>, IMessage, IEquatable<WebRtcUserInfoResponse>, IDeepCloneable<WebRtcUserInfoResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcUserInfoResponse> _parser = new MessageParser<WebRtcUserInfoResponse>(() => new WebRtcUserInfoResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private DeviceUuid deviceId_;

	private bool isMuted_;

	private bool isAdminMuted_;

	private bool isDeafened_;

	private bool isAdminDeafened_;

	private bool isAudio_;

	private bool isScreen_;

	private bool isVideo_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcUserInfoResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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

	[GeneratedCode("protoc", null)]
	public bool IsAudio
	{
		get
		{
			return isAudio_;
		}
		set
		{
			isAudio_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsScreen
	{
		get
		{
			return isScreen_;
		}
		set
		{
			isScreen_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsVideo
	{
		get
		{
			return isVideo_;
		}
		set
		{
			isVideo_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserInfoResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserInfoResponse(WebRtcUserInfoResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		deviceId_ = ((other.deviceId_ != null) ? other.deviceId_.Clone() : null);
		isMuted_ = other.isMuted_;
		isAdminMuted_ = other.isAdminMuted_;
		isDeafened_ = other.isDeafened_;
		isAdminDeafened_ = other.isAdminDeafened_;
		isAudio_ = other.isAudio_;
		isScreen_ = other.isScreen_;
		isVideo_ = other.isVideo_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserInfoResponse Clone()
	{
		return new WebRtcUserInfoResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcUserInfoResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcUserInfoResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
		if (IsAudio != other.IsAudio)
		{
			return false;
		}
		if (IsScreen != other.IsScreen)
		{
			return false;
		}
		if (IsVideo != other.IsVideo)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
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
		if (IsAudio)
		{
			num ^= IsAudio.GetHashCode();
		}
		if (IsScreen)
		{
			num ^= IsScreen.GetHashCode();
		}
		if (IsVideo)
		{
			num ^= IsVideo.GetHashCode();
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
		if (userId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(UserId);
		}
		if (deviceId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(DeviceId);
		}
		if (IsMuted)
		{
			P_0.WriteRawTag(48);
			P_0.WriteBool(IsMuted);
		}
		if (IsAdminMuted)
		{
			P_0.WriteRawTag(56);
			P_0.WriteBool(IsAdminMuted);
		}
		if (IsDeafened)
		{
			P_0.WriteRawTag(64);
			P_0.WriteBool(IsDeafened);
		}
		if (IsAdminDeafened)
		{
			P_0.WriteRawTag(72);
			P_0.WriteBool(IsAdminDeafened);
		}
		if (IsAudio)
		{
			P_0.WriteRawTag(80);
			P_0.WriteBool(IsAudio);
		}
		if (IsScreen)
		{
			P_0.WriteRawTag(88);
			P_0.WriteBool(IsScreen);
		}
		if (IsVideo)
		{
			P_0.WriteRawTag(96);
			P_0.WriteBool(IsVideo);
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
		if (IsAudio)
		{
			num += 2;
		}
		if (IsScreen)
		{
			num += 2;
		}
		if (IsVideo)
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
	public void MergeFrom(WebRtcUserInfoResponse other)
	{
		if (other == null)
		{
			return;
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
		if (other.IsAudio)
		{
			IsAudio = other.IsAudio;
		}
		if (other.IsScreen)
		{
			IsScreen = other.IsScreen;
		}
		if (other.IsVideo)
		{
			IsVideo = other.IsVideo;
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
			case 34u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 42u:
				if (deviceId_ == null)
				{
					DeviceId = new DeviceUuid();
				}
				P_0.ReadMessage(DeviceId);
				break;
			case 48u:
				IsMuted = P_0.ReadBool();
				break;
			case 56u:
				IsAdminMuted = P_0.ReadBool();
				break;
			case 64u:
				IsDeafened = P_0.ReadBool();
				break;
			case 72u:
				IsAdminDeafened = P_0.ReadBool();
				break;
			case 80u:
				IsAudio = P_0.ReadBool();
				break;
			case 88u:
				IsScreen = P_0.ReadBool();
				break;
			case 96u:
				IsVideo = P_0.ReadBool();
				break;
			}
		}
	}
}
