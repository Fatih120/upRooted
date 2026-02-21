using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.App.Settings;
using RootApp.AppServices.Grpc;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AppStoreGetResponse : IMessage<AppStoreGetResponse>, IMessage, IEquatable<AppStoreGetResponse>, IDeepCloneable<AppStoreGetResponse>, IBufferMessage
{
	private static readonly MessageParser<AppStoreGetResponse> _parser = new MessageParser<AppStoreGetResponse>(() => new AppStoreGetResponse());

	private UnknownFieldSet _unknownFields;

	private AppUuid id_;

	private AppOrganization appOrganization_;

	private AppCategory appCategory_ = AppCategory.Unspecified;

	private AppType appType_ = AppType.Unspecified;

	private string name_ = "";

	private string description_ = "";

	private float rating_;

	private int price_;

	private string iconAssetUri_ = "";

	private string bannerAssetUri_ = "";

	private static readonly FieldCodec<string> _repeated_screenshotAssetUris_codec = FieldCodec.ForString(114u);

	private readonly RepeatedField<string> screenshotAssetUris_ = new RepeatedField<string>();

	private AppStoreVersionResponse appVersion_;

	private CommunityPermission communityPermission_;

	private ChannelPermission channelPermission_;

	private AppPermission appPermission_;

	private GlobalSettings globalSettings_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreGetResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public AppUuid Id
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
	public AppCategory AppCategory
	{
		get
		{
			return appCategory_;
		}
		set
		{
			appCategory_ = value;
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
	public float Rating
	{
		get
		{
			return rating_;
		}
		set
		{
			rating_ = value;
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
	public string IconAssetUri
	{
		get
		{
			return iconAssetUri_;
		}
		set
		{
			iconAssetUri_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public RepeatedField<string> ScreenshotAssetUris => screenshotAssetUris_;

	[GeneratedCode("protoc", null)]
	public AppStoreVersionResponse AppVersion
	{
		get
		{
			return appVersion_;
		}
		set
		{
			appVersion_ = value;
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
	public AppPermission AppPermission
	{
		get
		{
			return appPermission_;
		}
		set
		{
			appPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettings GlobalSettings
	{
		get
		{
			return globalSettings_;
		}
		set
		{
			globalSettings_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetResponse(AppStoreGetResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		appOrganization_ = ((other.appOrganization_ != null) ? other.appOrganization_.Clone() : null);
		appCategory_ = other.appCategory_;
		appType_ = other.appType_;
		name_ = other.name_;
		description_ = other.description_;
		rating_ = other.rating_;
		price_ = other.price_;
		iconAssetUri_ = other.iconAssetUri_;
		bannerAssetUri_ = other.bannerAssetUri_;
		screenshotAssetUris_ = other.screenshotAssetUris_.Clone();
		appVersion_ = ((other.appVersion_ != null) ? other.appVersion_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		appPermission_ = ((other.appPermission_ != null) ? other.appPermission_.Clone() : null);
		globalSettings_ = ((other.globalSettings_ != null) ? other.globalSettings_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetResponse Clone()
	{
		return new AppStoreGetResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreGetResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreGetResponse other)
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
		if (!object.Equals(AppOrganization, other.AppOrganization))
		{
			return false;
		}
		if (AppCategory != other.AppCategory)
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
		if (Description != other.Description)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Rating, other.Rating))
		{
			return false;
		}
		if (Price != other.Price)
		{
			return false;
		}
		if (IconAssetUri != other.IconAssetUri)
		{
			return false;
		}
		if (BannerAssetUri != other.BannerAssetUri)
		{
			return false;
		}
		if (!screenshotAssetUris_.Equals(other.screenshotAssetUris_))
		{
			return false;
		}
		if (!object.Equals(AppVersion, other.AppVersion))
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
		if (!object.Equals(AppPermission, other.AppPermission))
		{
			return false;
		}
		if (!object.Equals(GlobalSettings, other.GlobalSettings))
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
		if (appOrganization_ != null)
		{
			num ^= AppOrganization.GetHashCode();
		}
		if (AppCategory != AppCategory.Unspecified)
		{
			num ^= AppCategory.GetHashCode();
		}
		if (AppType != AppType.Unspecified)
		{
			num ^= AppType.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Description.Length != 0)
		{
			num ^= Description.GetHashCode();
		}
		if (Rating != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Rating);
		}
		if (Price != 0)
		{
			num ^= Price.GetHashCode();
		}
		if (IconAssetUri.Length != 0)
		{
			num ^= IconAssetUri.GetHashCode();
		}
		if (BannerAssetUri.Length != 0)
		{
			num ^= BannerAssetUri.GetHashCode();
		}
		num ^= screenshotAssetUris_.GetHashCode();
		if (appVersion_ != null)
		{
			num ^= AppVersion.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
		}
		if (appPermission_ != null)
		{
			num ^= AppPermission.GetHashCode();
		}
		if (globalSettings_ != null)
		{
			num ^= GlobalSettings.GetHashCode();
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
		if (appOrganization_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppOrganization);
		}
		if (AppCategory != AppCategory.Unspecified)
		{
			P_0.WriteRawTag(48);
			P_0.WriteEnum((int)AppCategory);
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
		if (Description.Length != 0)
		{
			P_0.WriteRawTag(74);
			P_0.WriteString(Description);
		}
		if (Rating != 0f)
		{
			P_0.WriteRawTag(85);
			P_0.WriteFloat(Rating);
		}
		if (Price != 0)
		{
			P_0.WriteRawTag(88);
			P_0.WriteInt32(Price);
		}
		if (IconAssetUri.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(IconAssetUri);
		}
		if (BannerAssetUri.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(BannerAssetUri);
		}
		screenshotAssetUris_.WriteTo(ref P_0, _repeated_screenshotAssetUris_codec);
		if (appVersion_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(AppVersion);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(146, 1);
			P_0.WriteMessage(ChannelPermission);
		}
		if (appPermission_ != null)
		{
			P_0.WriteRawTag(154, 1);
			P_0.WriteMessage(AppPermission);
		}
		if (globalSettings_ != null)
		{
			P_0.WriteRawTag(162, 1);
			P_0.WriteMessage(GlobalSettings);
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
		if (appOrganization_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppOrganization);
		}
		if (AppCategory != AppCategory.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppCategory);
		}
		if (AppType != AppType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppType);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Description.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Description);
		}
		if (Rating != 0f)
		{
			num += 5;
		}
		if (Price != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Price);
		}
		if (IconAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconAssetUri);
		}
		if (BannerAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BannerAssetUri);
		}
		num += screenshotAssetUris_.CalculateSize(_repeated_screenshotAssetUris_codec);
		if (appVersion_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppVersion);
		}
		if (communityPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (appPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AppPermission);
		}
		if (globalSettings_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GlobalSettings);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreGetResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new AppUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.appOrganization_ != null)
		{
			if (appOrganization_ == null)
			{
				AppOrganization = new AppOrganization();
			}
			AppOrganization.MergeFrom(other.AppOrganization);
		}
		if (other.AppCategory != AppCategory.Unspecified)
		{
			AppCategory = other.AppCategory;
		}
		if (other.AppType != AppType.Unspecified)
		{
			AppType = other.AppType;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Description.Length != 0)
		{
			Description = other.Description;
		}
		if (other.Rating != 0f)
		{
			Rating = other.Rating;
		}
		if (other.Price != 0)
		{
			Price = other.Price;
		}
		if (other.IconAssetUri.Length != 0)
		{
			IconAssetUri = other.IconAssetUri;
		}
		if (other.BannerAssetUri.Length != 0)
		{
			BannerAssetUri = other.BannerAssetUri;
		}
		screenshotAssetUris_.Add(other.screenshotAssetUris_);
		if (other.appVersion_ != null)
		{
			if (appVersion_ == null)
			{
				AppVersion = new AppStoreVersionResponse();
			}
			AppVersion.MergeFrom(other.AppVersion);
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
		if (other.appPermission_ != null)
		{
			if (appPermission_ == null)
			{
				AppPermission = new AppPermission();
			}
			AppPermission.MergeFrom(other.AppPermission);
		}
		if (other.globalSettings_ != null)
		{
			if (globalSettings_ == null)
			{
				GlobalSettings = new GlobalSettings();
			}
			GlobalSettings.MergeFrom(other.GlobalSettings);
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
					Id = new AppUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (appOrganization_ == null)
				{
					AppOrganization = new AppOrganization();
				}
				P_0.ReadMessage(AppOrganization);
				break;
			case 48u:
				AppCategory = (AppCategory)P_0.ReadEnum();
				break;
			case 56u:
				AppType = (AppType)P_0.ReadEnum();
				break;
			case 66u:
				Name = P_0.ReadString();
				break;
			case 74u:
				Description = P_0.ReadString();
				break;
			case 85u:
				Rating = P_0.ReadFloat();
				break;
			case 88u:
				Price = P_0.ReadInt32();
				break;
			case 98u:
				IconAssetUri = P_0.ReadString();
				break;
			case 106u:
				BannerAssetUri = P_0.ReadString();
				break;
			case 114u:
				screenshotAssetUris_.AddEntriesFrom(ref P_0, _repeated_screenshotAssetUris_codec);
				break;
			case 122u:
				if (appVersion_ == null)
				{
					AppVersion = new AppStoreVersionResponse();
				}
				P_0.ReadMessage(AppVersion);
				break;
			case 138u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 146u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 154u:
				if (appPermission_ == null)
				{
					AppPermission = new AppPermission();
				}
				P_0.ReadMessage(AppPermission);
				break;
			case 162u:
				if (globalSettings_ == null)
				{
					GlobalSettings = new GlobalSettings();
				}
				P_0.ReadMessage(GlobalSettings);
				break;
			}
		}
	}
}
