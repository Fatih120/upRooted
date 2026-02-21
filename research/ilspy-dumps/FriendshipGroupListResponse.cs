using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FriendshipGroupListResponse : IMessage<FriendshipGroupListResponse>, IMessage, IEquatable<FriendshipGroupListResponse>, IDeepCloneable<FriendshipGroupListResponse>, IBufferMessage
{
	private static readonly MessageParser<FriendshipGroupListResponse> _parser = new MessageParser<FriendshipGroupListResponse>(() => new FriendshipGroupListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<FriendshipGroupResponse> _repeated_friendshipGroups_codec = FieldCodec.ForMessage(114u, FriendshipGroupResponse.Parser);

	private readonly RepeatedField<FriendshipGroupResponse> friendshipGroups_ = new RepeatedField<FriendshipGroupResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipGroupListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[9];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<FriendshipGroupResponse> FriendshipGroups => friendshipGroups_;

	[GeneratedCode("protoc", null)]
	public FriendshipGroupListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupListResponse(FriendshipGroupListResponse other)
		: this()
	{
		friendshipGroups_ = other.friendshipGroups_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupListResponse Clone()
	{
		return new FriendshipGroupListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipGroupListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipGroupListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!friendshipGroups_.Equals(other.friendshipGroups_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= friendshipGroups_.GetHashCode();
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
		friendshipGroups_.WriteTo(ref P_0, _repeated_friendshipGroups_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += friendshipGroups_.CalculateSize(_repeated_friendshipGroups_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipGroupListResponse other)
	{
		if (other != null)
		{
			friendshipGroups_.Add(other.friendshipGroups_);
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
			if (num3 != 114)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				friendshipGroups_.AddEntriesFrom(ref P_0, _repeated_friendshipGroups_codec);
			}
		}
	}
}
