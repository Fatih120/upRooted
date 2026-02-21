using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AppStoreListRequest : IMessage<AppStoreListRequest>, IMessage, IEquatable<AppStoreListRequest>, IDeepCloneable<AppStoreListRequest>, IBufferMessage
{
	private static readonly MessageParser<AppStoreListRequest> _parser = new MessageParser<AppStoreListRequest>(() => new AppStoreListRequest());

	private UnknownFieldSet _unknownFields;

	private AppType appType_ = AppType.Unspecified;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreListRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public AppType AppType
	{
		get
		{
			return appType_;
		}
		set
		{
			appType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListRequest(AppStoreListRequest other)
		: this()
	{
		appType_ = other.appType_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListRequest Clone()
	{
		return new AppStoreListRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreListRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreListRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (AppType != other.AppType)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (AppType != AppType.Unspecified)
		{
			num ^= AppType.GetHashCode();
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
		if (AppType != AppType.Unspecified)
		{
			P_0.WriteRawTag(32);
			P_0.WriteEnum((int)AppType);
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
		if (AppType != AppType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppType);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreListRequest other)
	{
		if (other != null)
		{
			if (other.AppType != AppType.Unspecified)
			{
				AppType = other.AppType;
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 32)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				AppType = (AppType)P_0.ReadEnum();
			}
		}
	}
}
