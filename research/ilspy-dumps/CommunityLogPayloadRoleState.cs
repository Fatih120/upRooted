using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Grpc;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadRoleState : IMessage, IMessage<CommunityLogPayloadRoleState>, IEquatable<CommunityLogPayloadRoleState>, IDeepCloneable<CommunityLogPayloadRoleState>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadRoleState> _parser = new MessageParser<CommunityLogPayloadRoleState>(() => new CommunityLogPayloadRoleState());

	private UnknownFieldSet _unknownFields;

	private string name_ = "";

	private string colorHex_ = "";

	private CommunityPermission communityPermission_;

	private ChannelPermission channelPermission_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadRoleState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string ColorHex
	{
		get
		{
			return colorHex_;
		}
		set
		{
			colorHex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermission CommunityPermission
	{
		get
		{
			return communityPermission_;
		}
		set
		{
			communityPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelPermission ChannelPermission
	{
		get
		{
			return channelPermission_;
		}
		set
		{
			channelPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadRoleState()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadRoleState(CommunityLogPayloadRoleState other)
		: this()
	{
		name_ = other.name_;
		colorHex_ = other.colorHex_;
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadRoleState Clone()
	{
		return new CommunityLogPayloadRoleState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadRoleState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadRoleState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (ColorHex != other.ColorHex)
		{
			return false;
		}
		if (!object.Equals(CommunityPermission, other.CommunityPermission))
		{
			return false;
		}
		if (!object.Equals(ChannelPermission, other.ChannelPermission))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (ColorHex.Length != 0)
		{
			num ^= ColorHex.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(10);
			P_0.WriteString(Name);
		}
		if (ColorHex.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(ColorHex);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(ChannelPermission);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (ColorHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ColorHex);
		}
		if (communityPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadRoleState other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.ColorHex.Length != 0)
		{
			ColorHex = other.ColorHex;
		}
		if (other.communityPermission_ != null)
		{
			if (communityPermission_ == null)
			{
				CommunityPermission = new CommunityPermission();
			}
			CommunityPermission.MergeFrom(other.CommunityPermission);
		}
		if (other.channelPermission_ != null)
		{
			if (channelPermission_ == null)
			{
				ChannelPermission = new ChannelPermission();
			}
			ChannelPermission.MergeFrom(other.ChannelPermission);
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
				Name = P_0.ReadString();
				break;
			case 18u:
				ColorHex = P_0.ReadString();
				break;
			case 26u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 34u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			}
		}
	}
}
