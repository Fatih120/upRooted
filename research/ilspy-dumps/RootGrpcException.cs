using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Exceptions;

public sealed class RootGrpcException : IMessage<RootGrpcException>, IMessage, IEquatable<RootGrpcException>, IDeepCloneable<RootGrpcException>, IBufferMessage
{
	private static readonly MessageParser<RootGrpcException> _parser = new MessageParser<RootGrpcException>(() => new RootGrpcException());

	private UnknownFieldSet _unknownFields;

	private ErrorCodeType errorCode_ = ErrorCodeType.Unspecified;

	private RootUuid id_;

	private UserUuid whoId_;

	private RootUuid whatId_;

	private RootUuid whereId_;

	private RootUuid parentId_;

	private RootGrpcExceptionPayload payload_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<RootGrpcException> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootGrpcExceptionReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ErrorCodeType ErrorCode
	{
		get
		{
			return errorCode_;
		}
		set
		{
			errorCode_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootUuid Id
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
	public UserUuid WhoId
	{
		get
		{
			return whoId_;
		}
		set
		{
			whoId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootUuid WhatId
	{
		get
		{
			return whatId_;
		}
		set
		{
			whatId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootUuid WhereId
	{
		get
		{
			return whereId_;
		}
		set
		{
			whereId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootUuid ParentId
	{
		get
		{
			return parentId_;
		}
		set
		{
			parentId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcExceptionPayload Payload
	{
		get
		{
			return payload_;
		}
		set
		{
			payload_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcException()
	{
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcException(RootGrpcException other)
		: this()
	{
		errorCode_ = other.errorCode_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		whoId_ = ((other.whoId_ != null) ? other.whoId_.Clone() : null);
		whatId_ = ((other.whatId_ != null) ? other.whatId_.Clone() : null);
		whereId_ = ((other.whereId_ != null) ? other.whereId_.Clone() : null);
		parentId_ = ((other.parentId_ != null) ? other.parentId_.Clone() : null);
		payload_ = ((other.payload_ != null) ? other.payload_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public RootGrpcException Clone()
	{
		return new RootGrpcException(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RootGrpcException);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(RootGrpcException other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ErrorCode != other.ErrorCode)
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(WhoId, other.WhoId))
		{
			return false;
		}
		if (!object.Equals(WhatId, other.WhatId))
		{
			return false;
		}
		if (!object.Equals(WhereId, other.WhereId))
		{
			return false;
		}
		if (!object.Equals(ParentId, other.ParentId))
		{
			return false;
		}
		if (!object.Equals(Payload, other.Payload))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (ErrorCode != ErrorCodeType.Unspecified)
		{
			num ^= ErrorCode.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (whoId_ != null)
		{
			num ^= WhoId.GetHashCode();
		}
		if (whatId_ != null)
		{
			num ^= WhatId.GetHashCode();
		}
		if (whereId_ != null)
		{
			num ^= WhereId.GetHashCode();
		}
		if (parentId_ != null)
		{
			num ^= ParentId.GetHashCode();
		}
		if (payload_ != null)
		{
			num ^= Payload.GetHashCode();
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
		if (ErrorCode != ErrorCodeType.Unspecified)
		{
			P_0.WriteRawTag(80);
			P_0.WriteEnum((int)ErrorCode);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Id);
		}
		if (whoId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(WhoId);
		}
		if (whatId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(WhatId);
		}
		if (whereId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(WhereId);
		}
		if (parentId_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(ParentId);
		}
		if (payload_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(Payload);
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
		if (ErrorCode != ErrorCodeType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ErrorCode);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (whoId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(WhoId);
		}
		if (whatId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(WhatId);
		}
		if (whereId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(WhereId);
		}
		if (parentId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ParentId);
		}
		if (payload_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Payload);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(RootGrpcException other)
	{
		if (other == null)
		{
			return;
		}
		if (other.ErrorCode != ErrorCodeType.Unspecified)
		{
			ErrorCode = other.ErrorCode;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new RootUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.whoId_ != null)
		{
			if (whoId_ == null)
			{
				WhoId = new UserUuid();
			}
			WhoId.MergeFrom(other.WhoId);
		}
		if (other.whatId_ != null)
		{
			if (whatId_ == null)
			{
				WhatId = new RootUuid();
			}
			WhatId.MergeFrom(other.WhatId);
		}
		if (other.whereId_ != null)
		{
			if (whereId_ == null)
			{
				WhereId = new RootUuid();
			}
			WhereId.MergeFrom(other.WhereId);
		}
		if (other.parentId_ != null)
		{
			if (parentId_ == null)
			{
				ParentId = new RootUuid();
			}
			ParentId.MergeFrom(other.ParentId);
		}
		if (other.payload_ != null)
		{
			if (payload_ == null)
			{
				Payload = new RootGrpcExceptionPayload();
			}
			Payload.MergeFrom(other.Payload);
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
			case 80u:
				ErrorCode = (ErrorCodeType)P_0.ReadEnum();
				break;
			case 90u:
				if (id_ == null)
				{
					Id = new RootUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 98u:
				if (whoId_ == null)
				{
					WhoId = new UserUuid();
				}
				P_0.ReadMessage(WhoId);
				break;
			case 106u:
				if (whatId_ == null)
				{
					WhatId = new RootUuid();
				}
				P_0.ReadMessage(WhatId);
				break;
			case 114u:
				if (whereId_ == null)
				{
					WhereId = new RootUuid();
				}
				P_0.ReadMessage(WhereId);
				break;
			case 122u:
				if (parentId_ == null)
				{
					ParentId = new RootUuid();
				}
				P_0.ReadMessage(ParentId);
				break;
			case 130u:
				if (payload_ == null)
				{
					Payload = new RootGrpcExceptionPayload();
				}
				P_0.ReadMessage(Payload);
				break;
			}
		}
	}
}
