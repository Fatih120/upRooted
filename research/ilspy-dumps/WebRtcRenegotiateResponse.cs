using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcRenegotiateResponse : IMessage<WebRtcRenegotiateResponse>, IMessage, IEquatable<WebRtcRenegotiateResponse>, IDeepCloneable<WebRtcRenegotiateResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcRenegotiateResponse> _parser = new MessageParser<WebRtcRenegotiateResponse>(() => new WebRtcRenegotiateResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<string> _single_errorCode_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string errorCode_;

	private static readonly FieldCodec<string> _single_errorDescription_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string errorDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcRenegotiateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public WebRtcRenegotiateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcRenegotiateResponse(WebRtcRenegotiateResponse other)
		: this()
	{
		ErrorCode = other.ErrorCode;
		ErrorDescription = other.ErrorDescription;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcRenegotiateResponse Clone()
	{
		return new WebRtcRenegotiateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcRenegotiateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcRenegotiateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
	public void MergeFrom(WebRtcRenegotiateResponse other)
	{
		if (other != null)
		{
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
