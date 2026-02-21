using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public sealed class AssetPreviewImage : IMessage<AssetPreviewImage>, IMessage, IEquatable<AssetPreviewImage>, IDeepCloneable<AssetPreviewImage>, IBufferMessage
{
	private static readonly MessageParser<AssetPreviewImage> _parser = new MessageParser<AssetPreviewImage>(() => new AssetPreviewImage());

	private UnknownFieldSet _unknownFields;

	private int width_;

	private int height_;

	private string url_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetPreviewImage> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public int Width
	{
		get
		{
			return width_;
		}
		set
		{
			width_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Height
	{
		get
		{
			return height_;
		}
		set
		{
			height_ = value;
		}
	}

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
	public AssetPreviewImage()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewImage(AssetPreviewImage other)
		: this()
	{
		width_ = other.width_;
		height_ = other.height_;
		url_ = other.url_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewImage Clone()
	{
		return new AssetPreviewImage(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetPreviewImage);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetPreviewImage other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Width != other.Width)
		{
			return false;
		}
		if (Height != other.Height)
		{
			return false;
		}
		if (Url != other.Url)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Width != 0)
		{
			num ^= Width.GetHashCode();
		}
		if (Height != 0)
		{
			num ^= Height.GetHashCode();
		}
		if (Url.Length != 0)
		{
			num ^= Url.GetHashCode();
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
		if (Width != 0)
		{
			P_0.WriteRawTag(8);
			P_0.WriteInt32(Width);
		}
		if (Height != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt32(Height);
		}
		if (Url.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(Url);
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
		if (Width != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Width);
		}
		if (Height != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Height);
		}
		if (Url.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Url);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetPreviewImage other)
	{
		if (other != null)
		{
			if (other.Width != 0)
			{
				Width = other.Width;
			}
			if (other.Height != 0)
			{
				Height = other.Height;
			}
			if (other.Url.Length != 0)
			{
				Url = other.Url;
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
			case 8u:
				Width = P_0.ReadInt32();
				break;
			case 16u:
				Height = P_0.ReadInt32();
				break;
			case 26u:
				Url = P_0.ReadString();
				break;
			}
		}
	}
}
