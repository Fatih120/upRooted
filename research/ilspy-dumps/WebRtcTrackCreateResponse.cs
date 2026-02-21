using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcTrackCreateResponse : IMessage<WebRtcTrackCreateResponse>, IMessage, IEquatable<WebRtcTrackCreateResponse>, IDeepCloneable<WebRtcTrackCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcTrackCreateResponse> _parser = new MessageParser<WebRtcTrackCreateResponse>(() => new WebRtcTrackCreateResponse());

	private UnknownFieldSet _unknownFields;

	private string trackId_ = "";

	private string location_ = "";

	private string mid_ = "";

	private static readonly FieldCodec<string> _single_errorCode_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string errorCode_;

	private static readonly FieldCodec<string> _single_errorDescription_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string errorDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcTrackCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[9];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string TrackId
	{
		get
		{
			return trackId_;
		}
		set
		{
			trackId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Location
	{
		get
		{
			return location_;
		}
		set
		{
			location_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Mid
	{
		get
		{
			return mid_;
		}
		set
		{
			mid_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public WebRtcTrackCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCreateResponse(WebRtcTrackCreateResponse other)
		: this()
	{
		trackId_ = other.trackId_;
		location_ = other.location_;
		mid_ = other.mid_;
		ErrorCode = other.ErrorCode;
		ErrorDescription = other.ErrorDescription;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCreateResponse Clone()
	{
		return new WebRtcTrackCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcTrackCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcTrackCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TrackId != other.TrackId)
		{
			return false;
		}
		if (Location != other.Location)
		{
			return false;
		}
		if (Mid != other.Mid)
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
		if (TrackId.Length != 0)
		{
			num ^= TrackId.GetHashCode();
		}
		if (Location.Length != 0)
		{
			num ^= Location.GetHashCode();
		}
		if (Mid.Length != 0)
		{
			num ^= Mid.GetHashCode();
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
		if (TrackId.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(TrackId);
		}
		if (Location.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Location);
		}
		if (Mid.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(Mid);
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
		if (TrackId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TrackId);
		}
		if (Location.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Location);
		}
		if (Mid.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Mid);
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
	public void MergeFrom(WebRtcTrackCreateResponse other)
	{
		if (other != null)
		{
			if (other.TrackId.Length != 0)
			{
				TrackId = other.TrackId;
			}
			if (other.Location.Length != 0)
			{
				Location = other.Location;
			}
			if (other.Mid.Length != 0)
			{
				Mid = other.Mid;
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
			case 42u:
				TrackId = P_0.ReadString();
				break;
			case 50u:
				Location = P_0.ReadString();
				break;
			case 58u:
				Mid = P_0.ReadString();
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
