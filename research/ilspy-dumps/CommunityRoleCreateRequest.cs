using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityRoleCreateRequest : IMessage<CommunityRoleCreateRequest>, IMessage, IEquatable<CommunityRoleCreateRequest>, IDeepCloneable<CommunityRoleCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityRoleCreateRequest> _parser = new MessageParser<CommunityRoleCreateRequest>(() => new CommunityRoleCreateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_colorHex_codec = FieldCodec.ForClassWrapper<string>(98u);

	private string colorHex_;

	private CommunityPermission communityPermission_;

	private ChannelPermission channelPermission_;

	private bool isMentionable_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityRoleCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityRoleReflection.Descriptor.MessageTypes[0];

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
			colorHex_ = value;
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
	public bool IsMentionable
	{
		get
		{
			return isMentionable_;
		}
		set
		{
			isMentionable_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleCreateRequest(CommunityRoleCreateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		name_ = other.name_;
		ColorHex = other.ColorHex;
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		isMentionable_ = other.isMentionable_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleCreateRequest Clone()
	{
		return new CommunityRoleCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityRoleCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityRoleCreateRequest other)
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
		if (IsMentionable != other.IsMentionable)
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (colorHex_ != null)
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
		if (IsMentionable)
		{
			num ^= IsMentionable.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Name);
		}
		if (colorHex_ != null)
		{
			_single_colorHex_codec.WriteTagAndValue(ref P_0, ColorHex);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(ChannelPermission);
		}
		if (IsMentionable)
		{
			P_0.WriteRawTag(136, 1);
			P_0.WriteBool(IsMentionable);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (colorHex_ != null)
		{
			num += _single_colorHex_codec.CalculateSizeWithTag(ColorHex);
		}
		if (communityPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		if (channelPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (IsMentionable)
		{
			num += 3;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityRoleCreateRequest other)
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
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.colorHex_ != null && (colorHex_ == null || other.ColorHex != ""))
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
		if (other.IsMentionable)
		{
			IsMentionable = other.IsMentionable;
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
				Name = P_0.ReadString();
				break;
			case 98u:
			{
				string text = _single_colorHex_codec.Read(ref P_0);
				if (colorHex_ == null || text != "")
				{
					ColorHex = text;
				}
				break;
			}
			case 122u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 130u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 136u:
				IsMentionable = P_0.ReadBool();
				break;
			}
		}
	}
}
