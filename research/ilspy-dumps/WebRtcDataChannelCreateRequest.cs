using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class WebRtcDataChannelCreateRequest : IMessage<WebRtcDataChannelCreateRequest>, IMessage, IEquatable<WebRtcDataChannelCreateRequest>, IDeepCloneable<WebRtcDataChannelCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<WebRtcDataChannelCreateRequest> _parser = new MessageParser<WebRtcDataChannelCreateRequest>(() => new WebRtcDataChannelCreateRequest());

	private UnknownFieldSet _unknownFields;

	private string dataChannelName_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcDataChannelCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string DataChannelName
	{
		get
		{
			return dataChannelName_;
		}
		set
		{
			dataChannelName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelCreateRequest(WebRtcDataChannelCreateRequest other)
		: this()
	{
		dataChannelName_ = other.dataChannelName_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcDataChannelCreateRequest Clone()
	{
		return new WebRtcDataChannelCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcDataChannelCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcDataChannelCreateRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (DataChannelName != other.DataChannelName)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (DataChannelName.Length != 0)
		{
			num ^= DataChannelName.GetHashCode();
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
		if (DataChannelName.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(DataChannelName);
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
		if (DataChannelName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DataChannelName);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcDataChannelCreateRequest other)
	{
		if (other != null)
		{
			if (other.DataChannelName.Length != 0)
			{
				DataChannelName = other.DataChannelName;
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
			if (num3 != 42)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				DataChannelName = P_0.ReadString();
			}
		}
	}
}
