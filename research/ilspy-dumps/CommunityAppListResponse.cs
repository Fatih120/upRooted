using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppListResponse : IMessage<CommunityAppListResponse>, IMessage, IEquatable<CommunityAppListResponse>, IDeepCloneable<CommunityAppListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppListResponse> _parser = new MessageParser<CommunityAppListResponse>(() => new CommunityAppListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityAppPreviewResponse> _repeated_apps_codec = FieldCodec.ForMessage(34u, CommunityAppPreviewResponse.Parser);

	private readonly RepeatedField<CommunityAppPreviewResponse> apps_ = new RepeatedField<CommunityAppPreviewResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityAppPreviewResponse> Apps => apps_;

	[GeneratedCode("protoc", null)]
	public CommunityAppListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppListResponse(CommunityAppListResponse other)
		: this()
	{
		apps_ = other.apps_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppListResponse Clone()
	{
		return new CommunityAppListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!apps_.Equals(other.apps_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= apps_.GetHashCode();
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
		apps_.WriteTo(ref P_0, _repeated_apps_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += apps_.CalculateSize(_repeated_apps_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppListResponse other)
	{
		if (other != null)
		{
			apps_.Add(other.apps_);
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				apps_.AddEntriesFrom(ref P_0, _repeated_apps_codec);
			}
		}
	}
}
