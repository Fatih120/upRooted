using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc;

public sealed class WebRtcSessionDescription : IMessage<WebRtcSessionDescription>, IMessage, IEquatable<WebRtcSessionDescription>, IDeepCloneable<WebRtcSessionDescription>, IBufferMessage
{
	private static readonly MessageParser<WebRtcSessionDescription> _parser = new MessageParser<WebRtcSessionDescription>(() => new WebRtcSessionDescription());

	private UnknownFieldSet _unknownFields;

	private string type_ = "";

	private string sdp_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcSessionDescription> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Type
	{
		get
		{
			return type_;
		}
		set
		{
			type_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Sdp
	{
		get
		{
			return sdp_;
		}
		set
		{
			sdp_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSessionDescription()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSessionDescription(WebRtcSessionDescription other)
		: this()
	{
		type_ = other.type_;
		sdp_ = other.sdp_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSessionDescription Clone()
	{
		return new WebRtcSessionDescription(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcSessionDescription);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcSessionDescription other)
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
		if (Sdp != other.Sdp)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Type.Length != 0)
		{
			num ^= Type.GetHashCode();
		}
		if (Sdp.Length != 0)
		{
			num ^= Sdp.GetHashCode();
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
		if (Type.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(Type);
		}
		if (Sdp.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Sdp);
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
		if (Type.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Type);
		}
		if (Sdp.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Sdp);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcSessionDescription other)
	{
		if (other != null)
		{
			if (other.Type.Length != 0)
			{
				Type = other.Type;
			}
			if (other.Sdp.Length != 0)
			{
				Sdp = other.Sdp;
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
				Type = P_0.ReadString();
				break;
			case 42u:
				Sdp = P_0.ReadString();
				break;
			}
		}
	}
}
