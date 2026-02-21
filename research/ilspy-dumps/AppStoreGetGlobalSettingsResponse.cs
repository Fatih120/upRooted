using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App.Settings;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AppStoreGetGlobalSettingsResponse : IMessage<AppStoreGetGlobalSettingsResponse>, IMessage, IEquatable<AppStoreGetGlobalSettingsResponse>, IDeepCloneable<AppStoreGetGlobalSettingsResponse>, IBufferMessage
{
	private static readonly MessageParser<AppStoreGetGlobalSettingsResponse> _parser = new MessageParser<AppStoreGetGlobalSettingsResponse>(() => new AppStoreGetGlobalSettingsResponse());

	private UnknownFieldSet _unknownFields;

	private GlobalSettings globalSettings_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreGetGlobalSettingsResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public GlobalSettings GlobalSettings
	{
		get
		{
			return globalSettings_;
		}
		set
		{
			globalSettings_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetGlobalSettingsResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetGlobalSettingsResponse(AppStoreGetGlobalSettingsResponse other)
		: this()
	{
		globalSettings_ = ((other.globalSettings_ != null) ? other.globalSettings_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetGlobalSettingsResponse Clone()
	{
		return new AppStoreGetGlobalSettingsResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreGetGlobalSettingsResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreGetGlobalSettingsResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(GlobalSettings, other.GlobalSettings))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (globalSettings_ != null)
		{
			num ^= GlobalSettings.GetHashCode();
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
		if (globalSettings_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(GlobalSettings);
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
		if (globalSettings_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(GlobalSettings);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreGetGlobalSettingsResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.globalSettings_ != null)
		{
			if (globalSettings_ == null)
			{
				GlobalSettings = new GlobalSettings();
			}
			GlobalSettings.MergeFrom(other.GlobalSettings);
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (globalSettings_ == null)
			{
				GlobalSettings = new GlobalSettings();
			}
			P_0.ReadMessage(GlobalSettings);
		}
	}
}
