using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.App;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AppStoreSearchRequest : IMessage<AppStoreSearchRequest>, IMessage, IEquatable<AppStoreSearchRequest>, IDeepCloneable<AppStoreSearchRequest>, IBufferMessage
{
	private static readonly MessageParser<AppStoreSearchRequest> _parser = new MessageParser<AppStoreSearchRequest>(() => new AppStoreSearchRequest());

	private UnknownFieldSet _unknownFields;

	private AppType appType_ = AppType.Unspecified;

	private AppOrganizationUuid appOrganizationId_;

	private AppCategory appCategory_ = AppCategory.Unspecified;

	private static readonly FieldCodec<string> _single_keywords_codec = FieldCodec.ForClassWrapper<string>(58u);

	private string keywords_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AppStoreSearchRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AppStoreReflection.Descriptor.MessageTypes[3];

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
	public AppOrganizationUuid AppOrganizationId
	{
		get
		{
			return appOrganizationId_;
		}
		set
		{
			appOrganizationId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppCategory AppCategory
	{
		get
		{
			return appCategory_;
		}
		set
		{
			appCategory_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Keywords
	{
		get
		{
			return keywords_;
		}
		set
		{
			keywords_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AppStoreSearchRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AppStoreSearchRequest(AppStoreSearchRequest other)
		: this()
	{
		appType_ = other.appType_;
		appOrganizationId_ = ((other.appOrganizationId_ != null) ? other.appOrganizationId_.Clone() : null);
		appCategory_ = other.appCategory_;
		Keywords = other.Keywords;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AppStoreSearchRequest Clone()
	{
		return new AppStoreSearchRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AppStoreSearchRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AppStoreSearchRequest other)
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
		if (!object.Equals(AppOrganizationId, other.AppOrganizationId))
		{
			return false;
		}
		if (AppCategory != other.AppCategory)
		{
			return false;
		}
		if (Keywords != other.Keywords)
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
		if (appOrganizationId_ != null)
		{
			num ^= AppOrganizationId.GetHashCode();
		}
		if (AppCategory != AppCategory.Unspecified)
		{
			num ^= AppCategory.GetHashCode();
		}
		if (keywords_ != null)
		{
			num ^= Keywords.GetHashCode();
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
		if (appOrganizationId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(AppOrganizationId);
		}
		if (AppCategory != AppCategory.Unspecified)
		{
			P_0.WriteRawTag(48);
			P_0.WriteEnum((int)AppCategory);
		}
		if (keywords_ != null)
		{
			_single_keywords_codec.WriteTagAndValue(ref P_0, Keywords);
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
		if (appOrganizationId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AppOrganizationId);
		}
		if (AppCategory != AppCategory.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)AppCategory);
		}
		if (keywords_ != null)
		{
			num += _single_keywords_codec.CalculateSizeWithTag(Keywords);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AppStoreSearchRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.AppType != AppType.Unspecified)
		{
			AppType = other.AppType;
		}
		if (other.appOrganizationId_ != null)
		{
			if (appOrganizationId_ == null)
			{
				AppOrganizationId = new AppOrganizationUuid();
			}
			AppOrganizationId.MergeFrom(other.AppOrganizationId);
		}
		if (other.AppCategory != AppCategory.Unspecified)
		{
			AppCategory = other.AppCategory;
		}
		if (other.keywords_ != null && (keywords_ == null || other.Keywords != ""))
		{
			Keywords = other.Keywords;
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
			case 32u:
				AppType = (AppType)P_0.ReadEnum();
				break;
			case 42u:
				if (appOrganizationId_ == null)
				{
					AppOrganizationId = new AppOrganizationUuid();
				}
				P_0.ReadMessage(AppOrganizationId);
				break;
			case 48u:
				AppCategory = (AppCategory)P_0.ReadEnum();
				break;
			case 58u:
			{
				string text = _single_keywords_codec.Read(ref P_0);
				if (keywords_ == null || text != "")
				{
					Keywords = text;
				}
				break;
			}
			}
		}
	}
}
