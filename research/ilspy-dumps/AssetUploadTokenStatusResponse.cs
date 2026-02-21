using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core.Enums;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AssetUploadTokenStatusResponse : IMessage<AssetUploadTokenStatusResponse>, IMessage, IEquatable<AssetUploadTokenStatusResponse>, IDeepCloneable<AssetUploadTokenStatusResponse>, IBufferMessage
{
	private static readonly MessageParser<AssetUploadTokenStatusResponse> _parser = new MessageParser<AssetUploadTokenStatusResponse>(() => new AssetUploadTokenStatusResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly MapField<string, UploadTokenStatus>.Codec _map_status_codec = new MapField<string, UploadTokenStatus>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForEnum(16u, (UploadTokenStatus x) => (int)x, (int x) => (UploadTokenStatus)x, UploadTokenStatus.Unspecified), 42u);

	private readonly MapField<string, UploadTokenStatus> status_ = new MapField<string, UploadTokenStatus>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetUploadTokenStatusResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MapField<string, UploadTokenStatus> Status => status_;

	[GeneratedCode("protoc", null)]
	public AssetUploadTokenStatusResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetUploadTokenStatusResponse(AssetUploadTokenStatusResponse other)
		: this()
	{
		status_ = other.status_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetUploadTokenStatusResponse Clone()
	{
		return new AssetUploadTokenStatusResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetUploadTokenStatusResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetUploadTokenStatusResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Status.Equals(other.Status))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= Status.GetHashCode();
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
		status_.WriteTo(ref P_0, _map_status_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += status_.CalculateSize(_map_status_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetUploadTokenStatusResponse other)
	{
		if (other != null)
		{
			status_.MergeFrom(other.status_);
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
			if (num3 != 42)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				status_.AddEntriesFrom(ref P_0, _map_status_codec);
			}
		}
	}
}
