using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityAppVersionUpdateNotificationPacket : IPacketCommunity, IPacket, IMessage<CommunityAppVersionUpdateNotificationPacket>, IMessage, IEquatable<CommunityAppVersionUpdateNotificationPacket>, IDeepCloneable<CommunityAppVersionUpdateNotificationPacket>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppVersionUpdateNotificationPacket> _parser = new MessageParser<CommunityAppVersionUpdateNotificationPacket>(() => new CommunityAppVersionUpdateNotificationPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private CommunityAppUuid communityAppId_;

	private AppUuid appId_;

	private string version_ = "";

	private AppVersionUuid appVersionId_;

	private static readonly FieldCodec<string> _single_clientAssetUri_codec = FieldCodec.ForClassWrapper<string>(66u);

	private string clientAssetUri_;

	private CommunityAppVersionUpdate update_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppVersionUpdateNotificationPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[6];

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
	public CommunityAppUuid CommunityAppId
	{
		get
		{
			return communityAppId_;
		}
		set
		{
			communityAppId_ = value;
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

	public static implicit operator PacketContainer(CommunityAppVersionUpdateNotificationPacket packet)
	{
		return new PacketContainer
		{
			CommunityAppVersionUpdateNotification = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdateNotificationPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdateNotificationPacket(CommunityAppVersionUpdateNotificationPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		version_ = other.version_;
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		ClientAssetUri = other.ClientAssetUri;
		update_ = ((other.update_ != null) ? other.update_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdateNotificationPacket Clone()
	{
		return new CommunityAppVersionUpdateNotificationPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppVersionUpdateNotificationPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppVersionUpdateNotificationPacket other)
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
		if (!object.Equals(CommunityAppId, other.CommunityAppId))
		{
			return false;
		}
		if (!object.Equals(AppId, other.AppId))
		{
			return false;
		}
		if (Version != other.Version)
		{
			return false;
		}
		if (!object.Equals(AppVersionId, other.AppVersionId))
		{
			return false;
		}
		if (ClientAssetUri != other.ClientAssetUri)
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
		if (PacketType != PacketType.Unspecified)
		{
			num ^= PacketType.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (communityAppId_ != null)
		{
			num ^= CommunityAppId.GetHashCode();
		}
		if (appId_ != null)
		{
			num ^= AppId.GetHashCode();
		}
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
		}
		if (appVersionId_ != null)
		{
			num ^= AppVersionId.GetHashCode();
		}
		if (clientAssetUri_ != null)
		{
			num ^= ClientAssetUri.GetHashCode();
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
		if (communityAppId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityAppId);
		}
		if (appId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppId);
		}
		if (Version.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Version);
		}
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(AppVersionId);
		}
		if (clientAssetUri_ != null)
		{
			_single_clientAssetUri_codec.WriteTagAndValue(ref P_0, ClientAssetUri);
		}
		if (update_ != null)
		{
			P_0.WriteRawTag(82);
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
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (communityAppId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityAppId);
		}
		if (appId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppId);
		}
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (appVersionId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppVersionId);
		}
		if (clientAssetUri_ != null)
		{
			num += _single_clientAssetUri_codec.CalculateSizeWithTag(ClientAssetUri);
		}
		if (update_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Update);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppVersionUpdateNotificationPacket other)
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
		if (other.communityAppId_ != null)
		{
			if (communityAppId_ == null)
			{
				CommunityAppId = new CommunityAppUuid();
			}
			CommunityAppId.MergeFrom(other.CommunityAppId);
		}
		if (other.appId_ != null)
		{
			if (appId_ == null)
			{
				AppId = new AppUuid();
			}
			AppId.MergeFrom(other.AppId);
		}
		if (other.Version.Length != 0)
		{
			Version = other.Version;
		}
		if (other.appVersionId_ != null)
		{
			if (appVersionId_ == null)
			{
				AppVersionId = new AppVersionUuid();
			}
			AppVersionId.MergeFrom(other.AppVersionId);
		}
		if (other.clientAssetUri_ != null && (clientAssetUri_ == null || other.ClientAssetUri != ""))
		{
			ClientAssetUri = other.ClientAssetUri;
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
				if (communityAppId_ == null)
				{
					CommunityAppId = new CommunityAppUuid();
				}
				P_0.ReadMessage(CommunityAppId);
				break;
			case 42u:
				if (appId_ == null)
				{
					AppId = new AppUuid();
				}
				P_0.ReadMessage(AppId);
				break;
			case 50u:
				Version = P_0.ReadString();
				break;
			case 58u:
				if (appVersionId_ == null)
				{
					AppVersionId = new AppVersionUuid();
				}
				P_0.ReadMessage(AppVersionId);
				break;
			case 66u:
			{
				string text = _single_clientAssetUri_codec.Read(ref P_0);
				if (clientAssetUri_ == null || text != "")
				{
					ClientAssetUri = text;
				}
				break;
			}
			case 82u:
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
