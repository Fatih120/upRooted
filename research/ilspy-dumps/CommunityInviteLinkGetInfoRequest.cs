using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityInviteLinkGetInfoRequest : IMessage<CommunityInviteLinkGetInfoRequest>, IMessage, IEquatable<CommunityInviteLinkGetInfoRequest>, IDeepCloneable<CommunityInviteLinkGetInfoRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityInviteLinkGetInfoRequest> _parser = new MessageParser<CommunityInviteLinkGetInfoRequest>(() => new CommunityInviteLinkGetInfoRequest());

	private UnknownFieldSet _unknownFields;

	private string code_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityInviteLinkGetInfoRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LinkReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Code
	{
		get
		{
			return code_;
		}
		set
		{
			code_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkGetInfoRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkGetInfoRequest(CommunityInviteLinkGetInfoRequest other)
		: this()
	{
		code_ = other.code_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkGetInfoRequest Clone()
	{
		return new CommunityInviteLinkGetInfoRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityInviteLinkGetInfoRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityInviteLinkGetInfoRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Code != other.Code)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Code.Length != 0)
		{
			num ^= Code.GetHashCode();
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
		if (Code.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Code);
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
		if (Code.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Code);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityInviteLinkGetInfoRequest other)
	{
		if (other != null)
		{
			if (other.Code.Length != 0)
			{
				Code = other.Code;
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
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				Code = P_0.ReadString();
			}
		}
	}
}
