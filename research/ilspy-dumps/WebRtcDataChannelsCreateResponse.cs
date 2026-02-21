using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcDataChannelsCreateResponse : IMessage<WebRtcDataChannelsCreateResponse>, IMessage, IEquatable<WebRtcDataChannelsCreateResponse>, IDeepCloneable<WebRtcDataChannelsCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcDataChannelsCreateResponse> _parser = new MessageParser<WebRtcDataChannelsCreateResponse>(() => new WebRtcDataChannelsCreateResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<WebRtcDataChannelCreateResponse> _repeated_dataChannels_codec = FieldCodec.ForMessage(34u, WebRtcDataChannelCreateResponse.Parser);

	private readonly RepeatedField<WebRtcDataChannelCreateResponse> dataChannels_ = new RepeatedField<WebRtcDataChannelCreateResponse>();

	private static readonly FieldCodec<string> _single_errorCode_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string errorCode_;

	private static readonly FieldCodec<string> _single_errorDescription_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string errorDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcDataChannelsCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcDataChannelCreateResponse> DataChannels => dataChannels_;

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
	public WebRtcDataChannelsCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelsCreateResponse(WebRtcDataChannelsCreateResponse other)
		: this()
	{
		dataChannels_ = other.dataChannels_.Clone();
		ErrorCode = other.ErrorCode;
		ErrorDescription = other.ErrorDescription;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelsCreateResponse Clone()
	{
		return new WebRtcDataChannelsCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcDataChannelsCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcDataChannelsCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!dataChannels_.Equals(other.dataChannels_))
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
		num ^= dataChannels_.GetHashCode();
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
		dataChannels_.WriteTo(ref P_0, _repeated_dataChannels_codec);
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
		num += dataChannels_.CalculateSize(_repeated_dataChannels_codec);
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
	public void MergeFrom(WebRtcDataChannelsCreateResponse other)
	{
		if (other != null)
		{
			dataChannels_.Add(other.dataChannels_);
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
			case 34u:
				dataChannels_.AddEntriesFrom(ref P_0, _repeated_dataChannels_codec);
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
