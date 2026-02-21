using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityAppAddedPacket : IPacketCommunity, IPacket, IMessage<CommunityAppAddedPacket>, IMessage, IEquatable<CommunityAppAddedPacket>, IDeepCloneable<CommunityAppAddedPacket>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppAddedPacket> _parser = new MessageParser<CommunityAppAddedPacket>(() => new CommunityAppAddedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private CommunityAppUuid id_;

	private AppUuid appId_;

	private AppType appType_ = AppType.Unspecified;

	private string name_ = "";

	private string version_ = "";

	private AppDeploymentStatus appDeploymentStatus_ = AppDeploymentStatus.Unspecified;

	private string iconAssetUri_ = "";

	private string bannerAssetUri_ = "";

	private static readonly FieldCodec<string> _single_clientAssetUri_codec = FieldCodec.ForClassWrapper<string>(114u);

	private string clientAssetUri_;

	private static readonly FieldCodec<string> _single_appHubUri_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string appHubUri_;

	private AppVersionUuid appVersionId_;

	private ChannelUuid channelId_;

	private ChannelPermission channelPermission_;

	private CommunityPermission communityPermission_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppAddedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PacketType PacketType
	{
		get
		{
			return packetType_;
		}
		set
		{
			packetType_ = value;
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
	public string AppHubUri
	{
		get
		{
			return appHubUri_;
		}
		set
		{
			appHubUri_ = value;
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
	public ChannelUuid ChannelId
	{
		get
		{
			return channelId_;
		}
		set
		{
			channelId_ = value;
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

	public static implicit operator PacketContainer(CommunityAppAddedPacket packet)
	{
		return new PacketContainer
		{
			CommunityAppAdded = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppAddedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppAddedPacket(CommunityAppAddedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		appType_ = other.appType_;
		name_ = other.name_;
		version_ = other.version_;
		appDeploymentStatus_ = other.appDeploymentStatus_;
		iconAssetUri_ = other.iconAssetUri_;
		bannerAssetUri_ = other.bannerAssetUri_;
		ClientAssetUri = other.ClientAssetUri;
		AppHubUri = other.AppHubUri;
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		channelId_ = ((other.channelId_ != null) ? other.channelId_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppAddedPacket Clone()
	{
		return new CommunityAppAddedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppAddedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppAddedPacket other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PacketType != other.PacketType)
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
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
		if (IconAssetUri != other.IconAssetUri)
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
		if (AppHubUri != other.AppHubUri)
		{
			return false;
		}
		if (!object.Equals(AppVersionId, other.AppVersionId))
		{
			return false;
		}
		if (!object.Equals(ChannelId, other.ChannelId))
		{
			return false;
		}
		if (!object.Equals(ChannelPermission, other.ChannelPermission))
		{
			return false;
		}
		if (!object.Equals(CommunityPermission, other.CommunityPermission))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (PacketType != PacketType.Unspecified)
		{
			num ^= PacketType.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
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
		if (IconAssetUri.Length != 0)
		{
			num ^= IconAssetUri.GetHashCode();
		}
		if (BannerAssetUri.Length != 0)
		{
			num ^= BannerAssetUri.GetHashCode();
		}
		if (clientAssetUri_ != null)
		{
			num ^= ClientAssetUri.GetHashCode();
		}
		if (appHubUri_ != null)
		{
			num ^= AppHubUri.GetHashCode();
		}
		if (appVersionId_ != null)
		{
			num ^= AppVersionId.GetHashCode();
		}
		if (channelId_ != null)
		{
			num ^= ChannelId.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
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
		if (PacketType != PacketType.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)PacketType);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CommunityId);
		}
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
		if (clientAssetUri_ != null)
		{
			_single_clientAssetUri_codec.WriteTagAndValue(ref P_0, ClientAssetUri);
		}
		if (appHubUri_ != null)
		{
			_single_appHubUri_codec.WriteTagAndValue(ref P_0, AppHubUri);
		}
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(AppVersionId);
		}
		if (channelId_ != null)
		{
			P_0.WriteRawTag(162, 1);
			P_0.WriteMessage(ChannelId);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(218, 1);
			P_0.WriteMessage(ChannelPermission);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(226, 1);
			P_0.WriteMessage(CommunityPermission);
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
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
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
		if (IconAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconAssetUri);
		}
		if (BannerAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(BannerAssetUri);
		}
		if (clientAssetUri_ != null)
		{
			num += _single_clientAssetUri_codec.CalculateSizeWithTag(ClientAssetUri);
		}
		if (appHubUri_ != null)
		{
			num += _single_appHubUri_codec.CalculateSizeWithTag(AppHubUri);
		}
		if (appVersionId_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AppVersionId);
		}
		if (channelId_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelId);
		}
		if (channelPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (communityPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppAddedPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
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
		if (other.IconAssetUri.Length != 0)
		{
			IconAssetUri = other.IconAssetUri;
		}
		if (other.BannerAssetUri.Length != 0)
		{
			BannerAssetUri = other.BannerAssetUri;
		}
		if (other.clientAssetUri_ != null && (clientAssetUri_ == null || other.ClientAssetUri != ""))
		{
			ClientAssetUri = other.ClientAssetUri;
		}
		if (other.appHubUri_ != null && (appHubUri_ == null || other.AppHubUri != ""))
		{
			AppHubUri = other.AppHubUri;
		}
		if (other.appVersionId_ != null)
		{
			if (appVersionId_ == null)
			{
				AppVersionId = new AppVersionUuid();
			}
			AppVersionId.MergeFrom(other.AppVersionId);
		}
		if (other.channelId_ != null)
		{
			if (channelId_ == null)
			{
				ChannelId = new ChannelUuid();
			}
			ChannelId.MergeFrom(other.ChannelId);
		}
		if (other.channelPermission_ != null)
		{
			if (channelPermission_ == null)
			{
				ChannelPermission = new ChannelPermission();
			}
			ChannelPermission.MergeFrom(other.ChannelPermission);
		}
		if (other.communityPermission_ != null)
		{
			if (communityPermission_ == null)
			{
				CommunityPermission = new CommunityPermission();
			}
			CommunityPermission.MergeFrom(other.CommunityPermission);
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
			case 8u:
				PacketType = (PacketType)P_0.ReadEnum();
				break;
			case 26u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
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
				IconAssetUri = P_0.ReadString();
				break;
			case 106u:
				BannerAssetUri = P_0.ReadString();
				break;
			case 114u:
			{
				string text2 = _single_clientAssetUri_codec.Read(ref P_0);
				if (clientAssetUri_ == null || text2 != "")
				{
					ClientAssetUri = text2;
				}
				break;
			}
			case 122u:
			{
				string text = _single_appHubUri_codec.Read(ref P_0);
				if (appHubUri_ == null || text != "")
				{
					AppHubUri = text;
				}
				break;
			}
			case 130u:
				if (appVersionId_ == null)
				{
					AppVersionId = new AppVersionUuid();
				}
				P_0.ReadMessage(AppVersionId);
				break;
			case 162u:
				if (channelId_ == null)
				{
					ChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(ChannelId);
				break;
			case 218u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 226u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			}
		}
	}
}
