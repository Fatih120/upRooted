using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.Assets;

public sealed class AssetPreviewVideo : IMessage<AssetPreviewVideo>, IMessage, IEquatable<AssetPreviewVideo>, IDeepCloneable<AssetPreviewVideo>, IBufferMessage
{
	private static readonly MessageParser<AssetPreviewVideo> _parser = new MessageParser<AssetPreviewVideo>(() => new AssetPreviewVideo());

	private UnknownFieldSet _unknownFields;

	private int width_;

	private int height_;

	private int bitrate_;

	private int fps_;

	private Duration duration_;

	private AssetVideoFormat format_ = AssetVideoFormat.Unspecified;

	private AssetVideoCodec codec_ = AssetVideoCodec.Unspecified;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetPreviewVideo> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[6];

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
	public int Bitrate
	{
		get
		{
			return bitrate_;
		}
		set
		{
			bitrate_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int Fps
	{
		get
		{
			return fps_;
		}
		set
		{
			fps_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Duration Duration
	{
		get
		{
			return duration_;
		}
		set
		{
			duration_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetVideoFormat Format
	{
		get
		{
			return format_;
		}
		set
		{
			format_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetVideoCodec Codec
	{
		get
		{
			return codec_;
		}
		set
		{
			codec_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewVideo()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewVideo(AssetPreviewVideo other)
		: this()
	{
		width_ = other.width_;
		height_ = other.height_;
		bitrate_ = other.bitrate_;
		fps_ = other.fps_;
		duration_ = ((other.duration_ != null) ? other.duration_.Clone() : null);
		format_ = other.format_;
		codec_ = other.codec_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewVideo Clone()
	{
		return new AssetPreviewVideo(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetPreviewVideo);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetPreviewVideo other)
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
		if (Bitrate != other.Bitrate)
		{
			return false;
		}
		if (Fps != other.Fps)
		{
			return false;
		}
		if (!object.Equals(Duration, other.Duration))
		{
			return false;
		}
		if (Format != other.Format)
		{
			return false;
		}
		if (Codec != other.Codec)
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
		if (Bitrate != 0)
		{
			num ^= Bitrate.GetHashCode();
		}
		if (Fps != 0)
		{
			num ^= Fps.GetHashCode();
		}
		if (duration_ != null)
		{
			num ^= Duration.GetHashCode();
		}
		if (Format != AssetVideoFormat.Unspecified)
		{
			num ^= Format.GetHashCode();
		}
		if (Codec != AssetVideoCodec.Unspecified)
		{
			num ^= Codec.GetHashCode();
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
		if (Bitrate != 0)
		{
			P_0.WriteRawTag(24);
			P_0.WriteInt32(Bitrate);
		}
		if (Fps != 0)
		{
			P_0.WriteRawTag(32);
			P_0.WriteInt32(Fps);
		}
		if (duration_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Duration);
		}
		if (Format != AssetVideoFormat.Unspecified)
		{
			P_0.WriteRawTag(48);
			P_0.WriteEnum((int)Format);
		}
		if (Codec != AssetVideoCodec.Unspecified)
		{
			P_0.WriteRawTag(56);
			P_0.WriteEnum((int)Codec);
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
		if (Bitrate != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Bitrate);
		}
		if (Fps != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Fps);
		}
		if (duration_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Duration);
		}
		if (Format != AssetVideoFormat.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Format);
		}
		if (Codec != AssetVideoCodec.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Codec);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetPreviewVideo other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Width != 0)
		{
			Width = other.Width;
		}
		if (other.Height != 0)
		{
			Height = other.Height;
		}
		if (other.Bitrate != 0)
		{
			Bitrate = other.Bitrate;
		}
		if (other.Fps != 0)
		{
			Fps = other.Fps;
		}
		if (other.duration_ != null)
		{
			if (duration_ == null)
			{
				Duration = new Duration();
			}
			Duration.MergeFrom(other.Duration);
		}
		if (other.Format != AssetVideoFormat.Unspecified)
		{
			Format = other.Format;
		}
		if (other.Codec != AssetVideoCodec.Unspecified)
		{
			Codec = other.Codec;
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
				Width = P_0.ReadInt32();
				break;
			case 16u:
				Height = P_0.ReadInt32();
				break;
			case 24u:
				Bitrate = P_0.ReadInt32();
				break;
			case 32u:
				Fps = P_0.ReadInt32();
				break;
			case 42u:
				if (duration_ == null)
				{
					Duration = new Duration();
				}
				P_0.ReadMessage(Duration);
				break;
			case 48u:
				Format = (AssetVideoFormat)P_0.ReadEnum();
				break;
			case 56u:
				Codec = (AssetVideoCodec)P_0.ReadEnum();
				break;
			}
		}
	}
}
