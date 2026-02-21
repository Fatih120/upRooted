using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingNumber : IMessage<GlobalSettingNumber>, IMessage, IEquatable<GlobalSettingNumber>, IDeepCloneable<GlobalSettingNumber>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingNumber> _parser = new MessageParser<GlobalSettingNumber>(() => new GlobalSettingNumber());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<double?> _single_value_codec = FieldCodec.ForStructWrapper<double>(42u);

	private double? value_;

	private static readonly FieldCodec<double?> _single_minValue_codec = FieldCodec.ForStructWrapper<double>(50u);

	private double? minValue_;

	private static readonly FieldCodec<double?> _single_maxValue_codec = FieldCodec.ForStructWrapper<double>(58u);

	private double? maxValue_;

	private static readonly FieldCodec<double?> _single_step_codec = FieldCodec.ForStructWrapper<double>(66u);

	private double? step_;

	private static readonly FieldCodec<double?> _single_defaultValue_codec = FieldCodec.ForStructWrapper<double>(74u);

	private double? defaultValue_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingNumber> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[13];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public double? Value
	{
		get
		{
			return value_;
		}
		set
		{
			value_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public double? MinValue
	{
		get
		{
			return minValue_;
		}
		set
		{
			minValue_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public double? MaxValue
	{
		get
		{
			return maxValue_;
		}
		set
		{
			maxValue_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public double? Step
	{
		get
		{
			return step_;
		}
		set
		{
			step_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public double? DefaultValue
	{
		get
		{
			return defaultValue_;
		}
		set
		{
			defaultValue_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingNumber()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingNumber(GlobalSettingNumber other)
		: this()
	{
		Value = other.Value;
		MinValue = other.MinValue;
		MaxValue = other.MaxValue;
		Step = other.Step;
		DefaultValue = other.DefaultValue;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingNumber Clone()
	{
		return new GlobalSettingNumber(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingNumber);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingNumber other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.Equals(Value, other.Value))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.Equals(MinValue, other.MinValue))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.Equals(MaxValue, other.MaxValue))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.Equals(Step, other.Step))
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.Equals(DefaultValue, other.DefaultValue))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (value_.HasValue)
		{
			num ^= ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.GetHashCode(Value);
		}
		if (minValue_.HasValue)
		{
			num ^= ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.GetHashCode(MinValue);
		}
		if (maxValue_.HasValue)
		{
			num ^= ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.GetHashCode(MaxValue);
		}
		if (step_.HasValue)
		{
			num ^= ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.GetHashCode(Step);
		}
		if (defaultValue_.HasValue)
		{
			num ^= ProtobufEqualityComparers.BitwiseNullableDoubleEqualityComparer.GetHashCode(DefaultValue);
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
		if (value_.HasValue)
		{
			_single_value_codec.WriteTagAndValue(ref P_0, Value);
		}
		if (minValue_.HasValue)
		{
			_single_minValue_codec.WriteTagAndValue(ref P_0, MinValue);
		}
		if (maxValue_.HasValue)
		{
			_single_maxValue_codec.WriteTagAndValue(ref P_0, MaxValue);
		}
		if (step_.HasValue)
		{
			_single_step_codec.WriteTagAndValue(ref P_0, Step);
		}
		if (defaultValue_.HasValue)
		{
			_single_defaultValue_codec.WriteTagAndValue(ref P_0, DefaultValue);
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
		if (value_.HasValue)
		{
			num += _single_value_codec.CalculateSizeWithTag(Value);
		}
		if (minValue_.HasValue)
		{
			num += _single_minValue_codec.CalculateSizeWithTag(MinValue);
		}
		if (maxValue_.HasValue)
		{
			num += _single_maxValue_codec.CalculateSizeWithTag(MaxValue);
		}
		if (step_.HasValue)
		{
			num += _single_step_codec.CalculateSizeWithTag(Step);
		}
		if (defaultValue_.HasValue)
		{
			num += _single_defaultValue_codec.CalculateSizeWithTag(DefaultValue);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingNumber other)
	{
		if (other != null)
		{
			if (other.value_.HasValue && (!value_.HasValue || other.Value != 0.0))
			{
				Value = other.Value;
			}
			if (other.minValue_.HasValue && (!minValue_.HasValue || other.MinValue != 0.0))
			{
				MinValue = other.MinValue;
			}
			if (other.maxValue_.HasValue && (!maxValue_.HasValue || other.MaxValue != 0.0))
			{
				MaxValue = other.MaxValue;
			}
			if (other.step_.HasValue && (!step_.HasValue || other.Step != 0.0))
			{
				Step = other.Step;
			}
			if (other.defaultValue_.HasValue && (!defaultValue_.HasValue || other.DefaultValue != 0.0))
			{
				DefaultValue = other.DefaultValue;
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
			case 42u:
			{
				double? num3 = _single_value_codec.Read(ref P_0);
				if (!value_.HasValue || num3 != 0.0)
				{
					Value = num3;
				}
				break;
			}
			case 50u:
			{
				double? num6 = _single_minValue_codec.Read(ref P_0);
				if (!minValue_.HasValue || num6 != 0.0)
				{
					MinValue = num6;
				}
				break;
			}
			case 58u:
			{
				double? num4 = _single_maxValue_codec.Read(ref P_0);
				if (!maxValue_.HasValue || num4 != 0.0)
				{
					MaxValue = num4;
				}
				break;
			}
			case 66u:
			{
				double? num5 = _single_step_codec.Read(ref P_0);
				if (!step_.HasValue || num5 != 0.0)
				{
					Step = num5;
				}
				break;
			}
			case 74u:
			{
				double? num2 = _single_defaultValue_codec.Read(ref P_0);
				if (!defaultValue_.HasValue || num2 != 0.0)
				{
					DefaultValue = num2;
				}
				break;
			}
			}
		}
	}
}
