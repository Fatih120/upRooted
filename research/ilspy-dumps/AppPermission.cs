using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.AppServices.Grpc;

public sealed class AppPermission : IMessage<AppPermission>, IMessage, IEquatable<AppPermission>, IDeepCloneable<AppPermission>, IBufferMessage
{
	private static readonly MessageParser<AppPermission> _parser = new MessageParser<AppPermission>(() => new AppPermission());

	private UnknownFieldSet _unknownFields;

	private bool hasNetwork_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppPermission> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppPermissionReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public bool HasNetwork
	{
		get
		{
			return hasNetwork_;
		}
		set
		{
			hasNetwork_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppPermission()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppPermission(AppPermission other)
		: this()
	{
		hasNetwork_ = other.hasNetwork_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppPermission Clone()
	{
		return new AppPermission(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppPermission);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppPermission other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (HasNetwork != other.HasNetwork)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (HasNetwork)
		{
			num ^= HasNetwork.GetHashCode();
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
		if (HasNetwork)
		{
			P_0.WriteRawTag(8);
			P_0.WriteBool(HasNetwork);
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
		if (HasNetwork)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppPermission other)
	{
		if (other != null)
		{
			if (other.HasNetwork)
			{
				HasNetwork = other.HasNetwork;
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
			if (num3 != 8)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				HasNetwork = P_0.ReadBool();
			}
		}
	}
}
