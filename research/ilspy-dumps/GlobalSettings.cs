using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.App.Settings;

public sealed class GlobalSettings : IMessage<GlobalSettings>, IMessage, IEquatable<GlobalSettings>, IDeepCloneable<GlobalSettings>, IBufferMessage
{
	private static readonly MessageParser<GlobalSettings> _parser = new MessageParser<GlobalSettings>(() => new GlobalSettings());

	private UnknownFieldSet _unknownFields;

	private AppUuid appId_;

	private static readonly FieldCodec<GlobalSettingGroup> _repeated_groups_codec = FieldCodec.ForMessage(58u, GlobalSettingGroup.Parser);

	private readonly RepeatedField<GlobalSettingGroup> groups_ = new RepeatedField<GlobalSettingGroup>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<GlobalSettings> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppSettingsReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public AppUuid AppId
	{
		get
		{
			return appId_;
		}
		set
		{
			appId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<GlobalSettingGroup> Groups => groups_;

	[GeneratedCode("protoc", null)]
	public GlobalSettings()
	{
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettings(GlobalSettings other)
		: this()
	{
		appId_ = ((other.appId_ != null) ? other.appId_.Clone() : null);
		groups_ = other.groups_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public GlobalSettings Clone()
	{
		return new GlobalSettings(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as GlobalSettings);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(GlobalSettings other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(AppId, other.AppId))
		{
			return false;
		}
		if (!groups_.Equals(other.groups_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (appId_ != null)
		{
			num ^= AppId.GetHashCode();
		}
		num ^= groups_.GetHashCode();
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
		if (appId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppId);
		}
		groups_.WriteTo(ref P_0, _repeated_groups_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (appId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppId);
		}
		num += groups_.CalculateSize(_repeated_groups_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(GlobalSettings other)
	{
		if (other == null)
		{
			return;
		}
		if (other.appId_ != null)
		{
			if (appId_ == null)
			{
				AppId = new AppUuid();
			}
			AppId.MergeFrom(other.AppId);
		}
		groups_.Add(other.groups_);
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
			case 42u:
				if (appId_ == null)
				{
					AppId = new AppUuid();
				}
				P_0.ReadMessage(AppId);
				break;
			case 58u:
				groups_.AddEntriesFrom(ref P_0, _repeated_groups_codec);
				break;
			}
		}
	}
}
