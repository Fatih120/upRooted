using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.App.Settings;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityGetForAppResponse : IMessage<CommunityGetForAppResponse>, IMessage, IEquatable<CommunityGetForAppResponse>, IDeepCloneable<CommunityGetForAppResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityGetForAppResponse> _parser = new MessageParser<CommunityGetForAppResponse>(() => new CommunityGetForAppResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid id_;

	private ChannelUuid channelId_;

	private GlobalSettings settings_;

	private static readonly FieldCodec<CommunityMemberShort> _repeated_communityMembers_codec = FieldCodec.ForMessage(106u, CommunityMemberShort.Parser);

	private readonly RepeatedField<CommunityMemberShort> communityMembers_ = new RepeatedField<CommunityMemberShort>();

	private static readonly FieldCodec<CommunityRoleResponse> _repeated_communityRoles_codec = FieldCodec.ForMessage(114u, CommunityRoleResponse.Parser);

	private readonly RepeatedField<CommunityRoleResponse> communityRoles_ = new RepeatedField<CommunityRoleResponse>();

	private bool isInitialized_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityGetForAppResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid ChannelId
	{
		get
		{
			return channelId_;
		}
		set
		{
			channelId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettings Settings
	{
		get
		{
			return settings_;
		}
		set
		{
			settings_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityMemberShort> CommunityMembers => communityMembers_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleResponse> CommunityRoles => communityRoles_;

	[GeneratedCode("protoc", null)]
	public bool IsInitialized
	{
		get
		{
			return isInitialized_;
		}
		set
		{
			isInitialized_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityGetForAppResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityGetForAppResponse(CommunityGetForAppResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		channelId_ = ((other.channelId_ != null) ? other.channelId_.Clone() : null);
		settings_ = ((other.settings_ != null) ? other.settings_.Clone() : null);
		communityMembers_ = other.communityMembers_.Clone();
		communityRoles_ = other.communityRoles_.Clone();
		isInitialized_ = other.isInitialized_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityGetForAppResponse Clone()
	{
		return new CommunityGetForAppResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityGetForAppResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityGetForAppResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(ChannelId, other.ChannelId))
		{
			return false;
		}
		if (!object.Equals(Settings, other.Settings))
		{
			return false;
		}
		if (!communityMembers_.Equals(other.communityMembers_))
		{
			return false;
		}
		if (!communityRoles_.Equals(other.communityRoles_))
		{
			return false;
		}
		if (IsInitialized != other.IsInitialized)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (channelId_ != null)
		{
			num ^= ChannelId.GetHashCode();
		}
		if (settings_ != null)
		{
			num ^= Settings.GetHashCode();
		}
		num ^= communityMembers_.GetHashCode();
		num ^= communityRoles_.GetHashCode();
		if (IsInitialized)
		{
			num ^= IsInitialized.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Id);
		}
		if (channelId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ChannelId);
		}
		if (settings_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Settings);
		}
		communityMembers_.WriteTo(ref P_0, _repeated_communityMembers_codec);
		communityRoles_.WriteTo(ref P_0, _repeated_communityRoles_codec);
		if (IsInitialized)
		{
			P_0.WriteRawTag(120);
			P_0.WriteBool(IsInitialized);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (channelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelId);
		}
		if (settings_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Settings);
		}
		num += communityMembers_.CalculateSize(_repeated_communityMembers_codec);
		num += communityRoles_.CalculateSize(_repeated_communityRoles_codec);
		if (IsInitialized)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityGetForAppResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.channelId_ != null)
		{
			if (channelId_ == null)
			{
				ChannelId = new ChannelUuid();
			}
			ChannelId.MergeFrom(other.ChannelId);
		}
		if (other.settings_ != null)
		{
			if (settings_ == null)
			{
				Settings = new GlobalSettings();
			}
			Settings.MergeFrom(other.Settings);
		}
		communityMembers_.Add(other.communityMembers_);
		communityRoles_.Add(other.communityRoles_);
		if (other.IsInitialized)
		{
			IsInitialized = other.IsInitialized;
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
			case 82u:
				if (id_ == null)
				{
					Id = new CommunityUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				if (channelId_ == null)
				{
					ChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(ChannelId);
				break;
			case 98u:
				if (settings_ == null)
				{
					Settings = new GlobalSettings();
				}
				P_0.ReadMessage(Settings);
				break;
			case 106u:
				communityMembers_.AddEntriesFrom(ref P_0, _repeated_communityMembers_codec);
				break;
			case 114u:
				communityRoles_.AddEntriesFrom(ref P_0, _repeated_communityRoles_codec);
				break;
			case 120u:
				IsInitialized = P_0.ReadBool();
				break;
			}
		}
	}
}
