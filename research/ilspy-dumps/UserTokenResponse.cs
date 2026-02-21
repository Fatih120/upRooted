using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserTokenResponse : IMessage<UserTokenResponse>, IMessage, IEquatable<UserTokenResponse>, IDeepCloneable<UserTokenResponse>, IBufferMessage
{
	private static readonly MessageParser<UserTokenResponse> _parser = new MessageParser<UserTokenResponse>(() => new UserTokenResponse());

	private UnknownFieldSet _unknownFields;

	private string clientToken_ = "";

	private string hubServerInfo_ = "";

	private string webApiUrl_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserTokenResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[18];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string ClientToken
	{
		get
		{
			return clientToken_;
		}
		set
		{
			clientToken_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

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
	public string WebApiUrl
	{
		get
		{
			return webApiUrl_;
		}
		set
		{
			webApiUrl_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public UserTokenResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserTokenResponse(UserTokenResponse other)
		: this()
	{
		clientToken_ = other.clientToken_;
		hubServerInfo_ = other.hubServerInfo_;
		webApiUrl_ = other.webApiUrl_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserTokenResponse Clone()
	{
		return new UserTokenResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserTokenResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserTokenResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ClientToken != other.ClientToken)
		{
			return false;
		}
		if (HubServerInfo != other.HubServerInfo)
		{
			return false;
		}
		if (WebApiUrl != other.WebApiUrl)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (ClientToken.Length != 0)
		{
			num ^= ClientToken.GetHashCode();
		}
		if (HubServerInfo.Length != 0)
		{
			num ^= HubServerInfo.GetHashCode();
		}
		if (WebApiUrl.Length != 0)
		{
			num ^= WebApiUrl.GetHashCode();
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
		if (ClientToken.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(ClientToken);
		}
		if (HubServerInfo.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(HubServerInfo);
		}
		if (WebApiUrl.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(WebApiUrl);
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
		if (ClientToken.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ClientToken);
		}
		if (HubServerInfo.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(HubServerInfo);
		}
		if (WebApiUrl.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(WebApiUrl);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserTokenResponse other)
	{
		if (other != null)
		{
			if (other.ClientToken.Length != 0)
			{
				ClientToken = other.ClientToken;
			}
			if (other.HubServerInfo.Length != 0)
			{
				HubServerInfo = other.HubServerInfo;
			}
			if (other.WebApiUrl.Length != 0)
			{
				WebApiUrl = other.WebApiUrl;
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
			case 82u:
				ClientToken = P_0.ReadString();
				break;
			case 90u:
				HubServerInfo = P_0.ReadString();
				break;
			case 98u:
				WebApiUrl = P_0.ReadString();
				break;
			}
		}
	}
}
