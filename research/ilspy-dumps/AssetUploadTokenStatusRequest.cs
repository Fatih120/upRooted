using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AssetUploadTokenStatusRequest : IMessage<AssetUploadTokenStatusRequest>, IMessage, IEquatable<AssetUploadTokenStatusRequest>, IDeepCloneable<AssetUploadTokenStatusRequest>, IBufferMessage
{
	private static readonly MessageParser<AssetUploadTokenStatusRequest> _parser = new MessageParser<AssetUploadTokenStatusRequest>(() => new AssetUploadTokenStatusRequest());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<string> _repeated_tokens_codec = FieldCodec.ForString(42u);

	private readonly RepeatedField<string> tokens_ = new RepeatedField<string>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetUploadTokenStatusRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<string> Tokens => tokens_;

	[GeneratedCode("protoc", null)]
	public AssetUploadTokenStatusRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetUploadTokenStatusRequest(AssetUploadTokenStatusRequest other)
		: this()
	{
		tokens_ = other.tokens_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetUploadTokenStatusRequest Clone()
	{
		return new AssetUploadTokenStatusRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetUploadTokenStatusRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetUploadTokenStatusRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!tokens_.Equals(other.tokens_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= tokens_.GetHashCode();
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
		tokens_.WriteTo(ref P_0, _repeated_tokens_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += tokens_.CalculateSize(_repeated_tokens_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetUploadTokenStatusRequest other)
	{
		if (other != null)
		{
			tokens_.Add(other.tokens_);
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
				tokens_.AddEntriesFrom(ref P_0, _repeated_tokens_codec);
			}
		}
	}
}
