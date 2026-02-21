using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class ChannelDeleteResponse : IMessage<ChannelDeleteResponse>, IMessage, IEquatable<ChannelDeleteResponse>, IDeepCloneable<ChannelDeleteResponse>, IBufferMessage
{
	private static readonly MessageParser<ChannelDeleteResponse> _parser = new MessageParser<ChannelDeleteResponse>(() => new ChannelDeleteResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityPermissionUpdatePacket communityPermissionUpdatePacket_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelDeleteResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityPermissionUpdatePacket CommunityPermissionUpdatePacket
	{
		get
		{
			return communityPermissionUpdatePacket_;
		}
		set
		{
			communityPermissionUpdatePacket_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelDeleteResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelDeleteResponse(ChannelDeleteResponse other)
		: this()
	{
		communityPermissionUpdatePacket_ = ((other.communityPermissionUpdatePacket_ != null) ? other.communityPermissionUpdatePacket_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelDeleteResponse Clone()
	{
		return new ChannelDeleteResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelDeleteResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelDeleteResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityPermissionUpdatePacket, other.CommunityPermissionUpdatePacket))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityPermissionUpdatePacket_ != null)
		{
			num ^= CommunityPermissionUpdatePacket.GetHashCode();
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
		if (communityPermissionUpdatePacket_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityPermissionUpdatePacket);
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
		if (communityPermissionUpdatePacket_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityPermissionUpdatePacket);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelDeleteResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityPermissionUpdatePacket_ != null)
		{
			if (communityPermissionUpdatePacket_ == null)
			{
				CommunityPermissionUpdatePacket = new CommunityPermissionUpdatePacket();
			}
			CommunityPermissionUpdatePacket.MergeFrom(other.CommunityPermissionUpdatePacket);
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
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
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (communityPermissionUpdatePacket_ == null)
			{
				CommunityPermissionUpdatePacket = new CommunityPermissionUpdatePacket();
			}
			P_0.ReadMessage(CommunityPermissionUpdatePacket);
		}
	}
}
