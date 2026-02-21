using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Messaging.Grpc;

public sealed class AppRpcMessageServerException : IMessage<AppRpcMessageServerException>, IMessage, IEquatable<AppRpcMessageServerException>, IDeepCloneable<AppRpcMessageServerException>, IBufferMessage
{
	private static readonly MessageParser<AppRpcMessageServerException> _parser = new MessageParser<AppRpcMessageServerException>(() => new AppRpcMessageServerException());

	private UnknownFieldSet _unknownFields;

	private string path_ = "";

	private int code_;

	private string message_ = "";

	private long requestId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppRpcMessageServerException> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Path
	{
		get
		{
			return path_;
		}
		set
		{
			path_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public int Code
	{
		get
		{
			return code_;
		}
		set
		{
			code_ = value;
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
	public long RequestId
	{
		get
		{
			return requestId_;
		}
		set
		{
			requestId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessageServerException()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessageServerException(AppRpcMessageServerException other)
		: this()
	{
		path_ = other.path_;
		code_ = other.code_;
		message_ = other.message_;
		requestId_ = other.requestId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessageServerException Clone()
	{
		return new AppRpcMessageServerException(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppRpcMessageServerException);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppRpcMessageServerException other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Path != other.Path)
		{
			return false;
		}
		if (Code != other.Code)
		{
			return false;
		}
		if (Message != other.Message)
		{
			return false;
		}
		if (RequestId != other.RequestId)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Path.Length != 0)
		{
			num ^= Path.GetHashCode();
		}
		if (Code != 0)
		{
			num ^= Code.GetHashCode();
		}
		if (Message.Length != 0)
		{
			num ^= Message.GetHashCode();
		}
		if (RequestId != 0)
		{
			num ^= RequestId.GetHashCode();
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
		if (Path.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(Path);
		}
		if (Code != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt32(Code);
		}
		if (Message.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(Message);
		}
		if (RequestId != 0)
		{
			P_0.WriteRawTag(32);
			P_0.WriteInt64(RequestId);
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
		if (Path.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Path);
		}
		if (Code != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Code);
		}
		if (Message.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Message);
		}
		if (RequestId != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(RequestId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppRpcMessageServerException other)
	{
		if (other != null)
		{
			if (other.Path.Length != 0)
			{
				Path = other.Path;
			}
			if (other.Code != 0)
			{
				Code = other.Code;
			}
			if (other.Message.Length != 0)
			{
				Message = other.Message;
			}
			if (other.RequestId != 0)
			{
				RequestId = other.RequestId;
			}
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
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
				Path = P_0.ReadString();
				break;
			case 16u:
				Code = P_0.ReadInt32();
				break;
			case 26u:
				Message = P_0.ReadString();
				break;
			case 32u:
				RequestId = P_0.ReadInt64();
				break;
			}
		}
	}
}
