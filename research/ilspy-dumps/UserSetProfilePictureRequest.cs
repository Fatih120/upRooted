using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class UserSetProfilePictureRequest : IMessage<UserSetProfilePictureRequest>, IMessage, IEquatable<UserSetProfilePictureRequest>, IDeepCloneable<UserSetProfilePictureRequest>, IBufferMessage
{
	private static readonly MessageParser<UserSetProfilePictureRequest> _parser = new MessageParser<UserSetProfilePictureRequest>(() => new UserSetProfilePictureRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private static readonly FieldCodec<string> _single_profileTokenUri_codec = FieldCodec.ForClassWrapper<string>(82u);

	private string profileTokenUri_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSetProfilePictureRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[18];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ProfileTokenUri
	{
		get
		{
			return profileTokenUri_;
		}
		set
		{
			profileTokenUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePictureRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePictureRequest(UserSetProfilePictureRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		ProfileTokenUri = other.ProfileTokenUri;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePictureRequest Clone()
	{
		return new UserSetProfilePictureRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSetProfilePictureRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSetProfilePictureRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
		}
		if (ProfileTokenUri != other.ProfileTokenUri)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (profileTokenUri_ != null)
		{
			num ^= ProfileTokenUri.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
		if (profileTokenUri_ != null)
		{
			_single_profileTokenUri_codec.WriteTagAndValue(ref P_0, ProfileTokenUri);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (profileTokenUri_ != null)
		{
			num += _single_profileTokenUri_codec.CalculateSizeWithTag(ProfileTokenUri);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserSetProfilePictureRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
		}
		if (other.profileTokenUri_ != null && (profileTokenUri_ == null || other.ProfileTokenUri != ""))
		{
			ProfileTokenUri = other.ProfileTokenUri;
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
				break;
			case 82u:
			{
				string text = _single_profileTokenUri_codec.Read(ref P_0);
				if (profileTokenUri_ == null || text != "")
				{
					ProfileTokenUri = text;
				}
				break;
			}
			}
		}
	}
}
