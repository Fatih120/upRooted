using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityInviteLinkGetInfoResponse : IMessage<CommunityInviteLinkGetInfoResponse>, IMessage, IEquatable<CommunityInviteLinkGetInfoResponse>, IDeepCloneable<CommunityInviteLinkGetInfoResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityInviteLinkGetInfoResponse> _parser = new MessageParser<CommunityInviteLinkGetInfoResponse>(() => new CommunityInviteLinkGetInfoResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private string name_ = "";

	private string pictureHex_ = "";

	private static readonly FieldCodec<string> _single_pictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(106u);

	private string pictureAssetUri_;

	private int communityMemberCount_;

	private UserUuid userId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityInviteLinkGetInfoResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LinkReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
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
	public int CommunityMemberCount
	{
		get
		{
			return communityMemberCount_;
		}
		set
		{
			communityMemberCount_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkGetInfoResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkGetInfoResponse(CommunityInviteLinkGetInfoResponse other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		name_ = other.name_;
		pictureHex_ = other.pictureHex_;
		PictureAssetUri = other.PictureAssetUri;
		communityMemberCount_ = other.communityMemberCount_;
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkGetInfoResponse Clone()
	{
		return new CommunityInviteLinkGetInfoResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityInviteLinkGetInfoResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityInviteLinkGetInfoResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
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
		if (CommunityMemberCount != other.CommunityMemberCount)
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
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
		if (CommunityMemberCount != 0)
		{
			num ^= CommunityMemberCount.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Name);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(PictureHex);
		}
		if (pictureAssetUri_ != null)
		{
			_single_pictureAssetUri_codec.WriteTagAndValue(ref P_0, PictureAssetUri);
		}
		if (CommunityMemberCount != 0)
		{
			P_0.WriteRawTag(112);
			P_0.WriteInt32(CommunityMemberCount);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(UserId);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
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
		if (CommunityMemberCount != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(CommunityMemberCount);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityInviteLinkGetInfoResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
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
		if (other.CommunityMemberCount != 0)
		{
			CommunityMemberCount = other.CommunityMemberCount;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
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
			case 82u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				Name = P_0.ReadString();
				break;
			case 98u:
				PictureHex = P_0.ReadString();
				break;
			case 106u:
			{
				string text = _single_pictureAssetUri_codec.Read(ref P_0);
				if (pictureAssetUri_ == null || text != "")
				{
					PictureAssetUri = text;
				}
				break;
			}
			case 112u:
				CommunityMemberCount = P_0.ReadInt32();
				break;
			case 122u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			}
		}
	}
}
