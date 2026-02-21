using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppChannel : IMessage<CommunityAppChannel>, IMessage, IEquatable<CommunityAppChannel>, IDeepCloneable<CommunityAppChannel>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppChannel> _parser = new MessageParser<CommunityAppChannel>(() => new CommunityAppChannel());

	private UnknownFieldSet _unknownFields;

	private ChannelUuid id_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_iconAssetUri_codec = FieldCodec.ForClassWrapper<string>(26u);

	private string iconAssetUri_;

	private int channelType_;

	private float position_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppChannel> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[13];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ChannelUuid Id
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
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string IconAssetUri
	{
		get
		{
			return iconAssetUri_;
		}
		set
		{
			iconAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int ChannelType
	{
		get
		{
			return channelType_;
		}
		set
		{
			channelType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public float Position
	{
		get
		{
			return position_;
		}
		set
		{
			position_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannel()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannel(CommunityAppChannel other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		IconAssetUri = other.IconAssetUri;
		channelType_ = other.channelType_;
		position_ = other.position_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannel Clone()
	{
		return new CommunityAppChannel(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppChannel);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppChannel other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (IconAssetUri != other.IconAssetUri)
		{
			return false;
		}
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (iconAssetUri_ != null)
		{
			num ^= IconAssetUri.GetHashCode();
		}
		if (ChannelType != 0)
		{
			num ^= ChannelType.GetHashCode();
		}
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
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
		if (id_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Id);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(Name);
		}
		if (iconAssetUri_ != null)
		{
			_single_iconAssetUri_codec.WriteTagAndValue(ref P_0, IconAssetUri);
		}
		if (ChannelType != 0)
		{
			P_0.WriteRawTag(32);
			P_0.WriteInt32(ChannelType);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(45);
			P_0.WriteFloat(Position);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (iconAssetUri_ != null)
		{
			num += _single_iconAssetUri_codec.CalculateSizeWithTag(IconAssetUri);
		}
		if (ChannelType != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(ChannelType);
		}
		if (Position != 0f)
		{
			num += 5;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppChannel other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.iconAssetUri_ != null && (iconAssetUri_ == null || other.IconAssetUri != ""))
		{
			IconAssetUri = other.IconAssetUri;
		}
		if (other.ChannelType != 0)
		{
			ChannelType = other.ChannelType;
		}
		if (other.Position != 0f)
		{
			Position = other.Position;
		}
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
			case 10u:
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 18u:
				Name = P_0.ReadString();
				break;
			case 26u:
			{
				string text = _single_iconAssetUri_codec.Read(ref P_0);
				if (iconAssetUri_ == null || text != "")
				{
					IconAssetUri = text;
				}
				break;
			}
			case 32u:
				ChannelType = P_0.ReadInt32();
				break;
			case 45u:
				Position = P_0.ReadFloat();
				break;
			}
		}
	}
}
