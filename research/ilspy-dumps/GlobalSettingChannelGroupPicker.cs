using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.App.Settings;

public sealed class GlobalSettingChannelGroupPicker : IMessage<GlobalSettingChannelGroupPicker>, IMessage, IEquatable<GlobalSettingChannelGroupPicker>, IDeepCloneable<GlobalSettingChannelGroupPicker>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingChannelGroupPicker> _parser = new MessageParser<GlobalSettingChannelGroupPicker>(() => new GlobalSettingChannelGroupPicker());

	private UnknownFieldSet _unknownFields;

	private bool multiSelect_;

	private static readonly FieldCodec<ChannelGroupUuid> _repeated_channelGroupIds_codec = FieldCodec.ForMessage(50u, ChannelGroupUuid.Parser);

	private readonly RepeatedField<ChannelGroupUuid> channelGroupIds_ = new RepeatedField<ChannelGroupUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingChannelGroupPicker> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public bool MultiSelect
	{
		get
		{
			return multiSelect_;
		}
		set
		{
			multiSelect_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupUuid> ChannelGroupIds => channelGroupIds_;

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelGroupPicker()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelGroupPicker(GlobalSettingChannelGroupPicker other)
		: this()
	{
		multiSelect_ = other.multiSelect_;
		channelGroupIds_ = other.channelGroupIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelGroupPicker Clone()
	{
		return new GlobalSettingChannelGroupPicker(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingChannelGroupPicker);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingChannelGroupPicker other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (MultiSelect != other.MultiSelect)
		{
			return false;
		}
		if (!channelGroupIds_.Equals(other.channelGroupIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (MultiSelect)
		{
			num ^= MultiSelect.GetHashCode();
		}
		num ^= channelGroupIds_.GetHashCode();
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
		if (MultiSelect)
		{
			P_0.WriteRawTag(40);
			P_0.WriteBool(MultiSelect);
		}
		channelGroupIds_.WriteTo(ref P_0, _repeated_channelGroupIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (MultiSelect)
		{
			num += 2;
		}
		num += channelGroupIds_.CalculateSize(_repeated_channelGroupIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingChannelGroupPicker other)
	{
		if (other != null)
		{
			if (other.MultiSelect)
			{
				MultiSelect = other.MultiSelect;
			}
			channelGroupIds_.Add(other.channelGroupIds_);
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
				MultiSelect = P_0.ReadBool();
				break;
			case 50u:
				channelGroupIds_.AddEntriesFrom(ref P_0, _repeated_channelGroupIds_codec);
				break;
			}
		}
	}
}
