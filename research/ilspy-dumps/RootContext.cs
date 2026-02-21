using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc;

public sealed class RootContext : IMessage<RootContext>, IMessage, IEquatable<RootContext>, IDeepCloneable<RootContext>, IBufferMessage
{
	private static readonly MessageParser<RootContext> _parser = new MessageParser<RootContext>(() => new RootContext());

	private UnknownFieldSet _unknownFields;

	private UserCommandTarget commandTarget_;

	private CommunityIdentity communityIdentity_;

	private DeviceUuid deviceId_;

	private CommandIdempotencyUuid commandId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<RootContext> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ContextReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserCommandTarget CommandTarget
	{
		get
		{
			return commandTarget_;
		}
		set
		{
			commandTarget_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityIdentity CommunityIdentity
	{
		get
		{
			return communityIdentity_;
		}
		set
		{
			communityIdentity_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DeviceUuid DeviceId
	{
		get
		{
			return deviceId_;
		}
		set
		{
			deviceId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommandIdempotencyUuid CommandId
	{
		get
		{
			return commandId_;
		}
		set
		{
			commandId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootContext()
	{
	}

	[GeneratedCode("protoc", null)]
	public RootContext(RootContext other)
		: this()
	{
		commandTarget_ = ((other.commandTarget_ != null) ? other.commandTarget_.Clone() : null);
		communityIdentity_ = ((other.communityIdentity_ != null) ? other.communityIdentity_.Clone() : null);
		deviceId_ = ((other.deviceId_ != null) ? other.deviceId_.Clone() : null);
		commandId_ = ((other.commandId_ != null) ? other.commandId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public RootContext Clone()
	{
		return new RootContext(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as RootContext);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(RootContext other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommandTarget, other.CommandTarget))
		{
			return false;
		}
		if (!object.Equals(CommunityIdentity, other.CommunityIdentity))
		{
			return false;
		}
		if (!object.Equals(DeviceId, other.DeviceId))
		{
			return false;
		}
		if (!object.Equals(CommandId, other.CommandId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (commandTarget_ != null)
		{
			num ^= CommandTarget.GetHashCode();
		}
		if (communityIdentity_ != null)
		{
			num ^= CommunityIdentity.GetHashCode();
		}
		if (deviceId_ != null)
		{
			num ^= DeviceId.GetHashCode();
		}
		if (commandId_ != null)
		{
			num ^= CommandId.GetHashCode();
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
		if (commandTarget_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(CommandTarget);
		}
		if (communityIdentity_ != null)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(CommunityIdentity);
		}
		if (deviceId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(DeviceId);
		}
		if (commandId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommandId);
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
		if (commandTarget_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommandTarget);
		}
		if (communityIdentity_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityIdentity);
		}
		if (deviceId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DeviceId);
		}
		if (commandId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommandId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(RootContext other)
	{
		if (other == null)
		{
			return;
		}
		if (other.commandTarget_ != null)
		{
			if (commandTarget_ == null)
			{
				CommandTarget = new UserCommandTarget();
			}
			CommandTarget.MergeFrom(other.CommandTarget);
		}
		if (other.communityIdentity_ != null)
		{
			if (communityIdentity_ == null)
			{
				CommunityIdentity = new CommunityIdentity();
			}
			CommunityIdentity.MergeFrom(other.CommunityIdentity);
		}
		if (other.deviceId_ != null)
		{
			if (deviceId_ == null)
			{
				DeviceId = new DeviceUuid();
			}
			DeviceId.MergeFrom(other.DeviceId);
		}
		if (other.commandId_ != null)
		{
			if (commandId_ == null)
			{
				CommandId = new CommandIdempotencyUuid();
			}
			CommandId.MergeFrom(other.CommandId);
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
				if (commandTarget_ == null)
				{
					CommandTarget = new UserCommandTarget();
				}
				P_0.ReadMessage(CommandTarget);
				break;
			case 18u:
				if (communityIdentity_ == null)
				{
					CommunityIdentity = new CommunityIdentity();
				}
				P_0.ReadMessage(CommunityIdentity);
				break;
			case 26u:
				if (deviceId_ == null)
				{
					DeviceId = new DeviceUuid();
				}
				P_0.ReadMessage(DeviceId);
				break;
			case 34u:
				if (commandId_ == null)
				{
					CommandId = new CommandIdempotencyUuid();
				}
				P_0.ReadMessage(CommandId);
				break;
			}
		}
	}
}
