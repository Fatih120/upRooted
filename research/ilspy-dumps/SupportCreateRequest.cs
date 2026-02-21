using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class SupportCreateRequest : IMessage<SupportCreateRequest>, IMessage, IEquatable<SupportCreateRequest>, IDeepCloneable<SupportCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<SupportCreateRequest> _parser = new MessageParser<SupportCreateRequest>(() => new SupportCreateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private bool allowContact_;

	private string subject_ = "";

	private string comment_ = "";

	private static readonly FieldCodec<string> _single_category_codec = FieldCodec.ForClassWrapper<string>(242u);

	private string category_;

	private static readonly FieldCodec<string> _single_platform_codec = FieldCodec.ForClassWrapper<string>(250u);

	private string platform_;

	private static readonly FieldCodec<string> _single_architecture_codec = FieldCodec.ForClassWrapper<string>(258u);

	private string architecture_;

	private static readonly FieldCodec<string> _single_osVersion_codec = FieldCodec.ForClassWrapper<string>(266u);

	private string osVersion_;

	private static readonly FieldCodec<string> _single_clientVersion_codec = FieldCodec.ForClassWrapper<string>(274u);

	private string clientVersion_;

	private static readonly FieldCodec<string> _single_source_codec = FieldCodec.ForClassWrapper<string>(282u);

	private string source_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<SupportCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SupportReflection.Descriptor.MessageTypes[0];

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
	public bool AllowContact
	{
		get
		{
			return allowContact_;
		}
		set
		{
			allowContact_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Subject
	{
		get
		{
			return subject_;
		}
		set
		{
			subject_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Comment
	{
		get
		{
			return comment_;
		}
		set
		{
			comment_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Category
	{
		get
		{
			return category_;
		}
		set
		{
			category_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Platform
	{
		get
		{
			return platform_;
		}
		set
		{
			platform_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Architecture
	{
		get
		{
			return architecture_;
		}
		set
		{
			architecture_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string OsVersion
	{
		get
		{
			return osVersion_;
		}
		set
		{
			osVersion_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ClientVersion
	{
		get
		{
			return clientVersion_;
		}
		set
		{
			clientVersion_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Source
	{
		get
		{
			return source_;
		}
		set
		{
			source_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public SupportCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public SupportCreateRequest(SupportCreateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		allowContact_ = other.allowContact_;
		subject_ = other.subject_;
		comment_ = other.comment_;
		Category = other.Category;
		Platform = other.Platform;
		Architecture = other.Architecture;
		OsVersion = other.OsVersion;
		ClientVersion = other.ClientVersion;
		Source = other.Source;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public SupportCreateRequest Clone()
	{
		return new SupportCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SupportCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(SupportCreateRequest other)
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
		if (AllowContact != other.AllowContact)
		{
			return false;
		}
		if (Subject != other.Subject)
		{
			return false;
		}
		if (Comment != other.Comment)
		{
			return false;
		}
		if (Category != other.Category)
		{
			return false;
		}
		if (Platform != other.Platform)
		{
			return false;
		}
		if (Architecture != other.Architecture)
		{
			return false;
		}
		if (OsVersion != other.OsVersion)
		{
			return false;
		}
		if (ClientVersion != other.ClientVersion)
		{
			return false;
		}
		if (Source != other.Source)
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
		if (AllowContact)
		{
			num ^= AllowContact.GetHashCode();
		}
		if (Subject.Length != 0)
		{
			num ^= Subject.GetHashCode();
		}
		if (Comment.Length != 0)
		{
			num ^= Comment.GetHashCode();
		}
		if (category_ != null)
		{
			num ^= Category.GetHashCode();
		}
		if (platform_ != null)
		{
			num ^= Platform.GetHashCode();
		}
		if (architecture_ != null)
		{
			num ^= Architecture.GetHashCode();
		}
		if (osVersion_ != null)
		{
			num ^= OsVersion.GetHashCode();
		}
		if (clientVersion_ != null)
		{
			num ^= ClientVersion.GetHashCode();
		}
		if (source_ != null)
		{
			num ^= Source.GetHashCode();
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
		if (AllowContact)
		{
			P_0.WriteRawTag(88);
			P_0.WriteBool(AllowContact);
		}
		if (Subject.Length != 0)
		{
			P_0.WriteRawTag(162, 1);
			P_0.WriteString(Subject);
		}
		if (Comment.Length != 0)
		{
			P_0.WriteRawTag(170, 1);
			P_0.WriteString(Comment);
		}
		if (category_ != null)
		{
			_single_category_codec.WriteTagAndValue(ref P_0, Category);
		}
		if (platform_ != null)
		{
			_single_platform_codec.WriteTagAndValue(ref P_0, Platform);
		}
		if (architecture_ != null)
		{
			_single_architecture_codec.WriteTagAndValue(ref P_0, Architecture);
		}
		if (osVersion_ != null)
		{
			_single_osVersion_codec.WriteTagAndValue(ref P_0, OsVersion);
		}
		if (clientVersion_ != null)
		{
			_single_clientVersion_codec.WriteTagAndValue(ref P_0, ClientVersion);
		}
		if (source_ != null)
		{
			_single_source_codec.WriteTagAndValue(ref P_0, Source);
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
		if (AllowContact)
		{
			num += 2;
		}
		if (Subject.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Subject);
		}
		if (Comment.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Comment);
		}
		if (category_ != null)
		{
			num += _single_category_codec.CalculateSizeWithTag(Category);
		}
		if (platform_ != null)
		{
			num += _single_platform_codec.CalculateSizeWithTag(Platform);
		}
		if (architecture_ != null)
		{
			num += _single_architecture_codec.CalculateSizeWithTag(Architecture);
		}
		if (osVersion_ != null)
		{
			num += _single_osVersion_codec.CalculateSizeWithTag(OsVersion);
		}
		if (clientVersion_ != null)
		{
			num += _single_clientVersion_codec.CalculateSizeWithTag(ClientVersion);
		}
		if (source_ != null)
		{
			num += _single_source_codec.CalculateSizeWithTag(Source);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(SupportCreateRequest other)
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
		if (other.AllowContact)
		{
			AllowContact = other.AllowContact;
		}
		if (other.Subject.Length != 0)
		{
			Subject = other.Subject;
		}
		if (other.Comment.Length != 0)
		{
			Comment = other.Comment;
		}
		if (other.category_ != null && (category_ == null || other.Category != ""))
		{
			Category = other.Category;
		}
		if (other.platform_ != null && (platform_ == null || other.Platform != ""))
		{
			Platform = other.Platform;
		}
		if (other.architecture_ != null && (architecture_ == null || other.Architecture != ""))
		{
			Architecture = other.Architecture;
		}
		if (other.osVersion_ != null && (osVersion_ == null || other.OsVersion != ""))
		{
			OsVersion = other.OsVersion;
		}
		if (other.clientVersion_ != null && (clientVersion_ == null || other.ClientVersion != ""))
		{
			ClientVersion = other.ClientVersion;
		}
		if (other.source_ != null && (source_ == null || other.Source != ""))
		{
			Source = other.Source;
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
			case 88u:
				AllowContact = P_0.ReadBool();
				break;
			case 162u:
				Subject = P_0.ReadString();
				break;
			case 170u:
				Comment = P_0.ReadString();
				break;
			case 242u:
			{
				string text4 = _single_category_codec.Read(ref P_0);
				if (category_ == null || text4 != "")
				{
					Category = text4;
				}
				break;
			}
			case 250u:
			{
				string text2 = _single_platform_codec.Read(ref P_0);
				if (platform_ == null || text2 != "")
				{
					Platform = text2;
				}
				break;
			}
			case 258u:
			{
				string text6 = _single_architecture_codec.Read(ref P_0);
				if (architecture_ == null || text6 != "")
				{
					Architecture = text6;
				}
				break;
			}
			case 266u:
			{
				string text5 = _single_osVersion_codec.Read(ref P_0);
				if (osVersion_ == null || text5 != "")
				{
					OsVersion = text5;
				}
				break;
			}
			case 274u:
			{
				string text3 = _single_clientVersion_codec.Read(ref P_0);
				if (clientVersion_ == null || text3 != "")
				{
					ClientVersion = text3;
				}
				break;
			}
			case 282u:
			{
				string text = _single_source_codec.Read(ref P_0);
				if (source_ == null || text != "")
				{
					Source = text;
				}
				break;
			}
			}
		}
	}
}
