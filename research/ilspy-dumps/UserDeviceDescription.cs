using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserDeviceDescription : IMessage<UserDeviceDescription>, IMessage, IEquatable<UserDeviceDescription>, IDeepCloneable<UserDeviceDescription>, IBufferMessage
{
	private static readonly MessageParser<UserDeviceDescription> _parser = new MessageParser<UserDeviceDescription>(() => new UserDeviceDescription());

	private UnknownFieldSet _unknownFields;

	private string os_ = "";

	private string osVersion_ = "";

	private string deviceName_ = "";

	private bool isMobile_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserDeviceDescription> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[21];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public string Os
	{
		get
		{
			return os_;
		}
		set
		{
			os_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string OsVersion
	{
		get
		{
			return osVersion_;
		}
		set
		{
			osVersion_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string DeviceName
	{
		get
		{
			return deviceName_;
		}
		set
		{
			deviceName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsMobile
	{
		get
		{
			return isMobile_;
		}
		set
		{
			isMobile_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserDeviceDescription()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserDeviceDescription(UserDeviceDescription other)
		: this()
	{
		os_ = other.os_;
		osVersion_ = other.osVersion_;
		deviceName_ = other.deviceName_;
		isMobile_ = other.isMobile_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserDeviceDescription Clone()
	{
		return new UserDeviceDescription(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserDeviceDescription);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserDeviceDescription other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Os != other.Os)
		{
			return false;
		}
		if (OsVersion != other.OsVersion)
		{
			return false;
		}
		if (DeviceName != other.DeviceName)
		{
			return false;
		}
		if (IsMobile != other.IsMobile)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Os.Length != 0)
		{
			num ^= Os.GetHashCode();
		}
		if (OsVersion.Length != 0)
		{
			num ^= OsVersion.GetHashCode();
		}
		if (DeviceName.Length != 0)
		{
			num ^= DeviceName.GetHashCode();
		}
		if (IsMobile)
		{
			num ^= IsMobile.GetHashCode();
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
		if (Os.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Os);
		}
		if (OsVersion.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(OsVersion);
		}
		if (DeviceName.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(DeviceName);
		}
		if (IsMobile)
		{
			P_0.WriteRawTag(104);
			P_0.WriteBool(IsMobile);
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
		if (Os.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Os);
		}
		if (OsVersion.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(OsVersion);
		}
		if (DeviceName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(DeviceName);
		}
		if (IsMobile)
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
	public void MergeFrom(UserDeviceDescription other)
	{
		if (other != null)
		{
			if (other.Os.Length != 0)
			{
				Os = other.Os;
			}
			if (other.OsVersion.Length != 0)
			{
				OsVersion = other.OsVersion;
			}
			if (other.DeviceName.Length != 0)
			{
				DeviceName = other.DeviceName;
			}
			if (other.IsMobile)
			{
				IsMobile = other.IsMobile;
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 82u:
				Os = P_0.ReadString();
				break;
			case 90u:
				OsVersion = P_0.ReadString();
				break;
			case 98u:
				DeviceName = P_0.ReadString();
				break;
			case 104u:
				IsMobile = P_0.ReadBool();
				break;
			}
		}
	}
}
