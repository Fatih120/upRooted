using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class WebRtcTracksCloseRequest : IMessage<WebRtcTracksCloseRequest>, IMessage, IEquatable<WebRtcTracksCloseRequest>, IDeepCloneable<WebRtcTracksCloseRequest>, IBufferMessage
{
	private static readonly MessageParser<WebRtcTracksCloseRequest> _parser = new MessageParser<WebRtcTracksCloseRequest>(() => new WebRtcTracksCloseRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private static readonly FieldCodec<WebRtcTrackCloseRequest> _repeated_tracks_codec = FieldCodec.ForMessage(98u, WebRtcTrackCloseRequest.Parser);

	private readonly RepeatedField<WebRtcTrackCloseRequest> tracks_ = new RepeatedField<WebRtcTrackCloseRequest>();

	private WebRtcSessionDescription sessionDescription_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcTracksCloseRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageContainerUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcTrackCloseRequest> Tracks => tracks_;

	[GeneratedCode("protoc", null)]
	public WebRtcSessionDescription SessionDescription
	{
		get
		{
			return sessionDescription_;
		}
		set
		{
			sessionDescription_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTracksCloseRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTracksCloseRequest(WebRtcTracksCloseRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		tracks_ = other.tracks_.Clone();
		sessionDescription_ = ((other.sessionDescription_ != null) ? other.sessionDescription_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTracksCloseRequest Clone()
	{
		return new WebRtcTracksCloseRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcTracksCloseRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcTracksCloseRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!tracks_.Equals(other.tracks_))
		{
			return false;
		}
		if (!object.Equals(SessionDescription, other.SessionDescription))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		num ^= tracks_.GetHashCode();
		if (sessionDescription_ != null)
		{
			num ^= SessionDescription.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ContainerId);
		}
		tracks_.WriteTo(ref P_0, _repeated_tracks_codec);
		if (sessionDescription_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(SessionDescription);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		num += tracks_.CalculateSize(_repeated_tracks_codec);
		if (sessionDescription_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SessionDescription);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcTracksCloseRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		tracks_.Add(other.tracks_);
		if (other.sessionDescription_ != null)
		{
			if (sessionDescription_ == null)
			{
				SessionDescription = new WebRtcSessionDescription();
			}
			SessionDescription.MergeFrom(other.SessionDescription);
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
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 82u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 98u:
				tracks_.AddEntriesFrom(ref P_0, _repeated_tracks_codec);
				break;
			case 106u:
				if (sessionDescription_ == null)
				{
					SessionDescription = new WebRtcSessionDescription();
				}
				P_0.ReadMessage(SessionDescription);
				break;
			}
		}
	}
}
