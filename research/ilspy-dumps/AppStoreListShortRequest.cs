using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AppStoreListShortRequest : IMessage<AppStoreListShortRequest>, IMessage, IEquatable<AppStoreListShortRequest>, IDeepCloneable<AppStoreListShortRequest>, IBufferMessage
{
	private static readonly MessageParser<AppStoreListShortRequest> _parser = new MessageParser<AppStoreListShortRequest>(() => new AppStoreListShortRequest());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<AppUuid> _repeated_ids_codec = FieldCodec.ForMessage(34u, AppUuid.Parser);

	private readonly RepeatedField<AppUuid> ids_ = new RepeatedField<AppUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreListShortRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[5];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<AppUuid> Ids => ids_;

	[GeneratedCode("protoc", null)]
	public AppStoreListShortRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListShortRequest(AppStoreListShortRequest other)
		: this()
	{
		ids_ = other.ids_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListShortRequest Clone()
	{
		return new AppStoreListShortRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreListShortRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreListShortRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!ids_.Equals(other.ids_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= ids_.GetHashCode();
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
		ids_.WriteTo(ref P_0, _repeated_ids_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += ids_.CalculateSize(_repeated_ids_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreListShortRequest other)
	{
		if (other != null)
		{
			ids_.Add(other.ids_);
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
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				ids_.AddEntriesFrom(ref P_0, _repeated_ids_codec);
			}
		}
	}
}
