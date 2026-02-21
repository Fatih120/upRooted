using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserSetProfilePictureResponse : IMessage<UserSetProfilePictureResponse>, IMessage, IEquatable<UserSetProfilePictureResponse>, IDeepCloneable<UserSetProfilePictureResponse>, IBufferMessage
{
	private static readonly MessageParser<UserSetProfilePictureResponse> _parser = new MessageParser<UserSetProfilePictureResponse>(() => new UserSetProfilePictureResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private string profilePictureAssetUri_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSetProfilePictureResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[21];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string ProfilePictureAssetUri
	{
		get
		{
			return profilePictureAssetUri_;
		}
		set
		{
			profilePictureAssetUri_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePictureResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePictureResponse(UserSetProfilePictureResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		profilePictureAssetUri_ = other.profilePictureAssetUri_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePictureResponse Clone()
	{
		return new UserSetProfilePictureResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSetProfilePictureResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSetProfilePictureResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (ProfilePictureAssetUri != other.ProfilePictureAssetUri)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (ProfilePictureAssetUri.Length != 0)
		{
			num ^= ProfilePictureAssetUri.GetHashCode();
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
		if (userId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(UserId);
		}
		if (ProfilePictureAssetUri.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(ProfilePictureAssetUri);
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
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (ProfilePictureAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ProfilePictureAssetUri);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserSetProfilePictureResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.ProfilePictureAssetUri.Length != 0)
		{
			ProfilePictureAssetUri = other.ProfilePictureAssetUri;
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
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 42u:
				ProfilePictureAssetUri = P_0.ReadString();
				break;
			}
		}
	}
}
