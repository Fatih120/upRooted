using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Messaging.Grpc;

public sealed class AppRpcMessage : IMessage<AppRpcMessage>, IMessage, IEquatable<AppRpcMessage>, IDeepCloneable<AppRpcMessage>, IBufferMessage
{
	private static readonly MessageParser<AppRpcMessage> _parser = new MessageParser<AppRpcMessage>(() => new AppRpcMessage());

	private UnknownFieldSet _unknownFields;

	private string path_ = "";

	private ByteString body_ = ByteString.Empty;

	private long requestId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppRpcMessage> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[0];

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
	public ByteString Body
	{
		get
		{
			return body_;
		}
		set
		{
			body_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public AppRpcMessage()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessage(AppRpcMessage other)
		: this()
	{
		path_ = other.path_;
		body_ = other.body_;
		requestId_ = other.requestId_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppRpcMessage Clone()
	{
		return new AppRpcMessage(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppRpcMessage);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppRpcMessage other)
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
		if (Body != other.Body)
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
		if (Body.Length != 0)
		{
			num ^= Body.GetHashCode();
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
			P_0.WriteRawTag(18);
			P_0.WriteString(Path);
		}
		if (Body.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteBytes(Body);
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
		if (Body.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeBytesSize(Body);
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
	public void MergeFrom(AppRpcMessage other)
	{
		if (other != null)
		{
			if (other.Path.Length != 0)
			{
				Path = other.Path;
			}
			if (other.Body.Length != 0)
			{
				Body = other.Body;
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
			case 18u:
				Path = P_0.ReadString();
				break;
			case 26u:
				Body = P_0.ReadBytes();
				break;
			case 32u:
				RequestId = P_0.ReadInt64();
				break;
			}
		}
	}
}
