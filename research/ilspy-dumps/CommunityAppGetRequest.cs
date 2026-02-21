using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityAppGetRequest : IMessage<CommunityAppGetRequest>, IMessage, IEquatable<CommunityAppGetRequest>, IDeepCloneable<CommunityAppGetRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppGetRequest> _parser = new MessageParser<CommunityAppGetRequest>(() => new CommunityAppGetRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private CommunityAppUuid communityAppId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppGetRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[0];

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
	public CommunityAppGetRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetRequest(CommunityAppGetRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetRequest Clone()
	{
		return new CommunityAppGetRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppGetRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppGetRequest other)
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
		if (!object.Equals(CommunityAppId, other.CommunityAppId))
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
		if (communityAppId_ != null)
		{
			num ^= CommunityAppId.GetHashCode();
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
		if (communityAppId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(CommunityAppId);
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
		if (communityAppId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityAppId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppGetRequest other)
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
		if (other.communityAppId_ != null)
		{
			if (communityAppId_ == null)
			{
				CommunityAppId = new CommunityAppUuid();
			}
			CommunityAppId.MergeFrom(other.CommunityAppId);
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
				if (communityAppId_ == null)
				{
					CommunityAppId = new CommunityAppUuid();
				}
				P_0.ReadMessage(CommunityAppId);
				break;
			}
		}
	}
}
