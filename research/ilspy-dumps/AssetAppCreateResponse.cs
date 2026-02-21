using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AssetAppCreateResponse : IMessage<AssetAppCreateResponse>, IMessage, IEquatable<AssetAppCreateResponse>, IDeepCloneable<AssetAppCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<AssetAppCreateResponse> _parser = new MessageParser<AssetAppCreateResponse>(() => new AssetAppCreateResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly MapField<string, string>.Codec _map_assets_codec = new MapField<string, string>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForString(18u, ""), 42u);

	private readonly MapField<string, string> assets_ = new MapField<string, string>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetAppCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MapField<string, string> Assets => assets_;

	[GeneratedCode("protoc", null)]
	public AssetAppCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetAppCreateResponse(AssetAppCreateResponse other)
		: this()
	{
		assets_ = other.assets_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetAppCreateResponse Clone()
	{
		return new AssetAppCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetAppCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetAppCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!Assets.Equals(other.Assets))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= Assets.GetHashCode();
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
		assets_.WriteTo(ref P_0, _map_assets_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += assets_.CalculateSize(_map_assets_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetAppCreateResponse other)
	{
		if (other != null)
		{
			assets_.MergeFrom(other.assets_);
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
				assets_.AddEntriesFrom(ref P_0, _map_assets_codec);
			}
		}
	}
}
