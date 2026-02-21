using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppPreviewResponse : IMessage<CommunityAppPreviewResponse>, IMessage, IEquatable<CommunityAppPreviewResponse>, IDeepCloneable<CommunityAppPreviewResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppPreviewResponse> _parser = new MessageParser<CommunityAppPreviewResponse>(() => new CommunityAppPreviewResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityAppUuid id_;

	private AppUuid appId_;

	private AppType appType_ = AppType.Unspecified;

	private string name_ = "";

	private string version_ = "";

	private AppDeploymentStatus appDeploymentStatus_ = AppDeploymentStatus.Unspecified;

	private AppVersionUuid appVersionId_;

	private int price_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppPreviewResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityAppUuid Id
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
	public AppUuid AppId
	{
		get
		{
			return appId_;
		}
		set
		{
			appId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppType AppType
	{
		get
		{
			return appType_;
		}
		set
		{
			appType_ = value;
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
	public string Version
	{
		get
		{
			return version_;
		}
		set
		{
			version_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public AppDeploymentStatus AppDeploymentStatus
	{
		get
		{
			return appDeploymentStatus_;
		}
		set
		{
			appDeploymentStatus_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppVersionUuid AppVersionId
	{
		get
		{
			return appVersionId_;
		}
		set
		{
			appVersionId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Price
	{
		get
		{
			return price_;
		}
		set
		{
			price_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppPreviewResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppPreviewResponse(CommunityAppPreviewResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		appType_ = other.appType_;
		name_ = other.name_;
		version_ = other.version_;
		appDeploymentStatus_ = other.appDeploymentStatus_;
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		price_ = other.price_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppPreviewResponse Clone()
	{
		return new CommunityAppPreviewResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppPreviewResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppPreviewResponse other)
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
		if (!object.Equals(AppId, other.AppId))
		{
			return false;
		}
		if (AppType != other.AppType)
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (Version != other.Version)
		{
			return false;
		}
		if (AppDeploymentStatus != other.AppDeploymentStatus)
		{
			return false;
		}
		if (!object.Equals(AppVersionId, other.AppVersionId))
		{
			return false;
		}
		if (Price != other.Price)
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
		if (appId_ != null)
		{
			num ^= AppId.GetHashCode();
		}
		if (AppType != AppType.Unspecified)
		{
			num ^= AppType.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			num ^= AppDeploymentStatus.GetHashCode();
		}
		if (appVersionId_ != null)
		{
			num ^= AppVersionId.GetHashCode();
		}
		if (Price != 0)
		{
			num ^= Price.GetHashCode();
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
		if (appId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(AppId);
		}
		if (AppType != AppType.Unspecified)
		{
			P_0.WriteRawTag(56);
			P_0.WriteEnum((int)AppType);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(66);
			P_0.WriteString(Name);
		}
		if (Version.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Version);
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			P_0.WriteRawTag(88);
			P_0.WriteEnum((int)AppDeploymentStatus);
		}
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(AppVersionId);
		}
		if (Price != 0)
		{
			P_0.WriteRawTag(104);
			P_0.WriteInt32(Price);
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
		if (appId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppId);
		}
		if (AppType != AppType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppType);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppDeploymentStatus);
		}
		if (appVersionId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppVersionId);
		}
		if (Price != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Price);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppPreviewResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityAppUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.appId_ != null)
		{
			if (appId_ == null)
			{
				AppId = new AppUuid();
			}
			AppId.MergeFrom(other.AppId);
		}
		if (other.AppType != AppType.Unspecified)
		{
			AppType = other.AppType;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Version.Length != 0)
		{
			Version = other.Version;
		}
		if (other.AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			AppDeploymentStatus = other.AppDeploymentStatus;
		}
		if (other.appVersionId_ != null)
		{
			if (appVersionId_ == null)
			{
				AppVersionId = new AppVersionUuid();
			}
			AppVersionId.MergeFrom(other.AppVersionId);
		}
		if (other.Price != 0)
		{
			Price = other.Price;
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
					Id = new CommunityAppUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 50u:
				if (appId_ == null)
				{
					AppId = new AppUuid();
				}
				P_0.ReadMessage(AppId);
				break;
			case 56u:
				AppType = (AppType)P_0.ReadEnum();
				break;
			case 66u:
				Name = P_0.ReadString();
				break;
			case 82u:
				Version = P_0.ReadString();
				break;
			case 88u:
				AppDeploymentStatus = (AppDeploymentStatus)P_0.ReadEnum();
				break;
			case 98u:
				if (appVersionId_ == null)
				{
					AppVersionId = new AppVersionUuid();
				}
				P_0.ReadMessage(AppVersionId);
				break;
			case 104u:
				Price = P_0.ReadInt32();
				break;
			}
		}
	}
}
