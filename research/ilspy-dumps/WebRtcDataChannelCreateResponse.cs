using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcDataChannelCreateResponse : IMessage<WebRtcDataChannelCreateResponse>, IMessage, IEquatable<WebRtcDataChannelCreateResponse>, IDeepCloneable<WebRtcDataChannelCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcDataChannelCreateResponse> _parser = new MessageParser<WebRtcDataChannelCreateResponse>(() => new WebRtcDataChannelCreateResponse());

	private UnknownFieldSet _unknownFields;

	private int id_;

	private string dataChannelName_ = "";

	private string location_ = "";

	private static readonly FieldCodec<string> _single_errorCode_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string errorCode_;

	private static readonly FieldCodec<string> _single_errorDescription_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string errorDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcDataChannelCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public int Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string DataChannelName
	{
		get
		{
			return dataChannelName_;
		}
		set
		{
			dataChannelName_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public WebRtcDataChannelCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelCreateResponse(WebRtcDataChannelCreateResponse other)
		: this()
	{
		id_ = other.id_;
		dataChannelName_ = other.dataChannelName_;
		location_ = other.location_;
		ErrorCode = other.ErrorCode;
		ErrorDescription = other.ErrorDescription;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelCreateResponse Clone()
	{
		return new WebRtcDataChannelCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcDataChannelCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcDataChannelCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Id != other.Id)
		{
			return false;
		}
		if (DataChannelName != other.DataChannelName)
		{
			return false;
		}
		if (Location != other.Location)
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
		if (Id != 0)
		{
			num ^= Id.GetHashCode();
		}
		if (DataChannelName.Length != 0)
		{
			num ^= DataChannelName.GetHashCode();
		}
		if (Location.Length != 0)
		{
			num ^= Location.GetHashCode();
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
		if (Id != 0)
		{
			P_0.WriteRawTag(32);
			P_0.WriteInt32(Id);
		}
		if (DataChannelName.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(DataChannelName);
		}
		if (Location.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Location);
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
		if (Id != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Id);
		}
		if (DataChannelName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DataChannelName);
		}
		if (Location.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Location);
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
	public void MergeFrom(WebRtcDataChannelCreateResponse other)
	{
		if (other != null)
		{
			if (other.Id != 0)
			{
				Id = other.Id;
			}
			if (other.DataChannelName.Length != 0)
			{
				DataChannelName = other.DataChannelName;
			}
			if (other.Location.Length != 0)
			{
				Location = other.Location;
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
			case 32u:
				Id = P_0.ReadInt32();
				break;
			case 42u:
				DataChannelName = P_0.ReadString();
				break;
			case 50u:
				Location = P_0.ReadString();
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
