using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityAppListChannelGroupsForAddRequest : IMessage<CommunityAppListChannelGroupsForAddRequest>, IMessage, IEquatable<CommunityAppListChannelGroupsForAddRequest>, IDeepCloneable<CommunityAppListChannelGroupsForAddRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppListChannelGroupsForAddRequest> _parser = new MessageParser<CommunityAppListChannelGroupsForAddRequest>(() => new CommunityAppListChannelGroupsForAddRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private AppUuid appId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppListChannelGroupsForAddRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[11];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public CommunityAppListChannelGroupsForAddRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppListChannelGroupsForAddRequest(CommunityAppListChannelGroupsForAddRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppListChannelGroupsForAddRequest Clone()
	{
		return new CommunityAppListChannelGroupsForAddRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppListChannelGroupsForAddRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppListChannelGroupsForAddRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(AppId, other.AppId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (appId_ != null)
		{
			num ^= AppId.GetHashCode();
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
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (appId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppListChannelGroupsForAddRequest other)
	{
		if (other == null)
		{
			return;
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
			}
		}
	}
}
