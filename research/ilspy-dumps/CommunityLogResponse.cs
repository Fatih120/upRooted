using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Payloads.CommunityLog;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityLogResponse : IMessage<CommunityLogResponse>, IMessage, IEquatable<CommunityLogResponse>, IDeepCloneable<CommunityLogResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogResponse> _parser = new MessageParser<CommunityLogResponse>(() => new CommunityLogResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityLogUuid id_;

	private UserUuid userId_;

	private string username_ = "";

	private static readonly FieldCodec<CommunityLogPayloadItem> _repeated_items_codec = FieldCodec.ForMessage(74u, CommunityLogPayloadItem.Parser);

	private readonly RepeatedField<CommunityLogPayloadItem> items_ = new RepeatedField<CommunityLogPayloadItem>();

	private CommunityLogPayloadItem primary_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityLogUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Username
	{
		get
		{
			return username_;
		}
		set
		{
			username_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityLogPayloadItem> Items => items_;

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadItem Primary
	{
		get
		{
			return primary_;
		}
		set
		{
			primary_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogResponse(CommunityLogResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		username_ = other.username_;
		items_ = other.items_.Clone();
		primary_ = ((other.primary_ != null) ? other.primary_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogResponse Clone()
	{
		return new CommunityLogResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (Username != other.Username)
		{
			return false;
		}
		if (!items_.Equals(other.items_))
		{
			return false;
		}
		if (!object.Equals(Primary, other.Primary))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (Username.Length != 0)
		{
			num ^= Username.GetHashCode();
		}
		num ^= items_.GetHashCode();
		if (primary_ != null)
		{
			num ^= Primary.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(UserId);
		}
		if (Username.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Username);
		}
		items_.WriteTo(ref P_0, _repeated_items_codec);
		if (primary_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Primary);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (Username.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Username);
		}
		num += items_.CalculateSize(_repeated_items_codec);
		if (primary_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Primary);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityLogUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.Username.Length != 0)
		{
			Username = other.Username;
		}
		items_.Add(other.items_);
		if (other.primary_ != null)
		{
			if (primary_ == null)
			{
				Primary = new CommunityLogPayloadItem();
			}
			Primary.MergeFrom(other.Primary);
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 34u:
				if (id_ == null)
				{
					Id = new CommunityLogUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 50u:
				Username = P_0.ReadString();
				break;
			case 74u:
				items_.AddEntriesFrom(ref P_0, _repeated_items_codec);
				break;
			case 82u:
				if (primary_ == null)
				{
					Primary = new CommunityLogPayloadItem();
				}
				P_0.ReadMessage(Primary);
				break;
			}
		}
	}
}
