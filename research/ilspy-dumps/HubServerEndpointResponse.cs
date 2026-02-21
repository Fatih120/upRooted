using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class HubServerEndpointResponse : IMessage<HubServerEndpointResponse>, IMessage, IEquatable<HubServerEndpointResponse>, IDeepCloneable<HubServerEndpointResponse>, IBufferMessage
{
	private static readonly MessageParser<HubServerEndpointResponse> _parser = new MessageParser<HubServerEndpointResponse>(() => new HubServerEndpointResponse());

	private UnknownFieldSet _unknownFields;

	private string hubServerInfo_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<HubServerEndpointResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[17];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string HubServerInfo
	{
		get
		{
			return hubServerInfo_;
		}
		set
		{
			hubServerInfo_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public HubServerEndpointResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public HubServerEndpointResponse(HubServerEndpointResponse other)
		: this()
	{
		hubServerInfo_ = other.hubServerInfo_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public HubServerEndpointResponse Clone()
	{
		return new HubServerEndpointResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HubServerEndpointResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(HubServerEndpointResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (HubServerInfo != other.HubServerInfo)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (HubServerInfo.Length != 0)
		{
			num ^= HubServerInfo.GetHashCode();
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
		if (HubServerInfo.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(HubServerInfo);
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
		if (HubServerInfo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(HubServerInfo);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(HubServerEndpointResponse other)
	{
		if (other != null)
		{
			if (other.HubServerInfo.Length != 0)
			{
				HubServerInfo = other.HubServerInfo;
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
				HubServerInfo = P_0.ReadString();
			}
		}
	}
}
