using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSettingGroup : IMessage<GlobalSettingGroup>, IMessage, IEquatable<GlobalSettingGroup>, IDeepCloneable<GlobalSettingGroup>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettingGroup> _parser = new MessageParser<GlobalSettingGroup>(() => new GlobalSettingGroup());

	private UnknownFieldSet _unknownFields;

	private string key_ = "";

	private string title_ = "";

	private int orderBy_;

	private static readonly FieldCodec<GlobalSetting> _repeated_items_codec = FieldCodec.ForMessage(66u, GlobalSetting.Parser);

	private readonly RepeatedField<GlobalSetting> items_ = new RepeatedField<GlobalSetting>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettingGroup> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Key
	{
		get
		{
			return key_;
		}
		set
		{
			key_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Title
	{
		get
		{
			return title_;
		}
		set
		{
			title_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public int OrderBy
	{
		get
		{
			return orderBy_;
		}
		set
		{
			orderBy_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<GlobalSetting> Items => items_;

	[GeneratedCode("protoc", null)]
	public GlobalSettingGroup()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingGroup(GlobalSettingGroup other)
		: this()
	{
		key_ = other.key_;
		title_ = other.title_;
		orderBy_ = other.orderBy_;
		items_ = other.items_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingGroup Clone()
	{
		return new GlobalSettingGroup(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettingGroup);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettingGroup other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Key != other.Key)
		{
			return false;
		}
		if (Title != other.Title)
		{
			return false;
		}
		if (OrderBy != other.OrderBy)
		{
			return false;
		}
		if (!items_.Equals(other.items_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Key.Length != 0)
		{
			num ^= Key.GetHashCode();
		}
		if (Title.Length != 0)
		{
			num ^= Title.GetHashCode();
		}
		if (OrderBy != 0)
		{
			num ^= OrderBy.GetHashCode();
		}
		num ^= items_.GetHashCode();
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
		if (Key.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Key);
		}
		if (Title.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Title);
		}
		if (OrderBy != 0)
		{
			P_0.WriteRawTag(56);
			P_0.WriteInt32(OrderBy);
		}
		items_.WriteTo(ref P_0, _repeated_items_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (Key.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Key);
		}
		if (Title.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Title);
		}
		if (OrderBy != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(OrderBy);
		}
		num += items_.CalculateSize(_repeated_items_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettingGroup other)
	{
		if (other != null)
		{
			if (other.Key.Length != 0)
			{
				Key = other.Key;
			}
			if (other.Title.Length != 0)
			{
				Title = other.Title;
			}
			if (other.OrderBy != 0)
			{
				OrderBy = other.OrderBy;
			}
			items_.Add(other.items_);
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
				Key = P_0.ReadString();
				break;
			case 50u:
				Title = P_0.ReadString();
				break;
			case 56u:
				OrderBy = P_0.ReadInt32();
				break;
			case 66u:
				items_.AddEntriesFrom(ref P_0, _repeated_items_codec);
				break;
			}
		}
	}
}
