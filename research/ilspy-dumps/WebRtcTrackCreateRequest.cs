using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class WebRtcTrackCreateRequest : IMessage<WebRtcTrackCreateRequest>, IMessage, IEquatable<WebRtcTrackCreateRequest>, IDeepCloneable<WebRtcTrackCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<WebRtcTrackCreateRequest> _parser = new MessageParser<WebRtcTrackCreateRequest>(() => new WebRtcTrackCreateRequest());

	private UnknownFieldSet _unknownFields;

	private string location_ = "";

	private string trackId_ = "";

	private static readonly FieldCodec<string> _single_mid_codec = FieldCodec.ForClassWrapper<string>(58u);

	private string mid_;

	private bool isAudio_;

	private bool isVideo_;

	private bool isScreen_;

	private bool isScreenAudio_;

	private static readonly FieldCodec<string> _single_preferredRid_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string preferredRid_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcTrackCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Location
	{
		get
		{
			return location_;
		}
		set
		{
			location_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string TrackId
	{
		get
		{
			return trackId_;
		}
		set
		{
			trackId_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Mid
	{
		get
		{
			return mid_;
		}
		set
		{
			mid_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsAudio
	{
		get
		{
			return isAudio_;
		}
		set
		{
			isAudio_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsVideo
	{
		get
		{
			return isVideo_;
		}
		set
		{
			isVideo_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsScreen
	{
		get
		{
			return isScreen_;
		}
		set
		{
			isScreen_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsScreenAudio
	{
		get
		{
			return isScreenAudio_;
		}
		set
		{
			isScreenAudio_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string PreferredRid
	{
		get
		{
			return preferredRid_;
		}
		set
		{
			preferredRid_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCreateRequest(WebRtcTrackCreateRequest other)
		: this()
	{
		location_ = other.location_;
		trackId_ = other.trackId_;
		Mid = other.Mid;
		isAudio_ = other.isAudio_;
		isVideo_ = other.isVideo_;
		isScreen_ = other.isScreen_;
		isScreenAudio_ = other.isScreenAudio_;
		PreferredRid = other.PreferredRid;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCreateRequest Clone()
	{
		return new WebRtcTrackCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcTrackCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcTrackCreateRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Location != other.Location)
		{
			return false;
		}
		if (TrackId != other.TrackId)
		{
			return false;
		}
		if (Mid != other.Mid)
		{
			return false;
		}
		if (IsAudio != other.IsAudio)
		{
			return false;
		}
		if (IsVideo != other.IsVideo)
		{
			return false;
		}
		if (IsScreen != other.IsScreen)
		{
			return false;
		}
		if (IsScreenAudio != other.IsScreenAudio)
		{
			return false;
		}
		if (PreferredRid != other.PreferredRid)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Location.Length != 0)
		{
			num ^= Location.GetHashCode();
		}
		if (TrackId.Length != 0)
		{
			num ^= TrackId.GetHashCode();
		}
		if (mid_ != null)
		{
			num ^= Mid.GetHashCode();
		}
		if (IsAudio)
		{
			num ^= IsAudio.GetHashCode();
		}
		if (IsVideo)
		{
			num ^= IsVideo.GetHashCode();
		}
		if (IsScreen)
		{
			num ^= IsScreen.GetHashCode();
		}
		if (IsScreenAudio)
		{
			num ^= IsScreenAudio.GetHashCode();
		}
		if (preferredRid_ != null)
		{
			num ^= PreferredRid.GetHashCode();
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
		if (Location.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(Location);
		}
		if (TrackId.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(TrackId);
		}
		if (mid_ != null)
		{
			_single_mid_codec.WriteTagAndValue(ref P_0, Mid);
		}
		if (IsAudio)
		{
			P_0.WriteRawTag(64);
			P_0.WriteBool(IsAudio);
		}
		if (IsVideo)
		{
			P_0.WriteRawTag(72);
			P_0.WriteBool(IsVideo);
		}
		if (IsScreen)
		{
			P_0.WriteRawTag(80);
			P_0.WriteBool(IsScreen);
		}
		if (IsScreenAudio)
		{
			P_0.WriteRawTag(88);
			P_0.WriteBool(IsScreenAudio);
		}
		if (preferredRid_ != null)
		{
			_single_preferredRid_codec.WriteTagAndValue(ref P_0, PreferredRid);
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
		if (Location.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Location);
		}
		if (TrackId.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(TrackId);
		}
		if (mid_ != null)
		{
			num += _single_mid_codec.CalculateSizeWithTag(Mid);
		}
		if (IsAudio)
		{
			num += 2;
		}
		if (IsVideo)
		{
			num += 2;
		}
		if (IsScreen)
		{
			num += 2;
		}
		if (IsScreenAudio)
		{
			num += 2;
		}
		if (preferredRid_ != null)
		{
			num += _single_preferredRid_codec.CalculateSizeWithTag(PreferredRid);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcTrackCreateRequest other)
	{
		if (other != null)
		{
			if (other.Location.Length != 0)
			{
				Location = other.Location;
			}
			if (other.TrackId.Length != 0)
			{
				TrackId = other.TrackId;
			}
			if (other.mid_ != null && (mid_ == null || other.Mid != ""))
			{
				Mid = other.Mid;
			}
			if (other.IsAudio)
			{
				IsAudio = other.IsAudio;
			}
			if (other.IsVideo)
			{
				IsVideo = other.IsVideo;
			}
			if (other.IsScreen)
			{
				IsScreen = other.IsScreen;
			}
			if (other.IsScreenAudio)
			{
				IsScreenAudio = other.IsScreenAudio;
			}
			if (other.preferredRid_ != null && (preferredRid_ == null || other.PreferredRid != ""))
			{
				PreferredRid = other.PreferredRid;
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
			case 34u:
				Location = P_0.ReadString();
				break;
			case 42u:
				TrackId = P_0.ReadString();
				break;
			case 58u:
			{
				string text2 = _single_mid_codec.Read(ref P_0);
				if (mid_ == null || text2 != "")
				{
					Mid = text2;
				}
				break;
			}
			case 64u:
				IsAudio = P_0.ReadBool();
				break;
			case 72u:
				IsVideo = P_0.ReadBool();
				break;
			case 80u:
				IsScreen = P_0.ReadBool();
				break;
			case 88u:
				IsScreenAudio = P_0.ReadBool();
				break;
			case 122u:
			{
				string text = _single_preferredRid_codec.Read(ref P_0);
				if (preferredRid_ == null || text != "")
				{
					PreferredRid = text;
				}
				break;
			}
			}
		}
	}
}
