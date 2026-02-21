using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.App;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public sealed class CommunityAppVersionUpdate : IMessage<CommunityAppVersionUpdate>, IMessage, IEquatable<CommunityAppVersionUpdate>, IDeepCloneable<CommunityAppVersionUpdate>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppVersionUpdate> _parser = new MessageParser<CommunityAppVersionUpdate>(() => new CommunityAppVersionUpdate());

	private UnknownFieldSet _unknownFields;

	private string version_ = "";

	private Timestamp estimatedUpdateAt_;

	private bool isFinal_;

	private static readonly FieldCodec<string> _single_clientAssetUri_codec = FieldCodec.ForClassWrapper<string>(66u);

	private string clientAssetUri_;

	private static readonly FieldCodec<string> _single_appHubUri_codec = FieldCodec.ForClassWrapper<string>(74u);

	private string appHubUri_;

	private AppDeploymentStatus appDeploymentStatus_ = AppDeploymentStatus.Unspecified;

	private AppVersionUuid appVersionId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppVersionUpdate> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public Timestamp EstimatedUpdateAt
	{
		get
		{
			return estimatedUpdateAt_;
		}
		set
		{
			estimatedUpdateAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsFinal
	{
		get
		{
			return isFinal_;
		}
		set
		{
			isFinal_ = value;
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
	public CommunityAppVersionUpdate()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdate(CommunityAppVersionUpdate other)
		: this()
	{
		version_ = other.version_;
		estimatedUpdateAt_ = ((other.estimatedUpdateAt_ != null) ? other.estimatedUpdateAt_.Clone() : null);
		isFinal_ = other.isFinal_;
		ClientAssetUri = other.ClientAssetUri;
		AppHubUri = other.AppHubUri;
		appDeploymentStatus_ = other.appDeploymentStatus_;
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdate Clone()
	{
		return new CommunityAppVersionUpdate(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppVersionUpdate);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppVersionUpdate other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Version != other.Version)
		{
			return false;
		}
		if (!object.Equals(EstimatedUpdateAt, other.EstimatedUpdateAt))
		{
			return false;
		}
		if (IsFinal != other.IsFinal)
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
		if (!object.Equals(AppVersionId, other.AppVersionId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Version.Length != 0)
		{
			num ^= Version.GetHashCode();
		}
		if (estimatedUpdateAt_ != null)
		{
			num ^= EstimatedUpdateAt.GetHashCode();
		}
		if (IsFinal)
		{
			num ^= IsFinal.GetHashCode();
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
		if (appVersionId_ != null)
		{
			num ^= AppVersionId.GetHashCode();
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
		if (Version.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(Version);
		}
		if (estimatedUpdateAt_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(EstimatedUpdateAt);
		}
		if (IsFinal)
		{
			P_0.WriteRawTag(56);
			P_0.WriteBool(IsFinal);
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
			P_0.WriteRawTag(80);
			P_0.WriteEnum((int)AppDeploymentStatus);
		}
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(AppVersionId);
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
		if (Version.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Version);
		}
		if (estimatedUpdateAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EstimatedUpdateAt);
		}
		if (IsFinal)
		{
			num += 2;
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
		if (appVersionId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppVersionId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppVersionUpdate other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Version.Length != 0)
		{
			Version = other.Version;
		}
		if (other.estimatedUpdateAt_ != null)
		{
			if (estimatedUpdateAt_ == null)
			{
				EstimatedUpdateAt = new Timestamp();
			}
			EstimatedUpdateAt.MergeFrom(other.EstimatedUpdateAt);
		}
		if (other.IsFinal)
		{
			IsFinal = other.IsFinal;
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
		if (other.appVersionId_ != null)
		{
			if (appVersionId_ == null)
			{
				AppVersionId = new AppVersionUuid();
			}
			AppVersionId.MergeFrom(other.AppVersionId);
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
				Version = P_0.ReadString();
				break;
			case 50u:
				if (estimatedUpdateAt_ == null)
				{
					EstimatedUpdateAt = new Timestamp();
				}
				P_0.ReadMessage(EstimatedUpdateAt);
				break;
			case 56u:
				IsFinal = P_0.ReadBool();
				break;
			case 66u:
			{
				string text2 = _single_clientAssetUri_codec.Read(ref P_0);
				if (clientAssetUri_ == null || text2 != "")
				{
					ClientAssetUri = text2;
				}
				break;
			}
			case 74u:
			{
				string text = _single_appHubUri_codec.Read(ref P_0);
				if (appHubUri_ == null || text != "")
				{
					AppHubUri = text;
				}
				break;
			}
			case 80u:
				AppDeploymentStatus = (AppDeploymentStatus)P_0.ReadEnum();
				break;
			case 90u:
				if (appVersionId_ == null)
				{
					AppVersionId = new AppVersionUuid();
				}
				P_0.ReadMessage(AppVersionId);
				break;
			}
		}
	}
}
