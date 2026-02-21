using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Google.Protobuf.WellKnownTypes;
using RootApp.Browser.Models;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Core;
using RootApp.Core.Converters;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.Browser.WebRtc.Models;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(UserResponse))]
[JsonSerializable(typeof(RootApp.Browser.Models.UserOnlineStatus), TypeInfoPropertyName = "UserOnlineStatusBrowser")]
[GeneratedCode("System.Text.Json.SourceGeneration", "10.0.14.7603")]
public class UserResponseJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
{
	private JsonTypeInfo<RootApp.Browser.Models.UserOnlineStatus>? _UserOnlineStatusBrowser;

	private JsonTypeInfo<UserResponse>? _UserResponse;

	private JsonTypeInfo<UserGuid>? _UserGuid;

	private JsonTypeInfo<RootApp.WebApi.Shared.Enums.UserOnlineStatus>? _UserOnlineStatus;

	private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

	private static readonly JsonEncodedText PropName_seconds = JsonEncodedText.Encode("seconds");

	private static readonly JsonEncodedText PropName_nanos = JsonEncodedText.Encode("nanos");

	private static readonly JsonEncodedText PropName_userId = JsonEncodedText.Encode("userId");

	private static readonly JsonEncodedText PropName_profilePictureAssetUri = JsonEncodedText.Encode("profilePictureAssetUri");

	private static readonly JsonEncodedText PropName_username = JsonEncodedText.Encode("username");

	private static readonly JsonEncodedText PropName_onlineStatus = JsonEncodedText.Encode("onlineStatus");

	private static readonly JsonEncodedText PropName_id = JsonEncodedText.Encode("id");

	private static readonly JsonEncodedText PropName_profilePictureUri = JsonEncodedText.Encode("profilePictureUri");

	private static readonly JsonEncodedText PropName_userName = JsonEncodedText.Encode("userName");

	private static readonly JsonEncodedText PropName_isFriend = JsonEncodedText.Encode("isFriend");

	private static readonly JsonEncodedText PropName_isBlocked = JsonEncodedText.Encode("isBlocked");

	private static readonly JsonEncodedText PropName_badges = JsonEncodedText.Encode("badges");

	private static readonly JsonEncodedText PropName_primaryBadge = JsonEncodedText.Encode("primaryBadge");

	private static readonly JsonEncodedText PropName_high64 = JsonEncodedText.Encode("high64");

	private static readonly JsonEncodedText PropName_low64 = JsonEncodedText.Encode("low64");

	private static readonly JsonEncodedText PropName_createdAt = JsonEncodedText.Encode("createdAt");

	public JsonTypeInfo<RootApp.Browser.Models.UserOnlineStatus> UserOnlineStatusBrowser => _UserOnlineStatusBrowser ?? (_UserOnlineStatusBrowser = (JsonTypeInfo<RootApp.Browser.Models.UserOnlineStatus>)base.Options.GetTypeInfo(typeof(RootApp.Browser.Models.UserOnlineStatus)));

	public JsonTypeInfo<UserResponse> UserResponse => _UserResponse ?? (_UserResponse = (JsonTypeInfo<UserResponse>)base.Options.GetTypeInfo(typeof(UserResponse)));

	public JsonTypeInfo<UserGuid> UserGuid => _UserGuid ?? (_UserGuid = (JsonTypeInfo<UserGuid>)base.Options.GetTypeInfo(typeof(UserGuid)));

	public JsonTypeInfo<RootApp.WebApi.Shared.Enums.UserOnlineStatus> UserOnlineStatus => _UserOnlineStatus ?? (_UserOnlineStatus = (JsonTypeInfo<RootApp.WebApi.Shared.Enums.UserOnlineStatus>)base.Options.GetTypeInfo(typeof(RootApp.WebApi.Shared.Enums.UserOnlineStatus)));

	public static UserResponseJsonContext Default { get; } = new UserResponseJsonContext(new JsonSerializerOptions(s_defaultOptions));

	protected override JsonSerializerOptions? GeneratedSerializerOptions { get; } = s_defaultOptions;

	private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<bool> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(P_0, JsonMetadataServices.BooleanConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<Timestamp> Create_Timestamp(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<Timestamp> jsonTypeInfo))
		{
			JsonObjectInfoValues<Timestamp> jsonObjectInfoValues = new JsonObjectInfoValues<Timestamp>
			{
				ObjectCreator = () => new Timestamp(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => TimestampPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(Timestamp).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<System.Type>(), null),
				SerializeHandler = TimestampSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] TimestampPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[2];
		JsonPropertyInfoValues<long> jsonPropertyInfoValues = new JsonPropertyInfoValues<long>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(Timestamp);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((Timestamp)obj).Seconds;
		jsonPropertyInfoValues.Setter = delegate(object obj, long value)
		{
			((Timestamp)obj).Seconds = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "Seconds";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(Timestamp).GetProperty("Seconds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<long> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<int> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<int>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(Timestamp);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((Timestamp)obj).Nanos;
		jsonPropertyInfoValues3.Setter = delegate(object obj, int value)
		{
			((Timestamp)obj).Nanos = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "Nanos";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(Timestamp).GetProperty("Nanos", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<int> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		return array;
	}

	private void TimestampSerializeHandler(Utf8JsonWriter writer, Timestamp? value)
	{
		if ((object)value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteNumber(PropName_seconds, value.Seconds);
		writer.WriteNumber(PropName_nanos, value.Nanos);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<RootApp.Browser.Models.UserOnlineStatus> Create_UserOnlineStatusBrowser(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<RootApp.Browser.Models.UserOnlineStatus> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<RootApp.Browser.Models.UserOnlineStatus>(P_0, JsonMetadataServices.GetEnumConverter<RootApp.Browser.Models.UserOnlineStatus>(P_0));
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<UserResponse> Create_UserResponse(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<UserResponse> jsonTypeInfo))
		{
			JsonObjectInfoValues<UserResponse> jsonObjectInfoValues = new JsonObjectInfoValues<UserResponse>();
			jsonObjectInfoValues.ObjectCreator = null;
			jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new UserResponse((GlobalUser)args[0]);
			jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => UserResponsePropInit(P_0);
			jsonObjectInfoValues.ConstructorParameterMetadataInitializer = UserResponseCtorParamInit;
			jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(UserResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new System.Type[1] { typeof(GlobalUser) }, null);
			jsonObjectInfoValues.SerializeHandler = UserResponseSerializeHandler;
			JsonObjectInfoValues<UserResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues2);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] UserResponsePropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[4];
		JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserResponse);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserResponse)obj).userId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((UserResponse)obj).userId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "userId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserResponse).GetProperty("userId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		array[0].IsGetNullable = false;
		array[0].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserResponse);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserResponse)obj).profilePictureAssetUri;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((UserResponse)obj).profilePictureAssetUri = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "profilePictureAssetUri";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserResponse).GetProperty("profilePictureAssetUri", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		array[1].IsGetNullable = false;
		array[1].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserResponse);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserResponse)obj).username;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((UserResponse)obj).username = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "username";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserResponse).GetProperty("username", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		array[2].IsGetNullable = false;
		array[2].IsSetNullable = false;
		JsonPropertyInfoValues<RootApp.Browser.Models.UserOnlineStatus> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<RootApp.Browser.Models.UserOnlineStatus>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(UserResponse);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((UserResponse)obj).onlineStatus;
		jsonPropertyInfoValues5.Setter = delegate(object obj, RootApp.Browser.Models.UserOnlineStatus value)
		{
			((UserResponse)obj).onlineStatus = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "onlineStatus";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(UserResponse).GetProperty("onlineStatus", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(RootApp.Browser.Models.UserOnlineStatus), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<RootApp.Browser.Models.UserOnlineStatus> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues6);
		return array;
	}

	private void UserResponseSerializeHandler(Utf8JsonWriter writer, UserResponse? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteString(PropName_userId, value.userId);
		writer.WriteString(PropName_profilePictureAssetUri, value.profilePictureAssetUri);
		writer.WriteString(PropName_username, value.username);
		writer.WritePropertyName(PropName_onlineStatus);
		JsonSerializer.Serialize(writer, value.onlineStatus, UserOnlineStatusBrowser);
		writer.WriteEndObject();
	}

	private static JsonParameterInfoValues[] UserResponseCtorParamInit()
	{
		return new JsonParameterInfoValues[1]
		{
			new JsonParameterInfoValues
			{
				Name = "globalUser",
				ParameterType = typeof(GlobalUser),
				Position = 0,
				HasDefaultValue = false,
				DefaultValue = null,
				IsNullable = false
			}
		};
	}

	private JsonTypeInfo<GlobalUser> Create_GlobalUser(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<GlobalUser> jsonTypeInfo))
		{
			JsonObjectInfoValues<GlobalUser> jsonObjectInfoValues = new JsonObjectInfoValues<GlobalUser>
			{
				ObjectCreator = null,
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => GlobalUserPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = null,
				SerializeHandler = GlobalUserSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] GlobalUserPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[8];
		JsonPropertyInfoValues<UserGuid> jsonPropertyInfoValues = new JsonPropertyInfoValues<UserGuid>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((GlobalUser)obj).Id;
		jsonPropertyInfoValues.Setter = null;
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "Id";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(UserGuid), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<UserGuid> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((GlobalUser)obj).ProfilePictureUri;
		jsonPropertyInfoValues3.Setter = delegate(object obj, string? value)
		{
			((GlobalUser)obj).ProfilePictureUri = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "ProfilePictureUri";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("ProfilePictureUri", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		array[1].IsGetNullable = false;
		array[1].IsSetNullable = false;
		jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((GlobalUser)obj).UserName;
		jsonPropertyInfoValues3.Setter = delegate(object obj, string? value)
		{
			((GlobalUser)obj).UserName = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "UserName";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("UserName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = jsonPropertyInfoValues3;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		array[2].IsGetNullable = false;
		array[2].IsSetNullable = false;
		JsonPropertyInfoValues<RootApp.WebApi.Shared.Enums.UserOnlineStatus> jsonPropertyInfoValues6 = new JsonPropertyInfoValues<RootApp.WebApi.Shared.Enums.UserOnlineStatus>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((GlobalUser)obj).OnlineStatus;
		jsonPropertyInfoValues6.Setter = delegate(object obj, RootApp.WebApi.Shared.Enums.UserOnlineStatus value)
		{
			((GlobalUser)obj).OnlineStatus = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "OnlineStatus";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("OnlineStatus", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(RootApp.WebApi.Shared.Enums.UserOnlineStatus), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<RootApp.WebApi.Shared.Enums.UserOnlineStatus> jsonPropertyInfoValues7 = jsonPropertyInfoValues6;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues8 = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues8.IsProperty = true;
		jsonPropertyInfoValues8.IsPublic = true;
		jsonPropertyInfoValues8.IsVirtual = false;
		jsonPropertyInfoValues8.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues8.Converter = null;
		jsonPropertyInfoValues8.Getter = (object obj) => ((GlobalUser)obj).IsFriend;
		jsonPropertyInfoValues8.Setter = delegate(object obj, bool value)
		{
			((GlobalUser)obj).IsFriend = value;
		};
		jsonPropertyInfoValues8.IgnoreCondition = null;
		jsonPropertyInfoValues8.HasJsonInclude = false;
		jsonPropertyInfoValues8.IsExtensionData = false;
		jsonPropertyInfoValues8.NumberHandling = null;
		jsonPropertyInfoValues8.PropertyName = "IsFriend";
		jsonPropertyInfoValues8.JsonPropertyName = null;
		jsonPropertyInfoValues8.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("IsFriend", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues9 = jsonPropertyInfoValues8;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		jsonPropertyInfoValues8 = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues8.IsProperty = true;
		jsonPropertyInfoValues8.IsPublic = true;
		jsonPropertyInfoValues8.IsVirtual = false;
		jsonPropertyInfoValues8.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues8.Converter = null;
		jsonPropertyInfoValues8.Getter = (object obj) => ((GlobalUser)obj).IsBlocked;
		jsonPropertyInfoValues8.Setter = delegate(object obj, bool value)
		{
			((GlobalUser)obj).IsBlocked = value;
		};
		jsonPropertyInfoValues8.IgnoreCondition = null;
		jsonPropertyInfoValues8.HasJsonInclude = false;
		jsonPropertyInfoValues8.IsExtensionData = false;
		jsonPropertyInfoValues8.NumberHandling = null;
		jsonPropertyInfoValues8.PropertyName = "IsBlocked";
		jsonPropertyInfoValues8.JsonPropertyName = null;
		jsonPropertyInfoValues8.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("IsBlocked", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues10 = jsonPropertyInfoValues8;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		JsonPropertyInfoValues<ObservableCollection<UserBadge>> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<ObservableCollection<UserBadge>>();
		jsonPropertyInfoValues11.IsProperty = true;
		jsonPropertyInfoValues11.IsPublic = true;
		jsonPropertyInfoValues11.IsVirtual = false;
		jsonPropertyInfoValues11.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues11.Converter = null;
		jsonPropertyInfoValues11.Getter = (object obj) => ((GlobalUser)obj).Badges;
		jsonPropertyInfoValues11.Setter = null;
		jsonPropertyInfoValues11.IgnoreCondition = null;
		jsonPropertyInfoValues11.HasJsonInclude = false;
		jsonPropertyInfoValues11.IsExtensionData = false;
		jsonPropertyInfoValues11.NumberHandling = null;
		jsonPropertyInfoValues11.PropertyName = "Badges";
		jsonPropertyInfoValues11.JsonPropertyName = null;
		jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("Badges", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ObservableCollection<UserBadge>), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<ObservableCollection<UserBadge>> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues12);
		array[6].IsGetNullable = false;
		JsonPropertyInfoValues<UserBadge> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<UserBadge>();
		jsonPropertyInfoValues13.IsProperty = true;
		jsonPropertyInfoValues13.IsPublic = true;
		jsonPropertyInfoValues13.IsVirtual = false;
		jsonPropertyInfoValues13.DeclaringType = typeof(GlobalUser);
		jsonPropertyInfoValues13.Converter = null;
		jsonPropertyInfoValues13.Getter = (object obj) => ((GlobalUser)obj).PrimaryBadge;
		jsonPropertyInfoValues13.Setter = null;
		jsonPropertyInfoValues13.IgnoreCondition = null;
		jsonPropertyInfoValues13.HasJsonInclude = false;
		jsonPropertyInfoValues13.IsExtensionData = false;
		jsonPropertyInfoValues13.NumberHandling = null;
		jsonPropertyInfoValues13.PropertyName = "PrimaryBadge";
		jsonPropertyInfoValues13.JsonPropertyName = null;
		jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(GlobalUser).GetProperty("PrimaryBadge", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(UserBadge), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<UserBadge> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues14);
		return array;
	}

	private void GlobalUserSerializeHandler(Utf8JsonWriter writer, GlobalUser? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_id);
		JsonSerializer.Serialize(writer, value.Id, UserGuid);
		writer.WriteString(PropName_profilePictureUri, value.ProfilePictureUri);
		writer.WriteString(PropName_userName, value.UserName);
		writer.WritePropertyName(PropName_onlineStatus);
		JsonSerializer.Serialize(writer, value.OnlineStatus, UserOnlineStatus);
		writer.WriteBoolean(PropName_isFriend, value.IsFriend);
		writer.WriteBoolean(PropName_isBlocked, value.IsBlocked);
		writer.WritePropertyName(PropName_badges);
		ObservableCollectionUserBadgeSerializeHandler(writer, value.Badges);
		writer.WritePropertyName(PropName_primaryBadge);
		UserBadgeSerializeHandler(writer, value.PrimaryBadge);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<BadgeUuid> Create_BadgeUuid(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<BadgeUuid> jsonTypeInfo))
		{
			JsonObjectInfoValues<BadgeUuid> jsonObjectInfoValues = new JsonObjectInfoValues<BadgeUuid>
			{
				ObjectCreator = () => new BadgeUuid(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => BadgeUuidPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(BadgeUuid).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<System.Type>(), null),
				SerializeHandler = BadgeUuidSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] BadgeUuidPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[2];
		JsonPropertyInfoValues<ulong> jsonPropertyInfoValues = new JsonPropertyInfoValues<ulong>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(BadgeUuid);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((BadgeUuid)obj).High64;
		jsonPropertyInfoValues.Setter = delegate(object obj, ulong value)
		{
			((BadgeUuid)obj).High64 = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "High64";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(BadgeUuid).GetProperty("High64", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ulong), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<ulong> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ulong>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(BadgeUuid);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((BadgeUuid)obj).Low64;
		jsonPropertyInfoValues.Setter = delegate(object obj, ulong value)
		{
			((BadgeUuid)obj).Low64 = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "Low64";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(BadgeUuid).GetProperty("Low64", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ulong), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<ulong> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		return array;
	}

	private void BadgeUuidSerializeHandler(Utf8JsonWriter writer, BadgeUuid? value)
	{
		if ((object)value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteNumber(PropName_high64, value.High64);
		writer.WriteNumber(PropName_low64, value.Low64);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<UserGuid> Create_UserGuid(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<UserGuid> jsonTypeInfo))
		{
			JsonConverter jsonConverter = ExpandConverter(typeof(UserGuid), new JsonRootGuidConverter<UserGuid>(), P_0);
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<UserGuid>(P_0, jsonConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<RootApp.WebApi.Shared.Enums.UserOnlineStatus> Create_UserOnlineStatus(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<RootApp.WebApi.Shared.Enums.UserOnlineStatus> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<RootApp.WebApi.Shared.Enums.UserOnlineStatus>(P_0, JsonMetadataServices.GetEnumConverter<RootApp.WebApi.Shared.Enums.UserOnlineStatus>(P_0));
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<UserBadge> Create_UserBadge(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<UserBadge> jsonTypeInfo))
		{
			JsonObjectInfoValues<UserBadge> jsonObjectInfoValues = new JsonObjectInfoValues<UserBadge>
			{
				ObjectCreator = () => new UserBadge(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => UserBadgePropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(UserBadge).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<System.Type>(), null),
				SerializeHandler = UserBadgeSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] UserBadgePropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[2];
		JsonPropertyInfoValues<BadgeUuid> jsonPropertyInfoValues = new JsonPropertyInfoValues<BadgeUuid>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserBadge);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserBadge)obj).Id;
		jsonPropertyInfoValues.Setter = delegate(object obj, BadgeUuid? value)
		{
			((UserBadge)obj).Id = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "Id";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserBadge).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(BadgeUuid), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<BadgeUuid> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<Timestamp> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<Timestamp>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(UserBadge);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((UserBadge)obj).CreatedAt;
		jsonPropertyInfoValues3.Setter = delegate(object obj, Timestamp? value)
		{
			((UserBadge)obj).CreatedAt = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "CreatedAt";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(UserBadge).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Timestamp), Array.Empty<System.Type>(), null);
		JsonPropertyInfoValues<Timestamp> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		return array;
	}

	private void UserBadgeSerializeHandler(Utf8JsonWriter writer, UserBadge? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_id);
		BadgeUuidSerializeHandler(writer, value.Id);
		writer.WritePropertyName(PropName_createdAt);
		TimestampSerializeHandler(writer, value.CreatedAt);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<ObservableCollection<UserBadge>> Create_ObservableCollectionUserBadge(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ObservableCollection<UserBadge>> jsonTypeInfo))
		{
			JsonCollectionInfoValues<ObservableCollection<UserBadge>> jsonCollectionInfoValues = new JsonCollectionInfoValues<ObservableCollection<UserBadge>>
			{
				ObjectCreator = () => new ObservableCollection<UserBadge>(),
				SerializeHandler = ObservableCollectionUserBadgeSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateIListInfo<ObservableCollection<UserBadge>, UserBadge>(P_0, jsonCollectionInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private void ObservableCollectionUserBadgeSerializeHandler(Utf8JsonWriter writer, ObservableCollection<UserBadge>? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartArray();
		for (int i = 0; i < value.Count; i++)
		{
			UserBadgeSerializeHandler(writer, value[i]);
		}
		writer.WriteEndArray();
	}

	private JsonTypeInfo<int> Create_Int32(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<int> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int>(P_0, JsonMetadataServices.Int32Converter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<long> Create_Int64(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<long> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<long>(P_0, JsonMetadataServices.Int64Converter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<string> Create_String(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<string> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(P_0, JsonMetadataServices.StringConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<ulong> Create_UInt64(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ulong> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<ulong>(P_0, JsonMetadataServices.UInt64Converter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	public UserResponseJsonContext(JsonSerializerOptions P_0)
		: base(P_0)
	{
	}

	private static bool TryGetTypeInfoForRuntimeCustomConverter<TJsonMetadataType>(JsonSerializerOptions P_0, out JsonTypeInfo<TJsonMetadataType> P_1)
	{
		JsonConverter runtimeConverterForType = GetRuntimeConverterForType(typeof(TJsonMetadataType), P_0);
		if (runtimeConverterForType != null)
		{
			P_1 = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(P_0, runtimeConverterForType);
			return true;
		}
		P_1 = null;
		return false;
	}

	private static JsonConverter? GetRuntimeConverterForType(System.Type P_0, JsonSerializerOptions P_1)
	{
		for (int i = 0; i < P_1.Converters.Count; i++)
		{
			JsonConverter jsonConverter = P_1.Converters[i];
			if (jsonConverter != null && jsonConverter.CanConvert(P_0))
			{
				return ExpandConverter(P_0, jsonConverter, P_1, false);
			}
		}
		return null;
	}

	private static JsonConverter ExpandConverter(System.Type P_0, JsonConverter P_1, JsonSerializerOptions P_2, bool P_3 = true)
	{
		if (P_3 && !P_1.CanConvert(P_0))
		{
			throw new InvalidOperationException($"The converter '{P_1.GetType()}' is not compatible with the type '{P_0}'.");
		}
		if (P_1 is JsonConverterFactory jsonConverterFactory)
		{
			P_1 = jsonConverterFactory.CreateConverter(P_0, P_2);
			if (P_1 == null || P_1 is JsonConverterFactory)
			{
				throw new InvalidOperationException($"The converter '{jsonConverterFactory.GetType()}' cannot return null or a JsonConverterFactory instance.");
			}
		}
		return P_1;
	}

	public override JsonTypeInfo? GetTypeInfo(System.Type P_0)
	{
		base.Options.TryGetTypeInfo(P_0, out JsonTypeInfo result);
		return result;
	}

	JsonTypeInfo? IJsonTypeInfoResolver.GetTypeInfo(System.Type P_0, JsonSerializerOptions P_1)
	{
		if (P_0 == typeof(bool))
		{
			return Create_Boolean(P_1);
		}
		if (P_0 == typeof(Timestamp))
		{
			return Create_Timestamp(P_1);
		}
		if (P_0 == typeof(RootApp.Browser.Models.UserOnlineStatus))
		{
			return Create_UserOnlineStatusBrowser(P_1);
		}
		if (P_0 == typeof(UserResponse))
		{
			return Create_UserResponse(P_1);
		}
		if (P_0 == typeof(GlobalUser))
		{
			return Create_GlobalUser(P_1);
		}
		if (P_0 == typeof(BadgeUuid))
		{
			return Create_BadgeUuid(P_1);
		}
		if (P_0 == typeof(UserGuid))
		{
			return Create_UserGuid(P_1);
		}
		if (P_0 == typeof(RootApp.WebApi.Shared.Enums.UserOnlineStatus))
		{
			return Create_UserOnlineStatus(P_1);
		}
		if (P_0 == typeof(UserBadge))
		{
			return Create_UserBadge(P_1);
		}
		if (P_0 == typeof(ObservableCollection<UserBadge>))
		{
			return Create_ObservableCollectionUserBadge(P_1);
		}
		if (P_0 == typeof(int))
		{
			return Create_Int32(P_1);
		}
		if (P_0 == typeof(long))
		{
			return Create_Int64(P_1);
		}
		if (P_0 == typeof(string))
		{
			return Create_String(P_1);
		}
		if (P_0 == typeof(ulong))
		{
			return Create_UInt64(P_1);
		}
		return null;
	}
}
