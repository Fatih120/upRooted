using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Grpc;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadAccessRuleState : IMessage, IMessage<CommunityLogPayloadAccessRuleState>, IEquatable<CommunityLogPayloadAccessRuleState>, IDeepCloneable<CommunityLogPayloadAccessRuleState>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadAccessRuleState> _parser = new MessageParser<CommunityLogPayloadAccessRuleState>(() => new CommunityLogPayloadAccessRuleState());

	private UnknownFieldSet _unknownFields;

	private ChannelOverlayPermission overlay_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadAccessRuleState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public ChannelOverlayPermission Overlay
	{
		get
		{
			return overlay_;
		}
		set
		{
			overlay_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRuleState()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRuleState(CommunityLogPayloadAccessRuleState other)
		: this()
	{
		overlay_ = ((other.overlay_ != null) ? other.overlay_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRuleState Clone()
	{
		return new CommunityLogPayloadAccessRuleState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadAccessRuleState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadAccessRuleState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Overlay, other.Overlay))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (overlay_ != null)
		{
			num ^= Overlay.GetHashCode();
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
		if (overlay_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Overlay);
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
		if (overlay_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Overlay);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadAccessRuleState other)
	{
		if (other == null)
		{
			return;
		}
		if (other.overlay_ != null)
		{
			if (overlay_ == null)
			{
				Overlay = new ChannelOverlayPermission();
			}
			Overlay.MergeFrom(other.Overlay);
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
			if (overlay_ == null)
			{
				Overlay = new ChannelOverlayPermission();
			}
			P_0.ReadMessage(Overlay);
		}
	}
}
