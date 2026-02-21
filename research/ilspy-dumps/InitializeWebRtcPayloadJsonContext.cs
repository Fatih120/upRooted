using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace RootApp.Browser.WebRtc.Models;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(InitializeWebRtcPayload))]
[JsonSerializable(typeof(WebRtcPermission))]
[JsonSerializable(typeof(UserTileVolume))]
[JsonSerializable(typeof(UserTileVolume[]))]
[JsonSerializable(typeof(UserMediaStreamConstraints))]
[JsonSerializable(typeof(DisplayMediaStreamConstraints))]
[JsonSerializable(typeof(Dictionary<string, bool>))]
[GeneratedCode("System.Text.Json.SourceGeneration", "10.0.14.7603")]
public class InitializeWebRtcPayloadJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
{
	private JsonTypeInfo<bool?>? _NullableBoolean;

	private JsonTypeInfo<char?>? _NullableChar;

	private JsonTypeInfo<double?>? _NullableDouble;

	private JsonTypeInfo<InitializeWebRtcPayload>? _InitializeWebRtcPayload;

	private JsonTypeInfo<int?>? _NullableInt32;

	private JsonTypeInfo<long?>? _NullableInt64;

	private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

	private static readonly JsonEncodedText PropName_autoGainControl = JsonEncodedText.Encode("autoGainControl");

	private static readonly JsonEncodedText PropName_channelCount = JsonEncodedText.Encode("channelCount");

	private static readonly JsonEncodedText PropName_echoCancellation = JsonEncodedText.Encode("echoCancellation");

	private static readonly JsonEncodedText PropName_latency = JsonEncodedText.Encode("latency");

	private static readonly JsonEncodedText PropName_noiseSuppression = JsonEncodedText.Encode("noiseSuppression");

	private static readonly JsonEncodedText PropName_sampleRate = JsonEncodedText.Encode("sampleRate");

	private static readonly JsonEncodedText PropName_sampleSize = JsonEncodedText.Encode("sampleSize");

	private static readonly JsonEncodedText PropName_volume = JsonEncodedText.Encode("volume");

	private static readonly JsonEncodedText PropName_groupId = JsonEncodedText.Encode("groupId");

	private static readonly JsonEncodedText PropName_exact = JsonEncodedText.Encode("exact");

	private static readonly JsonEncodedText PropName_ideal = JsonEncodedText.Encode("ideal");

	private static readonly JsonEncodedText PropName_min = JsonEncodedText.Encode("min");

	private static readonly JsonEncodedText PropName_max = JsonEncodedText.Encode("max");

	private static readonly JsonEncodedText PropName_video = JsonEncodedText.Encode("video");

	private static readonly JsonEncodedText PropName_audio = JsonEncodedText.Encode("audio");

	private static readonly JsonEncodedText PropName_selfBrowserSurface = JsonEncodedText.Encode("selfBrowserSurface");

	private static readonly JsonEncodedText PropName_systemAudio = JsonEncodedText.Encode("systemAudio");

	private static readonly JsonEncodedText PropName_windowAudio = JsonEncodedText.Encode("windowAudio");

	private static readonly JsonEncodedText PropName_surfaceSwitching = JsonEncodedText.Encode("surfaceSwitching");

	private static readonly JsonEncodedText PropName_monitorTypeSurfaces = JsonEncodedText.Encode("monitorTypeSurfaces");

	private static readonly JsonEncodedText PropName_theme = JsonEncodedText.Encode("theme");

	private static readonly JsonEncodedText PropName_callPlatform = JsonEncodedText.Encode("callPlatform");

	private static readonly JsonEncodedText PropName_currentDeviceId = JsonEncodedText.Encode("currentDeviceId");

	private static readonly JsonEncodedText PropName_permissions = JsonEncodedText.Encode("permissions");

	private static readonly JsonEncodedText PropName_currentUserId = JsonEncodedText.Encode("currentUserId");

	private static readonly JsonEncodedText PropName_containerId = JsonEncodedText.Encode("containerId");

	private static readonly JsonEncodedText PropName_communityId = JsonEncodedText.Encode("communityId");

	private static readonly JsonEncodedText PropName_webApiBaseUrl = JsonEncodedText.Encode("webApiBaseUrl");

	private static readonly JsonEncodedText PropName_videoDeviceId = JsonEncodedText.Encode("videoDeviceId");

	private static readonly JsonEncodedText PropName_audioInputDeviceId = JsonEncodedText.Encode("audioInputDeviceId");

	private static readonly JsonEncodedText PropName_audioOutputDeviceId = JsonEncodedText.Encode("audioOutputDeviceId");

	private static readonly JsonEncodedText PropName_screenAudioDeviceId = JsonEncodedText.Encode("screenAudioDeviceId");

	private static readonly JsonEncodedText PropName_preferredRid = JsonEncodedText.Encode("preferredRid");

	private static readonly JsonEncodedText PropName_isPushToTalkMode = JsonEncodedText.Encode("isPushToTalkMode");

	private static readonly JsonEncodedText PropName_debugMode = JsonEncodedText.Encode("debugMode");

	private static readonly JsonEncodedText PropName_logging = JsonEncodedText.Encode("logging");

	private static readonly JsonEncodedText PropName_initialTrackTypes = JsonEncodedText.Encode("initialTrackTypes");

	private static readonly JsonEncodedText PropName_inputVolume = JsonEncodedText.Encode("inputVolume");

	private static readonly JsonEncodedText PropName_outputVolume = JsonEncodedText.Encode("outputVolume");

	private static readonly JsonEncodedText PropName_tileVolumes = JsonEncodedText.Encode("tileVolumes");

	private static readonly JsonEncodedText PropName_defaultInputVolume = JsonEncodedText.Encode("defaultInputVolume");

	private static readonly JsonEncodedText PropName_defaultGlobalOutputVolume = JsonEncodedText.Encode("defaultGlobalOutputVolume");

	private static readonly JsonEncodedText PropName_defaultPrimaryOutputVolume = JsonEncodedText.Encode("defaultPrimaryOutputVolume");

	private static readonly JsonEncodedText PropName_defaultScreenOutputVolume = JsonEncodedText.Encode("defaultScreenOutputVolume");

	private static readonly JsonEncodedText PropName_ringingUserIds = JsonEncodedText.Encode("ringingUserIds");

	private static readonly JsonEncodedText PropName_ringTimeoutMs = JsonEncodedText.Encode("ringTimeoutMs");

	private static readonly JsonEncodedText PropName_activeUserIds = JsonEncodedText.Encode("activeUserIds");

	private static readonly JsonEncodedText PropName_preferredCodecs = JsonEncodedText.Encode("preferredCodecs");

	private static readonly JsonEncodedText PropName_preferredScreenContentHint = JsonEncodedText.Encode("preferredScreenContentHint");

	private static readonly JsonEncodedText PropName_userMediaConstraints = JsonEncodedText.Encode("userMediaConstraints");

	private static readonly JsonEncodedText PropName_displayMediaConstraints = JsonEncodedText.Encode("displayMediaConstraints");

	private static readonly JsonEncodedText PropName_experimentalFlags = JsonEncodedText.Encode("experimentalFlags");

	private static readonly JsonEncodedText PropName_displaySurface = JsonEncodedText.Encode("displaySurface");

	private static readonly JsonEncodedText PropName_logicalSurface = JsonEncodedText.Encode("logicalSurface");

	private static readonly JsonEncodedText PropName_cursor = JsonEncodedText.Encode("cursor");

	private static readonly JsonEncodedText PropName_screenPixelRatio = JsonEncodedText.Encode("screenPixelRatio");

	private static readonly JsonEncodedText PropName_restrictOwnAudio = JsonEncodedText.Encode("restrictOwnAudio");

	private static readonly JsonEncodedText PropName_suppressLocalAudioPlayback = JsonEncodedText.Encode("suppressLocalAudioPlayback");

	private static readonly JsonEncodedText PropName_width = JsonEncodedText.Encode("width");

	private static readonly JsonEncodedText PropName_height = JsonEncodedText.Encode("height");

	private static readonly JsonEncodedText PropName_frameRate = JsonEncodedText.Encode("frameRate");

	private static readonly JsonEncodedText PropName_aspectRatio = JsonEncodedText.Encode("aspectRatio");

	private static readonly JsonEncodedText PropName_resizeMode = JsonEncodedText.Encode("resizeMode");

	private static readonly JsonEncodedText PropName_userId = JsonEncodedText.Encode("userId");

	private static readonly JsonEncodedText PropName_tileType = JsonEncodedText.Encode("tileType");

	private static readonly JsonEncodedText PropName_facingMode = JsonEncodedText.Encode("facingMode");

	private static readonly JsonEncodedText PropName_backgroundBlur = JsonEncodedText.Encode("backgroundBlur");

	private static readonly JsonEncodedText PropName_name = JsonEncodedText.Encode("name");

	private static readonly JsonEncodedText PropName_profileLevelId = JsonEncodedText.Encode("profileLevelId");

	private static readonly JsonEncodedText PropName_packetizationMode = JsonEncodedText.Encode("packetizationMode");

	private static readonly JsonEncodedText PropName_channelManage = JsonEncodedText.Encode("channelManage");

	private static readonly JsonEncodedText PropName_channelManageApp = JsonEncodedText.Encode("channelManageApp");

	private static readonly JsonEncodedText PropName_channelView = JsonEncodedText.Encode("channelView");

	private static readonly JsonEncodedText PropName_channelUseExternalEmoji = JsonEncodedText.Encode("channelUseExternalEmoji");

	private static readonly JsonEncodedText PropName_channelCreateMessage = JsonEncodedText.Encode("channelCreateMessage");

	private static readonly JsonEncodedText PropName_channelManageMessage = JsonEncodedText.Encode("channelManageMessage");

	private static readonly JsonEncodedText PropName_channelManagePinnedMessages = JsonEncodedText.Encode("channelManagePinnedMessages");

	private static readonly JsonEncodedText PropName_channelViewMessageHistory = JsonEncodedText.Encode("channelViewMessageHistory");

	private static readonly JsonEncodedText PropName_channelCreateMessageAttachment = JsonEncodedText.Encode("channelCreateMessageAttachment");

	private static readonly JsonEncodedText PropName_channelCreateMessageMention = JsonEncodedText.Encode("channelCreateMessageMention");

	private static readonly JsonEncodedText PropName_channelCreateMessageReaction = JsonEncodedText.Encode("channelCreateMessageReaction");

	private static readonly JsonEncodedText PropName_channelMakeMessagePublic = JsonEncodedText.Encode("channelMakeMessagePublic");

	private static readonly JsonEncodedText PropName_channelMoveUserOther = JsonEncodedText.Encode("channelMoveUserOther");

	private static readonly JsonEncodedText PropName_channelVoiceTalk = JsonEncodedText.Encode("channelVoiceTalk");

	private static readonly JsonEncodedText PropName_channelVoiceMuteOther = JsonEncodedText.Encode("channelVoiceMuteOther");

	private static readonly JsonEncodedText PropName_channelVoiceDeafenOther = JsonEncodedText.Encode("channelVoiceDeafenOther");

	private static readonly JsonEncodedText PropName_channelVoiceKick = JsonEncodedText.Encode("channelVoiceKick");

	private static readonly JsonEncodedText PropName_channelVideoStreamMedia = JsonEncodedText.Encode("channelVideoStreamMedia");

	private static readonly JsonEncodedText PropName_channelCreateFile = JsonEncodedText.Encode("channelCreateFile");

	private static readonly JsonEncodedText PropName_channelManageFiles = JsonEncodedText.Encode("channelManageFiles");

	private static readonly JsonEncodedText PropName_channelViewFile = JsonEncodedText.Encode("channelViewFile");

	private static readonly JsonEncodedText PropName_channelAppKick = JsonEncodedText.Encode("channelAppKick");

	public JsonTypeInfo<bool?> NullableBoolean => _NullableBoolean ?? (_NullableBoolean = (JsonTypeInfo<bool?>)base.Options.GetTypeInfo(typeof(bool?)));

	public JsonTypeInfo<char?> NullableChar => _NullableChar ?? (_NullableChar = (JsonTypeInfo<char?>)base.Options.GetTypeInfo(typeof(char?)));

	public JsonTypeInfo<double?> NullableDouble => _NullableDouble ?? (_NullableDouble = (JsonTypeInfo<double?>)base.Options.GetTypeInfo(typeof(double?)));

	public JsonTypeInfo<InitializeWebRtcPayload> InitializeWebRtcPayload => _InitializeWebRtcPayload ?? (_InitializeWebRtcPayload = (JsonTypeInfo<InitializeWebRtcPayload>)base.Options.GetTypeInfo(typeof(InitializeWebRtcPayload)));

	public JsonTypeInfo<int?> NullableInt32 => _NullableInt32 ?? (_NullableInt32 = (JsonTypeInfo<int?>)base.Options.GetTypeInfo(typeof(int?)));

	public JsonTypeInfo<long?> NullableInt64 => _NullableInt64 ?? (_NullableInt64 = (JsonTypeInfo<long?>)base.Options.GetTypeInfo(typeof(long?)));

	public static InitializeWebRtcPayloadJsonContext Default { get; } = new InitializeWebRtcPayloadJsonContext(new JsonSerializerOptions(s_defaultOptions));

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

	private JsonTypeInfo<bool?> Create_NullableBoolean(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<bool?> jsonTypeInfo))
		{
			JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<bool>(P_0);
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool?>(P_0, nullableConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<char> Create_Char(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<char> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<char>(P_0, JsonMetadataServices.CharConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<char?> Create_NullableChar(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<char?> jsonTypeInfo))
		{
			JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<char>(P_0);
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<char?>(P_0, nullableConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<double> Create_Double(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<double> jsonTypeInfo))
		{
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<double>(P_0, JsonMetadataServices.DoubleConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<double?> Create_NullableDouble(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<double?> jsonTypeInfo))
		{
			JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<double>(P_0);
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<double?>(P_0, nullableConverter);
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private JsonTypeInfo<AudioTrackConstraints> Create_AudioTrackConstraints(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<AudioTrackConstraints> jsonTypeInfo))
		{
			JsonObjectInfoValues<AudioTrackConstraints> jsonObjectInfoValues = new JsonObjectInfoValues<AudioTrackConstraints>
			{
				ObjectCreator = () => new AudioTrackConstraints(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => AudioTrackConstraintsPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(AudioTrackConstraints).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = AudioTrackConstraintsSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] AudioTrackConstraintsPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[9];
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((AudioTrackConstraints)obj).autoGainControl;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((AudioTrackConstraints)obj).autoGainControl = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "autoGainControl";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("autoGainControl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((AudioTrackConstraints)obj).channelCount;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainLong? value)
		{
			((AudioTrackConstraints)obj).channelCount = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "channelCount";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("channelCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((AudioTrackConstraints)obj).echoCancellation;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((AudioTrackConstraints)obj).echoCancellation = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "echoCancellation";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("echoCancellation", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((AudioTrackConstraints)obj).latency;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((AudioTrackConstraints)obj).latency = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "latency";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("latency", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues7 = jsonPropertyInfoValues6;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((AudioTrackConstraints)obj).noiseSuppression;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((AudioTrackConstraints)obj).noiseSuppression = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "noiseSuppression";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("noiseSuppression", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues8 = jsonPropertyInfoValues;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((AudioTrackConstraints)obj).sampleRate;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainLong? value)
		{
			((AudioTrackConstraints)obj).sampleRate = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "sampleRate";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("sampleRate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues9 = jsonPropertyInfoValues3;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((AudioTrackConstraints)obj).sampleSize;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainLong? value)
		{
			((AudioTrackConstraints)obj).sampleSize = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "sampleSize";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("sampleSize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues10 = jsonPropertyInfoValues3;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((AudioTrackConstraints)obj).volume;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((AudioTrackConstraints)obj).volume = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "volume";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("volume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues11 = jsonPropertyInfoValues6;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues11);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues12 = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues12.IsProperty = true;
		jsonPropertyInfoValues12.IsPublic = true;
		jsonPropertyInfoValues12.IsVirtual = false;
		jsonPropertyInfoValues12.DeclaringType = typeof(AudioTrackConstraints);
		jsonPropertyInfoValues12.Converter = null;
		jsonPropertyInfoValues12.Getter = (object obj) => ((AudioTrackConstraints)obj).groupId;
		jsonPropertyInfoValues12.Setter = delegate(object obj, ConstrainString? value)
		{
			((AudioTrackConstraints)obj).groupId = value;
		};
		jsonPropertyInfoValues12.IgnoreCondition = null;
		jsonPropertyInfoValues12.HasJsonInclude = false;
		jsonPropertyInfoValues12.IsExtensionData = false;
		jsonPropertyInfoValues12.NumberHandling = null;
		jsonPropertyInfoValues12.PropertyName = "groupId";
		jsonPropertyInfoValues12.JsonPropertyName = null;
		jsonPropertyInfoValues12.AttributeProviderFactory = () => typeof(AudioTrackConstraints).GetProperty("groupId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues13 = jsonPropertyInfoValues12;
		array[8] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues13);
		return array;
	}

	private void AudioTrackConstraintsSerializeHandler(Utf8JsonWriter writer, AudioTrackConstraints? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_autoGainControl);
		ConstrainBooleanSerializeHandler(writer, value.autoGainControl);
		writer.WritePropertyName(PropName_channelCount);
		ConstrainLongSerializeHandler(writer, value.channelCount);
		writer.WritePropertyName(PropName_echoCancellation);
		ConstrainBooleanSerializeHandler(writer, value.echoCancellation);
		writer.WritePropertyName(PropName_latency);
		ConstrainDoubleSerializeHandler(writer, value.latency);
		writer.WritePropertyName(PropName_noiseSuppression);
		ConstrainBooleanSerializeHandler(writer, value.noiseSuppression);
		writer.WritePropertyName(PropName_sampleRate);
		ConstrainLongSerializeHandler(writer, value.sampleRate);
		writer.WritePropertyName(PropName_sampleSize);
		ConstrainLongSerializeHandler(writer, value.sampleSize);
		writer.WritePropertyName(PropName_volume);
		ConstrainDoubleSerializeHandler(writer, value.volume);
		writer.WritePropertyName(PropName_groupId);
		ConstrainStringSerializeHandler(writer, value.groupId);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<ConstrainBoolean> Create_ConstrainBoolean(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ConstrainBoolean> jsonTypeInfo))
		{
			JsonObjectInfoValues<ConstrainBoolean> jsonObjectInfoValues = new JsonObjectInfoValues<ConstrainBoolean>
			{
				ObjectCreator = () => new ConstrainBoolean(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => ConstrainBooleanPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(ConstrainBoolean).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = ConstrainBooleanSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] ConstrainBooleanPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[2];
		JsonPropertyInfoValues<bool?> jsonPropertyInfoValues = new JsonPropertyInfoValues<bool?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainBoolean);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainBoolean)obj).exact;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool? value)
		{
			((ConstrainBoolean)obj).exact = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "exact";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainBoolean).GetProperty("exact", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool?> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainBoolean);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainBoolean)obj).ideal;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool? value)
		{
			((ConstrainBoolean)obj).ideal = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "ideal";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainBoolean).GetProperty("ideal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool?> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		return array;
	}

	private void ConstrainBooleanSerializeHandler(Utf8JsonWriter writer, ConstrainBoolean? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_exact);
		JsonSerializer.Serialize(writer, value.exact, NullableBoolean);
		writer.WritePropertyName(PropName_ideal);
		JsonSerializer.Serialize(writer, value.ideal, NullableBoolean);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<ConstrainDouble> Create_ConstrainDouble(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ConstrainDouble> jsonTypeInfo))
		{
			JsonObjectInfoValues<ConstrainDouble> jsonObjectInfoValues = new JsonObjectInfoValues<ConstrainDouble>
			{
				ObjectCreator = () => new ConstrainDouble(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => ConstrainDoublePropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(ConstrainDouble).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = ConstrainDoubleSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] ConstrainDoublePropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[4];
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainDouble);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainDouble)obj).exact;
		jsonPropertyInfoValues.Setter = delegate(object obj, double? value)
		{
			((ConstrainDouble)obj).exact = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "exact";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainDouble).GetProperty("exact", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainDouble);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainDouble)obj).ideal;
		jsonPropertyInfoValues.Setter = delegate(object obj, double? value)
		{
			((ConstrainDouble)obj).ideal = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "ideal";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainDouble).GetProperty("ideal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainDouble);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainDouble)obj).min;
		jsonPropertyInfoValues.Setter = delegate(object obj, double? value)
		{
			((ConstrainDouble)obj).min = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "min";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainDouble).GetProperty("min", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues4 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainDouble);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainDouble)obj).max;
		jsonPropertyInfoValues.Setter = delegate(object obj, double? value)
		{
			((ConstrainDouble)obj).max = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "max";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainDouble).GetProperty("max", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		return array;
	}

	private void ConstrainDoubleSerializeHandler(Utf8JsonWriter writer, ConstrainDouble? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_exact);
		JsonSerializer.Serialize(writer, value.exact, NullableDouble);
		writer.WritePropertyName(PropName_ideal);
		JsonSerializer.Serialize(writer, value.ideal, NullableDouble);
		writer.WritePropertyName(PropName_min);
		JsonSerializer.Serialize(writer, value.min, NullableDouble);
		writer.WritePropertyName(PropName_max);
		JsonSerializer.Serialize(writer, value.max, NullableDouble);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<ConstrainLong> Create_ConstrainLong(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ConstrainLong> jsonTypeInfo))
		{
			JsonObjectInfoValues<ConstrainLong> jsonObjectInfoValues = new JsonObjectInfoValues<ConstrainLong>
			{
				ObjectCreator = () => new ConstrainLong(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => ConstrainLongPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(ConstrainLong).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = ConstrainLongSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] ConstrainLongPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[4];
		JsonPropertyInfoValues<long?> jsonPropertyInfoValues = new JsonPropertyInfoValues<long?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainLong);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainLong)obj).exact;
		jsonPropertyInfoValues.Setter = delegate(object obj, long? value)
		{
			((ConstrainLong)obj).exact = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "exact";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainLong).GetProperty("exact", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<long?> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<long?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainLong);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainLong)obj).ideal;
		jsonPropertyInfoValues.Setter = delegate(object obj, long? value)
		{
			((ConstrainLong)obj).ideal = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "ideal";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainLong).GetProperty("ideal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<long?> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<long?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainLong);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainLong)obj).min;
		jsonPropertyInfoValues.Setter = delegate(object obj, long? value)
		{
			((ConstrainLong)obj).min = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "min";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainLong).GetProperty("min", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<long?> jsonPropertyInfoValues4 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<long?>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainLong);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainLong)obj).max;
		jsonPropertyInfoValues.Setter = delegate(object obj, long? value)
		{
			((ConstrainLong)obj).max = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "max";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainLong).GetProperty("max", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<long?> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		return array;
	}

	private void ConstrainLongSerializeHandler(Utf8JsonWriter writer, ConstrainLong? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_exact);
		JsonSerializer.Serialize(writer, value.exact, NullableInt64);
		writer.WritePropertyName(PropName_ideal);
		JsonSerializer.Serialize(writer, value.ideal, NullableInt64);
		writer.WritePropertyName(PropName_min);
		JsonSerializer.Serialize(writer, value.min, NullableInt64);
		writer.WritePropertyName(PropName_max);
		JsonSerializer.Serialize(writer, value.max, NullableInt64);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<ConstrainString> Create_ConstrainString(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ConstrainString> jsonTypeInfo))
		{
			JsonObjectInfoValues<ConstrainString> jsonObjectInfoValues = new JsonObjectInfoValues<ConstrainString>
			{
				ObjectCreator = () => new ConstrainString(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => ConstrainStringPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(ConstrainString).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = ConstrainStringSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] ConstrainStringPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[2];
		JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainString);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainString)obj).exact;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((ConstrainString)obj).exact = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "exact";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainString).GetProperty("exact", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ConstrainString);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ConstrainString)obj).ideal;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((ConstrainString)obj).ideal = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "ideal";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConstrainString).GetProperty("ideal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		return array;
	}

	private void ConstrainStringSerializeHandler(Utf8JsonWriter writer, ConstrainString? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteString(PropName_exact, value.exact);
		writer.WriteString(PropName_ideal, value.ideal);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<DisplayMediaStreamConstraints> Create_DisplayMediaStreamConstraints(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<DisplayMediaStreamConstraints> jsonTypeInfo))
		{
			JsonObjectInfoValues<DisplayMediaStreamConstraints> jsonObjectInfoValues = new JsonObjectInfoValues<DisplayMediaStreamConstraints>
			{
				ObjectCreator = () => new DisplayMediaStreamConstraints(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => DisplayMediaStreamConstraintsPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = DisplayMediaStreamConstraintsSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] DisplayMediaStreamConstraintsPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[7];
		JsonPropertyInfoValues<ScreenTrackConstraints> jsonPropertyInfoValues = new JsonPropertyInfoValues<ScreenTrackConstraints>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).video;
		jsonPropertyInfoValues.Setter = delegate(object obj, ScreenTrackConstraints? value)
		{
			((DisplayMediaStreamConstraints)obj).video = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "video";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("video", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ScreenTrackConstraints), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ScreenTrackConstraints> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<AudioTrackConstraints> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<AudioTrackConstraints>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).audio;
		jsonPropertyInfoValues3.Setter = delegate(object obj, AudioTrackConstraints? value)
		{
			((DisplayMediaStreamConstraints)obj).audio = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "audio";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("audio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(AudioTrackConstraints), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<AudioTrackConstraints> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).selfBrowserSurface;
		jsonPropertyInfoValues5.Setter = delegate(object obj, string? value)
		{
			((DisplayMediaStreamConstraints)obj).selfBrowserSurface = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "selfBrowserSurface";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("selfBrowserSurface", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues6);
		jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).systemAudio;
		jsonPropertyInfoValues5.Setter = delegate(object obj, string? value)
		{
			((DisplayMediaStreamConstraints)obj).systemAudio = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "systemAudio";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("systemAudio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = jsonPropertyInfoValues5;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).windowAudio;
		jsonPropertyInfoValues5.Setter = delegate(object obj, string? value)
		{
			((DisplayMediaStreamConstraints)obj).windowAudio = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "windowAudio";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("windowAudio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues5;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).surfaceSwitching;
		jsonPropertyInfoValues5.Setter = delegate(object obj, string? value)
		{
			((DisplayMediaStreamConstraints)obj).surfaceSwitching = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "surfaceSwitching";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("surfaceSwitching", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = jsonPropertyInfoValues5;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(DisplayMediaStreamConstraints);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((DisplayMediaStreamConstraints)obj).monitorTypeSurfaces;
		jsonPropertyInfoValues5.Setter = delegate(object obj, string? value)
		{
			((DisplayMediaStreamConstraints)obj).monitorTypeSurfaces = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "monitorTypeSurfaces";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DisplayMediaStreamConstraints).GetProperty("monitorTypeSurfaces", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues5;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		return array;
	}

	private void DisplayMediaStreamConstraintsSerializeHandler(Utf8JsonWriter writer, DisplayMediaStreamConstraints? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_video);
		ScreenTrackConstraintsSerializeHandler(writer, value.video);
		writer.WritePropertyName(PropName_audio);
		AudioTrackConstraintsSerializeHandler(writer, value.audio);
		writer.WriteString(PropName_selfBrowserSurface, value.selfBrowserSurface);
		writer.WriteString(PropName_systemAudio, value.systemAudio);
		writer.WriteString(PropName_windowAudio, value.windowAudio);
		writer.WriteString(PropName_surfaceSwitching, value.surfaceSwitching);
		writer.WriteString(PropName_monitorTypeSurfaces, value.monitorTypeSurfaces);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<InitializeWebRtcPayload> Create_InitializeWebRtcPayload(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<InitializeWebRtcPayload> jsonTypeInfo))
		{
			JsonObjectInfoValues<InitializeWebRtcPayload> jsonObjectInfoValues = new JsonObjectInfoValues<InitializeWebRtcPayload>();
			jsonObjectInfoValues.ObjectCreator = null;
			jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new InitializeWebRtcPayload
			{
				theme = (string)args[0],
				callPlatform = (string)args[1],
				currentDeviceId = (string)args[2],
				currentUserId = (string)args[3],
				containerId = (string)args[4],
				webApiBaseUrl = (string)args[5],
				videoDeviceId = (string)args[6],
				audioInputDeviceId = (string)args[7],
				audioOutputDeviceId = (string)args[8],
				isPushToTalkMode = (bool)args[9],
				debugMode = (bool)args[10],
				logging = (string)args[11],
				ringingUserIds = (string[])args[12],
				ringTimeoutMs = (int)args[13],
				activeUserIds = (string[])args[14]
			};
			jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => InitializeWebRtcPayloadPropInit(P_0);
			jsonObjectInfoValues.ConstructorParameterMetadataInitializer = InitializeWebRtcPayloadCtorParamInit;
			jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
			jsonObjectInfoValues.SerializeHandler = InitializeWebRtcPayloadSerializeHandler;
			JsonObjectInfoValues<InitializeWebRtcPayload> jsonObjectInfoValues2 = jsonObjectInfoValues;
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues2);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] InitializeWebRtcPayloadPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[32];
		JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).theme;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).theme = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "theme";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("theme", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		array[0].IsRequired = true;
		array[0].IsGetNullable = false;
		array[0].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).callPlatform;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).callPlatform = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "callPlatform";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("callPlatform", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		array[1].IsRequired = true;
		array[1].IsGetNullable = false;
		array[1].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).currentDeviceId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).currentDeviceId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "currentDeviceId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("currentDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		array[2].IsRequired = true;
		array[2].IsGetNullable = false;
		array[2].IsSetNullable = false;
		JsonPropertyInfoValues<WebRtcPermission> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<WebRtcPermission>();
		jsonPropertyInfoValues5.IsProperty = true;
		jsonPropertyInfoValues5.IsPublic = true;
		jsonPropertyInfoValues5.IsVirtual = false;
		jsonPropertyInfoValues5.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues5.Converter = null;
		jsonPropertyInfoValues5.Getter = (object obj) => ((InitializeWebRtcPayload)obj).permissions;
		jsonPropertyInfoValues5.Setter = delegate(object obj, WebRtcPermission? value)
		{
			((InitializeWebRtcPayload)obj).permissions = value;
		};
		jsonPropertyInfoValues5.IgnoreCondition = null;
		jsonPropertyInfoValues5.HasJsonInclude = false;
		jsonPropertyInfoValues5.IsExtensionData = false;
		jsonPropertyInfoValues5.NumberHandling = null;
		jsonPropertyInfoValues5.PropertyName = "permissions";
		jsonPropertyInfoValues5.JsonPropertyName = null;
		jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("permissions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(WebRtcPermission), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<WebRtcPermission> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues6);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).currentUserId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).currentUserId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "currentUserId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("currentUserId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = jsonPropertyInfoValues;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		array[4].IsRequired = true;
		array[4].IsGetNullable = false;
		array[4].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).containerId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).containerId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "containerId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("containerId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		array[5].IsRequired = true;
		array[5].IsGetNullable = false;
		array[5].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).communityId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).communityId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "communityId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("communityId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = jsonPropertyInfoValues;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).webApiBaseUrl;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).webApiBaseUrl = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "webApiBaseUrl";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("webApiBaseUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		array[7].IsRequired = true;
		array[7].IsGetNullable = false;
		array[7].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).videoDeviceId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).videoDeviceId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "videoDeviceId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("videoDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = jsonPropertyInfoValues;
		array[8] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues11);
		array[8].IsRequired = true;
		array[8].IsGetNullable = false;
		array[8].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).audioInputDeviceId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).audioInputDeviceId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "audioInputDeviceId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("audioInputDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues;
		array[9] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues12);
		array[9].IsRequired = true;
		array[9].IsGetNullable = false;
		array[9].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).audioOutputDeviceId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).audioOutputDeviceId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "audioOutputDeviceId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("audioOutputDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = jsonPropertyInfoValues;
		array[10] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues13);
		array[10].IsRequired = true;
		array[10].IsGetNullable = false;
		array[10].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).screenAudioDeviceId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).screenAudioDeviceId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "screenAudioDeviceId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("screenAudioDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues;
		array[11] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues14);
		JsonPropertyInfoValues<char?> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<char?>();
		jsonPropertyInfoValues15.IsProperty = true;
		jsonPropertyInfoValues15.IsPublic = true;
		jsonPropertyInfoValues15.IsVirtual = false;
		jsonPropertyInfoValues15.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues15.Converter = null;
		jsonPropertyInfoValues15.Getter = (object obj) => ((InitializeWebRtcPayload)obj).preferredRid;
		jsonPropertyInfoValues15.Setter = delegate(object obj, char? value)
		{
			((InitializeWebRtcPayload)obj).preferredRid = value;
		};
		jsonPropertyInfoValues15.IgnoreCondition = null;
		jsonPropertyInfoValues15.HasJsonInclude = false;
		jsonPropertyInfoValues15.IsExtensionData = false;
		jsonPropertyInfoValues15.NumberHandling = null;
		jsonPropertyInfoValues15.PropertyName = "preferredRid";
		jsonPropertyInfoValues15.JsonPropertyName = null;
		jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("preferredRid", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(char?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<char?> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
		array[12] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues16);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues17.IsProperty = true;
		jsonPropertyInfoValues17.IsPublic = true;
		jsonPropertyInfoValues17.IsVirtual = false;
		jsonPropertyInfoValues17.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues17.Converter = null;
		jsonPropertyInfoValues17.Getter = (object obj) => ((InitializeWebRtcPayload)obj).isPushToTalkMode;
		jsonPropertyInfoValues17.Setter = delegate(object obj, bool value)
		{
			((InitializeWebRtcPayload)obj).isPushToTalkMode = value;
		};
		jsonPropertyInfoValues17.IgnoreCondition = null;
		jsonPropertyInfoValues17.HasJsonInclude = false;
		jsonPropertyInfoValues17.IsExtensionData = false;
		jsonPropertyInfoValues17.NumberHandling = null;
		jsonPropertyInfoValues17.PropertyName = "isPushToTalkMode";
		jsonPropertyInfoValues17.JsonPropertyName = null;
		jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("isPushToTalkMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
		array[13] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues18);
		array[13].IsRequired = true;
		jsonPropertyInfoValues17 = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues17.IsProperty = true;
		jsonPropertyInfoValues17.IsPublic = true;
		jsonPropertyInfoValues17.IsVirtual = false;
		jsonPropertyInfoValues17.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues17.Converter = null;
		jsonPropertyInfoValues17.Getter = (object obj) => ((InitializeWebRtcPayload)obj).debugMode;
		jsonPropertyInfoValues17.Setter = delegate(object obj, bool value)
		{
			((InitializeWebRtcPayload)obj).debugMode = value;
		};
		jsonPropertyInfoValues17.IgnoreCondition = null;
		jsonPropertyInfoValues17.HasJsonInclude = false;
		jsonPropertyInfoValues17.IsExtensionData = false;
		jsonPropertyInfoValues17.NumberHandling = null;
		jsonPropertyInfoValues17.PropertyName = "debugMode";
		jsonPropertyInfoValues17.JsonPropertyName = null;
		jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("debugMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues19 = jsonPropertyInfoValues17;
		array[14] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues19);
		array[14].IsRequired = true;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).logging;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).logging = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "logging";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("logging", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues20 = jsonPropertyInfoValues;
		array[15] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues20);
		array[15].IsRequired = true;
		array[15].IsGetNullable = false;
		array[15].IsSetNullable = false;
		JsonPropertyInfoValues<string[]> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<string[]>();
		jsonPropertyInfoValues21.IsProperty = true;
		jsonPropertyInfoValues21.IsPublic = true;
		jsonPropertyInfoValues21.IsVirtual = false;
		jsonPropertyInfoValues21.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues21.Converter = null;
		jsonPropertyInfoValues21.Getter = (object obj) => ((InitializeWebRtcPayload)obj).initialTrackTypes;
		jsonPropertyInfoValues21.Setter = delegate(object obj, string[]? value)
		{
			((InitializeWebRtcPayload)obj).initialTrackTypes = value;
		};
		jsonPropertyInfoValues21.IgnoreCondition = null;
		jsonPropertyInfoValues21.HasJsonInclude = false;
		jsonPropertyInfoValues21.IsExtensionData = false;
		jsonPropertyInfoValues21.NumberHandling = null;
		jsonPropertyInfoValues21.PropertyName = "initialTrackTypes";
		jsonPropertyInfoValues21.JsonPropertyName = null;
		jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("initialTrackTypes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string[]), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string[]> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
		array[16] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues22);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues23.IsProperty = true;
		jsonPropertyInfoValues23.IsPublic = true;
		jsonPropertyInfoValues23.IsVirtual = false;
		jsonPropertyInfoValues23.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues23.Converter = null;
		jsonPropertyInfoValues23.Getter = (object obj) => ((InitializeWebRtcPayload)obj).inputVolume;
		jsonPropertyInfoValues23.Setter = delegate(object obj, double? value)
		{
			((InitializeWebRtcPayload)obj).inputVolume = value;
		};
		jsonPropertyInfoValues23.IgnoreCondition = null;
		jsonPropertyInfoValues23.HasJsonInclude = false;
		jsonPropertyInfoValues23.IsExtensionData = false;
		jsonPropertyInfoValues23.NumberHandling = null;
		jsonPropertyInfoValues23.PropertyName = "inputVolume";
		jsonPropertyInfoValues23.JsonPropertyName = null;
		jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("inputVolume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
		array[17] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues24);
		jsonPropertyInfoValues23 = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues23.IsProperty = true;
		jsonPropertyInfoValues23.IsPublic = true;
		jsonPropertyInfoValues23.IsVirtual = false;
		jsonPropertyInfoValues23.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues23.Converter = null;
		jsonPropertyInfoValues23.Getter = (object obj) => ((InitializeWebRtcPayload)obj).outputVolume;
		jsonPropertyInfoValues23.Setter = delegate(object obj, double? value)
		{
			((InitializeWebRtcPayload)obj).outputVolume = value;
		};
		jsonPropertyInfoValues23.IgnoreCondition = null;
		jsonPropertyInfoValues23.HasJsonInclude = false;
		jsonPropertyInfoValues23.IsExtensionData = false;
		jsonPropertyInfoValues23.NumberHandling = null;
		jsonPropertyInfoValues23.PropertyName = "outputVolume";
		jsonPropertyInfoValues23.JsonPropertyName = null;
		jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("outputVolume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues25 = jsonPropertyInfoValues23;
		array[18] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues25);
		JsonPropertyInfoValues<UserTileVolume[]> jsonPropertyInfoValues26 = new JsonPropertyInfoValues<UserTileVolume[]>();
		jsonPropertyInfoValues26.IsProperty = true;
		jsonPropertyInfoValues26.IsPublic = true;
		jsonPropertyInfoValues26.IsVirtual = false;
		jsonPropertyInfoValues26.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues26.Converter = null;
		jsonPropertyInfoValues26.Getter = (object obj) => ((InitializeWebRtcPayload)obj).tileVolumes;
		jsonPropertyInfoValues26.Setter = delegate(object obj, UserTileVolume[]? value)
		{
			((InitializeWebRtcPayload)obj).tileVolumes = value;
		};
		jsonPropertyInfoValues26.IgnoreCondition = null;
		jsonPropertyInfoValues26.HasJsonInclude = false;
		jsonPropertyInfoValues26.IsExtensionData = false;
		jsonPropertyInfoValues26.NumberHandling = null;
		jsonPropertyInfoValues26.PropertyName = "tileVolumes";
		jsonPropertyInfoValues26.JsonPropertyName = null;
		jsonPropertyInfoValues26.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("tileVolumes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(UserTileVolume[]), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<UserTileVolume[]> jsonPropertyInfoValues27 = jsonPropertyInfoValues26;
		array[19] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues27);
		jsonPropertyInfoValues23 = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues23.IsProperty = true;
		jsonPropertyInfoValues23.IsPublic = true;
		jsonPropertyInfoValues23.IsVirtual = false;
		jsonPropertyInfoValues23.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues23.Converter = null;
		jsonPropertyInfoValues23.Getter = (object obj) => ((InitializeWebRtcPayload)obj).defaultInputVolume;
		jsonPropertyInfoValues23.Setter = delegate(object obj, double? value)
		{
			((InitializeWebRtcPayload)obj).defaultInputVolume = value;
		};
		jsonPropertyInfoValues23.IgnoreCondition = null;
		jsonPropertyInfoValues23.HasJsonInclude = false;
		jsonPropertyInfoValues23.IsExtensionData = false;
		jsonPropertyInfoValues23.NumberHandling = null;
		jsonPropertyInfoValues23.PropertyName = "defaultInputVolume";
		jsonPropertyInfoValues23.JsonPropertyName = null;
		jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("defaultInputVolume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues28 = jsonPropertyInfoValues23;
		array[20] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues28);
		jsonPropertyInfoValues23 = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues23.IsProperty = true;
		jsonPropertyInfoValues23.IsPublic = true;
		jsonPropertyInfoValues23.IsVirtual = false;
		jsonPropertyInfoValues23.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues23.Converter = null;
		jsonPropertyInfoValues23.Getter = (object obj) => ((InitializeWebRtcPayload)obj).defaultGlobalOutputVolume;
		jsonPropertyInfoValues23.Setter = delegate(object obj, double? value)
		{
			((InitializeWebRtcPayload)obj).defaultGlobalOutputVolume = value;
		};
		jsonPropertyInfoValues23.IgnoreCondition = null;
		jsonPropertyInfoValues23.HasJsonInclude = false;
		jsonPropertyInfoValues23.IsExtensionData = false;
		jsonPropertyInfoValues23.NumberHandling = null;
		jsonPropertyInfoValues23.PropertyName = "defaultGlobalOutputVolume";
		jsonPropertyInfoValues23.JsonPropertyName = null;
		jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("defaultGlobalOutputVolume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues29 = jsonPropertyInfoValues23;
		array[21] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues29);
		jsonPropertyInfoValues23 = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues23.IsProperty = true;
		jsonPropertyInfoValues23.IsPublic = true;
		jsonPropertyInfoValues23.IsVirtual = false;
		jsonPropertyInfoValues23.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues23.Converter = null;
		jsonPropertyInfoValues23.Getter = (object obj) => ((InitializeWebRtcPayload)obj).defaultPrimaryOutputVolume;
		jsonPropertyInfoValues23.Setter = delegate(object obj, double? value)
		{
			((InitializeWebRtcPayload)obj).defaultPrimaryOutputVolume = value;
		};
		jsonPropertyInfoValues23.IgnoreCondition = null;
		jsonPropertyInfoValues23.HasJsonInclude = false;
		jsonPropertyInfoValues23.IsExtensionData = false;
		jsonPropertyInfoValues23.NumberHandling = null;
		jsonPropertyInfoValues23.PropertyName = "defaultPrimaryOutputVolume";
		jsonPropertyInfoValues23.JsonPropertyName = null;
		jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("defaultPrimaryOutputVolume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues30 = jsonPropertyInfoValues23;
		array[22] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues30);
		jsonPropertyInfoValues23 = new JsonPropertyInfoValues<double?>();
		jsonPropertyInfoValues23.IsProperty = true;
		jsonPropertyInfoValues23.IsPublic = true;
		jsonPropertyInfoValues23.IsVirtual = false;
		jsonPropertyInfoValues23.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues23.Converter = null;
		jsonPropertyInfoValues23.Getter = (object obj) => ((InitializeWebRtcPayload)obj).defaultScreenOutputVolume;
		jsonPropertyInfoValues23.Setter = delegate(object obj, double? value)
		{
			((InitializeWebRtcPayload)obj).defaultScreenOutputVolume = value;
		};
		jsonPropertyInfoValues23.IgnoreCondition = null;
		jsonPropertyInfoValues23.HasJsonInclude = false;
		jsonPropertyInfoValues23.IsExtensionData = false;
		jsonPropertyInfoValues23.NumberHandling = null;
		jsonPropertyInfoValues23.PropertyName = "defaultScreenOutputVolume";
		jsonPropertyInfoValues23.JsonPropertyName = null;
		jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("defaultScreenOutputVolume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double?> jsonPropertyInfoValues31 = jsonPropertyInfoValues23;
		array[23] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues31);
		jsonPropertyInfoValues21 = new JsonPropertyInfoValues<string[]>();
		jsonPropertyInfoValues21.IsProperty = true;
		jsonPropertyInfoValues21.IsPublic = true;
		jsonPropertyInfoValues21.IsVirtual = false;
		jsonPropertyInfoValues21.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues21.Converter = null;
		jsonPropertyInfoValues21.Getter = (object obj) => ((InitializeWebRtcPayload)obj).ringingUserIds;
		jsonPropertyInfoValues21.Setter = delegate(object obj, string[]? value)
		{
			((InitializeWebRtcPayload)obj).ringingUserIds = value;
		};
		jsonPropertyInfoValues21.IgnoreCondition = null;
		jsonPropertyInfoValues21.HasJsonInclude = false;
		jsonPropertyInfoValues21.IsExtensionData = false;
		jsonPropertyInfoValues21.NumberHandling = null;
		jsonPropertyInfoValues21.PropertyName = "ringingUserIds";
		jsonPropertyInfoValues21.JsonPropertyName = null;
		jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("ringingUserIds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string[]), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string[]> jsonPropertyInfoValues32 = jsonPropertyInfoValues21;
		array[24] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues32);
		array[24].IsRequired = true;
		array[24].IsGetNullable = false;
		array[24].IsSetNullable = false;
		JsonPropertyInfoValues<int> jsonPropertyInfoValues33 = new JsonPropertyInfoValues<int>();
		jsonPropertyInfoValues33.IsProperty = true;
		jsonPropertyInfoValues33.IsPublic = true;
		jsonPropertyInfoValues33.IsVirtual = false;
		jsonPropertyInfoValues33.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues33.Converter = null;
		jsonPropertyInfoValues33.Getter = (object obj) => ((InitializeWebRtcPayload)obj).ringTimeoutMs;
		jsonPropertyInfoValues33.Setter = delegate(object obj, int value)
		{
			((InitializeWebRtcPayload)obj).ringTimeoutMs = value;
		};
		jsonPropertyInfoValues33.IgnoreCondition = null;
		jsonPropertyInfoValues33.HasJsonInclude = false;
		jsonPropertyInfoValues33.IsExtensionData = false;
		jsonPropertyInfoValues33.NumberHandling = null;
		jsonPropertyInfoValues33.PropertyName = "ringTimeoutMs";
		jsonPropertyInfoValues33.JsonPropertyName = null;
		jsonPropertyInfoValues33.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("ringTimeoutMs", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<int> jsonPropertyInfoValues34 = jsonPropertyInfoValues33;
		array[25] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues34);
		array[25].IsRequired = true;
		jsonPropertyInfoValues21 = new JsonPropertyInfoValues<string[]>();
		jsonPropertyInfoValues21.IsProperty = true;
		jsonPropertyInfoValues21.IsPublic = true;
		jsonPropertyInfoValues21.IsVirtual = false;
		jsonPropertyInfoValues21.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues21.Converter = null;
		jsonPropertyInfoValues21.Getter = (object obj) => ((InitializeWebRtcPayload)obj).activeUserIds;
		jsonPropertyInfoValues21.Setter = delegate(object obj, string[]? value)
		{
			((InitializeWebRtcPayload)obj).activeUserIds = value;
		};
		jsonPropertyInfoValues21.IgnoreCondition = null;
		jsonPropertyInfoValues21.HasJsonInclude = false;
		jsonPropertyInfoValues21.IsExtensionData = false;
		jsonPropertyInfoValues21.NumberHandling = null;
		jsonPropertyInfoValues21.PropertyName = "activeUserIds";
		jsonPropertyInfoValues21.JsonPropertyName = null;
		jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("activeUserIds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string[]), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string[]> jsonPropertyInfoValues35 = jsonPropertyInfoValues21;
		array[26] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues35);
		array[26].IsRequired = true;
		array[26].IsGetNullable = false;
		array[26].IsSetNullable = false;
		JsonPropertyInfoValues<WebRtcCodec[]> jsonPropertyInfoValues36 = new JsonPropertyInfoValues<WebRtcCodec[]>();
		jsonPropertyInfoValues36.IsProperty = true;
		jsonPropertyInfoValues36.IsPublic = true;
		jsonPropertyInfoValues36.IsVirtual = false;
		jsonPropertyInfoValues36.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues36.Converter = null;
		jsonPropertyInfoValues36.Getter = (object obj) => ((InitializeWebRtcPayload)obj).preferredCodecs;
		jsonPropertyInfoValues36.Setter = delegate(object obj, WebRtcCodec[]? value)
		{
			((InitializeWebRtcPayload)obj).preferredCodecs = value;
		};
		jsonPropertyInfoValues36.IgnoreCondition = null;
		jsonPropertyInfoValues36.HasJsonInclude = false;
		jsonPropertyInfoValues36.IsExtensionData = false;
		jsonPropertyInfoValues36.NumberHandling = null;
		jsonPropertyInfoValues36.PropertyName = "preferredCodecs";
		jsonPropertyInfoValues36.JsonPropertyName = null;
		jsonPropertyInfoValues36.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("preferredCodecs", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(WebRtcCodec[]), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<WebRtcCodec[]> jsonPropertyInfoValues37 = jsonPropertyInfoValues36;
		array[27] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues37);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((InitializeWebRtcPayload)obj).preferredScreenContentHint;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((InitializeWebRtcPayload)obj).preferredScreenContentHint = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "preferredScreenContentHint";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("preferredScreenContentHint", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues38 = jsonPropertyInfoValues;
		array[28] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues38);
		JsonPropertyInfoValues<UserMediaStreamConstraints> jsonPropertyInfoValues39 = new JsonPropertyInfoValues<UserMediaStreamConstraints>();
		jsonPropertyInfoValues39.IsProperty = true;
		jsonPropertyInfoValues39.IsPublic = true;
		jsonPropertyInfoValues39.IsVirtual = false;
		jsonPropertyInfoValues39.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues39.Converter = null;
		jsonPropertyInfoValues39.Getter = (object obj) => ((InitializeWebRtcPayload)obj).userMediaConstraints;
		jsonPropertyInfoValues39.Setter = delegate(object obj, UserMediaStreamConstraints? value)
		{
			((InitializeWebRtcPayload)obj).userMediaConstraints = value;
		};
		jsonPropertyInfoValues39.IgnoreCondition = null;
		jsonPropertyInfoValues39.HasJsonInclude = false;
		jsonPropertyInfoValues39.IsExtensionData = false;
		jsonPropertyInfoValues39.NumberHandling = null;
		jsonPropertyInfoValues39.PropertyName = "userMediaConstraints";
		jsonPropertyInfoValues39.JsonPropertyName = null;
		jsonPropertyInfoValues39.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("userMediaConstraints", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(UserMediaStreamConstraints), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<UserMediaStreamConstraints> jsonPropertyInfoValues40 = jsonPropertyInfoValues39;
		array[29] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues40);
		JsonPropertyInfoValues<DisplayMediaStreamConstraints> jsonPropertyInfoValues41 = new JsonPropertyInfoValues<DisplayMediaStreamConstraints>();
		jsonPropertyInfoValues41.IsProperty = true;
		jsonPropertyInfoValues41.IsPublic = true;
		jsonPropertyInfoValues41.IsVirtual = false;
		jsonPropertyInfoValues41.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues41.Converter = null;
		jsonPropertyInfoValues41.Getter = (object obj) => ((InitializeWebRtcPayload)obj).displayMediaConstraints;
		jsonPropertyInfoValues41.Setter = delegate(object obj, DisplayMediaStreamConstraints? value)
		{
			((InitializeWebRtcPayload)obj).displayMediaConstraints = value;
		};
		jsonPropertyInfoValues41.IgnoreCondition = null;
		jsonPropertyInfoValues41.HasJsonInclude = false;
		jsonPropertyInfoValues41.IsExtensionData = false;
		jsonPropertyInfoValues41.NumberHandling = null;
		jsonPropertyInfoValues41.PropertyName = "displayMediaConstraints";
		jsonPropertyInfoValues41.JsonPropertyName = null;
		jsonPropertyInfoValues41.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("displayMediaConstraints", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DisplayMediaStreamConstraints), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<DisplayMediaStreamConstraints> jsonPropertyInfoValues42 = jsonPropertyInfoValues41;
		array[30] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues42);
		JsonPropertyInfoValues<Dictionary<string, bool>> jsonPropertyInfoValues43 = new JsonPropertyInfoValues<Dictionary<string, bool>>();
		jsonPropertyInfoValues43.IsProperty = true;
		jsonPropertyInfoValues43.IsPublic = true;
		jsonPropertyInfoValues43.IsVirtual = false;
		jsonPropertyInfoValues43.DeclaringType = typeof(InitializeWebRtcPayload);
		jsonPropertyInfoValues43.Converter = null;
		jsonPropertyInfoValues43.Getter = (object obj) => ((InitializeWebRtcPayload)obj).experimentalFlags;
		jsonPropertyInfoValues43.Setter = delegate(object obj, Dictionary<string, bool>? value)
		{
			((InitializeWebRtcPayload)obj).experimentalFlags = value;
		};
		jsonPropertyInfoValues43.IgnoreCondition = null;
		jsonPropertyInfoValues43.HasJsonInclude = false;
		jsonPropertyInfoValues43.IsExtensionData = false;
		jsonPropertyInfoValues43.NumberHandling = null;
		jsonPropertyInfoValues43.PropertyName = "experimentalFlags";
		jsonPropertyInfoValues43.JsonPropertyName = null;
		jsonPropertyInfoValues43.AttributeProviderFactory = () => typeof(InitializeWebRtcPayload).GetProperty("experimentalFlags", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Dictionary<string, bool>), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<Dictionary<string, bool>> jsonPropertyInfoValues44 = jsonPropertyInfoValues43;
		array[31] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues44);
		return array;
	}

	private void InitializeWebRtcPayloadSerializeHandler(Utf8JsonWriter writer, InitializeWebRtcPayload? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteString(PropName_theme, value.theme);
		writer.WriteString(PropName_callPlatform, value.callPlatform);
		writer.WriteString(PropName_currentDeviceId, value.currentDeviceId);
		writer.WritePropertyName(PropName_permissions);
		WebRtcPermissionSerializeHandler(writer, value.permissions);
		writer.WriteString(PropName_currentUserId, value.currentUserId);
		writer.WriteString(PropName_containerId, value.containerId);
		writer.WriteString(PropName_communityId, value.communityId);
		writer.WriteString(PropName_webApiBaseUrl, value.webApiBaseUrl);
		writer.WriteString(PropName_videoDeviceId, value.videoDeviceId);
		writer.WriteString(PropName_audioInputDeviceId, value.audioInputDeviceId);
		writer.WriteString(PropName_audioOutputDeviceId, value.audioOutputDeviceId);
		writer.WriteString(PropName_screenAudioDeviceId, value.screenAudioDeviceId);
		writer.WritePropertyName(PropName_preferredRid);
		JsonSerializer.Serialize(writer, value.preferredRid, NullableChar);
		writer.WriteBoolean(PropName_isPushToTalkMode, value.isPushToTalkMode);
		writer.WriteBoolean(PropName_debugMode, value.debugMode);
		writer.WriteString(PropName_logging, value.logging);
		writer.WritePropertyName(PropName_initialTrackTypes);
		StringArraySerializeHandler(writer, value.initialTrackTypes);
		writer.WritePropertyName(PropName_inputVolume);
		JsonSerializer.Serialize(writer, value.inputVolume, NullableDouble);
		writer.WritePropertyName(PropName_outputVolume);
		JsonSerializer.Serialize(writer, value.outputVolume, NullableDouble);
		writer.WritePropertyName(PropName_tileVolumes);
		UserTileVolumeArraySerializeHandler(writer, value.tileVolumes);
		writer.WritePropertyName(PropName_defaultInputVolume);
		JsonSerializer.Serialize(writer, value.defaultInputVolume, NullableDouble);
		writer.WritePropertyName(PropName_defaultGlobalOutputVolume);
		JsonSerializer.Serialize(writer, value.defaultGlobalOutputVolume, NullableDouble);
		writer.WritePropertyName(PropName_defaultPrimaryOutputVolume);
		JsonSerializer.Serialize(writer, value.defaultPrimaryOutputVolume, NullableDouble);
		writer.WritePropertyName(PropName_defaultScreenOutputVolume);
		JsonSerializer.Serialize(writer, value.defaultScreenOutputVolume, NullableDouble);
		writer.WritePropertyName(PropName_ringingUserIds);
		StringArraySerializeHandler(writer, value.ringingUserIds);
		writer.WriteNumber(PropName_ringTimeoutMs, value.ringTimeoutMs);
		writer.WritePropertyName(PropName_activeUserIds);
		StringArraySerializeHandler(writer, value.activeUserIds);
		writer.WritePropertyName(PropName_preferredCodecs);
		WebRtcCodecArraySerializeHandler(writer, value.preferredCodecs);
		writer.WriteString(PropName_preferredScreenContentHint, value.preferredScreenContentHint);
		writer.WritePropertyName(PropName_userMediaConstraints);
		UserMediaStreamConstraintsSerializeHandler(writer, value.userMediaConstraints);
		writer.WritePropertyName(PropName_displayMediaConstraints);
		DisplayMediaStreamConstraintsSerializeHandler(writer, value.displayMediaConstraints);
		writer.WritePropertyName(PropName_experimentalFlags);
		DictionaryStringBooleanSerializeHandler(writer, value.experimentalFlags);
		writer.WriteEndObject();
	}

	private static JsonParameterInfoValues[] InitializeWebRtcPayloadCtorParamInit()
	{
		return new JsonParameterInfoValues[15]
		{
			new JsonParameterInfoValues
			{
				Name = "theme",
				ParameterType = typeof(string),
				Position = 0,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "callPlatform",
				ParameterType = typeof(string),
				Position = 1,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "currentDeviceId",
				ParameterType = typeof(string),
				Position = 2,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "currentUserId",
				ParameterType = typeof(string),
				Position = 3,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "containerId",
				ParameterType = typeof(string),
				Position = 4,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "webApiBaseUrl",
				ParameterType = typeof(string),
				Position = 5,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "videoDeviceId",
				ParameterType = typeof(string),
				Position = 6,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "audioInputDeviceId",
				ParameterType = typeof(string),
				Position = 7,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "audioOutputDeviceId",
				ParameterType = typeof(string),
				Position = 8,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "isPushToTalkMode",
				ParameterType = typeof(bool),
				Position = 9,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "debugMode",
				ParameterType = typeof(bool),
				Position = 10,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "logging",
				ParameterType = typeof(string),
				Position = 11,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "ringingUserIds",
				ParameterType = typeof(string[]),
				Position = 12,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "ringTimeoutMs",
				ParameterType = typeof(int),
				Position = 13,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "activeUserIds",
				ParameterType = typeof(string[]),
				Position = 14,
				IsNullable = false,
				IsMemberInitializer = true
			}
		};
	}

	private JsonTypeInfo<ScreenTrackConstraints> Create_ScreenTrackConstraints(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<ScreenTrackConstraints> jsonTypeInfo))
		{
			JsonObjectInfoValues<ScreenTrackConstraints> jsonObjectInfoValues = new JsonObjectInfoValues<ScreenTrackConstraints>
			{
				ObjectCreator = () => new ScreenTrackConstraints(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => ScreenTrackConstraintsPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = ScreenTrackConstraintsSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] ScreenTrackConstraintsPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[11];
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ScreenTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ScreenTrackConstraints)obj).displaySurface;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainString? value)
		{
			((ScreenTrackConstraints)obj).displaySurface = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "displaySurface";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetProperty("displaySurface", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(ScreenTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((ScreenTrackConstraints)obj).logicalSurface;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((ScreenTrackConstraints)obj).logicalSurface = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "logicalSurface";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetProperty("logicalSurface", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(ScreenTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((ScreenTrackConstraints)obj).cursor;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainString? value)
		{
			((ScreenTrackConstraints)obj).cursor = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "cursor";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetProperty("cursor", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(ScreenTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((ScreenTrackConstraints)obj).screenPixelRatio;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((ScreenTrackConstraints)obj).screenPixelRatio = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "screenPixelRatio";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetProperty("screenPixelRatio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues7 = jsonPropertyInfoValues6;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(ScreenTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((ScreenTrackConstraints)obj).restrictOwnAudio;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((ScreenTrackConstraints)obj).restrictOwnAudio = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "restrictOwnAudio";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetProperty("restrictOwnAudio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues8 = jsonPropertyInfoValues3;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(ScreenTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((ScreenTrackConstraints)obj).suppressLocalAudioPlayback;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((ScreenTrackConstraints)obj).suppressLocalAudioPlayback = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "suppressLocalAudioPlayback";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ScreenTrackConstraints).GetProperty("suppressLocalAudioPlayback", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues9 = jsonPropertyInfoValues3;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues10 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues10.IsProperty = true;
		jsonPropertyInfoValues10.IsPublic = true;
		jsonPropertyInfoValues10.IsVirtual = false;
		jsonPropertyInfoValues10.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues10.Converter = null;
		jsonPropertyInfoValues10.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).width;
		jsonPropertyInfoValues10.Setter = delegate(object obj, ConstrainLong? value)
		{
			((BaseVideoTrackConstraints)obj).width = value;
		};
		jsonPropertyInfoValues10.IgnoreCondition = null;
		jsonPropertyInfoValues10.HasJsonInclude = false;
		jsonPropertyInfoValues10.IsExtensionData = false;
		jsonPropertyInfoValues10.NumberHandling = null;
		jsonPropertyInfoValues10.PropertyName = "width";
		jsonPropertyInfoValues10.JsonPropertyName = null;
		jsonPropertyInfoValues10.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("width", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues11 = jsonPropertyInfoValues10;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues11);
		jsonPropertyInfoValues10 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues10.IsProperty = true;
		jsonPropertyInfoValues10.IsPublic = true;
		jsonPropertyInfoValues10.IsVirtual = false;
		jsonPropertyInfoValues10.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues10.Converter = null;
		jsonPropertyInfoValues10.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).height;
		jsonPropertyInfoValues10.Setter = delegate(object obj, ConstrainLong? value)
		{
			((BaseVideoTrackConstraints)obj).height = value;
		};
		jsonPropertyInfoValues10.IgnoreCondition = null;
		jsonPropertyInfoValues10.HasJsonInclude = false;
		jsonPropertyInfoValues10.IsExtensionData = false;
		jsonPropertyInfoValues10.NumberHandling = null;
		jsonPropertyInfoValues10.PropertyName = "height";
		jsonPropertyInfoValues10.JsonPropertyName = null;
		jsonPropertyInfoValues10.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("height", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues12 = jsonPropertyInfoValues10;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues12);
		jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).frameRate;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((BaseVideoTrackConstraints)obj).frameRate = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "frameRate";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("frameRate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues13 = jsonPropertyInfoValues6;
		array[8] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues13);
		jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).aspectRatio;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((BaseVideoTrackConstraints)obj).aspectRatio = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "aspectRatio";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("aspectRatio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues14 = jsonPropertyInfoValues6;
		array[9] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues14);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).resizeMode;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainString? value)
		{
			((BaseVideoTrackConstraints)obj).resizeMode = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "resizeMode";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("resizeMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues15 = jsonPropertyInfoValues;
		array[10] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues15);
		return array;
	}

	private void ScreenTrackConstraintsSerializeHandler(Utf8JsonWriter writer, ScreenTrackConstraints? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_displaySurface);
		ConstrainStringSerializeHandler(writer, value.displaySurface);
		writer.WritePropertyName(PropName_logicalSurface);
		ConstrainBooleanSerializeHandler(writer, value.logicalSurface);
		writer.WritePropertyName(PropName_cursor);
		ConstrainStringSerializeHandler(writer, value.cursor);
		writer.WritePropertyName(PropName_screenPixelRatio);
		ConstrainDoubleSerializeHandler(writer, value.screenPixelRatio);
		writer.WritePropertyName(PropName_restrictOwnAudio);
		ConstrainBooleanSerializeHandler(writer, value.restrictOwnAudio);
		writer.WritePropertyName(PropName_suppressLocalAudioPlayback);
		ConstrainBooleanSerializeHandler(writer, value.suppressLocalAudioPlayback);
		writer.WritePropertyName(PropName_width);
		ConstrainLongSerializeHandler(writer, value.width);
		writer.WritePropertyName(PropName_height);
		ConstrainLongSerializeHandler(writer, value.height);
		writer.WritePropertyName(PropName_frameRate);
		ConstrainDoubleSerializeHandler(writer, value.frameRate);
		writer.WritePropertyName(PropName_aspectRatio);
		ConstrainDoubleSerializeHandler(writer, value.aspectRatio);
		writer.WritePropertyName(PropName_resizeMode);
		ConstrainStringSerializeHandler(writer, value.resizeMode);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<UserMediaStreamConstraints> Create_UserMediaStreamConstraints(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<UserMediaStreamConstraints> jsonTypeInfo))
		{
			JsonObjectInfoValues<UserMediaStreamConstraints> jsonObjectInfoValues = new JsonObjectInfoValues<UserMediaStreamConstraints>
			{
				ObjectCreator = () => new UserMediaStreamConstraints(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => UserMediaStreamConstraintsPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(UserMediaStreamConstraints).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = UserMediaStreamConstraintsSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] UserMediaStreamConstraintsPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[2];
		JsonPropertyInfoValues<VideoTrackConstraints> jsonPropertyInfoValues = new JsonPropertyInfoValues<VideoTrackConstraints>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserMediaStreamConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserMediaStreamConstraints)obj).video;
		jsonPropertyInfoValues.Setter = delegate(object obj, VideoTrackConstraints? value)
		{
			((UserMediaStreamConstraints)obj).video = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "video";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserMediaStreamConstraints).GetProperty("video", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(VideoTrackConstraints), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<VideoTrackConstraints> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<AudioTrackConstraints> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<AudioTrackConstraints>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(UserMediaStreamConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((UserMediaStreamConstraints)obj).audio;
		jsonPropertyInfoValues3.Setter = delegate(object obj, AudioTrackConstraints? value)
		{
			((UserMediaStreamConstraints)obj).audio = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "audio";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(UserMediaStreamConstraints).GetProperty("audio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(AudioTrackConstraints), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<AudioTrackConstraints> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		return array;
	}

	private void UserMediaStreamConstraintsSerializeHandler(Utf8JsonWriter writer, UserMediaStreamConstraints? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_video);
		VideoTrackConstraintsSerializeHandler(writer, value.video);
		writer.WritePropertyName(PropName_audio);
		AudioTrackConstraintsSerializeHandler(writer, value.audio);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<UserTileVolume> Create_UserTileVolume(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<UserTileVolume> jsonTypeInfo))
		{
			JsonObjectInfoValues<UserTileVolume> jsonObjectInfoValues = new JsonObjectInfoValues<UserTileVolume>
			{
				ObjectCreator = () => new UserTileVolume(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => UserTileVolumePropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(UserTileVolume).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = UserTileVolumeSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] UserTileVolumePropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[3];
		JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserTileVolume);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserTileVolume)obj).userId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((UserTileVolume)obj).userId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "userId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserTileVolume).GetProperty("userId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		array[0].IsGetNullable = false;
		array[0].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(UserTileVolume);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((UserTileVolume)obj).tileType;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((UserTileVolume)obj).tileType = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "tileType";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserTileVolume).GetProperty("tileType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		array[1].IsGetNullable = false;
		array[1].IsSetNullable = false;
		JsonPropertyInfoValues<double> jsonPropertyInfoValues4 = new JsonPropertyInfoValues<double>();
		jsonPropertyInfoValues4.IsProperty = true;
		jsonPropertyInfoValues4.IsPublic = true;
		jsonPropertyInfoValues4.IsVirtual = false;
		jsonPropertyInfoValues4.DeclaringType = typeof(UserTileVolume);
		jsonPropertyInfoValues4.Converter = null;
		jsonPropertyInfoValues4.Getter = (object obj) => ((UserTileVolume)obj).volume;
		jsonPropertyInfoValues4.Setter = delegate(object obj, double value)
		{
			((UserTileVolume)obj).volume = value;
		};
		jsonPropertyInfoValues4.IgnoreCondition = null;
		jsonPropertyInfoValues4.HasJsonInclude = false;
		jsonPropertyInfoValues4.IsExtensionData = false;
		jsonPropertyInfoValues4.NumberHandling = null;
		jsonPropertyInfoValues4.PropertyName = "volume";
		jsonPropertyInfoValues4.JsonPropertyName = null;
		jsonPropertyInfoValues4.AttributeProviderFactory = () => typeof(UserTileVolume).GetProperty("volume", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<double> jsonPropertyInfoValues5 = jsonPropertyInfoValues4;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		return array;
	}

	private void UserTileVolumeSerializeHandler(Utf8JsonWriter writer, UserTileVolume? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteString(PropName_userId, value.userId);
		writer.WriteString(PropName_tileType, value.tileType);
		writer.WriteNumber(PropName_volume, value.volume);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<UserTileVolume[]> Create_UserTileVolumeArray(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<UserTileVolume[]> jsonTypeInfo))
		{
			JsonCollectionInfoValues<UserTileVolume[]> jsonCollectionInfoValues = new JsonCollectionInfoValues<UserTileVolume[]>
			{
				ObjectCreator = null,
				SerializeHandler = UserTileVolumeArraySerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateArrayInfo(P_0, jsonCollectionInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private void UserTileVolumeArraySerializeHandler(Utf8JsonWriter writer, UserTileVolume[]? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartArray();
		for (int i = 0; i < value.Length; i++)
		{
			UserTileVolumeSerializeHandler(writer, value[i]);
		}
		writer.WriteEndArray();
	}

	private JsonTypeInfo<VideoTrackConstraints> Create_VideoTrackConstraints(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<VideoTrackConstraints> jsonTypeInfo))
		{
			JsonObjectInfoValues<VideoTrackConstraints> jsonObjectInfoValues = new JsonObjectInfoValues<VideoTrackConstraints>
			{
				ObjectCreator = () => new VideoTrackConstraints(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => VideoTrackConstraintsPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(VideoTrackConstraints).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = VideoTrackConstraintsSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] VideoTrackConstraintsPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[8];
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(VideoTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((VideoTrackConstraints)obj).facingMode;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainString? value)
		{
			((VideoTrackConstraints)obj).facingMode = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "facingMode";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(VideoTrackConstraints).GetProperty("facingMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ConstrainBoolean>();
		jsonPropertyInfoValues3.IsProperty = true;
		jsonPropertyInfoValues3.IsPublic = true;
		jsonPropertyInfoValues3.IsVirtual = false;
		jsonPropertyInfoValues3.DeclaringType = typeof(VideoTrackConstraints);
		jsonPropertyInfoValues3.Converter = null;
		jsonPropertyInfoValues3.Getter = (object obj) => ((VideoTrackConstraints)obj).backgroundBlur;
		jsonPropertyInfoValues3.Setter = delegate(object obj, ConstrainBoolean? value)
		{
			((VideoTrackConstraints)obj).backgroundBlur = value;
		};
		jsonPropertyInfoValues3.IgnoreCondition = null;
		jsonPropertyInfoValues3.HasJsonInclude = false;
		jsonPropertyInfoValues3.IsExtensionData = false;
		jsonPropertyInfoValues3.NumberHandling = null;
		jsonPropertyInfoValues3.PropertyName = "backgroundBlur";
		jsonPropertyInfoValues3.JsonPropertyName = null;
		jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(VideoTrackConstraints).GetProperty("backgroundBlur", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainBoolean), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainBoolean> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(VideoTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((VideoTrackConstraints)obj).groupId;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainString? value)
		{
			((VideoTrackConstraints)obj).groupId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "groupId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(VideoTrackConstraints).GetProperty("groupId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).width;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainLong? value)
		{
			((BaseVideoTrackConstraints)obj).width = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "width";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("width", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues7 = jsonPropertyInfoValues6;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		jsonPropertyInfoValues6 = new JsonPropertyInfoValues<ConstrainLong>();
		jsonPropertyInfoValues6.IsProperty = true;
		jsonPropertyInfoValues6.IsPublic = true;
		jsonPropertyInfoValues6.IsVirtual = false;
		jsonPropertyInfoValues6.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues6.Converter = null;
		jsonPropertyInfoValues6.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).height;
		jsonPropertyInfoValues6.Setter = delegate(object obj, ConstrainLong? value)
		{
			((BaseVideoTrackConstraints)obj).height = value;
		};
		jsonPropertyInfoValues6.IgnoreCondition = null;
		jsonPropertyInfoValues6.HasJsonInclude = false;
		jsonPropertyInfoValues6.IsExtensionData = false;
		jsonPropertyInfoValues6.NumberHandling = null;
		jsonPropertyInfoValues6.PropertyName = "height";
		jsonPropertyInfoValues6.JsonPropertyName = null;
		jsonPropertyInfoValues6.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("height", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainLong), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainLong> jsonPropertyInfoValues8 = jsonPropertyInfoValues6;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues9.IsProperty = true;
		jsonPropertyInfoValues9.IsPublic = true;
		jsonPropertyInfoValues9.IsVirtual = false;
		jsonPropertyInfoValues9.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues9.Converter = null;
		jsonPropertyInfoValues9.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).frameRate;
		jsonPropertyInfoValues9.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((BaseVideoTrackConstraints)obj).frameRate = value;
		};
		jsonPropertyInfoValues9.IgnoreCondition = null;
		jsonPropertyInfoValues9.HasJsonInclude = false;
		jsonPropertyInfoValues9.IsExtensionData = false;
		jsonPropertyInfoValues9.NumberHandling = null;
		jsonPropertyInfoValues9.PropertyName = "frameRate";
		jsonPropertyInfoValues9.JsonPropertyName = null;
		jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("frameRate", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		jsonPropertyInfoValues9 = new JsonPropertyInfoValues<ConstrainDouble>();
		jsonPropertyInfoValues9.IsProperty = true;
		jsonPropertyInfoValues9.IsPublic = true;
		jsonPropertyInfoValues9.IsVirtual = false;
		jsonPropertyInfoValues9.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues9.Converter = null;
		jsonPropertyInfoValues9.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).aspectRatio;
		jsonPropertyInfoValues9.Setter = delegate(object obj, ConstrainDouble? value)
		{
			((BaseVideoTrackConstraints)obj).aspectRatio = value;
		};
		jsonPropertyInfoValues9.IgnoreCondition = null;
		jsonPropertyInfoValues9.HasJsonInclude = false;
		jsonPropertyInfoValues9.IsExtensionData = false;
		jsonPropertyInfoValues9.NumberHandling = null;
		jsonPropertyInfoValues9.PropertyName = "aspectRatio";
		jsonPropertyInfoValues9.JsonPropertyName = null;
		jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("aspectRatio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainDouble), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainDouble> jsonPropertyInfoValues11 = jsonPropertyInfoValues9;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues11);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<ConstrainString>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(BaseVideoTrackConstraints);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((BaseVideoTrackConstraints)obj).resizeMode;
		jsonPropertyInfoValues.Setter = delegate(object obj, ConstrainString? value)
		{
			((BaseVideoTrackConstraints)obj).resizeMode = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "resizeMode";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(BaseVideoTrackConstraints).GetProperty("resizeMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ConstrainString), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<ConstrainString> jsonPropertyInfoValues12 = jsonPropertyInfoValues;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues12);
		return array;
	}

	private void VideoTrackConstraintsSerializeHandler(Utf8JsonWriter writer, VideoTrackConstraints? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_facingMode);
		ConstrainStringSerializeHandler(writer, value.facingMode);
		writer.WritePropertyName(PropName_backgroundBlur);
		ConstrainBooleanSerializeHandler(writer, value.backgroundBlur);
		writer.WritePropertyName(PropName_groupId);
		ConstrainStringSerializeHandler(writer, value.groupId);
		writer.WritePropertyName(PropName_width);
		ConstrainLongSerializeHandler(writer, value.width);
		writer.WritePropertyName(PropName_height);
		ConstrainLongSerializeHandler(writer, value.height);
		writer.WritePropertyName(PropName_frameRate);
		ConstrainDoubleSerializeHandler(writer, value.frameRate);
		writer.WritePropertyName(PropName_aspectRatio);
		ConstrainDoubleSerializeHandler(writer, value.aspectRatio);
		writer.WritePropertyName(PropName_resizeMode);
		ConstrainStringSerializeHandler(writer, value.resizeMode);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<WebRtcCodec> Create_WebRtcCodec(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<WebRtcCodec> jsonTypeInfo))
		{
			JsonObjectInfoValues<WebRtcCodec> jsonObjectInfoValues = new JsonObjectInfoValues<WebRtcCodec>
			{
				ObjectCreator = () => new WebRtcCodec(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => WebRtcCodecPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(WebRtcCodec).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = WebRtcCodecSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] WebRtcCodecPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[3];
		JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcCodec);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcCodec)obj).name;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((WebRtcCodec)obj).name = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "name";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcCodec).GetProperty("name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		array[0].IsGetNullable = false;
		array[0].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcCodec);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcCodec)obj).profileLevelId;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((WebRtcCodec)obj).profileLevelId = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "profileLevelId";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcCodec).GetProperty("profileLevelId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		JsonPropertyInfoValues<int?> jsonPropertyInfoValues4 = new JsonPropertyInfoValues<int?>();
		jsonPropertyInfoValues4.IsProperty = true;
		jsonPropertyInfoValues4.IsPublic = true;
		jsonPropertyInfoValues4.IsVirtual = false;
		jsonPropertyInfoValues4.DeclaringType = typeof(WebRtcCodec);
		jsonPropertyInfoValues4.Converter = null;
		jsonPropertyInfoValues4.Getter = (object obj) => ((WebRtcCodec)obj).packetizationMode;
		jsonPropertyInfoValues4.Setter = delegate(object obj, int? value)
		{
			((WebRtcCodec)obj).packetizationMode = value;
		};
		jsonPropertyInfoValues4.IgnoreCondition = null;
		jsonPropertyInfoValues4.HasJsonInclude = false;
		jsonPropertyInfoValues4.IsExtensionData = false;
		jsonPropertyInfoValues4.NumberHandling = null;
		jsonPropertyInfoValues4.PropertyName = "packetizationMode";
		jsonPropertyInfoValues4.JsonPropertyName = null;
		jsonPropertyInfoValues4.AttributeProviderFactory = () => typeof(WebRtcCodec).GetProperty("packetizationMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<int?> jsonPropertyInfoValues5 = jsonPropertyInfoValues4;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		return array;
	}

	private void WebRtcCodecSerializeHandler(Utf8JsonWriter writer, WebRtcCodec? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteString(PropName_name, value.name);
		writer.WriteString(PropName_profileLevelId, value.profileLevelId);
		writer.WritePropertyName(PropName_packetizationMode);
		JsonSerializer.Serialize(writer, value.packetizationMode, NullableInt32);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<WebRtcCodec[]> Create_WebRtcCodecArray(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<WebRtcCodec[]> jsonTypeInfo))
		{
			JsonCollectionInfoValues<WebRtcCodec[]> jsonCollectionInfoValues = new JsonCollectionInfoValues<WebRtcCodec[]>
			{
				ObjectCreator = null,
				SerializeHandler = WebRtcCodecArraySerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateArrayInfo(P_0, jsonCollectionInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private void WebRtcCodecArraySerializeHandler(Utf8JsonWriter writer, WebRtcCodec[]? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartArray();
		for (int i = 0; i < value.Length; i++)
		{
			WebRtcCodecSerializeHandler(writer, value[i]);
		}
		writer.WriteEndArray();
	}

	private JsonTypeInfo<WebRtcPermission> Create_WebRtcPermission(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<WebRtcPermission> jsonTypeInfo))
		{
			JsonObjectInfoValues<WebRtcPermission> jsonObjectInfoValues = new JsonObjectInfoValues<WebRtcPermission>
			{
				ObjectCreator = () => new WebRtcPermission(),
				ObjectWithParameterizedConstructorCreator = null,
				PropertyMetadataInitializer = (JsonSerializerContext _) => WebRtcPermissionPropInit(P_0),
				ConstructorParameterMetadataInitializer = null,
				ConstructorAttributeProviderFactory = () => typeof(WebRtcPermission).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null),
				SerializeHandler = WebRtcPermissionSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] WebRtcPermissionPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[22];
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelManage;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelManage = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelManage";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelManage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelManageApp;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelManageApp = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelManageApp";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelManageApp", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelView;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelView = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelView";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelView", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues4 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelUseExternalEmoji;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelUseExternalEmoji = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelUseExternalEmoji";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelUseExternalEmoji", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelCreateMessage;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelCreateMessage = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelCreateMessage";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelCreateMessage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues6 = jsonPropertyInfoValues;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues6);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelManageMessage;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelManageMessage = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelManageMessage";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelManageMessage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues7 = jsonPropertyInfoValues;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelManagePinnedMessages;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelManagePinnedMessages = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelManagePinnedMessages";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelManagePinnedMessages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues8 = jsonPropertyInfoValues;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelViewMessageHistory;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelViewMessageHistory = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelViewMessageHistory";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelViewMessageHistory", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues9 = jsonPropertyInfoValues;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelCreateMessageAttachment;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelCreateMessageAttachment = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelCreateMessageAttachment";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelCreateMessageAttachment", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues10 = jsonPropertyInfoValues;
		array[8] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelCreateMessageMention;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelCreateMessageMention = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelCreateMessageMention";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelCreateMessageMention", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues11 = jsonPropertyInfoValues;
		array[9] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues11);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelCreateMessageReaction;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelCreateMessageReaction = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelCreateMessageReaction";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelCreateMessageReaction", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues12 = jsonPropertyInfoValues;
		array[10] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues12);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelMakeMessagePublic;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelMakeMessagePublic = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelMakeMessagePublic";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelMakeMessagePublic", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = jsonPropertyInfoValues;
		array[11] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues13);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelMoveUserOther;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelMoveUserOther = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelMoveUserOther";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelMoveUserOther", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues;
		array[12] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues14);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelVoiceTalk;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelVoiceTalk = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelVoiceTalk";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelVoiceTalk", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues15 = jsonPropertyInfoValues;
		array[13] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues15);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelVoiceMuteOther;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelVoiceMuteOther = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelVoiceMuteOther";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelVoiceMuteOther", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues16 = jsonPropertyInfoValues;
		array[14] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues16);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelVoiceDeafenOther;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelVoiceDeafenOther = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelVoiceDeafenOther";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelVoiceDeafenOther", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues17 = jsonPropertyInfoValues;
		array[15] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues17);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelVoiceKick;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelVoiceKick = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelVoiceKick";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelVoiceKick", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues18 = jsonPropertyInfoValues;
		array[16] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues18);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelVideoStreamMedia;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelVideoStreamMedia = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelVideoStreamMedia";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelVideoStreamMedia", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues19 = jsonPropertyInfoValues;
		array[17] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues19);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelCreateFile;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelCreateFile = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelCreateFile";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelCreateFile", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues20 = jsonPropertyInfoValues;
		array[18] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues20);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelManageFiles;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelManageFiles = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelManageFiles";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelManageFiles", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues21 = jsonPropertyInfoValues;
		array[19] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues21);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelViewFile;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelViewFile = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelViewFile";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelViewFile", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues22 = jsonPropertyInfoValues;
		array[20] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues22);
		jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(WebRtcPermission);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((WebRtcPermission)obj).channelAppKick;
		jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
		{
			((WebRtcPermission)obj).channelAppKick = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "channelAppKick";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebRtcPermission).GetProperty("channelAppKick", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<bool> jsonPropertyInfoValues23 = jsonPropertyInfoValues;
		array[21] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues23);
		return array;
	}

	private void WebRtcPermissionSerializeHandler(Utf8JsonWriter writer, WebRtcPermission? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteBoolean(PropName_channelManage, value.channelManage);
		writer.WriteBoolean(PropName_channelManageApp, value.channelManageApp);
		writer.WriteBoolean(PropName_channelView, value.channelView);
		writer.WriteBoolean(PropName_channelUseExternalEmoji, value.channelUseExternalEmoji);
		writer.WriteBoolean(PropName_channelCreateMessage, value.channelCreateMessage);
		writer.WriteBoolean(PropName_channelManageMessage, value.channelManageMessage);
		writer.WriteBoolean(PropName_channelManagePinnedMessages, value.channelManagePinnedMessages);
		writer.WriteBoolean(PropName_channelViewMessageHistory, value.channelViewMessageHistory);
		writer.WriteBoolean(PropName_channelCreateMessageAttachment, value.channelCreateMessageAttachment);
		writer.WriteBoolean(PropName_channelCreateMessageMention, value.channelCreateMessageMention);
		writer.WriteBoolean(PropName_channelCreateMessageReaction, value.channelCreateMessageReaction);
		writer.WriteBoolean(PropName_channelMakeMessagePublic, value.channelMakeMessagePublic);
		writer.WriteBoolean(PropName_channelMoveUserOther, value.channelMoveUserOther);
		writer.WriteBoolean(PropName_channelVoiceTalk, value.channelVoiceTalk);
		writer.WriteBoolean(PropName_channelVoiceMuteOther, value.channelVoiceMuteOther);
		writer.WriteBoolean(PropName_channelVoiceDeafenOther, value.channelVoiceDeafenOther);
		writer.WriteBoolean(PropName_channelVoiceKick, value.channelVoiceKick);
		writer.WriteBoolean(PropName_channelVideoStreamMedia, value.channelVideoStreamMedia);
		writer.WriteBoolean(PropName_channelCreateFile, value.channelCreateFile);
		writer.WriteBoolean(PropName_channelManageFiles, value.channelManageFiles);
		writer.WriteBoolean(PropName_channelViewFile, value.channelViewFile);
		writer.WriteBoolean(PropName_channelAppKick, value.channelAppKick);
		writer.WriteEndObject();
	}

	private JsonTypeInfo<Dictionary<string, bool>> Create_DictionaryStringBoolean(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<Dictionary<string, bool>> jsonTypeInfo))
		{
			JsonCollectionInfoValues<Dictionary<string, bool>> jsonCollectionInfoValues = new JsonCollectionInfoValues<Dictionary<string, bool>>
			{
				ObjectCreator = () => new Dictionary<string, bool>(),
				SerializeHandler = DictionaryStringBooleanSerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateDictionaryInfo<Dictionary<string, bool>, string, bool>(P_0, jsonCollectionInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private void DictionaryStringBooleanSerializeHandler(Utf8JsonWriter writer, Dictionary<string, bool>? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		foreach (KeyValuePair<string, bool> item in value)
		{
			writer.WriteBoolean(item.Key, item.Value);
		}
		writer.WriteEndObject();
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

	private JsonTypeInfo<int?> Create_NullableInt32(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<int?> jsonTypeInfo))
		{
			JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<int>(P_0);
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int?>(P_0, nullableConverter);
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

	private JsonTypeInfo<long?> Create_NullableInt64(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<long?> jsonTypeInfo))
		{
			JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<long>(P_0);
			jsonTypeInfo = JsonMetadataServices.CreateValueInfo<long?>(P_0, nullableConverter);
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

	private JsonTypeInfo<string[]> Create_StringArray(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<string[]> jsonTypeInfo))
		{
			JsonCollectionInfoValues<string[]> jsonCollectionInfoValues = new JsonCollectionInfoValues<string[]>
			{
				ObjectCreator = null,
				SerializeHandler = StringArraySerializeHandler
			};
			jsonTypeInfo = JsonMetadataServices.CreateArrayInfo(P_0, jsonCollectionInfoValues);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private void StringArraySerializeHandler(Utf8JsonWriter writer, string[]? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartArray();
		for (int i = 0; i < value.Length; i++)
		{
			writer.WriteStringValue(value[i]);
		}
		writer.WriteEndArray();
	}

	public InitializeWebRtcPayloadJsonContext(JsonSerializerOptions P_0)
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

	private static JsonConverter? GetRuntimeConverterForType(Type P_0, JsonSerializerOptions P_1)
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

	private static JsonConverter ExpandConverter(Type P_0, JsonConverter P_1, JsonSerializerOptions P_2, bool P_3 = true)
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

	public override JsonTypeInfo? GetTypeInfo(Type P_0)
	{
		base.Options.TryGetTypeInfo(P_0, out JsonTypeInfo result);
		return result;
	}

	JsonTypeInfo? IJsonTypeInfoResolver.GetTypeInfo(Type P_0, JsonSerializerOptions P_1)
	{
		if (P_0 == typeof(bool))
		{
			return Create_Boolean(P_1);
		}
		if (P_0 == typeof(bool?))
		{
			return Create_NullableBoolean(P_1);
		}
		if (P_0 == typeof(char))
		{
			return Create_Char(P_1);
		}
		if (P_0 == typeof(char?))
		{
			return Create_NullableChar(P_1);
		}
		if (P_0 == typeof(double))
		{
			return Create_Double(P_1);
		}
		if (P_0 == typeof(double?))
		{
			return Create_NullableDouble(P_1);
		}
		if (P_0 == typeof(AudioTrackConstraints))
		{
			return Create_AudioTrackConstraints(P_1);
		}
		if (P_0 == typeof(ConstrainBoolean))
		{
			return Create_ConstrainBoolean(P_1);
		}
		if (P_0 == typeof(ConstrainDouble))
		{
			return Create_ConstrainDouble(P_1);
		}
		if (P_0 == typeof(ConstrainLong))
		{
			return Create_ConstrainLong(P_1);
		}
		if (P_0 == typeof(ConstrainString))
		{
			return Create_ConstrainString(P_1);
		}
		if (P_0 == typeof(DisplayMediaStreamConstraints))
		{
			return Create_DisplayMediaStreamConstraints(P_1);
		}
		if (P_0 == typeof(InitializeWebRtcPayload))
		{
			return Create_InitializeWebRtcPayload(P_1);
		}
		if (P_0 == typeof(ScreenTrackConstraints))
		{
			return Create_ScreenTrackConstraints(P_1);
		}
		if (P_0 == typeof(UserMediaStreamConstraints))
		{
			return Create_UserMediaStreamConstraints(P_1);
		}
		if (P_0 == typeof(UserTileVolume))
		{
			return Create_UserTileVolume(P_1);
		}
		if (P_0 == typeof(UserTileVolume[]))
		{
			return Create_UserTileVolumeArray(P_1);
		}
		if (P_0 == typeof(VideoTrackConstraints))
		{
			return Create_VideoTrackConstraints(P_1);
		}
		if (P_0 == typeof(WebRtcCodec))
		{
			return Create_WebRtcCodec(P_1);
		}
		if (P_0 == typeof(WebRtcCodec[]))
		{
			return Create_WebRtcCodecArray(P_1);
		}
		if (P_0 == typeof(WebRtcPermission))
		{
			return Create_WebRtcPermission(P_1);
		}
		if (P_0 == typeof(Dictionary<string, bool>))
		{
			return Create_DictionaryStringBoolean(P_1);
		}
		if (P_0 == typeof(int))
		{
			return Create_Int32(P_1);
		}
		if (P_0 == typeof(int?))
		{
			return Create_NullableInt32(P_1);
		}
		if (P_0 == typeof(long))
		{
			return Create_Int64(P_1);
		}
		if (P_0 == typeof(long?))
		{
			return Create_NullableInt64(P_1);
		}
		if (P_0 == typeof(string))
		{
			return Create_String(P_1);
		}
		if (P_0 == typeof(string[]))
		{
			return Create_StringArray(P_1);
		}
		return null;
	}
}
