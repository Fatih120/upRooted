using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.Assets;

public sealed class AssetInformation : IMessage<AssetInformation>, IMessage, IEquatable<AssetInformation>, IDeepCloneable<AssetInformation>, IBufferMessage
{
	public enum LinkOneofCase
	{
		None,
		Url,
		Image,
		Video,
		Invalid,
		File
	}

	private static readonly MessageParser<AssetInformation> _parser = new MessageParser<AssetInformation>(() => new AssetInformation());

	private UnknownFieldSet _unknownFields;

	private Timestamp linkExpiresAt_;

	private AssetUuid assetId_;

	private AssetPreview preview_;

	private object link_;

	private LinkOneofCase linkCase_ = LinkOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetInformation> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[9];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Url
	{
		get
		{
			return HasUrl ? ((string)link_) : "";
		}
		set
		{
			link_ = ProtoPreconditions.CheckNotNull(value, "value");
			linkCase_ = LinkOneofCase.Url;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool HasUrl => linkCase_ == LinkOneofCase.Url;

	[GeneratedCode("protoc", null)]
	public AssetImage Image
	{
		get
		{
			return (linkCase_ == LinkOneofCase.Image) ? ((AssetImage)link_) : null;
		}
		set
		{
			link_ = value;
			linkCase_ = ((value != null) ? LinkOneofCase.Image : LinkOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetVideo Video
	{
		get
		{
			return (linkCase_ == LinkOneofCase.Video) ? ((AssetVideo)link_) : null;
		}
		set
		{
			link_ = value;
			linkCase_ = ((value != null) ? LinkOneofCase.Video : LinkOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetInvalid Invalid
	{
		get
		{
			return HasInvalid ? ((AssetInvalid)link_) : AssetInvalid.Unspecified;
		}
		set
		{
			link_ = value;
			linkCase_ = LinkOneofCase.Invalid;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool HasInvalid => linkCase_ == LinkOneofCase.Invalid;

	[GeneratedCode("protoc", null)]
	public AssetFile File
	{
		get
		{
			return (linkCase_ == LinkOneofCase.File) ? ((AssetFile)link_) : null;
		}
		set
		{
			link_ = value;
			linkCase_ = ((value != null) ? LinkOneofCase.File : LinkOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp LinkExpiresAt
	{
		get
		{
			return linkExpiresAt_;
		}
		set
		{
			linkExpiresAt_ = value;
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
	public AssetPreview Preview
	{
		get
		{
			return preview_;
		}
		set
		{
			preview_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public LinkOneofCase LinkCase => linkCase_;

	[GeneratedCode("protoc", null)]
	public AssetInformation()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetInformation(AssetInformation other)
		: this()
	{
		linkExpiresAt_ = ((other.linkExpiresAt_ != null) ? other.linkExpiresAt_.Clone() : null);
		assetId_ = ((other.assetId_ != null) ? other.assetId_.Clone() : null);
		preview_ = ((other.preview_ != null) ? other.preview_.Clone() : null);
		switch (other.LinkCase)
		{
		case LinkOneofCase.Url:
			Url = other.Url;
			break;
		case LinkOneofCase.Image:
			Image = other.Image.Clone();
			break;
		case LinkOneofCase.Video:
			Video = other.Video.Clone();
			break;
		case LinkOneofCase.Invalid:
			Invalid = other.Invalid;
			break;
		case LinkOneofCase.File:
			File = other.File.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetInformation Clone()
	{
		return new AssetInformation(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearUrl()
	{
		if (HasUrl)
		{
			ClearLink();
		}
	}

	[GeneratedCode("protoc", null)]
	public void ClearInvalid()
	{
		if (HasInvalid)
		{
			ClearLink();
		}
	}

	[GeneratedCode("protoc", null)]
	public void ClearLink()
	{
		linkCase_ = LinkOneofCase.None;
		link_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetInformation);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetInformation other)
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
		if (!object.Equals(Image, other.Image))
		{
			return false;
		}
		if (!object.Equals(Video, other.Video))
		{
			return false;
		}
		if (Invalid != other.Invalid)
		{
			return false;
		}
		if (!object.Equals(File, other.File))
		{
			return false;
		}
		if (!object.Equals(LinkExpiresAt, other.LinkExpiresAt))
		{
			return false;
		}
		if (!object.Equals(AssetId, other.AssetId))
		{
			return false;
		}
		if (!object.Equals(Preview, other.Preview))
		{
			return false;
		}
		if (LinkCase != other.LinkCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (HasUrl)
		{
			num ^= Url.GetHashCode();
		}
		if (linkCase_ == LinkOneofCase.Image)
		{
			num ^= Image.GetHashCode();
		}
		if (linkCase_ == LinkOneofCase.Video)
		{
			num ^= Video.GetHashCode();
		}
		if (HasInvalid)
		{
			num ^= Invalid.GetHashCode();
		}
		if (linkCase_ == LinkOneofCase.File)
		{
			num ^= File.GetHashCode();
		}
		if (linkExpiresAt_ != null)
		{
			num ^= LinkExpiresAt.GetHashCode();
		}
		if (assetId_ != null)
		{
			num ^= AssetId.GetHashCode();
		}
		if (preview_ != null)
		{
			num ^= Preview.GetHashCode();
		}
		num ^= (int)linkCase_;
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
		if (HasUrl)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(Url);
		}
		if (linkCase_ == LinkOneofCase.Image)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(Image);
		}
		if (linkCase_ == LinkOneofCase.Video)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(Video);
		}
		if (HasInvalid)
		{
			P_0.WriteRawTag(32);
			P_0.WriteEnum((int)Invalid);
		}
		if (linkCase_ == LinkOneofCase.File)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(File);
		}
		if (linkExpiresAt_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(LinkExpiresAt);
		}
		if (assetId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(AssetId);
		}
		if (preview_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Preview);
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
		if (HasUrl)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Url);
		}
		if (linkCase_ == LinkOneofCase.Image)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Image);
		}
		if (linkCase_ == LinkOneofCase.Video)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Video);
		}
		if (HasInvalid)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Invalid);
		}
		if (linkCase_ == LinkOneofCase.File)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(File);
		}
		if (linkExpiresAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LinkExpiresAt);
		}
		if (assetId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AssetId);
		}
		if (preview_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Preview);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetInformation other)
	{
		if (other == null)
		{
			return;
		}
		if (other.linkExpiresAt_ != null)
		{
			if (linkExpiresAt_ == null)
			{
				LinkExpiresAt = new Timestamp();
			}
			LinkExpiresAt.MergeFrom(other.LinkExpiresAt);
		}
		if (other.assetId_ != null)
		{
			if (assetId_ == null)
			{
				AssetId = new AssetUuid();
			}
			AssetId.MergeFrom(other.AssetId);
		}
		if (other.preview_ != null)
		{
			if (preview_ == null)
			{
				Preview = new AssetPreview();
			}
			Preview.MergeFrom(other.Preview);
		}
		switch (other.LinkCase)
		{
		case LinkOneofCase.Url:
			Url = other.Url;
			break;
		case LinkOneofCase.Image:
			if (Image == null)
			{
				Image = new AssetImage();
			}
			Image.MergeFrom(other.Image);
			break;
		case LinkOneofCase.Video:
			if (Video == null)
			{
				Video = new AssetVideo();
			}
			Video.MergeFrom(other.Video);
			break;
		case LinkOneofCase.Invalid:
			Invalid = other.Invalid;
			break;
		case LinkOneofCase.File:
			if (File == null)
			{
				File = new AssetFile();
			}
			File.MergeFrom(other.File);
			break;
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
			case 10u:
				Url = P_0.ReadString();
				break;
			case 18u:
			{
				AssetImage assetImage = new AssetImage();
				if (linkCase_ == LinkOneofCase.Image)
				{
					assetImage.MergeFrom(Image);
				}
				P_0.ReadMessage(assetImage);
				Image = assetImage;
				break;
			}
			case 26u:
			{
				AssetVideo assetVideo = new AssetVideo();
				if (linkCase_ == LinkOneofCase.Video)
				{
					assetVideo.MergeFrom(Video);
				}
				P_0.ReadMessage(assetVideo);
				Video = assetVideo;
				break;
			}
			case 32u:
				link_ = P_0.ReadEnum();
				linkCase_ = LinkOneofCase.Invalid;
				break;
			case 42u:
			{
				AssetFile assetFile = new AssetFile();
				if (linkCase_ == LinkOneofCase.File)
				{
					assetFile.MergeFrom(File);
				}
				P_0.ReadMessage(assetFile);
				File = assetFile;
				break;
			}
			case 82u:
				if (linkExpiresAt_ == null)
				{
					LinkExpiresAt = new Timestamp();
				}
				P_0.ReadMessage(LinkExpiresAt);
				break;
			case 90u:
				if (assetId_ == null)
				{
					AssetId = new AssetUuid();
				}
				P_0.ReadMessage(AssetId);
				break;
			case 98u:
				if (preview_ == null)
				{
					Preview = new AssetPreview();
				}
				P_0.ReadMessage(Preview);
				break;
			}
		}
	}
}
