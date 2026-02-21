using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Exceptions.Payloads;

public sealed class RequestValidatorListExceptionPayload : IMessage<RequestValidatorListExceptionPayload>, IMessage, IEquatable<RequestValidatorListExceptionPayload>, IDeepCloneable<RequestValidatorListExceptionPayload>, IBufferMessage
{
	private static readonly MessageParser<RequestValidatorListExceptionPayload> _parser = new MessageParser<RequestValidatorListExceptionPayload>(() => new RequestValidatorListExceptionPayload());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<RequestValidatorExceptionPayload> _repeated_errors_codec = FieldCodec.ForMessage(82u, RequestValidatorExceptionPayload.Parser);

	private readonly RepeatedField<RequestValidatorExceptionPayload> errors_ = new RepeatedField<RequestValidatorExceptionPayload>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<RequestValidatorListExceptionPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootExceptionPayloadReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<RequestValidatorExceptionPayload> Errors => errors_;

	public static implicit operator RootGrpcExceptionPayload(RequestValidatorListExceptionPayload payload)
	{
		return new RootGrpcExceptionPayload
		{
			RequestValidatorList = payload
		};
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorListExceptionPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorListExceptionPayload(RequestValidatorListExceptionPayload other)
		: this()
	{
		errors_ = other.errors_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public RequestValidatorListExceptionPayload Clone()
	{
		return new RequestValidatorListExceptionPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RequestValidatorListExceptionPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(RequestValidatorListExceptionPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!errors_.Equals(other.errors_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= errors_.GetHashCode();
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
		errors_.WriteTo(ref P_0, _repeated_errors_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += errors_.CalculateSize(_repeated_errors_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(RequestValidatorListExceptionPayload other)
	{
		if (other != null)
		{
			errors_.Add(other.errors_);
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				errors_.AddEntriesFrom(ref P_0, _repeated_errors_codec);
			}
		}
	}
}
