using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace RootApp.Assets;

public sealed class AssetVideo : IMessage<AssetVideo>, IMessage, IEquatable<AssetVideo>, IDeepCloneable<AssetVideo>, IBufferMessage
{
	private static readonly MessageParser<AssetVideo> _parser = new MessageParser<AssetVideo>(() => new AssetVideo());

	private UnknownFieldSet _unknownFields;

	private Duration duration_;

	private string hlsUrl_ = "";

	private string dashUrl_ = "";

	private string thumbnailUrl_ = "";

	private string previewUrl_ = "";

	private string downloadUrl_ = "";

	private AssetAspectRatio aspectRatio_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AssetVideo> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AssetInformationReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string HlsUrl
	{
		get
		{
			return hlsUrl_;
		}
		set
		{
			hlsUrl_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string DashUrl
	{
		get
		{
			return dashUrl_;
		}
		set
		{
			dashUrl_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string ThumbnailUrl
	{
		get
		{
			return thumbnailUrl_;
		}
		set
		{
			thumbnailUrl_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PreviewUrl
	{
		get
		{
			return previewUrl_;
		}
		set
		{
			previewUrl_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string DownloadUrl
	{
		get
		{
			return downloadUrl_;
		}
		set
		{
			downloadUrl_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public AssetVideo()
	{
	}

	[GeneratedCode("protoc", null)]
	public AssetVideo(AssetVideo other)
		: this()
	{
		duration_ = ((other.duration_ != null) ? other.duration_.Clone() : null);
		hlsUrl_ = other.hlsUrl_;
		dashUrl_ = other.dashUrl_;
		thumbnailUrl_ = other.thumbnailUrl_;
		previewUrl_ = other.previewUrl_;
		downloadUrl_ = other.downloadUrl_;
		aspectRatio_ = ((other.aspectRatio_ != null) ? other.aspectRatio_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AssetVideo Clone()
	{
		return new AssetVideo(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AssetVideo);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AssetVideo other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Duration, other.Duration))
		{
			return false;
		}
		if (HlsUrl != other.HlsUrl)
		{
			return false;
		}
		if (DashUrl != other.DashUrl)
		{
			return false;
		}
		if (ThumbnailUrl != other.ThumbnailUrl)
		{
			return false;
		}
		if (PreviewUrl != other.PreviewUrl)
		{
			return false;
		}
		if (DownloadUrl != other.DownloadUrl)
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
		if (duration_ != null)
		{
			num ^= Duration.GetHashCode();
		}
		if (HlsUrl.Length != 0)
		{
			num ^= HlsUrl.GetHashCode();
		}
		if (DashUrl.Length != 0)
		{
			num ^= DashUrl.GetHashCode();
		}
		if (ThumbnailUrl.Length != 0)
		{
			num ^= ThumbnailUrl.GetHashCode();
		}
		if (PreviewUrl.Length != 0)
		{
			num ^= PreviewUrl.GetHashCode();
		}
		if (DownloadUrl.Length != 0)
		{
			num ^= DownloadUrl.GetHashCode();
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
		if (duration_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Duration);
		}
		if (HlsUrl.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(HlsUrl);
		}
		if (DashUrl.Length != 0)
		{
			P_0.WriteRawTag(26);
			P_0.WriteString(DashUrl);
		}
		if (ThumbnailUrl.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(ThumbnailUrl);
		}
		if (PreviewUrl.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(PreviewUrl);
		}
		if (DownloadUrl.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(DownloadUrl);
		}
		if (aspectRatio_ != null)
		{
			P_0.WriteRawTag(58);
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
		if (duration_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Duration);
		}
		if (HlsUrl.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(HlsUrl);
		}
		if (DashUrl.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DashUrl);
		}
		if (ThumbnailUrl.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ThumbnailUrl);
		}
		if (PreviewUrl.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PreviewUrl);
		}
		if (DownloadUrl.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DownloadUrl);
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
	public void MergeFrom(AssetVideo other)
	{
		if (other == null)
		{
			return;
		}
		if (other.duration_ != null)
		{
			if (duration_ == null)
			{
				Duration = new Duration();
			}
			Duration.MergeFrom(other.Duration);
		}
		if (other.HlsUrl.Length != 0)
		{
			HlsUrl = other.HlsUrl;
		}
		if (other.DashUrl.Length != 0)
		{
			DashUrl = other.DashUrl;
		}
		if (other.ThumbnailUrl.Length != 0)
		{
			ThumbnailUrl = other.ThumbnailUrl;
		}
		if (other.PreviewUrl.Length != 0)
		{
			PreviewUrl = other.PreviewUrl;
		}
		if (other.DownloadUrl.Length != 0)
		{
			DownloadUrl = other.DownloadUrl;
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
			case 10u:
				if (duration_ == null)
				{
					Duration = new Duration();
				}
				P_0.ReadMessage(Duration);
				break;
			case 18u:
				HlsUrl = P_0.ReadString();
				break;
			case 26u:
				DashUrl = P_0.ReadString();
				break;
			case 34u:
				ThumbnailUrl = P_0.ReadString();
				break;
			case 42u:
				PreviewUrl = P_0.ReadString();
				break;
			case 50u:
				DownloadUrl = P_0.ReadString();
				break;
			case 58u:
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
