using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcGetIceInfoResponse : IMessage<WebRtcGetIceInfoResponse>, IMessage, IEquatable<WebRtcGetIceInfoResponse>, IDeepCloneable<WebRtcGetIceInfoResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcGetIceInfoResponse> _parser = new MessageParser<WebRtcGetIceInfoResponse>(() => new WebRtcGetIceInfoResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<string> _repeated_urls_codec = FieldCodec.ForString(10u);

	private readonly RepeatedField<string> urls_ = new RepeatedField<string>();

	private string username_ = "";

	private string credentials_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcGetIceInfoResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Urls => urls_;

	[GeneratedCode("protoc", null)]
	public string Username
	{
		get
		{
			return username_;
		}
		set
		{
			username_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Credentials
	{
		get
		{
			return credentials_;
		}
		set
		{
			credentials_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcGetIceInfoResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcGetIceInfoResponse(WebRtcGetIceInfoResponse other)
		: this()
	{
		urls_ = other.urls_.Clone();
		username_ = other.username_;
		credentials_ = other.credentials_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcGetIceInfoResponse Clone()
	{
		return new WebRtcGetIceInfoResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcGetIceInfoResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcGetIceInfoResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!urls_.Equals(other.urls_))
		{
			return false;
		}
		if (Username != other.Username)
		{
			return false;
		}
		if (Credentials != other.Credentials)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= urls_.GetHashCode();
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
		}
		if (Credentials.Length != 0)
		{
			num ^= Credentials.GetHashCode();
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
		urls_.WriteTo(ref P_0, _repeated_urls_codec);
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(Username);
		}
		if (Credentials.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(Credentials);
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
		num += urls_.CalculateSize(_repeated_urls_codec);
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
		}
		if (Credentials.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Credentials);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcGetIceInfoResponse other)
	{
		if (other != null)
		{
			urls_.Add(other.urls_);
			if (other.Username.Length != 0)
			{
				Username = other.Username;
			}
			if (other.Credentials.Length != 0)
			{
				Credentials = other.Credentials;
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
			case 10u:
				urls_.AddEntriesFrom(ref P_0, _repeated_urls_codec);
				break;
			case 18u:
				Username = P_0.ReadString();
				break;
			case 26u:
				Credentials = P_0.ReadString();
				break;
			}
		}
	}
}
