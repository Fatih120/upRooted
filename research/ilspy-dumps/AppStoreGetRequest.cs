using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AppStoreGetRequest : IMessage<AppStoreGetRequest>, IMessage, IEquatable<AppStoreGetRequest>, IDeepCloneable<AppStoreGetRequest>, IBufferMessage
{
	private static readonly MessageParser<AppStoreGetRequest> _parser = new MessageParser<AppStoreGetRequest>(() => new AppStoreGetRequest());

	private UnknownFieldSet _unknownFields;

	private AppUuid id_;

	private AppVersionUuid appVersionId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreGetRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public AppUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppVersionUuid AppVersionId
	{
		get
		{
			return appVersionId_;
		}
		set
		{
			appVersionId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetRequest(AppStoreGetRequest other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		appVersionId_ = ((other.appVersionId_ != null) ? other.appVersionId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreGetRequest Clone()
	{
		return new AppStoreGetRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreGetRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreGetRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(AppVersionId, other.AppVersionId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (appVersionId_ != null)
		{
			num ^= AppVersionId.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (appVersionId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppVersionId);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (appVersionId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppVersionId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreGetRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new AppUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.appVersionId_ != null)
		{
			if (appVersionId_ == null)
			{
				AppVersionId = new AppVersionUuid();
			}
			AppVersionId.MergeFrom(other.AppVersionId);
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
				if (id_ == null)
				{
					Id = new AppUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (appVersionId_ == null)
				{
					AppVersionId = new AppVersionUuid();
				}
				P_0.ReadMessage(AppVersionId);
				break;
			}
		}
	}
}
