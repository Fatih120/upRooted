using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Core;

public sealed class SemanticVersion : IComparable<SemanticVersion>, IMessage<SemanticVersion>, IMessage, IEquatable<SemanticVersion>, IDeepCloneable<SemanticVersion>, IBufferMessage
{
	private static readonly MessageParser<SemanticVersion> _parser = new MessageParser<SemanticVersion>(() => new SemanticVersion());

	private UnknownFieldSet _unknownFields;

	private uint major_;

	private uint minor_;

	private uint patch_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<SemanticVersion> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => SemanticVersionReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public uint Major
	{
		get
		{
			return major_;
		}
		set
		{
			major_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public uint Minor
	{
		get
		{
			return minor_;
		}
		set
		{
			minor_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public uint Patch
	{
		get
		{
			return patch_;
		}
		set
		{
			patch_ = value;
		}
	}

	public static SemanticVersion? Parse(string input)
	{
		if (Version.TryParse(input, out Version version))
		{
			return new SemanticVersion
			{
				Major = (uint)version.Major,
				Minor = (uint)version.Minor,
				Patch = ((version.Build > 0) ? ((uint)version.Build) : 0u)
			};
		}
		return null;
	}

	public static bool TryParse(string input, out SemanticVersion? result)
	{
		result = null;
		if (Version.TryParse(input, out Version version))
		{
			result = new SemanticVersion
			{
				Major = (uint)version.Major,
				Minor = (uint)version.Minor,
				Patch = ((version.Build > 0) ? ((uint)version.Build) : 0u)
			};
			return true;
		}
		return false;
	}

	public string ToVersionString()
	{
		return $"{Major}.{Minor}.{Patch}";
	}

	public int CompareTo(SemanticVersion? other)
	{
		if (other == null)
		{
			return -1;
		}
		if (Major != other.Major)
		{
			return Major.CompareTo(other.Major);
		}
		if (Minor != other.Minor)
		{
			return Minor.CompareTo(other.Minor);
		}
		if (Patch != other.Patch)
		{
			return Patch.CompareTo(other.Patch);
		}
		return 0;
	}

	public static bool operator ==(SemanticVersion? left, SemanticVersion? right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		return left.CompareTo(right) == 0;
	}

	public static bool operator !=(SemanticVersion? left, SemanticVersion? right)
	{
		return !(left == right);
	}

	public static bool operator <(SemanticVersion? left, SemanticVersion? right)
	{
		if ((object)left == null)
		{
			return (object)right != null;
		}
		return left.CompareTo(right) < 0;
	}

	public static bool operator <=(SemanticVersion? left, SemanticVersion? right)
	{
		if ((object)left == null)
		{
			return true;
		}
		return left.CompareTo(right) <= 0;
	}

	public static bool operator >(SemanticVersion? left, SemanticVersion? right)
	{
		if ((object)left == null)
		{
			return false;
		}
		return left.CompareTo(right) > 0;
	}

	public static bool operator >=(SemanticVersion? left, SemanticVersion? right)
	{
		if ((object)left == null)
		{
			return (object)right == null;
		}
		return left.CompareTo(right) >= 0;
	}

	[GeneratedCode("protoc", null)]
	public SemanticVersion()
	{
	}

	[GeneratedCode("protoc", null)]
	public SemanticVersion(SemanticVersion other)
		: this()
	{
		major_ = other.major_;
		minor_ = other.minor_;
		patch_ = other.patch_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public SemanticVersion Clone()
	{
		return new SemanticVersion(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as SemanticVersion);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(SemanticVersion other)
	{
		if ((object)other == null)
		{
			return false;
		}
		if ((object)other == this)
		{
			return true;
		}
		if (Major != other.Major)
		{
			return false;
		}
		if (Minor != other.Minor)
		{
			return false;
		}
		if (Patch != other.Patch)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (Major != 0)
		{
			num ^= Major.GetHashCode();
		}
		if (Minor != 0)
		{
			num ^= Minor.GetHashCode();
		}
		if (Patch != 0)
		{
			num ^= Patch.GetHashCode();
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
		if (Major != 0)
		{
			P_0.WriteRawTag(168, 1);
			P_0.WriteUInt32(Major);
		}
		if (Minor != 0)
		{
			P_0.WriteRawTag(176, 1);
			P_0.WriteUInt32(Minor);
		}
		if (Patch != 0)
		{
			P_0.WriteRawTag(184, 1);
			P_0.WriteUInt32(Patch);
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
		if (Major != 0)
		{
			num += 2 + CodedOutputStream.ComputeUInt32Size(Major);
		}
		if (Minor != 0)
		{
			num += 2 + CodedOutputStream.ComputeUInt32Size(Minor);
		}
		if (Patch != 0)
		{
			num += 2 + CodedOutputStream.ComputeUInt32Size(Patch);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(SemanticVersion other)
	{
		if (!(other == null))
		{
			if (other.Major != 0)
			{
				Major = other.Major;
			}
			if (other.Minor != 0)
			{
				Minor = other.Minor;
			}
			if (other.Patch != 0)
			{
				Patch = other.Patch;
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
			case 168u:
				Major = P_0.ReadUInt32();
				break;
			case 176u:
				Minor = P_0.ReadUInt32();
				break;
			case 184u:
				Patch = P_0.ReadUInt32();
				break;
			}
		}
	}
}
