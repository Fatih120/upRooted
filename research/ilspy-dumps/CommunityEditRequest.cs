using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityEditRequest : IMessage<CommunityEditRequest>, IMessage, IEquatable<CommunityEditRequest>, IDeepCloneable<CommunityEditRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityEditRequest> _parser = new MessageParser<CommunityEditRequest>(() => new CommunityEditRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private string name_ = "";

	private string pictureHex_ = "";

	private bool updatePicture_;

	private static readonly FieldCodec<string> _single_pictureTokenUri_codec = FieldCodec.ForClassWrapper<string>(114u);

	private string pictureTokenUri_;

	private ChannelUuid defaultChannelId_;

	private bool rejectUnverifiedEmail_;

	private CommunityJoinThrottle joinThrottle_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityEditRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

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
	public bool UpdatePicture
	{
		get
		{
			return updatePicture_;
		}
		set
		{
			updatePicture_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureTokenUri
	{
		get
		{
			return pictureTokenUri_;
		}
		set
		{
			pictureTokenUri_ = value;
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
	public bool RejectUnverifiedEmail
	{
		get
		{
			return rejectUnverifiedEmail_;
		}
		set
		{
			rejectUnverifiedEmail_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityJoinThrottle JoinThrottle
	{
		get
		{
			return joinThrottle_;
		}
		set
		{
			joinThrottle_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityEditRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityEditRequest(CommunityEditRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		name_ = other.name_;
		pictureHex_ = other.pictureHex_;
		updatePicture_ = other.updatePicture_;
		PictureTokenUri = other.PictureTokenUri;
		defaultChannelId_ = ((other.defaultChannelId_ != null) ? other.defaultChannelId_.Clone() : null);
		rejectUnverifiedEmail_ = other.rejectUnverifiedEmail_;
		joinThrottle_ = ((other.joinThrottle_ != null) ? other.joinThrottle_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityEditRequest Clone()
	{
		return new CommunityEditRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityEditRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityEditRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
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
		if (UpdatePicture != other.UpdatePicture)
		{
			return false;
		}
		if (PictureTokenUri != other.PictureTokenUri)
		{
			return false;
		}
		if (!object.Equals(DefaultChannelId, other.DefaultChannelId))
		{
			return false;
		}
		if (RejectUnverifiedEmail != other.RejectUnverifiedEmail)
		{
			return false;
		}
		if (!object.Equals(JoinThrottle, other.JoinThrottle))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
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
		if (UpdatePicture)
		{
			num ^= UpdatePicture.GetHashCode();
		}
		if (pictureTokenUri_ != null)
		{
			num ^= PictureTokenUri.GetHashCode();
		}
		if (defaultChannelId_ != null)
		{
			num ^= DefaultChannelId.GetHashCode();
		}
		if (RejectUnverifiedEmail)
		{
			num ^= RejectUnverifiedEmail.GetHashCode();
		}
		if (joinThrottle_ != null)
		{
			num ^= JoinThrottle.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
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
		if (UpdatePicture)
		{
			P_0.WriteRawTag(104);
			P_0.WriteBool(UpdatePicture);
		}
		if (pictureTokenUri_ != null)
		{
			_single_pictureTokenUri_codec.WriteTagAndValue(ref P_0, PictureTokenUri);
		}
		if (defaultChannelId_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(DefaultChannelId);
		}
		if (RejectUnverifiedEmail)
		{
			P_0.WriteRawTag(128, 1);
			P_0.WriteBool(RejectUnverifiedEmail);
		}
		if (joinThrottle_ != null)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(JoinThrottle);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
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
		if (UpdatePicture)
		{
			num += 2;
		}
		if (pictureTokenUri_ != null)
		{
			num += _single_pictureTokenUri_codec.CalculateSizeWithTag(PictureTokenUri);
		}
		if (defaultChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DefaultChannelId);
		}
		if (RejectUnverifiedEmail)
		{
			num += 3;
		}
		if (joinThrottle_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(JoinThrottle);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityEditRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
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
		if (other.UpdatePicture)
		{
			UpdatePicture = other.UpdatePicture;
		}
		if (other.pictureTokenUri_ != null && (pictureTokenUri_ == null || other.PictureTokenUri != ""))
		{
			PictureTokenUri = other.PictureTokenUri;
		}
		if (other.defaultChannelId_ != null)
		{
			if (defaultChannelId_ == null)
			{
				DefaultChannelId = new ChannelUuid();
			}
			DefaultChannelId.MergeFrom(other.DefaultChannelId);
		}
		if (other.RejectUnverifiedEmail)
		{
			RejectUnverifiedEmail = other.RejectUnverifiedEmail;
		}
		if (other.joinThrottle_ != null)
		{
			if (joinThrottle_ == null)
			{
				JoinThrottle = new CommunityJoinThrottle();
			}
			JoinThrottle.MergeFrom(other.JoinThrottle);
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
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
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
			case 104u:
				UpdatePicture = P_0.ReadBool();
				break;
			case 114u:
			{
				string text = _single_pictureTokenUri_codec.Read(ref P_0);
				if (pictureTokenUri_ == null || text != "")
				{
					PictureTokenUri = text;
				}
				break;
			}
			case 122u:
				if (defaultChannelId_ == null)
				{
					DefaultChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(DefaultChannelId);
				break;
			case 128u:
				RejectUnverifiedEmail = P_0.ReadBool();
				break;
			case 138u:
				if (joinThrottle_ == null)
				{
					JoinThrottle = new CommunityJoinThrottle();
				}
				P_0.ReadMessage(JoinThrottle);
				break;
			}
		}
	}
}
