using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.App.Settings;

public sealed class GlobalSettingRoleOrMemberPicker : IMessage<GlobalSettingRoleOrMemberPicker>, IMessage, IEquatable<GlobalSettingRoleOrMemberPicker>, IDeepCloneable<GlobalSettingRoleOrMemberPicker>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingRoleOrMemberPicker> _parser = new MessageParser<GlobalSettingRoleOrMemberPicker>(() => new GlobalSettingRoleOrMemberPicker());

	private UnknownFieldSet _unknownFields;

	private RoleOrMemberPickerBehavior selectBehavior_ = RoleOrMemberPickerBehavior.Unspecified;

	private static readonly FieldCodec<UserUuid> _repeated_userIds_codec = FieldCodec.ForMessage(50u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> userIds_ = new RepeatedField<UserUuid>();

	private static readonly FieldCodec<CommunityRoleUuid> _repeated_communityRoleIds_codec = FieldCodec.ForMessage(58u, CommunityRoleUuid.Parser);

	private readonly RepeatedField<CommunityRoleUuid> communityRoleIds_ = new RepeatedField<CommunityRoleUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingRoleOrMemberPicker> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RoleOrMemberPickerBehavior SelectBehavior
	{
		get
		{
			return selectBehavior_;
		}
		set
		{
			selectBehavior_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> UserIds => userIds_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleUuid> CommunityRoleIds => communityRoleIds_;

	[GeneratedCode("protoc", null)]
	public GlobalSettingRoleOrMemberPicker()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingRoleOrMemberPicker(GlobalSettingRoleOrMemberPicker other)
		: this()
	{
		selectBehavior_ = other.selectBehavior_;
		userIds_ = other.userIds_.Clone();
		communityRoleIds_ = other.communityRoleIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingRoleOrMemberPicker Clone()
	{
		return new GlobalSettingRoleOrMemberPicker(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingRoleOrMemberPicker);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingRoleOrMemberPicker other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SelectBehavior != other.SelectBehavior)
		{
			return false;
		}
		if (!userIds_.Equals(other.userIds_))
		{
			return false;
		}
		if (!communityRoleIds_.Equals(other.communityRoleIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (SelectBehavior != RoleOrMemberPickerBehavior.Unspecified)
		{
			num ^= SelectBehavior.GetHashCode();
		}
		num ^= userIds_.GetHashCode();
		num ^= communityRoleIds_.GetHashCode();
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
		if (SelectBehavior != RoleOrMemberPickerBehavior.Unspecified)
		{
			P_0.WriteRawTag(40);
			P_0.WriteEnum((int)SelectBehavior);
		}
		userIds_.WriteTo(ref P_0, _repeated_userIds_codec);
		communityRoleIds_.WriteTo(ref P_0, _repeated_communityRoleIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (SelectBehavior != RoleOrMemberPickerBehavior.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)SelectBehavior);
		}
		num += userIds_.CalculateSize(_repeated_userIds_codec);
		num += communityRoleIds_.CalculateSize(_repeated_communityRoleIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingRoleOrMemberPicker other)
	{
		if (other != null)
		{
			if (other.SelectBehavior != RoleOrMemberPickerBehavior.Unspecified)
			{
				SelectBehavior = other.SelectBehavior;
			}
			userIds_.Add(other.userIds_);
			communityRoleIds_.Add(other.communityRoleIds_);
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
			case 40u:
				SelectBehavior = (RoleOrMemberPickerBehavior)P_0.ReadEnum();
				break;
			case 50u:
				userIds_.AddEntriesFrom(ref P_0, _repeated_userIds_codec);
				break;
			case 58u:
				communityRoleIds_.AddEntriesFrom(ref P_0, _repeated_communityRoleIds_codec);
				break;
			}
		}
	}
}
