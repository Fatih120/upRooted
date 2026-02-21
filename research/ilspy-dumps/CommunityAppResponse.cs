using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppResponse : IMessage<CommunityAppResponse>, IMessage, IEquatable<CommunityAppResponse>, IDeepCloneable<CommunityAppResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppResponse> _parser = new MessageParser<CommunityAppResponse>(() => new CommunityAppResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityAppUuid id_;

	private AppUuid appId_;

	private AppType appType_ = AppType.Unspecified;

	private string name_ = "";

	private AppVersionUuid appVersionId_;

	private string version_ = "";

	private static readonly FieldCodec<string> _single_clientAssetUri_codec = FieldCodec.ForClassWrapper<string>(90u);

	private string clientAssetUri_;

	private static readonly FieldCodec<string> _single_appHubUri_codec = FieldCodec.ForClassWrapper<string>(98u);

	private string appHubUri_;

	private AppDeploymentStatus appDeploymentStatus_ = AppDeploymentStatus.Unspecified;

	private ChannelUuid channelId_;

	private CommunityPermission communityPermission_;

	private ChannelPermission channelPermission_;

	private CommunityAppVersionUpdate update_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[5];

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
	public CommunityAppResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppResponse(CommunityAppResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		appType_ = other.appType_;
		name_ = other.name_;
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		version_ = other.version_;
		ClientAssetUri = other.ClientAssetUri;
		AppHubUri = other.AppHubUri;
		appDeploymentStatus_ = other.appDeploymentStatus_;
		channelId_ = ((other.channelId_ != null) ? other.channelId_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		update_ = ((other.update_ != null) ? other.update_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppResponse Clone()
	{
		return new CommunityAppResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppResponse other)
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
		if (ClientAssetUri != other.ClientAssetUri)
		{
			return false;
		}
		if (AppHubUri != other.AppHubUri)
		{
			return false;
		}
		if (AppDeploymentStatus != other.AppDeploymentStatus)
		{
			return false;
		}
		if (!object.Equals(ChannelId, other.ChannelId))
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
		if (clientAssetUri_ != null)
		{
			num ^= ClientAssetUri.GetHashCode();
		}
		if (appHubUri_ != null)
		{
			num ^= AppHubUri.GetHashCode();
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			num ^= AppDeploymentStatus.GetHashCode();
		}
		if (channelId_ != null)
		{
			num ^= ChannelId.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
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
		if (clientAssetUri_ != null)
		{
			_single_clientAssetUri_codec.WriteTagAndValue(ref P_0, ClientAssetUri);
		}
		if (appHubUri_ != null)
		{
			_single_appHubUri_codec.WriteTagAndValue(ref P_0, AppHubUri);
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			P_0.WriteRawTag(104);
			P_0.WriteEnum((int)AppDeploymentStatus);
		}
		if (channelId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(ChannelId);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(ChannelPermission);
		}
		if (update_ != null)
		{
			P_0.WriteRawTag(138, 1);
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
		if (clientAssetUri_ != null)
		{
			num += _single_clientAssetUri_codec.CalculateSizeWithTag(ClientAssetUri);
		}
		if (appHubUri_ != null)
		{
			num += _single_appHubUri_codec.CalculateSizeWithTag(AppHubUri);
		}
		if (AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppDeploymentStatus);
		}
		if (channelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelId);
		}
		if (communityPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
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
	public void MergeFrom(CommunityAppResponse other)
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
		if (other.clientAssetUri_ != null && (clientAssetUri_ == null || other.ClientAssetUri != ""))
		{
			ClientAssetUri = other.ClientAssetUri;
		}
		if (other.appHubUri_ != null && (appHubUri_ == null || other.AppHubUri != ""))
		{
			AppHubUri = other.AppHubUri;
		}
		if (other.AppDeploymentStatus != AppDeploymentStatus.Unspecified)
		{
			AppDeploymentStatus = other.AppDeploymentStatus;
		}
		if (other.channelId_ != null)
		{
			if (channelId_ == null)
			{
				ChannelId = new ChannelUuid();
			}
			ChannelId.MergeFrom(other.ChannelId);
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
			case 90u:
			{
				string text = _single_clientAssetUri_codec.Read(ref P_0);
				if (clientAssetUri_ == null || text != "")
				{
					ClientAssetUri = text;
				}
				break;
			}
			case 98u:
			{
				string text2 = _single_appHubUri_codec.Read(ref P_0);
				if (appHubUri_ == null || text2 != "")
				{
					AppHubUri = text2;
				}
				break;
			}
			case 104u:
				AppDeploymentStatus = (AppDeploymentStatus)P_0.ReadEnum();
				break;
			case 114u:
				if (channelId_ == null)
				{
					ChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(ChannelId);
				break;
			case 122u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 130u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 138u:
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
