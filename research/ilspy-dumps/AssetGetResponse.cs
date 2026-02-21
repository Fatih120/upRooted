using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Assets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AssetGetResponse : IMessage<AssetGetResponse>, IMessage, IEquatable<AssetGetResponse>, IDeepCloneable<AssetGetResponse>, IBufferMessage
{
	private static readonly MessageParser<AssetGetResponse> _parser = new MessageParser<AssetGetResponse>(() => new AssetGetResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly MapField<string, AssetImage>.Codec _map_legacyAssets_codec = new MapField<string, AssetImage>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForMessage(18u, AssetImage.Parser), 10u);

	private readonly MapField<string, AssetImage> legacyAssets_ = new MapField<string, AssetImage>();

	private static readonly MapField<string, AssetInformation>.Codec _map_assets_codec = new MapField<string, AssetInformation>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForMessage(18u, AssetInformation.Parser), 18u);

	private readonly MapField<string, AssetInformation> assets_ = new MapField<string, AssetInformation>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetGetResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MapField<string, AssetImage> LegacyAssets => legacyAssets_;

	[GeneratedCode("protoc", null)]
	public MapField<string, AssetInformation> Assets => assets_;

	[GeneratedCode("protoc", null)]
	public AssetGetResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetGetResponse(AssetGetResponse other)
		: this()
	{
		legacyAssets_ = other.legacyAssets_.Clone();
		assets_ = other.assets_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetGetResponse Clone()
	{
		return new AssetGetResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetGetResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetGetResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!LegacyAssets.Equals(other.LegacyAssets))
		{
			return false;
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
		num ^= LegacyAssets.GetHashCode();
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
		legacyAssets_.WriteTo(ref P_0, _map_legacyAssets_codec);
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
		num += legacyAssets_.CalculateSize(_map_legacyAssets_codec);
		num += assets_.CalculateSize(_map_assets_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetGetResponse other)
	{
		if (other != null)
		{
			legacyAssets_.MergeFrom(other.legacyAssets_);
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 10u:
				legacyAssets_.AddEntriesFrom(ref P_0, _map_legacyAssets_codec);
				break;
			case 18u:
				assets_.AddEntriesFrom(ref P_0, _map_assets_codec);
				break;
			}
		}
	}
}
