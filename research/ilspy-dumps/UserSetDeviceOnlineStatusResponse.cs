using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserSetDeviceOnlineStatusResponse : IMessage<UserSetDeviceOnlineStatusResponse>, IMessage, IEquatable<UserSetDeviceOnlineStatusResponse>, IDeepCloneable<UserSetDeviceOnlineStatusResponse>, IBufferMessage
{
	private static readonly MessageParser<UserSetDeviceOnlineStatusResponse> _parser = new MessageParser<UserSetDeviceOnlineStatusResponse>(() => new UserSetDeviceOnlineStatusResponse());

	private UnknownFieldSet _unknownFields;

	private UserOnlineStatus onlineStatus_ = UserOnlineStatus.Unspecified;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSetDeviceOnlineStatusResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserOnlineStatus OnlineStatus
	{
		get
		{
			return onlineStatus_;
		}
		set
		{
			onlineStatus_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetDeviceOnlineStatusResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSetDeviceOnlineStatusResponse(UserSetDeviceOnlineStatusResponse other)
		: this()
	{
		onlineStatus_ = other.onlineStatus_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSetDeviceOnlineStatusResponse Clone()
	{
		return new UserSetDeviceOnlineStatusResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSetDeviceOnlineStatusResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSetDeviceOnlineStatusResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (OnlineStatus != other.OnlineStatus)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			num ^= OnlineStatus.GetHashCode();
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
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			P_0.WriteRawTag(80);
			P_0.WriteEnum((int)OnlineStatus);
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
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)OnlineStatus);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserSetDeviceOnlineStatusResponse other)
	{
		if (other != null)
		{
			if (other.OnlineStatus != UserOnlineStatus.Unspecified)
			{
				OnlineStatus = other.OnlineStatus;
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
			if (num3 != 80)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				OnlineStatus = (UserOnlineStatus)P_0.ReadEnum();
			}
		}
	}
}
