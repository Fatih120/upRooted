using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.App.Settings;

public sealed class GlobalSetting : IMessage<GlobalSetting>, IMessage, IEquatable<GlobalSetting>, IDeepCloneable<GlobalSetting>, IBufferMessage
{
	public enum ItemOneofCase
	{
		None = 0,
		Text = 10,
		Number = 11,
		Checkbox = 12,
		RoleOrMember = 13,
		Channel = 14,
		ChannelGroup = 15,
		Select = 16,
		Timestamp = 17,
		Time = 18,
		Date = 19,
		Color = 20,
		Button = 21
	}

	private static readonly MessageParser<GlobalSetting> _parser = new MessageParser<GlobalSetting>(() => new GlobalSetting());

	private UnknownFieldSet _unknownFields;

	private string key_ = "";

	private string title_ = "";

	private static readonly FieldCodec<string> _single_description_codec = FieldCodec.ForClassWrapper<string>(26u);

	private string description_;

	private bool required_;

	private int orderBy_;

	private string confirmation_ = "";

	private object item_;

	private ItemOneofCase itemCase_ = ItemOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSetting> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[2];

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
	public string Description
	{
		get
		{
			return description_;
		}
		set
		{
			description_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool Required
	{
		get
		{
			return required_;
		}
		set
		{
			required_ = value;
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
	public string Confirmation
	{
		get
		{
			return confirmation_;
		}
		set
		{
			confirmation_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingText Text
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Text) ? ((GlobalSettingText)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Text : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingNumber Number
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Number) ? ((GlobalSettingNumber)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Number : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingCheckbox Checkbox
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Checkbox) ? ((GlobalSettingCheckbox)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Checkbox : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingRoleOrMemberPicker RoleOrMember
	{
		get
		{
			return (itemCase_ == ItemOneofCase.RoleOrMember) ? ((GlobalSettingRoleOrMemberPicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.RoleOrMember : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelPicker Channel
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Channel) ? ((GlobalSettingChannelPicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Channel : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingChannelGroupPicker ChannelGroup
	{
		get
		{
			return (itemCase_ == ItemOneofCase.ChannelGroup) ? ((GlobalSettingChannelGroupPicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.ChannelGroup : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingSelect Select
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Select) ? ((GlobalSettingSelect)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Select : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimestampPicker Timestamp
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Timestamp) ? ((GlobalSettingTimestampPicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Timestamp : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingTimePicker Time
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Time) ? ((GlobalSettingTimePicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Time : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingDatePicker Date
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Date) ? ((GlobalSettingDatePicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Date : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingColorPicker Color
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Color) ? ((GlobalSettingColorPicker)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Color : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettingButton Button
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Button) ? ((GlobalSettingButton)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Button : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ItemOneofCase ItemCase => itemCase_;

	[GeneratedCode("protoc", null)]
	public GlobalSetting()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSetting(GlobalSetting other)
		: this()
	{
		key_ = other.key_;
		title_ = other.title_;
		Description = other.Description;
		required_ = other.required_;
		orderBy_ = other.orderBy_;
		confirmation_ = other.confirmation_;
		switch (other.ItemCase)
		{
		case ItemOneofCase.Text:
			Text = other.Text.Clone();
			break;
		case ItemOneofCase.Number:
			Number = other.Number.Clone();
			break;
		case ItemOneofCase.Checkbox:
			Checkbox = other.Checkbox.Clone();
			break;
		case ItemOneofCase.RoleOrMember:
			RoleOrMember = other.RoleOrMember.Clone();
			break;
		case ItemOneofCase.Channel:
			Channel = other.Channel.Clone();
			break;
		case ItemOneofCase.ChannelGroup:
			ChannelGroup = other.ChannelGroup.Clone();
			break;
		case ItemOneofCase.Select:
			Select = other.Select.Clone();
			break;
		case ItemOneofCase.Timestamp:
			Timestamp = other.Timestamp.Clone();
			break;
		case ItemOneofCase.Time:
			Time = other.Time.Clone();
			break;
		case ItemOneofCase.Date:
			Date = other.Date.Clone();
			break;
		case ItemOneofCase.Color:
			Color = other.Color.Clone();
			break;
		case ItemOneofCase.Button:
			Button = other.Button.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSetting Clone()
	{
		return new GlobalSetting(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearItem()
	{
		itemCase_ = ItemOneofCase.None;
		item_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSetting);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSetting other)
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
		if (Description != other.Description)
		{
			return false;
		}
		if (Required != other.Required)
		{
			return false;
		}
		if (OrderBy != other.OrderBy)
		{
			return false;
		}
		if (Confirmation != other.Confirmation)
		{
			return false;
		}
		if (!object.Equals(Text, other.Text))
		{
			return false;
		}
		if (!object.Equals(Number, other.Number))
		{
			return false;
		}
		if (!object.Equals(Checkbox, other.Checkbox))
		{
			return false;
		}
		if (!object.Equals(RoleOrMember, other.RoleOrMember))
		{
			return false;
		}
		if (!object.Equals(Channel, other.Channel))
		{
			return false;
		}
		if (!object.Equals(ChannelGroup, other.ChannelGroup))
		{
			return false;
		}
		if (!object.Equals(Select, other.Select))
		{
			return false;
		}
		if (!object.Equals(Timestamp, other.Timestamp))
		{
			return false;
		}
		if (!object.Equals(Time, other.Time))
		{
			return false;
		}
		if (!object.Equals(Date, other.Date))
		{
			return false;
		}
		if (!object.Equals(Color, other.Color))
		{
			return false;
		}
		if (!object.Equals(Button, other.Button))
		{
			return false;
		}
		if (ItemCase != other.ItemCase)
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
		if (description_ != null)
		{
			num ^= Description.GetHashCode();
		}
		if (Required)
		{
			num ^= Required.GetHashCode();
		}
		if (OrderBy != 0)
		{
			num ^= OrderBy.GetHashCode();
		}
		if (Confirmation.Length != 0)
		{
			num ^= Confirmation.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Text)
		{
			num ^= Text.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Number)
		{
			num ^= Number.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Checkbox)
		{
			num ^= Checkbox.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.RoleOrMember)
		{
			num ^= RoleOrMember.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Channel)
		{
			num ^= Channel.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.ChannelGroup)
		{
			num ^= ChannelGroup.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Select)
		{
			num ^= Select.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Timestamp)
		{
			num ^= Timestamp.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Time)
		{
			num ^= Time.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Date)
		{
			num ^= Date.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Color)
		{
			num ^= Color.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Button)
		{
			num ^= Button.GetHashCode();
		}
		num ^= (int)itemCase_;
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
			P_0.WriteRawTag(10);
			P_0.WriteString(Key);
		}
		if (Title.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(Title);
		}
		if (description_ != null)
		{
			_single_description_codec.WriteTagAndValue(ref P_0, Description);
		}
		if (Required)
		{
			P_0.WriteRawTag(32);
			P_0.WriteBool(Required);
		}
		if (OrderBy != 0)
		{
			P_0.WriteRawTag(40);
			P_0.WriteInt32(OrderBy);
		}
		if (Confirmation.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Confirmation);
		}
		if (itemCase_ == ItemOneofCase.Text)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Text);
		}
		if (itemCase_ == ItemOneofCase.Number)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Number);
		}
		if (itemCase_ == ItemOneofCase.Checkbox)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Checkbox);
		}
		if (itemCase_ == ItemOneofCase.RoleOrMember)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(RoleOrMember);
		}
		if (itemCase_ == ItemOneofCase.Channel)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(Channel);
		}
		if (itemCase_ == ItemOneofCase.ChannelGroup)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(ChannelGroup);
		}
		if (itemCase_ == ItemOneofCase.Select)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(Select);
		}
		if (itemCase_ == ItemOneofCase.Timestamp)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(Timestamp);
		}
		if (itemCase_ == ItemOneofCase.Time)
		{
			P_0.WriteRawTag(146, 1);
			P_0.WriteMessage(Time);
		}
		if (itemCase_ == ItemOneofCase.Date)
		{
			P_0.WriteRawTag(154, 1);
			P_0.WriteMessage(Date);
		}
		if (itemCase_ == ItemOneofCase.Color)
		{
			P_0.WriteRawTag(162, 1);
			P_0.WriteMessage(Color);
		}
		if (itemCase_ == ItemOneofCase.Button)
		{
			P_0.WriteRawTag(170, 1);
			P_0.WriteMessage(Button);
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
		if (Key.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Key);
		}
		if (Title.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Title);
		}
		if (description_ != null)
		{
			num += _single_description_codec.CalculateSizeWithTag(Description);
		}
		if (Required)
		{
			num += 2;
		}
		if (OrderBy != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(OrderBy);
		}
		if (Confirmation.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Confirmation);
		}
		if (itemCase_ == ItemOneofCase.Text)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Text);
		}
		if (itemCase_ == ItemOneofCase.Number)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Number);
		}
		if (itemCase_ == ItemOneofCase.Checkbox)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Checkbox);
		}
		if (itemCase_ == ItemOneofCase.RoleOrMember)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(RoleOrMember);
		}
		if (itemCase_ == ItemOneofCase.Channel)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Channel);
		}
		if (itemCase_ == ItemOneofCase.ChannelGroup)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelGroup);
		}
		if (itemCase_ == ItemOneofCase.Select)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Select);
		}
		if (itemCase_ == ItemOneofCase.Timestamp)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Timestamp);
		}
		if (itemCase_ == ItemOneofCase.Time)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Time);
		}
		if (itemCase_ == ItemOneofCase.Date)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Date);
		}
		if (itemCase_ == ItemOneofCase.Color)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Color);
		}
		if (itemCase_ == ItemOneofCase.Button)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Button);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSetting other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Key.Length != 0)
		{
			Key = other.Key;
		}
		if (other.Title.Length != 0)
		{
			Title = other.Title;
		}
		if (other.description_ != null && (description_ == null || other.Description != ""))
		{
			Description = other.Description;
		}
		if (other.Required)
		{
			Required = other.Required;
		}
		if (other.OrderBy != 0)
		{
			OrderBy = other.OrderBy;
		}
		if (other.Confirmation.Length != 0)
		{
			Confirmation = other.Confirmation;
		}
		switch (other.ItemCase)
		{
		case ItemOneofCase.Text:
			if (Text == null)
			{
				Text = new GlobalSettingText();
			}
			Text.MergeFrom(other.Text);
			break;
		case ItemOneofCase.Number:
			if (Number == null)
			{
				Number = new GlobalSettingNumber();
			}
			Number.MergeFrom(other.Number);
			break;
		case ItemOneofCase.Checkbox:
			if (Checkbox == null)
			{
				Checkbox = new GlobalSettingCheckbox();
			}
			Checkbox.MergeFrom(other.Checkbox);
			break;
		case ItemOneofCase.RoleOrMember:
			if (RoleOrMember == null)
			{
				RoleOrMember = new GlobalSettingRoleOrMemberPicker();
			}
			RoleOrMember.MergeFrom(other.RoleOrMember);
			break;
		case ItemOneofCase.Channel:
			if (Channel == null)
			{
				Channel = new GlobalSettingChannelPicker();
			}
			Channel.MergeFrom(other.Channel);
			break;
		case ItemOneofCase.ChannelGroup:
			if (ChannelGroup == null)
			{
				ChannelGroup = new GlobalSettingChannelGroupPicker();
			}
			ChannelGroup.MergeFrom(other.ChannelGroup);
			break;
		case ItemOneofCase.Select:
			if (Select == null)
			{
				Select = new GlobalSettingSelect();
			}
			Select.MergeFrom(other.Select);
			break;
		case ItemOneofCase.Timestamp:
			if (Timestamp == null)
			{
				Timestamp = new GlobalSettingTimestampPicker();
			}
			Timestamp.MergeFrom(other.Timestamp);
			break;
		case ItemOneofCase.Time:
			if (Time == null)
			{
				Time = new GlobalSettingTimePicker();
			}
			Time.MergeFrom(other.Time);
			break;
		case ItemOneofCase.Date:
			if (Date == null)
			{
				Date = new GlobalSettingDatePicker();
			}
			Date.MergeFrom(other.Date);
			break;
		case ItemOneofCase.Color:
			if (Color == null)
			{
				Color = new GlobalSettingColorPicker();
			}
			Color.MergeFrom(other.Color);
			break;
		case ItemOneofCase.Button:
			if (Button == null)
			{
				Button = new GlobalSettingButton();
			}
			Button.MergeFrom(other.Button);
			break;
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
				Key = P_0.ReadString();
				break;
			case 18u:
				Title = P_0.ReadString();
				break;
			case 26u:
			{
				string text = _single_description_codec.Read(ref P_0);
				if (description_ == null || text != "")
				{
					Description = text;
				}
				break;
			}
			case 32u:
				Required = P_0.ReadBool();
				break;
			case 40u:
				OrderBy = P_0.ReadInt32();
				break;
			case 50u:
				Confirmation = P_0.ReadString();
				break;
			case 82u:
			{
				GlobalSettingText globalSettingText = new GlobalSettingText();
				if (itemCase_ == ItemOneofCase.Text)
				{
					globalSettingText.MergeFrom(Text);
				}
				P_0.ReadMessage(globalSettingText);
				Text = globalSettingText;
				break;
			}
			case 90u:
			{
				GlobalSettingNumber globalSettingNumber = new GlobalSettingNumber();
				if (itemCase_ == ItemOneofCase.Number)
				{
					globalSettingNumber.MergeFrom(Number);
				}
				P_0.ReadMessage(globalSettingNumber);
				Number = globalSettingNumber;
				break;
			}
			case 98u:
			{
				GlobalSettingCheckbox globalSettingCheckbox = new GlobalSettingCheckbox();
				if (itemCase_ == ItemOneofCase.Checkbox)
				{
					globalSettingCheckbox.MergeFrom(Checkbox);
				}
				P_0.ReadMessage(globalSettingCheckbox);
				Checkbox = globalSettingCheckbox;
				break;
			}
			case 106u:
			{
				GlobalSettingRoleOrMemberPicker globalSettingRoleOrMemberPicker = new GlobalSettingRoleOrMemberPicker();
				if (itemCase_ == ItemOneofCase.RoleOrMember)
				{
					globalSettingRoleOrMemberPicker.MergeFrom(RoleOrMember);
				}
				P_0.ReadMessage(globalSettingRoleOrMemberPicker);
				RoleOrMember = globalSettingRoleOrMemberPicker;
				break;
			}
			case 114u:
			{
				GlobalSettingChannelPicker globalSettingChannelPicker = new GlobalSettingChannelPicker();
				if (itemCase_ == ItemOneofCase.Channel)
				{
					globalSettingChannelPicker.MergeFrom(Channel);
				}
				P_0.ReadMessage(globalSettingChannelPicker);
				Channel = globalSettingChannelPicker;
				break;
			}
			case 122u:
			{
				GlobalSettingChannelGroupPicker globalSettingChannelGroupPicker = new GlobalSettingChannelGroupPicker();
				if (itemCase_ == ItemOneofCase.ChannelGroup)
				{
					globalSettingChannelGroupPicker.MergeFrom(ChannelGroup);
				}
				P_0.ReadMessage(globalSettingChannelGroupPicker);
				ChannelGroup = globalSettingChannelGroupPicker;
				break;
			}
			case 130u:
			{
				GlobalSettingSelect globalSettingSelect = new GlobalSettingSelect();
				if (itemCase_ == ItemOneofCase.Select)
				{
					globalSettingSelect.MergeFrom(Select);
				}
				P_0.ReadMessage(globalSettingSelect);
				Select = globalSettingSelect;
				break;
			}
			case 138u:
			{
				GlobalSettingTimestampPicker globalSettingTimestampPicker = new GlobalSettingTimestampPicker();
				if (itemCase_ == ItemOneofCase.Timestamp)
				{
					globalSettingTimestampPicker.MergeFrom(Timestamp);
				}
				P_0.ReadMessage(globalSettingTimestampPicker);
				Timestamp = globalSettingTimestampPicker;
				break;
			}
			case 146u:
			{
				GlobalSettingTimePicker globalSettingTimePicker = new GlobalSettingTimePicker();
				if (itemCase_ == ItemOneofCase.Time)
				{
					globalSettingTimePicker.MergeFrom(Time);
				}
				P_0.ReadMessage(globalSettingTimePicker);
				Time = globalSettingTimePicker;
				break;
			}
			case 154u:
			{
				GlobalSettingDatePicker globalSettingDatePicker = new GlobalSettingDatePicker();
				if (itemCase_ == ItemOneofCase.Date)
				{
					globalSettingDatePicker.MergeFrom(Date);
				}
				P_0.ReadMessage(globalSettingDatePicker);
				Date = globalSettingDatePicker;
				break;
			}
			case 162u:
			{
				GlobalSettingColorPicker globalSettingColorPicker = new GlobalSettingColorPicker();
				if (itemCase_ == ItemOneofCase.Color)
				{
					globalSettingColorPicker.MergeFrom(Color);
				}
				P_0.ReadMessage(globalSettingColorPicker);
				Color = globalSettingColorPicker;
				break;
			}
			case 170u:
			{
				GlobalSettingButton globalSettingButton = new GlobalSettingButton();
				if (itemCase_ == ItemOneofCase.Button)
				{
					globalSettingButton.MergeFrom(Button);
				}
				P_0.ReadMessage(globalSettingButton);
				Button = globalSettingButton;
				break;
			}
			}
		}
	}
}
