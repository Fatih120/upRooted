using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AppStoreListVersionResponse : IMessage<AppStoreListVersionResponse>, IMessage, IEquatable<AppStoreListVersionResponse>, IDeepCloneable<AppStoreListVersionResponse>, IBufferMessage
{
	private static readonly MessageParser<AppStoreListVersionResponse> _parser = new MessageParser<AppStoreListVersionResponse>(() => new AppStoreListVersionResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<AppStoreVersionResponse> _repeated_versions_codec = FieldCodec.ForMessage(34u, AppStoreVersionResponse.Parser);

	private readonly RepeatedField<AppStoreVersionResponse> versions_ = new RepeatedField<AppStoreVersionResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreListVersionResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<AppStoreVersionResponse> Versions => versions_;

	[GeneratedCode("protoc", null)]
	public AppStoreListVersionResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListVersionResponse(AppStoreListVersionResponse other)
		: this()
	{
		versions_ = other.versions_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreListVersionResponse Clone()
	{
		return new AppStoreListVersionResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreListVersionResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreListVersionResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!versions_.Equals(other.versions_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= versions_.GetHashCode();
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
		versions_.WriteTo(ref P_0, _repeated_versions_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += versions_.CalculateSize(_repeated_versions_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreListVersionResponse other)
	{
		if (other != null)
		{
			versions_.Add(other.versions_);
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
				versions_.AddEntriesFrom(ref P_0, _repeated_versions_codec);
			}
		}
	}
}
