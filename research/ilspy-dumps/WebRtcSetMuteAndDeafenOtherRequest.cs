using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class WebRtcSetMuteAndDeafenOtherRequest : IMessage<WebRtcSetMuteAndDeafenOtherRequest>, IMessage, IEquatable<WebRtcSetMuteAndDeafenOtherRequest>, IDeepCloneable<WebRtcSetMuteAndDeafenOtherRequest>, IBufferMessage
{
	private static readonly MessageParser<WebRtcSetMuteAndDeafenOtherRequest> _parser = new MessageParser<WebRtcSetMuteAndDeafenOtherRequest>(() => new WebRtcSetMuteAndDeafenOtherRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private UserUuid userId_;

	private static readonly FieldCodec<bool?> _single_isMuted_codec = FieldCodec.ForStructWrapper<bool>(106u);

	private bool? isMuted_;

	private static readonly FieldCodec<bool?> _single_isDeafened_codec = FieldCodec.ForStructWrapper<bool>(114u);

	private bool? isDeafened_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<WebRtcSetMuteAndDeafenOtherRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => WebRtcReflection.Descriptor.MessageTypes[9];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

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
	public MessageContainerUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? IsMuted
	{
		get
		{
			return isMuted_;
		}
		set
		{
			isMuted_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool? IsDeafened
	{
		get
		{
			return isDeafened_;
		}
		set
		{
			isDeafened_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSetMuteAndDeafenOtherRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSetMuteAndDeafenOtherRequest(WebRtcSetMuteAndDeafenOtherRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		IsMuted = other.IsMuted;
		IsDeafened = other.IsDeafened;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public WebRtcSetMuteAndDeafenOtherRequest Clone()
	{
		return new WebRtcSetMuteAndDeafenOtherRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as WebRtcSetMuteAndDeafenOtherRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(WebRtcSetMuteAndDeafenOtherRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (IsMuted != other.IsMuted)
		{
			return false;
		}
		if (IsDeafened != other.IsDeafened)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (isMuted_.HasValue)
		{
			num ^= IsMuted.GetHashCode();
		}
		if (isDeafened_.HasValue)
		{
			num ^= IsDeafened.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ContainerId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(UserId);
		}
		if (isMuted_.HasValue)
		{
			_single_isMuted_codec.WriteTagAndValue(ref P_0, IsMuted);
		}
		if (isDeafened_.HasValue)
		{
			_single_isDeafened_codec.WriteTagAndValue(ref P_0, IsDeafened);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (isMuted_.HasValue)
		{
			num += _single_isMuted_codec.CalculateSizeWithTag(IsMuted);
		}
		if (isDeafened_.HasValue)
		{
			num += _single_isDeafened_codec.CalculateSizeWithTag(IsDeafened);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(WebRtcSetMuteAndDeafenOtherRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.isMuted_.HasValue && (!isMuted_.HasValue || other.IsMuted != false))
		{
			IsMuted = other.IsMuted;
		}
		if (other.isDeafened_.HasValue && (!isDeafened_.HasValue || other.IsDeafened != false))
		{
			IsDeafened = other.IsDeafened;
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
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 82u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 98u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 106u:
			{
				bool? flag2 = _single_isMuted_codec.Read(ref P_0);
				if (!isMuted_.HasValue || flag2 != false)
				{
					IsMuted = flag2;
				}
				break;
			}
			case 114u:
			{
				bool? flag = _single_isDeafened_codec.Read(ref P_0);
				if (!isDeafened_.HasValue || flag != false)
				{
					IsDeafened = flag;
				}
				break;
			}
			}
		}
	}
}
