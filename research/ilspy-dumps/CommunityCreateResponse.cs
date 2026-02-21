using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityCreateResponse : IMessage<CommunityCreateResponse>, IMessage, IEquatable<CommunityCreateResponse>, IDeepCloneable<CommunityCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityCreateResponse> _parser = new MessageParser<CommunityCreateResponse>(() => new CommunityCreateResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid id_;

	private UserUuid ownerUserId_;

	private ChannelUuid defaultChannelId_;

	private string name_ = "";

	private string pictureHex_ = "";

	private static readonly FieldCodec<string> _single_pictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(74u);

	private string pictureAssetUri_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid Id
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
	public UserUuid OwnerUserId
	{
		get
		{
			return ownerUserId_;
		}
		set
		{
			ownerUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid DefaultChannelId
	{
		get
		{
			return defaultChannelId_;
		}
		set
		{
			defaultChannelId_ = value;
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
	public string PictureHex
	{
		get
		{
			return pictureHex_;
		}
		set
		{
			pictureHex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureAssetUri
	{
		get
		{
			return pictureAssetUri_;
		}
		set
		{
			pictureAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityCreateResponse(CommunityCreateResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		ownerUserId_ = ((other.ownerUserId_ != null) ? other.ownerUserId_.Clone() : null);
		defaultChannelId_ = ((other.defaultChannelId_ != null) ? other.defaultChannelId_.Clone() : null);
		name_ = other.name_;
		pictureHex_ = other.pictureHex_;
		PictureAssetUri = other.PictureAssetUri;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityCreateResponse Clone()
	{
		return new CommunityCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityCreateResponse other)
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
		if (!object.Equals(OwnerUserId, other.OwnerUserId))
		{
			return false;
		}
		if (!object.Equals(DefaultChannelId, other.DefaultChannelId))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (PictureHex != other.PictureHex)
		{
			return false;
		}
		if (PictureAssetUri != other.PictureAssetUri)
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
		if (ownerUserId_ != null)
		{
			num ^= OwnerUserId.GetHashCode();
		}
		if (defaultChannelId_ != null)
		{
			num ^= DefaultChannelId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (PictureHex.Length != 0)
		{
			num ^= PictureHex.GetHashCode();
		}
		if (pictureAssetUri_ != null)
		{
			num ^= PictureAssetUri.GetHashCode();
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (ownerUserId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(OwnerUserId);
		}
		if (defaultChannelId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(DefaultChannelId);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(Name);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(66);
			P_0.WriteString(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			_single_pictureAssetUri_codec.WriteTagAndValue(ref P_0, PictureAssetUri);
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
		if (ownerUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OwnerUserId);
		}
		if (defaultChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DefaultChannelId);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (PictureHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			num += _single_pictureAssetUri_codec.CalculateSizeWithTag(PictureAssetUri);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityCreateResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.ownerUserId_ != null)
		{
			if (ownerUserId_ == null)
			{
				OwnerUserId = new UserUuid();
			}
			OwnerUserId.MergeFrom(other.OwnerUserId);
		}
		if (other.defaultChannelId_ != null)
		{
			if (defaultChannelId_ == null)
			{
				DefaultChannelId = new ChannelUuid();
			}
			DefaultChannelId.MergeFrom(other.DefaultChannelId);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.PictureHex.Length != 0)
		{
			PictureHex = other.PictureHex;
		}
		if (other.pictureAssetUri_ != null && (pictureAssetUri_ == null || other.PictureAssetUri != ""))
		{
			PictureAssetUri = other.PictureAssetUri;
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
			case 34u:
				if (id_ == null)
				{
					Id = new CommunityUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (ownerUserId_ == null)
				{
					OwnerUserId = new UserUuid();
				}
				P_0.ReadMessage(OwnerUserId);
				break;
			case 50u:
				if (defaultChannelId_ == null)
				{
					DefaultChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(DefaultChannelId);
				break;
			case 58u:
				Name = P_0.ReadString();
				break;
			case 66u:
				PictureHex = P_0.ReadString();
				break;
			case 74u:
			{
				string text = _single_pictureAssetUri_codec.Read(ref P_0);
				if (pictureAssetUri_ == null || text != "")
				{
					PictureAssetUri = text;
				}
				break;
			}
			}
		}
	}
}
