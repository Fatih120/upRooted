using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.App.Settings;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppGetResponse : IMessage<CommunityAppGetResponse>, IMessage, IEquatable<CommunityAppGetResponse>, IDeepCloneable<CommunityAppGetResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppGetResponse> _parser = new MessageParser<CommunityAppGetResponse>(() => new CommunityAppGetResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityAppUuid id_;

	private AppUuid appId_;

	private AppType appType_ = AppType.Unspecified;

	private string name_ = "";

	private AppVersionUuid appVersionId_;

	private string version_ = "";

	private AppDeploymentStatus appDeploymentStatus_ = AppDeploymentStatus.Unspecified;

	private string bannerAssetUri_ = "";

	private static readonly FieldCodec<string> _single_clientAssetUri_codec = FieldCodec.ForClassWrapper<string>(114u);

	private string clientAssetUri_;

	private string description_ = "";

	private CommunityPermission communityPermission_;

	private ChannelPermission channelPermission_;

	private int price_;

	private AppOrganization appOrganization_;

	private bool canAppOrganizationAccessLogs_;

	private CommunityAppChannelResponse channel_;

	private GlobalSettings settings_;

	private bool canRate_;

	private CommunityAppVersionUpdate update_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppGetResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[1];

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
	public string BannerAssetUri
	{
		get
		{
			return bannerAssetUri_;
		}
		set
		{
			bannerAssetUri_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string ClientAssetUri
	{
		get
		{
			return clientAssetUri_;
		}
		set
		{
			clientAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Description
	{
		get
		{
			return description_;
		}
		set
		{
			description_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermission CommunityPermission
	{
		get
		{
			return communityPermission_;
		}
		set
		{
			communityPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelPermission ChannelPermission
	{
		get
		{
			return channelPermission_;
		}
		set
		{
			channelPermission_ = value;
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
	public AppOrganization AppOrganization
	{
		get
		{
			return appOrganization_;
		}
		set
		{
			appOrganization_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool CanAppOrganizationAccessLogs
	{
		get
		{
			return canAppOrganizationAccessLogs_;
		}
		set
		{
			canAppOrganizationAccessLogs_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannelResponse Channel
	{
		get
		{
			return channel_;
		}
		set
		{
			channel_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettings Settings
	{
		get
		{
			return settings_;
		}
		set
		{
			settings_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool CanRate
	{
		get
		{
			return canRate_;
		}
		set
		{
			canRate_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdate Update
	{
		get
		{
			return update_;
		}
		set
		{
			update_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetResponse(CommunityAppGetResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		appType_ = other.appType_;
		name_ = other.name_;
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		version_ = other.version_;
		appDeploymentStatus_ = other.appDeploymentStatus_;
		bannerAssetUri_ = other.bannerAssetUri_;
		ClientAssetUri = other.ClientAssetUri;
		description_ = other.description_;
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		price_ = other.price_;
		appOrganization_ = ((other.appOrganization_ != null) ? other.appOrganization_.Clone() : null);
		canAppOrganizationAccessLogs_ = other.canAppOrganizationAccessLogs_;
		channel_ = ((other.channel_ != null) ? other.channel_.Clone() : null);
		settings_ = ((other.settings_ != null) ? other.settings_.Clone() : null);
		canRate_ = other.canRate_;
		update_ = ((other.update_ != null) ? other.update_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetResponse Clone()
	{
		return new CommunityAppGetResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppGetResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppGetResponse other)
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
		if (!object.Equals(AppVersionId, other.AppVersionId))
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
		if (BannerAssetUri != other.BannerAssetUri)
		{
			return false;
		}
		if (ClientAssetUri != other.ClientAssetUri)
		{
			return false;
		}
		if (Description != other.Description)
		{
			return false;
		}
		if (!object.Equals(CommunityPermission, other.CommunityPermission))
		{
			return false;
		}
		if (!object.Equals(ChannelPermission, other.ChannelPermission))
		{
			return false;
		}
		if (Price != other.Price)
		{
			return false;
		}
		if (!object.Equals(AppOrganization, other.AppOrganization))
		{
			return false;
		}
		if (CanAppOrganizationAccessLogs != other.CanAppOrganizationAccessLogs)
		{
			return false;
		}
		if (!object.Equals(Channel, other.Channel))
		{
			return false;
		}
		if (!object.Equals(Settings, other.Settings))
		{
			return false;
		}
		if (CanRate != other.CanRate)
		{
			return false;
		}
		if (!object.Equals(Update, other.Update))
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
		if (appVersionId_ != null)
		{
			num ^= AppVersionId.GetHashCode();
		}
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			num ^= AppDeploymentStatus.GetHashCode();
		}
		if (BannerAssetUri.Length != 0)
		{
			num ^= BannerAssetUri.GetHashCode();
		}
		if (clientAssetUri_ != null)
		{
			num ^= ClientAssetUri.GetHashCode();
		}
		if (Description.Length != 0)
		{
			num ^= Description.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
		}
		if (Price != 0)
		{
			num ^= Price.GetHashCode();
		}
		if (appOrganization_ != null)
		{
			num ^= AppOrganization.GetHashCode();
		}
		if (CanAppOrganizationAccessLogs)
		{
			num ^= CanAppOrganizationAccessLogs.GetHashCode();
		}
		if (channel_ != null)
		{
			num ^= Channel.GetHashCode();
		}
		if (settings_ != null)
		{
			num ^= Settings.GetHashCode();
		}
		if (CanRate)
		{
			num ^= CanRate.GetHashCode();
		}
		if (update_ != null)
		{
			num ^= Update.GetHashCode();
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
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(AppVersionId);
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
		if (BannerAssetUri.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(BannerAssetUri);
		}
		if (clientAssetUri_ != null)
		{
			_single_clientAssetUri_codec.WriteTagAndValue(ref P_0, ClientAssetUri);
		}
		if (Description.Length != 0)
		{
			P_0.WriteRawTag(122);
			P_0.WriteString(Description);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(ChannelPermission);
		}
		if (Price != 0)
		{
			P_0.WriteRawTag(160, 1);
			P_0.WriteInt32(Price);
		}
		if (appOrganization_ != null)
		{
			P_0.WriteRawTag(194, 1);
			P_0.WriteMessage(AppOrganization);
		}
		if (CanAppOrganizationAccessLogs)
		{
			P_0.WriteRawTag(200, 1);
			P_0.WriteBool(CanAppOrganizationAccessLogs);
		}
		if (channel_ != null)
		{
			P_0.WriteRawTag(210, 1);
			P_0.WriteMessage(Channel);
		}
		if (settings_ != null)
		{
			P_0.WriteRawTag(242, 1);
			P_0.WriteMessage(Settings);
		}
		if (CanRate)
		{
			P_0.WriteRawTag(192, 2);
			P_0.WriteBool(CanRate);
		}
		if (update_ != null)
		{
			P_0.WriteRawTag(146, 3);
			P_0.WriteMessage(Update);
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
		if (appVersionId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppVersionId);
		}
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppDeploymentStatus);
		}
		if (BannerAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BannerAssetUri);
		}
		if (clientAssetUri_ != null)
		{
			num += _single_clientAssetUri_codec.CalculateSizeWithTag(ClientAssetUri);
		}
		if (Description.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Description);
		}
		if (communityPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (Price != 0)
		{
			num += 2 + CodedOutputStream.ComputeInt32Size(Price);
		}
		if (appOrganization_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AppOrganization);
		}
		if (CanAppOrganizationAccessLogs)
		{
			num += 3;
		}
		if (channel_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Channel);
		}
		if (settings_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Settings);
		}
		if (CanRate)
		{
			num += 3;
		}
		if (update_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Update);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppGetResponse other)
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
		if (other.appVersionId_ != null)
		{
			if (appVersionId_ == null)
			{
				AppVersionId = new AppVersionUuid();
			}
			AppVersionId.MergeFrom(other.AppVersionId);
		}
		if (other.Version.Length != 0)
		{
			Version = other.Version;
		}
		if (other.AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			AppDeploymentStatus = other.AppDeploymentStatus;
		}
		if (other.BannerAssetUri.Length != 0)
		{
			BannerAssetUri = other.BannerAssetUri;
		}
		if (other.clientAssetUri_ != null && (clientAssetUri_ == null || other.ClientAssetUri != ""))
		{
			ClientAssetUri = other.ClientAssetUri;
		}
		if (other.Description.Length != 0)
		{
			Description = other.Description;
		}
		if (other.communityPermission_ != null)
		{
			if (communityPermission_ == null)
			{
				CommunityPermission = new CommunityPermission();
			}
			CommunityPermission.MergeFrom(other.CommunityPermission);
		}
		if (other.channelPermission_ != null)
		{
			if (channelPermission_ == null)
			{
				ChannelPermission = new ChannelPermission();
			}
			ChannelPermission.MergeFrom(other.ChannelPermission);
		}
		if (other.Price != 0)
		{
			Price = other.Price;
		}
		if (other.appOrganization_ != null)
		{
			if (appOrganization_ == null)
			{
				AppOrganization = new AppOrganization();
			}
			AppOrganization.MergeFrom(other.AppOrganization);
		}
		if (other.CanAppOrganizationAccessLogs)
		{
			CanAppOrganizationAccessLogs = other.CanAppOrganizationAccessLogs;
		}
		if (other.channel_ != null)
		{
			if (channel_ == null)
			{
				Channel = new CommunityAppChannelResponse();
			}
			Channel.MergeFrom(other.Channel);
		}
		if (other.settings_ != null)
		{
			if (settings_ == null)
			{
				Settings = new GlobalSettings();
			}
			Settings.MergeFrom(other.Settings);
		}
		if (other.CanRate)
		{
			CanRate = other.CanRate;
		}
		if (other.update_ != null)
		{
			if (update_ == null)
			{
				Update = new CommunityAppVersionUpdate();
			}
			Update.MergeFrom(other.Update);
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
			case 74u:
				if (appVersionId_ == null)
				{
					AppVersionId = new AppVersionUuid();
				}
				P_0.ReadMessage(AppVersionId);
				break;
			case 82u:
				Version = P_0.ReadString();
				break;
			case 88u:
				AppDeploymentStatus = (AppDeploymentStatus)P_0.ReadEnum();
				break;
			case 106u:
				BannerAssetUri = P_0.ReadString();
				break;
			case 114u:
			{
				string text = _single_clientAssetUri_codec.Read(ref P_0);
				if (clientAssetUri_ == null || text != "")
				{
					ClientAssetUri = text;
				}
				break;
			}
			case 122u:
				Description = P_0.ReadString();
				break;
			case 130u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 138u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 160u:
				Price = P_0.ReadInt32();
				break;
			case 194u:
				if (appOrganization_ == null)
				{
					AppOrganization = new AppOrganization();
				}
				P_0.ReadMessage(AppOrganization);
				break;
			case 200u:
				CanAppOrganizationAccessLogs = P_0.ReadBool();
				break;
			case 210u:
				if (channel_ == null)
				{
					Channel = new CommunityAppChannelResponse();
				}
				P_0.ReadMessage(Channel);
				break;
			case 242u:
				if (settings_ == null)
				{
					Settings = new GlobalSettings();
				}
				P_0.ReadMessage(Settings);
				break;
			case 320u:
				CanRate = P_0.ReadBool();
				break;
			case 402u:
				if (update_ == null)
				{
					Update = new CommunityAppVersionUpdate();
				}
				P_0.ReadMessage(Update);
				break;
			}
		}
	}
}
