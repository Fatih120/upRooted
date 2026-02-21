using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Exceptions.Payloads;

public sealed class UploadStatusListExceptionPayload : IMessage<UploadStatusListExceptionPayload>, IMessage, IEquatable<UploadStatusListExceptionPayload>, IDeepCloneable<UploadStatusListExceptionPayload>, IBufferMessage
{
	private static readonly MessageParser<UploadStatusListExceptionPayload> _parser = new MessageParser<UploadStatusListExceptionPayload>(() => new UploadStatusListExceptionPayload());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<UploadStatusExceptionPayload> _repeated_uploadStatuses_codec = FieldCodec.ForMessage(82u, UploadStatusExceptionPayload.Parser);

	private readonly RepeatedField<UploadStatusExceptionPayload> uploadStatuses_ = new RepeatedField<UploadStatusExceptionPayload>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<UploadStatusListExceptionPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootExceptionPayloadReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UploadStatusExceptionPayload> UploadStatuses => uploadStatuses_;

	public static implicit operator RootGrpcExceptionPayload(UploadStatusListExceptionPayload payload)
	{
		return new RootGrpcExceptionPayload
		{
			UploadStatusList = payload
		};
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusListExceptionPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusListExceptionPayload(UploadStatusListExceptionPayload other)
		: this()
	{
		uploadStatuses_ = other.uploadStatuses_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusListExceptionPayload Clone()
	{
		return new UploadStatusListExceptionPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UploadStatusListExceptionPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UploadStatusListExceptionPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!uploadStatuses_.Equals(other.uploadStatuses_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= uploadStatuses_.GetHashCode();
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
		uploadStatuses_.WriteTo(ref P_0, _repeated_uploadStatuses_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += uploadStatuses_.CalculateSize(_repeated_uploadStatuses_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UploadStatusListExceptionPayload other)
	{
		if (other != null)
		{
			uploadStatuses_.Add(other.uploadStatuses_);
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
				uploadStatuses_.AddEntriesFrom(ref P_0, _repeated_uploadStatuses_codec);
			}
		}
	}
}
