using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public sealed class AssetImageLink : IMessage<AssetImageLink>, IMessage, IEquatable<AssetImageLink>, IDeepCloneable<AssetImageLink>, IBufferMessage
{
	private static readonly MessageParser<AssetImageLink> _parser = new MessageParser<AssetImageLink>(() => new AssetImageLink());

	private UnknownFieldSet _unknownFields;

	private string url_ = "";

	private int largestDimensionLimit_;

	private int widthEstimate_;

	private int heightEstimate_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetImageLink> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Url
	{
		get
		{
			return url_;
		}
		set
		{
			url_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public int LargestDimensionLimit
	{
		get
		{
			return largestDimensionLimit_;
		}
		set
		{
			largestDimensionLimit_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int WidthEstimate
	{
		get
		{
			return widthEstimate_;
		}
		set
		{
			widthEstimate_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int HeightEstimate
	{
		get
		{
			return heightEstimate_;
		}
		set
		{
			heightEstimate_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetImageLink()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetImageLink(AssetImageLink other)
		: this()
	{
		url_ = other.url_;
		largestDimensionLimit_ = other.largestDimensionLimit_;
		widthEstimate_ = other.widthEstimate_;
		heightEstimate_ = other.heightEstimate_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetImageLink Clone()
	{
		return new AssetImageLink(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetImageLink);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetImageLink other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Url != other.Url)
		{
			return false;
		}
		if (LargestDimensionLimit != other.LargestDimensionLimit)
		{
			return false;
		}
		if (WidthEstimate != other.WidthEstimate)
		{
			return false;
		}
		if (HeightEstimate != other.HeightEstimate)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Url.Length != 0)
		{
			num ^= Url.GetHashCode();
		}
		if (LargestDimensionLimit != 0)
		{
			num ^= LargestDimensionLimit.GetHashCode();
		}
		if (WidthEstimate != 0)
		{
			num ^= WidthEstimate.GetHashCode();
		}
		if (HeightEstimate != 0)
		{
			num ^= HeightEstimate.GetHashCode();
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
		if (Url.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Url);
		}
		if (LargestDimensionLimit != 0)
		{
			P_0.WriteRawTag(88);
			P_0.WriteInt32(LargestDimensionLimit);
		}
		if (WidthEstimate != 0)
		{
			P_0.WriteRawTag(96);
			P_0.WriteInt32(WidthEstimate);
		}
		if (HeightEstimate != 0)
		{
			P_0.WriteRawTag(104);
			P_0.WriteInt32(HeightEstimate);
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
		if (Url.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Url);
		}
		if (LargestDimensionLimit != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(LargestDimensionLimit);
		}
		if (WidthEstimate != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(WidthEstimate);
		}
		if (HeightEstimate != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(HeightEstimate);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetImageLink other)
	{
		if (other != null)
		{
			if (other.Url.Length != 0)
			{
				Url = other.Url;
			}
			if (other.LargestDimensionLimit != 0)
			{
				LargestDimensionLimit = other.LargestDimensionLimit;
			}
			if (other.WidthEstimate != 0)
			{
				WidthEstimate = other.WidthEstimate;
			}
			if (other.HeightEstimate != 0)
			{
				HeightEstimate = other.HeightEstimate;
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 82u:
				Url = P_0.ReadString();
				break;
			case 88u:
				LargestDimensionLimit = P_0.ReadInt32();
				break;
			case 96u:
				WidthEstimate = P_0.ReadInt32();
				break;
			case 104u:
				HeightEstimate = P_0.ReadInt32();
				break;
			}
		}
	}
}
