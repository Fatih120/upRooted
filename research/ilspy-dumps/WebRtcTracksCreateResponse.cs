using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcTracksCreateResponse : IMessage<WebRtcTracksCreateResponse>, IMessage, IEquatable<WebRtcTracksCreateResponse>, IDeepCloneable<WebRtcTracksCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcTracksCreateResponse> _parser = new MessageParser<WebRtcTracksCreateResponse>(() => new WebRtcTracksCreateResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<WebRtcTrackCreateResponse> _repeated_tracks_codec = FieldCodec.ForMessage(34u, WebRtcTrackCreateResponse.Parser);

	private readonly RepeatedField<WebRtcTrackCreateResponse> tracks_ = new RepeatedField<WebRtcTrackCreateResponse>();

	private WebRtcSessionDescription sessionDescription_;

	private bool requiresImmediateRenegotiation_;

	private static readonly FieldCodec<string> _single_errorCode_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string errorCode_;

	private static readonly FieldCodec<string> _single_errorDescription_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string errorDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcTracksCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcTrackCreateResponse> Tracks => tracks_;

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
	public bool RequiresImmediateRenegotiation
	{
		get
		{
			return requiresImmediateRenegotiation_;
		}
		set
		{
			requiresImmediateRenegotiation_ = value;
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
	public WebRtcTracksCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTracksCreateResponse(WebRtcTracksCreateResponse other)
		: this()
	{
		tracks_ = other.tracks_.Clone();
		sessionDescription_ = ((other.sessionDescription_ != null) ? other.sessionDescription_.Clone() : null);
		requiresImmediateRenegotiation_ = other.requiresImmediateRenegotiation_;
		ErrorCode = other.ErrorCode;
		ErrorDescription = other.ErrorDescription;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTracksCreateResponse Clone()
	{
		return new WebRtcTracksCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcTracksCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcTracksCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!tracks_.Equals(other.tracks_))
		{
			return false;
		}
		if (!object.Equals(SessionDescription, other.SessionDescription))
		{
			return false;
		}
		if (RequiresImmediateRenegotiation != other.RequiresImmediateRenegotiation)
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
		num ^= tracks_.GetHashCode();
		if (sessionDescription_ != null)
		{
			num ^= SessionDescription.GetHashCode();
		}
		if (RequiresImmediateRenegotiation)
		{
			num ^= RequiresImmediateRenegotiation.GetHashCode();
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
		tracks_.WriteTo(ref P_0, _repeated_tracks_codec);
		if (sessionDescription_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(SessionDescription);
		}
		if (RequiresImmediateRenegotiation)
		{
			P_0.WriteRawTag(48);
			P_0.WriteBool(RequiresImmediateRenegotiation);
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
		num += tracks_.CalculateSize(_repeated_tracks_codec);
		if (sessionDescription_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SessionDescription);
		}
		if (RequiresImmediateRenegotiation)
		{
			num += 2;
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
	public void MergeFrom(WebRtcTracksCreateResponse other)
	{
		if (other == null)
		{
			return;
		}
		tracks_.Add(other.tracks_);
		if (other.sessionDescription_ != null)
		{
			if (sessionDescription_ == null)
			{
				SessionDescription = new WebRtcSessionDescription();
			}
			SessionDescription.MergeFrom(other.SessionDescription);
		}
		if (other.RequiresImmediateRenegotiation)
		{
			RequiresImmediateRenegotiation = other.RequiresImmediateRenegotiation;
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
				tracks_.AddEntriesFrom(ref P_0, _repeated_tracks_codec);
				break;
			case 42u:
				if (sessionDescription_ == null)
				{
					SessionDescription = new WebRtcSessionDescription();
				}
				P_0.ReadMessage(SessionDescription);
				break;
			case 48u:
				RequiresImmediateRenegotiation = P_0.ReadBool();
				break;
			case 122u:
			{
				string text2 = _single_errorCode_codec.Read(ref P_0);
				if (errorCode_ == null || text2 != "")
				{
					ErrorCode = text2;
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
