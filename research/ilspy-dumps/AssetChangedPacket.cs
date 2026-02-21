using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class AssetChangedPacket : IPacket, IMessage<AssetChangedPacket>, IMessage, IEquatable<AssetChangedPacket>, IDeepCloneable<AssetChangedPacket>, IBufferMessage
{
	private static readonly MessageParser<AssetChangedPacket> _parser = new MessageParser<AssetChangedPacket>(() => new AssetChangedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private AssetUuid assetId_;

	private Timestamp timestamp_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetChangedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PacketType PacketType
	{
		get
		{
			return packetType_;
		}
		set
		{
			packetType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetUuid AssetId
	{
		get
		{
			return assetId_;
		}
		set
		{
			assetId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp Timestamp
	{
		get
		{
			return timestamp_;
		}
		set
		{
			timestamp_ = value;
		}
	}

	public static implicit operator PacketContainer(AssetChangedPacket packet)
	{
		return new PacketContainer
		{
			AssetChanged = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public AssetChangedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetChangedPacket(AssetChangedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		assetId_ = ((other.assetId_ != null) ? other.assetId_.Clone() : null);
		timestamp_ = ((other.timestamp_ != null) ? other.timestamp_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetChangedPacket Clone()
	{
		return new AssetChangedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetChangedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetChangedPacket other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PacketType != other.PacketType)
		{
			return false;
		}
		if (!object.Equals(AssetId, other.AssetId))
		{
			return false;
		}
		if (!object.Equals(Timestamp, other.Timestamp))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (PacketType != PacketType.Unspecified)
		{
			num ^= PacketType.GetHashCode();
		}
		if (assetId_ != null)
		{
			num ^= AssetId.GetHashCode();
		}
		if (timestamp_ != null)
		{
			num ^= Timestamp.GetHashCode();
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
		if (PacketType != PacketType.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)PacketType);
		}
		if (assetId_ != null)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(AssetId);
		}
		if (timestamp_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(Timestamp);
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
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (assetId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AssetId);
		}
		if (timestamp_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Timestamp);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetChangedPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.assetId_ != null)
		{
			if (assetId_ == null)
			{
				AssetId = new AssetUuid();
			}
			AssetId.MergeFrom(other.AssetId);
		}
		if (other.timestamp_ != null)
		{
			if (timestamp_ == null)
			{
				Timestamp = new Timestamp();
			}
			Timestamp.MergeFrom(other.Timestamp);
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
			case 8u:
				PacketType = (PacketType)P_0.ReadEnum();
				break;
			case 18u:
				if (assetId_ == null)
				{
					AssetId = new AssetUuid();
				}
				P_0.ReadMessage(AssetId);
				break;
			case 26u:
				if (timestamp_ == null)
				{
					Timestamp = new Timestamp();
				}
				P_0.ReadMessage(Timestamp);
				break;
			}
		}
	}
}
