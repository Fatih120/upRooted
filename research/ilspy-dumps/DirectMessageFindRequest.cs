using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class DirectMessageFindRequest : IMessage<DirectMessageFindRequest>, IMessage, IEquatable<DirectMessageFindRequest>, IDeepCloneable<DirectMessageFindRequest>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageFindRequest> _parser = new MessageParser<DirectMessageFindRequest>(() => new DirectMessageFindRequest());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<UserUuid> _repeated_memberUserIds_codec = FieldCodec.ForMessage(82u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> memberUserIds_ = new RepeatedField<UserUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageFindRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> MemberUserIds => memberUserIds_;

	[GeneratedCode("protoc", null)]
	public DirectMessageFindRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageFindRequest(DirectMessageFindRequest other)
		: this()
	{
		memberUserIds_ = other.memberUserIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageFindRequest Clone()
	{
		return new DirectMessageFindRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageFindRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageFindRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!memberUserIds_.Equals(other.memberUserIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= memberUserIds_.GetHashCode();
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
		memberUserIds_.WriteTo(ref P_0, _repeated_memberUserIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += memberUserIds_.CalculateSize(_repeated_memberUserIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageFindRequest other)
	{
		if (other != null)
		{
			memberUserIds_.Add(other.memberUserIds_);
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
				memberUserIds_.AddEntriesFrom(ref P_0, _repeated_memberUserIds_codec);
			}
		}
	}
}
