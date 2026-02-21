using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FileDownloadResponse : IMessage<FileDownloadResponse>, IMessage, IEquatable<FileDownloadResponse>, IDeepCloneable<FileDownloadResponse>, IBufferMessage
{
	private static readonly MessageParser<FileDownloadResponse> _parser = new MessageParser<FileDownloadResponse>(() => new FileDownloadResponse());

	private UnknownFieldSet _unknownFields;

	private FileResponse file_;

	private string client_ = "";

	private string bucket_ = "";

	private string key_ = "";

	private string eTag_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileDownloadResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public FileResponse File
	{
		get
		{
			return file_;
		}
		set
		{
			file_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Client
	{
		get
		{
			return client_;
		}
		set
		{
			client_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Bucket
	{
		get
		{
			return bucket_;
		}
		set
		{
			bucket_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string Key
	{
		get
		{
			return key_;
		}
		set
		{
			key_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string ETag
	{
		get
		{
			return eTag_;
		}
		set
		{
			eTag_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public FileDownloadResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileDownloadResponse(FileDownloadResponse other)
		: this()
	{
		file_ = ((other.file_ != null) ? other.file_.Clone() : null);
		client_ = other.client_;
		bucket_ = other.bucket_;
		key_ = other.key_;
		eTag_ = other.eTag_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileDownloadResponse Clone()
	{
		return new FileDownloadResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileDownloadResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileDownloadResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(File, other.File))
		{
			return false;
		}
		if (Client != other.Client)
		{
			return false;
		}
		if (Bucket != other.Bucket)
		{
			return false;
		}
		if (Key != other.Key)
		{
			return false;
		}
		if (ETag != other.ETag)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (file_ != null)
		{
			num ^= File.GetHashCode();
		}
		if (Client.Length != 0)
		{
			num ^= Client.GetHashCode();
		}
		if (Bucket.Length != 0)
		{
			num ^= Bucket.GetHashCode();
		}
		if (Key.Length != 0)
		{
			num ^= Key.GetHashCode();
		}
		if (ETag.Length != 0)
		{
			num ^= ETag.GetHashCode();
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
		if (file_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(File);
		}
		if (Client.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Client);
		}
		if (Bucket.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Bucket);
		}
		if (Key.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(Key);
		}
		if (ETag.Length != 0)
		{
			P_0.WriteRawTag(114);
			P_0.WriteString(ETag);
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
		if (file_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(File);
		}
		if (Client.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Client);
		}
		if (Bucket.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Bucket);
		}
		if (Key.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Key);
		}
		if (ETag.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ETag);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileDownloadResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.file_ != null)
		{
			if (file_ == null)
			{
				File = new FileResponse();
			}
			File.MergeFrom(other.File);
		}
		if (other.Client.Length != 0)
		{
			Client = other.Client;
		}
		if (other.Bucket.Length != 0)
		{
			Bucket = other.Bucket;
		}
		if (other.Key.Length != 0)
		{
			Key = other.Key;
		}
		if (other.ETag.Length != 0)
		{
			ETag = other.ETag;
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
			case 82u:
				if (file_ == null)
				{
					File = new FileResponse();
				}
				P_0.ReadMessage(File);
				break;
			case 90u:
				Client = P_0.ReadString();
				break;
			case 98u:
				Bucket = P_0.ReadString();
				break;
			case 106u:
				Key = P_0.ReadString();
				break;
			case 114u:
				ETag = P_0.ReadString();
				break;
			}
		}
	}
}
