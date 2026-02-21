using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Exceptions.Payloads;

public sealed class UploadStatusExceptionPayload : IMessage<UploadStatusExceptionPayload>, IMessage, IEquatable<UploadStatusExceptionPayload>, IDeepCloneable<UploadStatusExceptionPayload>, IBufferMessage
{
	private static readonly MessageParser<UploadStatusExceptionPayload> _parser = new MessageParser<UploadStatusExceptionPayload>(() => new UploadStatusExceptionPayload());

	private UnknownFieldSet _unknownFields;

	private string tokenUri_ = "";

	private static readonly FieldCodec<bool?> _single_result_codec = FieldCodec.ForStructWrapper<bool>(90u);

	private bool? result_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UploadStatusExceptionPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => RootExceptionPayloadReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string TokenUri
	{
		get
		{
			return tokenUri_;
		}
		set
		{
			tokenUri_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? Result
	{
		get
		{
			return result_;
		}
		set
		{
			result_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusExceptionPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusExceptionPayload(UploadStatusExceptionPayload other)
		: this()
	{
		tokenUri_ = other.tokenUri_;
		Result = other.Result;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UploadStatusExceptionPayload Clone()
	{
		return new UploadStatusExceptionPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UploadStatusExceptionPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UploadStatusExceptionPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (TokenUri != other.TokenUri)
		{
			return false;
		}
		if (Result != other.Result)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (TokenUri.Length != 0)
		{
			num ^= TokenUri.GetHashCode();
		}
		if (result_.HasValue)
		{
			num ^= Result.GetHashCode();
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
		if (TokenUri.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(TokenUri);
		}
		if (result_.HasValue)
		{
			_single_result_codec.WriteTagAndValue(ref P_0, Result);
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
		if (TokenUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TokenUri);
		}
		if (result_.HasValue)
		{
			num += _single_result_codec.CalculateSizeWithTag(Result);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UploadStatusExceptionPayload other)
	{
		if (other != null)
		{
			if (other.TokenUri.Length != 0)
			{
				TokenUri = other.TokenUri;
			}
			if (other.result_.HasValue && (!result_.HasValue || other.Result != false))
			{
				Result = other.Result;
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
				TokenUri = P_0.ReadString();
				break;
			case 90u:
			{
				bool? flag = _single_result_codec.Read(ref P_0);
				if (!result_.HasValue || flag != false)
				{
					Result = flag;
				}
				break;
			}
			}
		}
	}
}
