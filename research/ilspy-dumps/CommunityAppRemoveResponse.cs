using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppRemoveResponse : IMessage<CommunityAppRemoveResponse>, IMessage, IEquatable<CommunityAppRemoveResponse>, IDeepCloneable<CommunityAppRemoveResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppRemoveResponse> _parser = new MessageParser<CommunityAppRemoveResponse>(() => new CommunityAppRemoveResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityPermissionUpdatePacket update_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppRemoveResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityPermissionUpdatePacket Update
	{
		get
		{
			return update_;
		}
		set
		{
			update_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppRemoveResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppRemoveResponse(CommunityAppRemoveResponse other)
		: this()
	{
		update_ = ((other.update_ != null) ? other.update_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppRemoveResponse Clone()
	{
		return new CommunityAppRemoveResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppRemoveResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppRemoveResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Update, other.Update))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (update_ != null)
		{
			num ^= Update.GetHashCode();
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
		if (update_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Update);
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
		if (update_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Update);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppRemoveResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.update_ != null)
		{
			if (update_ == null)
			{
				Update = new CommunityPermissionUpdatePacket();
			}
			Update.MergeFrom(other.Update);
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
			if (update_ == null)
			{
				Update = new CommunityPermissionUpdatePacket();
			}
			P_0.ReadMessage(Update);
		}
	}
}
