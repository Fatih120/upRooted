using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Assets;

namespace RootApp.WebApi.Shared.Packets;

public sealed class MessageReferenceMaps : IMessage<MessageReferenceMaps>, IMessage, IEquatable<MessageReferenceMaps>, IDeepCloneable<MessageReferenceMaps>, IBufferMessage
{
	private static readonly MessageParser<MessageReferenceMaps> _parser = new MessageParser<MessageReferenceMaps>(() => new MessageReferenceMaps());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<MessageReferenceMapUser> _repeated_users_codec = FieldCodec.ForMessage(82u, MessageReferenceMapUser.Parser);

	private readonly RepeatedField<MessageReferenceMapUser> users_ = new RepeatedField<MessageReferenceMapUser>();

	private static readonly FieldCodec<MessageReferenceMapChannel> _repeated_channels_codec = FieldCodec.ForMessage(90u, MessageReferenceMapChannel.Parser);

	private readonly RepeatedField<MessageReferenceMapChannel> channels_ = new RepeatedField<MessageReferenceMapChannel>();

	private static readonly FieldCodec<MessageReferenceMapCommunityRole> _repeated_roles_codec = FieldCodec.ForMessage(98u, MessageReferenceMapCommunityRole.Parser);

	private readonly RepeatedField<MessageReferenceMapCommunityRole> roles_ = new RepeatedField<MessageReferenceMapCommunityRole>();

	private static readonly MapField<string, AssetImage>.Codec _map_imageAssets_codec = new MapField<string, AssetImage>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForMessage(18u, AssetImage.Parser), 106u);

	private readonly MapField<string, AssetImage> imageAssets_ = new MapField<string, AssetImage>();

	private static readonly MapField<string, AssetInformation>.Codec _map_assets_codec = new MapField<string, AssetInformation>.Codec(FieldCodec.ForString(10u, ""), FieldCodec.ForMessage(18u, AssetInformation.Parser), 114u);

	private readonly MapField<string, AssetInformation> assets_ = new MapField<string, AssetInformation>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageReferenceMaps> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReferenceMapsReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageReferenceMapUser> Users => users_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageReferenceMapChannel> Channels => channels_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageReferenceMapCommunityRole> Roles => roles_;

	[GeneratedCode("protoc", null)]
	public MapField<string, AssetImage> ImageAssets => imageAssets_;

	[GeneratedCode("protoc", null)]
	public MapField<string, AssetInformation> Assets => assets_;

	[GeneratedCode("protoc", null)]
	public MessageReferenceMaps()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMaps(MessageReferenceMaps other)
		: this()
	{
		users_ = other.users_.Clone();
		channels_ = other.channels_.Clone();
		roles_ = other.roles_.Clone();
		imageAssets_ = other.imageAssets_.Clone();
		assets_ = other.assets_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageReferenceMaps Clone()
	{
		return new MessageReferenceMaps(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageReferenceMaps);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageReferenceMaps other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!users_.Equals(other.users_))
		{
			return false;
		}
		if (!channels_.Equals(other.channels_))
		{
			return false;
		}
		if (!roles_.Equals(other.roles_))
		{
			return false;
		}
		if (!ImageAssets.Equals(other.ImageAssets))
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
		num ^= users_.GetHashCode();
		num ^= channels_.GetHashCode();
		num ^= roles_.GetHashCode();
		num ^= ImageAssets.GetHashCode();
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
		users_.WriteTo(ref P_0, _repeated_users_codec);
		channels_.WriteTo(ref P_0, _repeated_channels_codec);
		roles_.WriteTo(ref P_0, _repeated_roles_codec);
		imageAssets_.WriteTo(ref P_0, _map_imageAssets_codec);
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
		num += users_.CalculateSize(_repeated_users_codec);
		num += channels_.CalculateSize(_repeated_channels_codec);
		num += roles_.CalculateSize(_repeated_roles_codec);
		num += imageAssets_.CalculateSize(_map_imageAssets_codec);
		num += assets_.CalculateSize(_map_assets_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageReferenceMaps other)
	{
		if (other != null)
		{
			users_.Add(other.users_);
			channels_.Add(other.channels_);
			roles_.Add(other.roles_);
			imageAssets_.MergeFrom(other.imageAssets_);
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
			case 82u:
				users_.AddEntriesFrom(ref P_0, _repeated_users_codec);
				break;
			case 90u:
				channels_.AddEntriesFrom(ref P_0, _repeated_channels_codec);
				break;
			case 98u:
				roles_.AddEntriesFrom(ref P_0, _repeated_roles_codec);
				break;
			case 106u:
				imageAssets_.AddEntriesFrom(ref P_0, _map_imageAssets_codec);
				break;
			case 114u:
				assets_.AddEntriesFrom(ref P_0, _map_assets_codec);
				break;
			}
		}
	}
}
