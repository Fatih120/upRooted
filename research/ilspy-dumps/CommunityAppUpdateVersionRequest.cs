using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App.Settings;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityAppUpdateVersionRequest : IMessage<CommunityAppUpdateVersionRequest>, IMessage, IEquatable<CommunityAppUpdateVersionRequest>, IDeepCloneable<CommunityAppUpdateVersionRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppUpdateVersionRequest> _parser = new MessageParser<CommunityAppUpdateVersionRequest>(() => new CommunityAppUpdateVersionRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private AppUuid appId_;

	private CommunityAppUuid communityAppId_;

	private GlobalSettings globalSettings_;

	private AppVersionUuid appVersionId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppUpdateVersionRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[3];

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
	public CommunityAppUpdateVersionRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppUpdateVersionRequest(CommunityAppUpdateVersionRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		globalSettings_ = ((other.globalSettings_ != null) ? other.globalSettings_.Clone() : null);
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppUpdateVersionRequest Clone()
	{
		return new CommunityAppUpdateVersionRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppUpdateVersionRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppUpdateVersionRequest other)
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
		if (!object.Equals(AppId, other.AppId))
		{
			return false;
		}
		if (!object.Equals(CommunityAppId, other.CommunityAppId))
		{
			return false;
		}
		if (!object.Equals(GlobalSettings, other.GlobalSettings))
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
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (appId_ != null)
		{
			num ^= AppId.GetHashCode();
		}
		if (communityAppId_ != null)
		{
			num ^= CommunityAppId.GetHashCode();
		}
		if (globalSettings_ != null)
		{
			num ^= GlobalSettings.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityId);
		}
		if (appId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppId);
		}
		if (communityAppId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(CommunityAppId);
		}
		if (globalSettings_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(GlobalSettings);
		}
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(66);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (appId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppId);
		}
		if (communityAppId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityAppId);
		}
		if (globalSettings_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(GlobalSettings);
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
	public void MergeFrom(CommunityAppUpdateVersionRequest other)
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
		if (other.appId_ != null)
		{
			if (appId_ == null)
			{
				AppId = new AppUuid();
			}
			AppId.MergeFrom(other.AppId);
		}
		if (other.communityAppId_ != null)
		{
			if (communityAppId_ == null)
			{
				CommunityAppId = new CommunityAppUuid();
			}
			CommunityAppId.MergeFrom(other.CommunityAppId);
		}
		if (other.globalSettings_ != null)
		{
			if (globalSettings_ == null)
			{
				GlobalSettings = new GlobalSettings();
			}
			GlobalSettings.MergeFrom(other.GlobalSettings);
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 34u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 42u:
				if (appId_ == null)
				{
					AppId = new AppUuid();
				}
				P_0.ReadMessage(AppId);
				break;
			case 50u:
				if (communityAppId_ == null)
				{
					CommunityAppId = new CommunityAppUuid();
				}
				P_0.ReadMessage(CommunityAppId);
				break;
			case 58u:
				if (globalSettings_ == null)
				{
					GlobalSettings = new GlobalSettings();
				}
				P_0.ReadMessage(GlobalSettings);
				break;
			case 66u:
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
