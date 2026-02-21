using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.App.Settings;

public sealed class GlobalSettingChannelPicker : IMessage<GlobalSettingChannelPicker>, IMessage, IEquatable<GlobalSettingChannelPicker>, IDeepCloneable<GlobalSettingChannelPicker>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingChannelPicker> _parser = new MessageParser<GlobalSettingChannelPicker>(() => new GlobalSettingChannelPicker());

	private UnknownFieldSet _unknownFields;

	private bool multiSelect_;

	private static readonly FieldCodec<ChannelUuid> _repeated_channelIds_codec = FieldCodec.ForMessage(50u, ChannelUuid.Parser);

	private readonly RepeatedField<ChannelUuid> channelIds_ = new RepeatedField<ChannelUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingChannelPicker> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[4];

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
	public RepeatedField<ChannelUuid> ChannelIds => channelIds_;

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelPicker()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelPicker(GlobalSettingChannelPicker other)
		: this()
	{
		multiSelect_ = other.multiSelect_;
		channelIds_ = other.channelIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelPicker Clone()
	{
		return new GlobalSettingChannelPicker(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingChannelPicker);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingChannelPicker other)
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
		if (!channelIds_.Equals(other.channelIds_))
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
		num ^= channelIds_.GetHashCode();
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
		channelIds_.WriteTo(ref P_0, _repeated_channelIds_codec);
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
		num += channelIds_.CalculateSize(_repeated_channelIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingChannelPicker other)
	{
		if (other != null)
		{
			if (other.MultiSelect)
			{
				MultiSelect = other.MultiSelect;
			}
			channelIds_.Add(other.channelIds_);
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
				channelIds_.AddEntriesFrom(ref P_0, _repeated_channelIds_codec);
				break;
			}
		}
	}
}
