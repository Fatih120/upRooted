using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class WebRtcListTracksForSessionResponse : IMessage<WebRtcListTracksForSessionResponse>, IMessage, IEquatable<WebRtcListTracksForSessionResponse>, IDeepCloneable<WebRtcListTracksForSessionResponse>, IBufferMessage
{
	private static readonly MessageParser<WebRtcListTracksForSessionResponse> _parser = new MessageParser<WebRtcListTracksForSessionResponse>(() => new WebRtcListTracksForSessionResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<WebRtcTrackCreateResponse> _repeated_tracks_codec = FieldCodec.ForMessage(34u, WebRtcTrackCreateResponse.Parser);

	private readonly RepeatedField<WebRtcTrackCreateResponse> tracks_ = new RepeatedField<WebRtcTrackCreateResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcListTracksForSessionResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[14];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcTrackCreateResponse> Tracks => tracks_;

	[GeneratedCode("protoc", null)]
	public WebRtcListTracksForSessionResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcListTracksForSessionResponse(WebRtcListTracksForSessionResponse other)
		: this()
	{
		tracks_ = other.tracks_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcListTracksForSessionResponse Clone()
	{
		return new WebRtcListTracksForSessionResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcListTracksForSessionResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcListTracksForSessionResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!tracks_.Equals(other.tracks_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= tracks_.GetHashCode();
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
		tracks_.WriteTo(ref P_0, _repeated_tracks_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += tracks_.CalculateSize(_repeated_tracks_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcListTracksForSessionResponse other)
	{
		if (other != null)
		{
			tracks_.Add(other.tracks_);
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				tracks_.AddEntriesFrom(ref P_0, _repeated_tracks_codec);
			}
		}
	}
}
