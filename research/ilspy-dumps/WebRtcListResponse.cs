using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcListResponse : IMessage<WebRtcListResponse>, IMessage, IEquatable<WebRtcListResponse>, IDeepCloneable<WebRtcListResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcListResponse> _parser = new MessageParser<WebRtcListResponse>(() => new WebRtcListResponse());

	private UnknownFieldSet _unknownFields;

	private Timestamp createdAt_;

	private static readonly FieldCodec<WebRtcUserInfoResponse> _repeated_members_codec = FieldCodec.ForMessage(90u, WebRtcUserInfoResponse.Parser);

	private readonly RepeatedField<WebRtcUserInfoResponse> members_ = new RepeatedField<WebRtcUserInfoResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public Timestamp CreatedAt
	{
		get
		{
			return createdAt_;
		}
		set
		{
			createdAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcUserInfoResponse> Members => members_;

	[GeneratedCode("protoc", null)]
	public WebRtcListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcListResponse(WebRtcListResponse other)
		: this()
	{
		createdAt_ = ((other.createdAt_ != null) ? other.createdAt_.Clone() : null);
		members_ = other.members_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcListResponse Clone()
	{
		return new WebRtcListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CreatedAt, other.CreatedAt))
		{
			return false;
		}
		if (!members_.Equals(other.members_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (createdAt_ != null)
		{
			num ^= CreatedAt.GetHashCode();
		}
		num ^= members_.GetHashCode();
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
		if (createdAt_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CreatedAt);
		}
		members_.WriteTo(ref P_0, _repeated_members_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (createdAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreatedAt);
		}
		num += members_.CalculateSize(_repeated_members_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcListResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.createdAt_ != null)
		{
			if (createdAt_ == null)
			{
				CreatedAt = new Timestamp();
			}
			CreatedAt.MergeFrom(other.CreatedAt);
		}
		members_.Add(other.members_);
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
			case 82u:
				if (createdAt_ == null)
				{
					CreatedAt = new Timestamp();
				}
				P_0.ReadMessage(CreatedAt);
				break;
			case 90u:
				members_.AddEntriesFrom(ref P_0, _repeated_members_codec);
				break;
			}
		}
	}
}
