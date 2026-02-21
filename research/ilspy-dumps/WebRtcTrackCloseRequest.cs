using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class WebRtcTrackCloseRequest : IMessage<WebRtcTrackCloseRequest>, IMessage, IEquatable<WebRtcTrackCloseRequest>, IDeepCloneable<WebRtcTrackCloseRequest>, IBufferMessage
{
	private static readonly MessageParser<WebRtcTrackCloseRequest> _parser = new MessageParser<WebRtcTrackCloseRequest>(() => new WebRtcTrackCloseRequest());

	private UnknownFieldSet _unknownFields;

	private string mid_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcTrackCloseRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Mid
	{
		get
		{
			return mid_;
		}
		set
		{
			mid_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCloseRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCloseRequest(WebRtcTrackCloseRequest other)
		: this()
	{
		mid_ = other.mid_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcTrackCloseRequest Clone()
	{
		return new WebRtcTrackCloseRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcTrackCloseRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcTrackCloseRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Mid != other.Mid)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Mid.Length != 0)
		{
			num ^= Mid.GetHashCode();
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
		if (Mid.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Mid);
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
		if (Mid.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Mid);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcTrackCloseRequest other)
	{
		if (other != null)
		{
			if (other.Mid.Length != 0)
			{
				Mid = other.Mid;
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				Mid = P_0.ReadString();
			}
		}
	}
}
