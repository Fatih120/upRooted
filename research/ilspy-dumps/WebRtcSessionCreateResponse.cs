using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcSessionCreateResponse : IMessage<WebRtcSessionCreateResponse>, IMessage, IEquatable<WebRtcSessionCreateResponse>, IDeepCloneable<WebRtcSessionCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcSessionCreateResponse> _parser = new MessageParser<WebRtcSessionCreateResponse>(() => new WebRtcSessionCreateResponse());

	private UnknownFieldSet _unknownFields;

	private WebRtcSessionDescription sessionDescription_;

	private static readonly FieldCodec<string> _single_sessionId_codec = FieldCodec.ForClassWrapper<string>(42u);

	private string sessionId_;

	private static readonly FieldCodec<WebRtcUserDevicePacket> _repeated_members_codec = FieldCodec.ForMessage(50u, WebRtcUserDevicePacket.Parser);

	private readonly RepeatedField<WebRtcUserDevicePacket> members_ = new RepeatedField<WebRtcUserDevicePacket>();

	private ulong audioBandwidth_;

	private ulong videoBandwidth_;

	private ulong screenBandwidth_;

	private ulong screenAudioBandwidth_;

	private static readonly FieldCodec<string> _single_errorCode_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string errorCode_;

	private static readonly FieldCodec<string> _single_errorDescription_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string errorDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcSessionCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public WebRtcSessionDescription SessionDescription
	{
		get
		{
			return sessionDescription_;
		}
		set
		{
			sessionDescription_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string SessionId
	{
		get
		{
			return sessionId_;
		}
		set
		{
			sessionId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcUserDevicePacket> Members => members_;

	[GeneratedCode("protoc", null)]
	public ulong AudioBandwidth
	{
		get
		{
			return audioBandwidth_;
		}
		set
		{
			audioBandwidth_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ulong VideoBandwidth
	{
		get
		{
			return videoBandwidth_;
		}
		set
		{
			videoBandwidth_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ulong ScreenBandwidth
	{
		get
		{
			return screenBandwidth_;
		}
		set
		{
			screenBandwidth_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ulong ScreenAudioBandwidth
	{
		get
		{
			return screenAudioBandwidth_;
		}
		set
		{
			screenAudioBandwidth_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ErrorCode
	{
		get
		{
			return errorCode_;
		}
		set
		{
			errorCode_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ErrorDescription
	{
		get
		{
			return errorDescription_;
		}
		set
		{
			errorDescription_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSessionCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSessionCreateResponse(WebRtcSessionCreateResponse other)
		: this()
	{
		sessionDescription_ = ((other.sessionDescription_ != null) ? other.sessionDescription_.Clone() : null);
		SessionId = other.SessionId;
		members_ = other.members_.Clone();
		audioBandwidth_ = other.audioBandwidth_;
		videoBandwidth_ = other.videoBandwidth_;
		screenBandwidth_ = other.screenBandwidth_;
		screenAudioBandwidth_ = other.screenAudioBandwidth_;
		ErrorCode = other.ErrorCode;
		ErrorDescription = other.ErrorDescription;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSessionCreateResponse Clone()
	{
		return new WebRtcSessionCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcSessionCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcSessionCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(SessionDescription, other.SessionDescription))
		{
			return false;
		}
		if (SessionId != other.SessionId)
		{
			return false;
		}
		if (!members_.Equals(other.members_))
		{
			return false;
		}
		if (AudioBandwidth != other.AudioBandwidth)
		{
			return false;
		}
		if (VideoBandwidth != other.VideoBandwidth)
		{
			return false;
		}
		if (ScreenBandwidth != other.ScreenBandwidth)
		{
			return false;
		}
		if (ScreenAudioBandwidth != other.ScreenAudioBandwidth)
		{
			return false;
		}
		if (ErrorCode != other.ErrorCode)
		{
			return false;
		}
		if (ErrorDescription != other.ErrorDescription)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (sessionDescription_ != null)
		{
			num ^= SessionDescription.GetHashCode();
		}
		if (sessionId_ != null)
		{
			num ^= SessionId.GetHashCode();
		}
		num ^= members_.GetHashCode();
		if (AudioBandwidth != 0)
		{
			num ^= AudioBandwidth.GetHashCode();
		}
		if (VideoBandwidth != 0)
		{
			num ^= VideoBandwidth.GetHashCode();
		}
		if (ScreenBandwidth != 0)
		{
			num ^= ScreenBandwidth.GetHashCode();
		}
		if (ScreenAudioBandwidth != 0)
		{
			num ^= ScreenAudioBandwidth.GetHashCode();
		}
		if (errorCode_ != null)
		{
			num ^= ErrorCode.GetHashCode();
		}
		if (errorDescription_ != null)
		{
			num ^= ErrorDescription.GetHashCode();
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
		if (sessionDescription_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(SessionDescription);
		}
		if (sessionId_ != null)
		{
			_single_sessionId_codec.WriteTagAndValue(ref P_0, SessionId);
		}
		members_.WriteTo(ref P_0, _repeated_members_codec);
		if (AudioBandwidth != 0)
		{
			P_0.WriteRawTag(56);
			P_0.WriteUInt64(AudioBandwidth);
		}
		if (VideoBandwidth != 0)
		{
			P_0.WriteRawTag(64);
			P_0.WriteUInt64(VideoBandwidth);
		}
		if (ScreenBandwidth != 0)
		{
			P_0.WriteRawTag(72);
			P_0.WriteUInt64(ScreenBandwidth);
		}
		if (ScreenAudioBandwidth != 0)
		{
			P_0.WriteRawTag(80);
			P_0.WriteUInt64(ScreenAudioBandwidth);
		}
		if (errorCode_ != null)
		{
			_single_errorCode_codec.WriteTagAndValue(ref P_0, ErrorCode);
		}
		if (errorDescription_ != null)
		{
			_single_errorDescription_codec.WriteTagAndValue(ref P_0, ErrorDescription);
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
		if (sessionDescription_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SessionDescription);
		}
		if (sessionId_ != null)
		{
			num += _single_sessionId_codec.CalculateSizeWithTag(SessionId);
		}
		num += members_.CalculateSize(_repeated_members_codec);
		if (AudioBandwidth != 0)
		{
			num += 1 + CodedOutputStream.ComputeUInt64Size(AudioBandwidth);
		}
		if (VideoBandwidth != 0)
		{
			num += 1 + CodedOutputStream.ComputeUInt64Size(VideoBandwidth);
		}
		if (ScreenBandwidth != 0)
		{
			num += 1 + CodedOutputStream.ComputeUInt64Size(ScreenBandwidth);
		}
		if (ScreenAudioBandwidth != 0)
		{
			num += 1 + CodedOutputStream.ComputeUInt64Size(ScreenAudioBandwidth);
		}
		if (errorCode_ != null)
		{
			num += _single_errorCode_codec.CalculateSizeWithTag(ErrorCode);
		}
		if (errorDescription_ != null)
		{
			num += _single_errorDescription_codec.CalculateSizeWithTag(ErrorDescription);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcSessionCreateResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.sessionDescription_ != null)
		{
			if (sessionDescription_ == null)
			{
				SessionDescription = new WebRtcSessionDescription();
			}
			SessionDescription.MergeFrom(other.SessionDescription);
		}
		if (other.sessionId_ != null && (sessionId_ == null || other.SessionId != ""))
		{
			SessionId = other.SessionId;
		}
		members_.Add(other.members_);
		if (other.AudioBandwidth != 0)
		{
			AudioBandwidth = other.AudioBandwidth;
		}
		if (other.VideoBandwidth != 0)
		{
			VideoBandwidth = other.VideoBandwidth;
		}
		if (other.ScreenBandwidth != 0)
		{
			ScreenBandwidth = other.ScreenBandwidth;
		}
		if (other.ScreenAudioBandwidth != 0)
		{
			ScreenAudioBandwidth = other.ScreenAudioBandwidth;
		}
		if (other.errorCode_ != null && (errorCode_ == null || other.ErrorCode != ""))
		{
			ErrorCode = other.ErrorCode;
		}
		if (other.errorDescription_ != null && (errorDescription_ == null || other.ErrorDescription != ""))
		{
			ErrorDescription = other.ErrorDescription;
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
				if (sessionDescription_ == null)
				{
					SessionDescription = new WebRtcSessionDescription();
				}
				P_0.ReadMessage(SessionDescription);
				break;
			case 42u:
			{
				string text2 = _single_sessionId_codec.Read(ref P_0);
				if (sessionId_ == null || text2 != "")
				{
					SessionId = text2;
				}
				break;
			}
			case 50u:
				members_.AddEntriesFrom(ref P_0, _repeated_members_codec);
				break;
			case 56u:
				AudioBandwidth = P_0.ReadUInt64();
				break;
			case 64u:
				VideoBandwidth = P_0.ReadUInt64();
				break;
			case 72u:
				ScreenBandwidth = P_0.ReadUInt64();
				break;
			case 80u:
				ScreenAudioBandwidth = P_0.ReadUInt64();
				break;
			case 122u:
			{
				string text3 = _single_errorCode_codec.Read(ref P_0);
				if (errorCode_ == null || text3 != "")
				{
					ErrorCode = text3;
				}
				break;
			}
			case 130u:
			{
				string text = _single_errorDescription_codec.Read(ref P_0);
				if (errorDescription_ == null || text != "")
				{
					ErrorDescription = text;
				}
				break;
			}
			}
		}
	}
}
