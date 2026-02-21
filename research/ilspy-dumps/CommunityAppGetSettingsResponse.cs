using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App.Settings;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityAppGetSettingsResponse : IMessage<CommunityAppGetSettingsResponse>, IMessage, IEquatable<CommunityAppGetSettingsResponse>, IDeepCloneable<CommunityAppGetSettingsResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityAppGetSettingsResponse> _parser = new MessageParser<CommunityAppGetSettingsResponse>(() => new CommunityAppGetSettingsResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private GlobalSettings settings_;

	private ChannelUuid channelId_;

	private CommunityAppVersionUpdateFull update_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityAppGetSettingsResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityAppReflection.Descriptor.MessageTypes[12];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
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
	public CommunityAppVersionUpdateFull Update
	{
		get
		{
			return update_;
		}
		set
		{
			update_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetSettingsResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetSettingsResponse(CommunityAppGetSettingsResponse other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		settings_ = ((other.settings_ != null) ? other.settings_.Clone() : null);
		channelId_ = ((other.channelId_ != null) ? other.channelId_.Clone() : null);
		update_ = ((other.update_ != null) ? other.update_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppGetSettingsResponse Clone()
	{
		return new CommunityAppGetSettingsResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityAppGetSettingsResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityAppGetSettingsResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(Settings, other.Settings))
		{
			return false;
		}
		if (!object.Equals(ChannelId, other.ChannelId))
		{
			return false;
		}
		if (!object.Equals(Update, other.Update))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (settings_ != null)
		{
			num ^= Settings.GetHashCode();
		}
		if (channelId_ != null)
		{
			num ^= ChannelId.GetHashCode();
		}
		if (update_ != null)
		{
			num ^= Update.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityId);
		}
		if (settings_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Settings);
		}
		if (channelId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(ChannelId);
		}
		if (update_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(Update);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (settings_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Settings);
		}
		if (channelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelId);
		}
		if (update_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Update);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityAppGetSettingsResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.settings_ != null)
		{
			if (settings_ == null)
			{
				Settings = new GlobalSettings();
			}
			Settings.MergeFrom(other.Settings);
		}
		if (other.channelId_ != null)
		{
			if (channelId_ == null)
			{
				ChannelId = new ChannelUuid();
			}
			ChannelId.MergeFrom(other.ChannelId);
		}
		if (other.update_ != null)
		{
			if (update_ == null)
			{
				Update = new CommunityAppVersionUpdateFull();
			}
			Update.MergeFrom(other.Update);
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
			case 34u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 42u:
				if (settings_ == null)
				{
					Settings = new GlobalSettings();
				}
				P_0.ReadMessage(Settings);
				break;
			case 50u:
				if (channelId_ == null)
				{
					ChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(ChannelId);
				break;
			case 58u:
				if (update_ == null)
				{
					Update = new CommunityAppVersionUpdateFull();
				}
				P_0.ReadMessage(Update);
				break;
			}
		}
	}
}
