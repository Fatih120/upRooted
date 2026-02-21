using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AccessRuleListResponse : IMessage<AccessRuleListResponse>, IMessage, IEquatable<AccessRuleListResponse>, IDeepCloneable<AccessRuleListResponse>, IBufferMessage
{
	private static readonly MessageParser<AccessRuleListResponse> _parser = new MessageParser<AccessRuleListResponse>(() => new AccessRuleListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<AccessRuleResponse> _repeated_accessRules_codec = FieldCodec.ForMessage(82u, AccessRuleResponse.Parser);

	private readonly RepeatedField<AccessRuleResponse> accessRules_ = new RepeatedField<AccessRuleResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AccessRuleListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AccessRuleReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<AccessRuleResponse> AccessRules => accessRules_;

	[GeneratedCode("protoc", null)]
	public AccessRuleListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleListResponse(AccessRuleListResponse other)
		: this()
	{
		accessRules_ = other.accessRules_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleListResponse Clone()
	{
		return new AccessRuleListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccessRuleListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AccessRuleListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!accessRules_.Equals(other.accessRules_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= accessRules_.GetHashCode();
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
		accessRules_.WriteTo(ref P_0, _repeated_accessRules_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += accessRules_.CalculateSize(_repeated_accessRules_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AccessRuleListResponse other)
	{
		if (other != null)
		{
			accessRules_.Add(other.accessRules_);
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
				accessRules_.AddEntriesFrom(ref P_0, _repeated_accessRules_codec);
			}
		}
	}
}
