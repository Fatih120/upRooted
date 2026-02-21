using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Exceptions.Payloads;

public sealed class RequestValidatorExceptionPayload : IMessage<RequestValidatorExceptionPayload>, IMessage, IEquatable<RequestValidatorExceptionPayload>, IDeepCloneable<RequestValidatorExceptionPayload>, IBufferMessage
{
	private static readonly MessageParser<RequestValidatorExceptionPayload> _parser = new MessageParser<RequestValidatorExceptionPayload>(() => new RequestValidatorExceptionPayload());

	private UnknownFieldSet _unknownFields;

	private string propertyName_ = "";

	private string errorMessage_ = "";

	private string errorCode_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<RequestValidatorExceptionPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootExceptionPayloadReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string PropertyName
	{
		get
		{
			return propertyName_;
		}
		set
		{
			propertyName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string ErrorMessage
	{
		get
		{
			return errorMessage_;
		}
		set
		{
			errorMessage_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string ErrorCode
	{
		get
		{
			return errorCode_;
		}
		set
		{
			errorCode_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorExceptionPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorExceptionPayload(RequestValidatorExceptionPayload other)
		: this()
	{
		propertyName_ = other.propertyName_;
		errorMessage_ = other.errorMessage_;
		errorCode_ = other.errorCode_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorExceptionPayload Clone()
	{
		return new RequestValidatorExceptionPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RequestValidatorExceptionPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(RequestValidatorExceptionPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PropertyName != other.PropertyName)
		{
			return false;
		}
		if (ErrorMessage != other.ErrorMessage)
		{
			return false;
		}
		if (ErrorCode != other.ErrorCode)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (PropertyName.Length != 0)
		{
			num ^= PropertyName.GetHashCode();
		}
		if (ErrorMessage.Length != 0)
		{
			num ^= ErrorMessage.GetHashCode();
		}
		if (ErrorCode.Length != 0)
		{
			num ^= ErrorCode.GetHashCode();
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
		if (PropertyName.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(PropertyName);
		}
		if (ErrorMessage.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(ErrorMessage);
		}
		if (ErrorCode.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(ErrorCode);
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
		if (PropertyName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PropertyName);
		}
		if (ErrorMessage.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ErrorMessage);
		}
		if (ErrorCode.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ErrorCode);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(RequestValidatorExceptionPayload other)
	{
		if (other != null)
		{
			if (other.PropertyName.Length != 0)
			{
				PropertyName = other.PropertyName;
			}
			if (other.ErrorMessage.Length != 0)
			{
				ErrorMessage = other.ErrorMessage;
			}
			if (other.ErrorCode.Length != 0)
			{
				ErrorCode = other.ErrorCode;
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
			case 82u:
				PropertyName = P_0.ReadString();
				break;
			case 90u:
				ErrorMessage = P_0.ReadString();
				break;
			case 98u:
				ErrorCode = P_0.ReadString();
				break;
			}
		}
	}
}
