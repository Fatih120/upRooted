using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityCreateRequest : IMessage<CommunityCreateRequest>, IMessage, IEquatable<CommunityCreateRequest>, IDeepCloneable<CommunityCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityCreateRequest> _parser = new MessageParser<CommunityCreateRequest>(() => new CommunityCreateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private string name_ = "";

	private string pictureHex_ = "";

	private string templateType_ = "";

	private static readonly FieldCodec<string> _single_iconUploadTokenUri_codec = FieldCodec.ForClassWrapper<string>(106u);

	private string iconUploadTokenUri_;

	private bool rejectUnverifiedEmail_;

	private CommunityJoinThrottle joinThrottle_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[0];

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
	public string TemplateType
	{
		get
		{
			return templateType_;
		}
		set
		{
			templateType_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string IconUploadTokenUri
	{
		get
		{
			return iconUploadTokenUri_;
		}
		set
		{
			iconUploadTokenUri_ = value;
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
	public CommunityCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityCreateRequest(CommunityCreateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		name_ = other.name_;
		pictureHex_ = other.pictureHex_;
		templateType_ = other.templateType_;
		IconUploadTokenUri = other.IconUploadTokenUri;
		rejectUnverifiedEmail_ = other.rejectUnverifiedEmail_;
		joinThrottle_ = ((other.joinThrottle_ != null) ? other.joinThrottle_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityCreateRequest Clone()
	{
		return new CommunityCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityCreateRequest other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (PictureHex != other.PictureHex)
		{
			return false;
		}
		if (TemplateType != other.TemplateType)
		{
			return false;
		}
		if (IconUploadTokenUri != other.IconUploadTokenUri)
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (PictureHex.Length != 0)
		{
			num ^= PictureHex.GetHashCode();
		}
		if (TemplateType.Length != 0)
		{
			num ^= TemplateType.GetHashCode();
		}
		if (iconUploadTokenUri_ != null)
		{
			num ^= IconUploadTokenUri.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Name);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(PictureHex);
		}
		if (TemplateType.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(TemplateType);
		}
		if (iconUploadTokenUri_ != null)
		{
			_single_iconUploadTokenUri_codec.WriteTagAndValue(ref P_0, IconUploadTokenUri);
		}
		if (RejectUnverifiedEmail)
		{
			P_0.WriteRawTag(112);
			P_0.WriteBool(RejectUnverifiedEmail);
		}
		if (joinThrottle_ != null)
		{
			P_0.WriteRawTag(122);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (PictureHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureHex);
		}
		if (TemplateType.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TemplateType);
		}
		if (iconUploadTokenUri_ != null)
		{
			num += _single_iconUploadTokenUri_codec.CalculateSizeWithTag(IconUploadTokenUri);
		}
		if (RejectUnverifiedEmail)
		{
			num += 2;
		}
		if (joinThrottle_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(JoinThrottle);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityCreateRequest other)
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
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.PictureHex.Length != 0)
		{
			PictureHex = other.PictureHex;
		}
		if (other.TemplateType.Length != 0)
		{
			TemplateType = other.TemplateType;
		}
		if (other.iconUploadTokenUri_ != null && (iconUploadTokenUri_ == null || other.IconUploadTokenUri != ""))
		{
			IconUploadTokenUri = other.IconUploadTokenUri;
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
				Name = P_0.ReadString();
				break;
			case 90u:
				PictureHex = P_0.ReadString();
				break;
			case 98u:
				TemplateType = P_0.ReadString();
				break;
			case 106u:
			{
				string text = _single_iconUploadTokenUri_codec.Read(ref P_0);
				if (iconUploadTokenUri_ == null || text != "")
				{
					IconUploadTokenUri = text;
				}
				break;
			}
			case 112u:
				RejectUnverifiedEmail = P_0.ReadBool();
				break;
			case 122u:
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
