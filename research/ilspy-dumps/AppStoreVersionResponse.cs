using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AppStoreVersionResponse : IMessage<AppStoreVersionResponse>, IMessage, IEquatable<AppStoreVersionResponse>, IDeepCloneable<AppStoreVersionResponse>, IBufferMessage
{
	private static readonly MessageParser<AppStoreVersionResponse> _parser = new MessageParser<AppStoreVersionResponse>(() => new AppStoreVersionResponse());

	private UnknownFieldSet _unknownFields;

	private AppVersionUuid id_;

	private SemanticVersion version_;

	private string notes_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreVersionResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public AppVersionUuid Id
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
	public SemanticVersion Version
	{
		get
		{
			return version_;
		}
		set
		{
			version_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Notes
	{
		get
		{
			return notes_;
		}
		set
		{
			notes_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public AppStoreVersionResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreVersionResponse(AppStoreVersionResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		version_ = ((other.version_ != null) ? other.version_.Clone() : null);
		notes_ = other.notes_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreVersionResponse Clone()
	{
		return new AppStoreVersionResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreVersionResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreVersionResponse other)
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
		if (!object.Equals(Version, other.Version))
		{
			return false;
		}
		if (Notes != other.Notes)
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
		if (version_ != null)
		{
			num ^= Version.GetHashCode();
		}
		if (Notes.Length != 0)
		{
			num ^= Notes.GetHashCode();
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
		if (version_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(Version);
		}
		if (Notes.Length != 0)
		{
			P_0.WriteRawTag(66);
			P_0.WriteString(Notes);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(Id);
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
		if (version_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Version);
		}
		if (Notes.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Notes);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreVersionResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new AppVersionUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.version_ != null)
		{
			if (version_ == null)
			{
				Version = new SemanticVersion();
			}
			Version.MergeFrom(other.Version);
		}
		if (other.Notes.Length != 0)
		{
			Notes = other.Notes;
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
			case 58u:
				if (version_ == null)
				{
					Version = new SemanticVersion();
				}
				P_0.ReadMessage(Version);
				break;
			case 66u:
				Notes = P_0.ReadString();
				break;
			case 74u:
				if (id_ == null)
				{
					Id = new AppVersionUuid();
				}
				P_0.ReadMessage(Id);
				break;
			}
		}
	}
}
