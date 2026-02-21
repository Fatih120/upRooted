using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.Assets;

public sealed class AssetPreview : IMessage<AssetPreview>, IMessage, IEquatable<AssetPreview>, IDeepCloneable<AssetPreview>, IBufferMessage
{
	public enum DetailsOneofCase
	{
		None = 0,
		Audio = 10,
		Video = 11
	}

	private static readonly MessageParser<AssetPreview> _parser = new MessageParser<AssetPreview>(() => new AssetPreview());

	private UnknownFieldSet _unknownFields;

	private AssetPreviewType type_ = AssetPreviewType.Unspecified;

	private Timestamp updatedAt_;

	private string title_ = "";

	private string description_ = "";

	private static readonly FieldCodec<AssetPreviewImage> _repeated_previews_codec = FieldCodec.ForMessage(42u, AssetPreviewImage.Parser);

	private readonly RepeatedField<AssetPreviewImage> previews_ = new RepeatedField<AssetPreviewImage>();

	private object details_;

	private DetailsOneofCase detailsCase_ = DetailsOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetPreview> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public AssetPreviewType Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp UpdatedAt
	{
		get
		{
			return updatedAt_;
		}
		set
		{
			updatedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Title
	{
		get
		{
			return title_;
		}
		set
		{
			title_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public RepeatedField<AssetPreviewImage> Previews => previews_;

	[GeneratedCode("protoc", null)]
	public AssetPreviewAudio Audio
	{
		get
		{
			return (detailsCase_ == DetailsOneofCase.Audio) ? ((AssetPreviewAudio)details_) : null;
		}
		set
		{
			details_ = value;
			detailsCase_ = ((value != null) ? DetailsOneofCase.Audio : DetailsOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetPreviewVideo Video
	{
		get
		{
			return (detailsCase_ == DetailsOneofCase.Video) ? ((AssetPreviewVideo)details_) : null;
		}
		set
		{
			details_ = value;
			detailsCase_ = ((value != null) ? DetailsOneofCase.Video : DetailsOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DetailsOneofCase DetailsCase => detailsCase_;

	[GeneratedCode("protoc", null)]
	public AssetPreview()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetPreview(AssetPreview other)
		: this()
	{
		type_ = other.type_;
		updatedAt_ = ((other.updatedAt_ != null) ? other.updatedAt_.Clone() : null);
		title_ = other.title_;
		description_ = other.description_;
		previews_ = other.previews_.Clone();
		switch (other.DetailsCase)
		{
		case DetailsOneofCase.Audio:
			Audio = other.Audio.Clone();
			break;
		case DetailsOneofCase.Video:
			Video = other.Video.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetPreview Clone()
	{
		return new AssetPreview(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearDetails()
	{
		detailsCase_ = DetailsOneofCase.None;
		details_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetPreview);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetPreview other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Type != other.Type)
		{
			return false;
		}
		if (!object.Equals(UpdatedAt, other.UpdatedAt))
		{
			return false;
		}
		if (Title != other.Title)
		{
			return false;
		}
		if (Description != other.Description)
		{
			return false;
		}
		if (!previews_.Equals(other.previews_))
		{
			return false;
		}
		if (!object.Equals(Audio, other.Audio))
		{
			return false;
		}
		if (!object.Equals(Video, other.Video))
		{
			return false;
		}
		if (DetailsCase != other.DetailsCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Type != AssetPreviewType.Unspecified)
		{
			num ^= Type.GetHashCode();
		}
		if (updatedAt_ != null)
		{
			num ^= UpdatedAt.GetHashCode();
		}
		if (Title.Length != 0)
		{
			num ^= Title.GetHashCode();
		}
		if (Description.Length != 0)
		{
			num ^= Description.GetHashCode();
		}
		num ^= previews_.GetHashCode();
		if (detailsCase_ == DetailsOneofCase.Audio)
		{
			num ^= Audio.GetHashCode();
		}
		if (detailsCase_ == DetailsOneofCase.Video)
		{
			num ^= Video.GetHashCode();
		}
		num ^= (int)detailsCase_;
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
		if (Type != AssetPreviewType.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)Type);
		}
		if (updatedAt_ != null)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(UpdatedAt);
		}
		if (Title.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(Title);
		}
		if (Description.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(Description);
		}
		previews_.WriteTo(ref P_0, _repeated_previews_codec);
		if (detailsCase_ == DetailsOneofCase.Audio)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Audio);
		}
		if (detailsCase_ == DetailsOneofCase.Video)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Video);
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
		if (Type != AssetPreviewType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)Type);
		}
		if (updatedAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UpdatedAt);
		}
		if (Title.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Title);
		}
		if (Description.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Description);
		}
		num += previews_.CalculateSize(_repeated_previews_codec);
		if (detailsCase_ == DetailsOneofCase.Audio)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Audio);
		}
		if (detailsCase_ == DetailsOneofCase.Video)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Video);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AssetPreview other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Type != AssetPreviewType.Unspecified)
		{
			Type = other.Type;
		}
		if (other.updatedAt_ != null)
		{
			if (updatedAt_ == null)
			{
				UpdatedAt = new Timestamp();
			}
			UpdatedAt.MergeFrom(other.UpdatedAt);
		}
		if (other.Title.Length != 0)
		{
			Title = other.Title;
		}
		if (other.Description.Length != 0)
		{
			Description = other.Description;
		}
		previews_.Add(other.previews_);
		switch (other.DetailsCase)
		{
		case DetailsOneofCase.Audio:
			if (Audio == null)
			{
				Audio = new AssetPreviewAudio();
			}
			Audio.MergeFrom(other.Audio);
			break;
		case DetailsOneofCase.Video:
			if (Video == null)
			{
				Video = new AssetPreviewVideo();
			}
			Video.MergeFrom(other.Video);
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
			case 8u:
				Type = (AssetPreviewType)P_0.ReadEnum();
				break;
			case 18u:
				if (updatedAt_ == null)
				{
					UpdatedAt = new Timestamp();
				}
				P_0.ReadMessage(UpdatedAt);
				break;
			case 26u:
				Title = P_0.ReadString();
				break;
			case 34u:
				Description = P_0.ReadString();
				break;
			case 42u:
				previews_.AddEntriesFrom(ref P_0, _repeated_previews_codec);
				break;
			case 82u:
			{
				AssetPreviewAudio assetPreviewAudio = new AssetPreviewAudio();
				if (detailsCase_ == DetailsOneofCase.Audio)
				{
					assetPreviewAudio.MergeFrom(Audio);
				}
				P_0.ReadMessage(assetPreviewAudio);
				Audio = assetPreviewAudio;
				break;
			}
			case 90u:
			{
				AssetPreviewVideo assetPreviewVideo = new AssetPreviewVideo();
				if (detailsCase_ == DetailsOneofCase.Video)
				{
					assetPreviewVideo.MergeFrom(Video);
				}
				P_0.ReadMessage(assetPreviewVideo);
				Video = assetPreviewVideo;
				break;
			}
			}
		}
	}
}
