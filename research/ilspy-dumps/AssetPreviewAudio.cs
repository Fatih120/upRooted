using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.Assets;

public sealed class AssetPreviewAudio : IMessage<AssetPreviewAudio>, IMessage, IEquatable<AssetPreviewAudio>, IDeepCloneable<AssetPreviewAudio>, IBufferMessage
{
	private static readonly MessageParser<AssetPreviewAudio> _parser = new MessageParser<AssetPreviewAudio>(() => new AssetPreviewAudio());

	private UnknownFieldSet _unknownFields;

	private int bitrate_;

	private int sampleRate_;

	private Duration duration_;

	private AssetAudioFormat format_ = AssetAudioFormat.Unspecified;

	private AssetAudioCodec codec_ = AssetAudioCodec.Unspecified;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetPreviewAudio> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public int SampleRate
	{
		get
		{
			return sampleRate_;
		}
		set
		{
			sampleRate_ = value;
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
	public AssetAudioFormat Format
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
	public AssetAudioCodec Codec
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
	public AssetPreviewAudio()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewAudio(AssetPreviewAudio other)
		: this()
	{
		bitrate_ = other.bitrate_;
		sampleRate_ = other.sampleRate_;
		duration_ = ((other.duration_ != null) ? other.duration_.Clone() : null);
		format_ = other.format_;
		codec_ = other.codec_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewAudio Clone()
	{
		return new AssetPreviewAudio(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetPreviewAudio);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetPreviewAudio other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Bitrate != other.Bitrate)
		{
			return false;
		}
		if (SampleRate != other.SampleRate)
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
		if (Bitrate != 0)
		{
			num ^= Bitrate.GetHashCode();
		}
		if (SampleRate != 0)
		{
			num ^= SampleRate.GetHashCode();
		}
		if (duration_ != null)
		{
			num ^= Duration.GetHashCode();
		}
		if (Format != AssetAudioFormat.Unspecified)
		{
			num ^= Format.GetHashCode();
		}
		if (Codec != AssetAudioCodec.Unspecified)
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
		if (Bitrate != 0)
		{
			P_0.WriteRawTag(8);
			P_0.WriteInt32(Bitrate);
		}
		if (SampleRate != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt32(SampleRate);
		}
		if (duration_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(Duration);
		}
		if (Format != AssetAudioFormat.Unspecified)
		{
			P_0.WriteRawTag(32);
			P_0.WriteEnum((int)Format);
		}
		if (Codec != AssetAudioCodec.Unspecified)
		{
			P_0.WriteRawTag(40);
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
		if (Bitrate != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(Bitrate);
		}
		if (SampleRate != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(SampleRate);
		}
		if (duration_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Duration);
		}
		if (Format != AssetAudioFormat.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Format);
		}
		if (Codec != AssetAudioCodec.Unspecified)
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
	public void MergeFrom(AssetPreviewAudio other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Bitrate != 0)
		{
			Bitrate = other.Bitrate;
		}
		if (other.SampleRate != 0)
		{
			SampleRate = other.SampleRate;
		}
		if (other.duration_ != null)
		{
			if (duration_ == null)
			{
				Duration = new Duration();
			}
			Duration.MergeFrom(other.Duration);
		}
		if (other.Format != AssetAudioFormat.Unspecified)
		{
			Format = other.Format;
		}
		if (other.Codec != AssetAudioCodec.Unspecified)
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
				Bitrate = P_0.ReadInt32();
				break;
			case 16u:
				SampleRate = P_0.ReadInt32();
				break;
			case 26u:
				if (duration_ == null)
				{
					Duration = new Duration();
				}
				P_0.ReadMessage(Duration);
				break;
			case 32u:
				Format = (AssetAudioFormat)P_0.ReadEnum();
				break;
			case 40u:
				Codec = (AssetAudioCodec)P_0.ReadEnum();
				break;
			}
		}
	}
}
