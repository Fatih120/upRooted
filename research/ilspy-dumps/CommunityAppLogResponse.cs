using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppLogResponse : IMessage<CommunityAppLogResponse>, IMessage, IEquatable<CommunityAppLogResponse>, IDeepCloneable<CommunityAppLogResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppLogResponse> _parser = new MessageParser<CommunityAppLogResponse>(() => new CommunityAppLogResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private CommunityAppUuid communityAppId_;

	private CommunityAppLogType communityAppLogType_ = CommunityAppLogType.Unspecified;

	private string message_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppLogResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppLogReflection.Descriptor.MessageTypes[0];

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
	public CommunityAppLogType CommunityAppLogType
	{
		get
		{
			return communityAppLogType_;
		}
		set
		{
			communityAppLogType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Message
	{
		get
		{
			return message_;
		}
		set
		{
			message_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppLogResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppLogResponse(CommunityAppLogResponse other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		communityAppLogType_ = other.communityAppLogType_;
		message_ = other.message_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppLogResponse Clone()
	{
		return new CommunityAppLogResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppLogResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppLogResponse other)
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
		if (CommunityAppLogType != other.CommunityAppLogType)
		{
			return false;
		}
		if (Message != other.Message)
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
		if (CommunityAppLogType != CommunityAppLogType.Unspecified)
		{
			num ^= CommunityAppLogType.GetHashCode();
		}
		if (Message.Length != 0)
		{
			num ^= Message.GetHashCode();
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
		if (CommunityAppLogType != CommunityAppLogType.Unspecified)
		{
			P_0.WriteRawTag(48);
			P_0.WriteEnum((int)CommunityAppLogType);
		}
		if (Message.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(Message);
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
		if (CommunityAppLogType != CommunityAppLogType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CommunityAppLogType);
		}
		if (Message.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Message);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppLogResponse other)
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
		if (other.CommunityAppLogType != CommunityAppLogType.Unspecified)
		{
			CommunityAppLogType = other.CommunityAppLogType;
		}
		if (other.Message.Length != 0)
		{
			Message = other.Message;
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
			case 48u:
				CommunityAppLogType = (CommunityAppLogType)P_0.ReadEnum();
				break;
			case 58u:
				Message = P_0.ReadString();
				break;
			}
		}
	}
}
