using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.Assets;

public sealed class AssetImage : IMessage<AssetImage>, IMessage, IEquatable<AssetImage>, IDeepCloneable<AssetImage>, IBufferMessage
{
	private static readonly MessageParser<AssetImage> _parser = new MessageParser<AssetImage>(() => new AssetImage());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<AssetImageLink> _repeated_assetLinks_codec = FieldCodec.ForMessage(82u, AssetImageLink.Parser);

	private readonly RepeatedField<AssetImageLink> assetLinks_ = new RepeatedField<AssetImageLink>();

	private ByteString webP_ = ByteString.Empty;

	private AssetAspectRatio aspectRatio_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetImage> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<AssetImageLink> AssetLinks => assetLinks_;

	[GeneratedCode("protoc", null)]
	public ByteString WebP
	{
		get
		{
			return webP_;
		}
		set
		{
			webP_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetAspectRatio AspectRatio
	{
		get
		{
			return aspectRatio_;
		}
		set
		{
			aspectRatio_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetImage()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetImage(AssetImage other)
		: this()
	{
		assetLinks_ = other.assetLinks_.Clone();
		webP_ = other.webP_;
		aspectRatio_ = ((other.aspectRatio_ != null) ? other.aspectRatio_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetImage Clone()
	{
		return new AssetImage(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetImage);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetImage other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!assetLinks_.Equals(other.assetLinks_))
		{
			return false;
		}
		if (WebP != other.WebP)
		{
			return false;
		}
		if (!object.Equals(AspectRatio, other.AspectRatio))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= assetLinks_.GetHashCode();
		if (WebP.Length != 0)
		{
			num ^= WebP.GetHashCode();
		}
		if (aspectRatio_ != null)
		{
			num ^= AspectRatio.GetHashCode();
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
		assetLinks_.WriteTo(ref P_0, _repeated_assetLinks_codec);
		if (WebP.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteBytes(WebP);
		}
		if (aspectRatio_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(AspectRatio);
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
		num += assetLinks_.CalculateSize(_repeated_assetLinks_codec);
		if (WebP.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeBytesSize(WebP);
		}
		if (aspectRatio_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AspectRatio);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetImage other)
	{
		if (other == null)
		{
			return;
		}
		assetLinks_.Add(other.assetLinks_);
		if (other.WebP.Length != 0)
		{
			WebP = other.WebP;
		}
		if (other.aspectRatio_ != null)
		{
			if (aspectRatio_ == null)
			{
				AspectRatio = new AssetAspectRatio();
			}
			AspectRatio.MergeFrom(other.AspectRatio);
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
			case 82u:
				assetLinks_.AddEntriesFrom(ref P_0, _repeated_assetLinks_codec);
				break;
			case 90u:
				WebP = P_0.ReadBytes();
				break;
			case 98u:
				if (aspectRatio_ == null)
				{
					AspectRatio = new AssetAspectRatio();
				}
				P_0.ReadMessage(AspectRatio);
				break;
			}
		}
	}
}
