using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadMessageState : IMessage, IMessage<CommunityLogPayloadMessageState>, IEquatable<CommunityLogPayloadMessageState>, IDeepCloneable<CommunityLogPayloadMessageState>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadMessageState> _parser = new MessageParser<CommunityLogPayloadMessageState>(() => new CommunityLogPayloadMessageState());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<string> _single_messageContent_codec = FieldCodec.ForClassWrapper<string>(58u);

	private string messageContent_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadMessageState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string MessageContent
	{
		get
		{
			return messageContent_;
		}
		set
		{
			messageContent_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessageState()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessageState(CommunityLogPayloadMessageState other)
		: this()
	{
		MessageContent = other.MessageContent;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessageState Clone()
	{
		return new CommunityLogPayloadMessageState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadMessageState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadMessageState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MessageContent != other.MessageContent)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (messageContent_ != null)
		{
			num ^= MessageContent.GetHashCode();
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
		if (messageContent_ != null)
		{
			_single_messageContent_codec.WriteTagAndValue(ref P_0, MessageContent);
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
		if (messageContent_ != null)
		{
			num += _single_messageContent_codec.CalculateSizeWithTag(MessageContent);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadMessageState other)
	{
		if (other != null)
		{
			if (other.messageContent_ != null && (messageContent_ == null || other.MessageContent != ""))
			{
				MessageContent = other.MessageContent;
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 58)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			string text = _single_messageContent_codec.Read(ref P_0);
			if (messageContent_ == null || text != "")
			{
				MessageContent = text;
			}
		}
	}
}
