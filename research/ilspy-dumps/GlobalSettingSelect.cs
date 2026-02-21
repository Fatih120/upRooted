using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingSelect : IMessage<GlobalSettingSelect>, IMessage, IEquatable<GlobalSettingSelect>, IDeepCloneable<GlobalSettingSelect>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingSelect> _parser = new MessageParser<GlobalSettingSelect>(() => new GlobalSettingSelect());

	private UnknownFieldSet _unknownFields;

	private bool multiSelect_;

	private static readonly FieldCodec<string> _repeated_values_codec = FieldCodec.ForString(50u);

	private readonly RepeatedField<string> values_ = new RepeatedField<string>();

	private static readonly FieldCodec<GlobalSettingSelectOption> _repeated_options_codec = FieldCodec.ForMessage(58u, GlobalSettingSelectOption.Parser);

	private readonly RepeatedField<GlobalSettingSelectOption> options_ = new RepeatedField<GlobalSettingSelectOption>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingSelect> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[15];

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
	public RepeatedField<string> Values => values_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<GlobalSettingSelectOption> Options => options_;

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelect()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelect(GlobalSettingSelect other)
		: this()
	{
		multiSelect_ = other.multiSelect_;
		values_ = other.values_.Clone();
		options_ = other.options_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelect Clone()
	{
		return new GlobalSettingSelect(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingSelect);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingSelect other)
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
		if (!values_.Equals(other.values_))
		{
			return false;
		}
		if (!options_.Equals(other.options_))
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
		num ^= values_.GetHashCode();
		num ^= options_.GetHashCode();
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
		values_.WriteTo(ref P_0, _repeated_values_codec);
		options_.WriteTo(ref P_0, _repeated_options_codec);
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
		num += values_.CalculateSize(_repeated_values_codec);
		num += options_.CalculateSize(_repeated_options_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingSelect other)
	{
		if (other != null)
		{
			if (other.MultiSelect)
			{
				MultiSelect = other.MultiSelect;
			}
			values_.Add(other.values_);
			options_.Add(other.options_);
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
				values_.AddEntriesFrom(ref P_0, _repeated_values_codec);
				break;
			case 58u:
				options_.AddEntriesFrom(ref P_0, _repeated_options_codec);
				break;
			}
		}
	}
}
