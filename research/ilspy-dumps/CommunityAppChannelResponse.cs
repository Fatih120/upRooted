using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppChannelResponse : IMessage<CommunityAppChannelResponse>, IMessage, IEquatable<CommunityAppChannelResponse>, IDeepCloneable<CommunityAppChannelResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppChannelResponse> _parser = new MessageParser<CommunityAppChannelResponse>(() => new CommunityAppChannelResponse());

	private UnknownFieldSet _unknownFields;

	private ChannelUuid id_;

	private string name_ = "";

	private string description_ = "";

	private string iconAssetUri_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppChannelResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ChannelUuid Id
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
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Description
	{
		get
		{
			return description_;
		}
		set
		{
			description_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string IconAssetUri
	{
		get
		{
			return iconAssetUri_;
		}
		set
		{
			iconAssetUri_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannelResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannelResponse(CommunityAppChannelResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		description_ = other.description_;
		iconAssetUri_ = other.iconAssetUri_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppChannelResponse Clone()
	{
		return new CommunityAppChannelResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppChannelResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppChannelResponse other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (Description != other.Description)
		{
			return false;
		}
		if (IconAssetUri != other.IconAssetUri)
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Description.Length != 0)
		{
			num ^= Description.GetHashCode();
		}
		if (IconAssetUri.Length != 0)
		{
			num ^= IconAssetUri.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Name);
		}
		if (Description.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Description);
		}
		if (IconAssetUri.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(IconAssetUri);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Description.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Description);
		}
		if (IconAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconAssetUri);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppChannelResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Description.Length != 0)
		{
			Description = other.Description;
		}
		if (other.IconAssetUri.Length != 0)
		{
			IconAssetUri = other.IconAssetUri;
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
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				Name = P_0.ReadString();
				break;
			case 50u:
				Description = P_0.ReadString();
				break;
			case 58u:
				IconAssetUri = P_0.ReadString();
				break;
			}
		}
	}
}
