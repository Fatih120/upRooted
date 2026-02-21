using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace RootApp.Browser.RootApps.Models;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(RootThemeCss))]
[GeneratedCode("System.Text.Json.SourceGeneration", "10.0.14.7603")]
public class RootThemeCssJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
{
	private JsonTypeInfo<RootThemeCss>? _RootThemeCss;

	private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

	private static readonly JsonEncodedText PropName_themeBase = JsonEncodedText.Encode("themeBase");

	private static readonly JsonEncodedText PropName_brandPrimary = JsonEncodedText.Encode("brandPrimary");

	private static readonly JsonEncodedText PropName_brandSecondary = JsonEncodedText.Encode("brandSecondary");

	private static readonly JsonEncodedText PropName_brandTertiary = JsonEncodedText.Encode("brandTertiary");

	private static readonly JsonEncodedText PropName_textPrimary = JsonEncodedText.Encode("textPrimary");

	private static readonly JsonEncodedText PropName_textSecondary = JsonEncodedText.Encode("textSecondary");

	private static readonly JsonEncodedText PropName_textTertiary = JsonEncodedText.Encode("textTertiary");

	private static readonly JsonEncodedText PropName_textWhite = JsonEncodedText.Encode("textWhite");

	private static readonly JsonEncodedText PropName_backgroundPrimary = JsonEncodedText.Encode("backgroundPrimary");

	private static readonly JsonEncodedText PropName_backgroundSecondary = JsonEncodedText.Encode("backgroundSecondary");

	private static readonly JsonEncodedText PropName_backgroundTertiary = JsonEncodedText.Encode("backgroundTertiary");

	private static readonly JsonEncodedText PropName_input = JsonEncodedText.Encode("input");

	private static readonly JsonEncodedText PropName_border = JsonEncodedText.Encode("border");

	private static readonly JsonEncodedText PropName_highlightLight = JsonEncodedText.Encode("highlightLight");

	private static readonly JsonEncodedText PropName_highlightNormal = JsonEncodedText.Encode("highlightNormal");

	private static readonly JsonEncodedText PropName_highlightStrong = JsonEncodedText.Encode("highlightStrong");

	private static readonly JsonEncodedText PropName_info = JsonEncodedText.Encode("info");

	private static readonly JsonEncodedText PropName_warning = JsonEncodedText.Encode("warning");

	private static readonly JsonEncodedText PropName_error = JsonEncodedText.Encode("error");

	private static readonly JsonEncodedText PropName_muted = JsonEncodedText.Encode("muted");

	private static readonly JsonEncodedText PropName_link = JsonEncodedText.Encode("link");

	public JsonTypeInfo<RootThemeCss> RootThemeCss => _RootThemeCss ?? (_RootThemeCss = (JsonTypeInfo<RootThemeCss>)base.Options.GetTypeInfo(typeof(RootThemeCss)));

	public static RootThemeCssJsonContext Default { get; } = new RootThemeCssJsonContext(new JsonSerializerOptions(s_defaultOptions));

	protected override JsonSerializerOptions? GeneratedSerializerOptions { get; } = s_defaultOptions;

	private JsonTypeInfo<RootThemeCss> Create_RootThemeCss(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<RootThemeCss> jsonTypeInfo))
		{
			JsonObjectInfoValues<RootThemeCss> jsonObjectInfoValues = new JsonObjectInfoValues<RootThemeCss>();
			jsonObjectInfoValues.ObjectCreator = null;
			jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new RootThemeCss
			{
				themeBase = (string)args[0],
				brandPrimary = (string)args[1],
				brandSecondary = (string)args[2],
				brandTertiary = (string)args[3],
				textPrimary = (string)args[4],
				textSecondary = (string)args[5],
				textTertiary = (string)args[6],
				textWhite = (string)args[7],
				backgroundPrimary = (string)args[8],
				backgroundSecondary = (string)args[9],
				backgroundTertiary = (string)args[10],
				input = (string)args[11],
				border = (string)args[12],
				highlightLight = (string)args[13],
				highlightNormal = (string)args[14],
				highlightStrong = (string)args[15],
				info = (string)args[16],
				warning = (string)args[17],
				error = (string)args[18],
				muted = (string)args[19],
				link = (string)args[20]
			};
			jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => RootThemeCssPropInit(P_0);
			jsonObjectInfoValues.ConstructorParameterMetadataInitializer = RootThemeCssCtorParamInit;
			jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(RootThemeCss).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
			jsonObjectInfoValues.SerializeHandler = RootThemeCssSerializeHandler;
			JsonObjectInfoValues<RootThemeCss> jsonObjectInfoValues2 = jsonObjectInfoValues;
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues2);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] RootThemeCssPropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[21];
		JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).themeBase;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).themeBase = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "themeBase";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("themeBase", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		array[0].IsRequired = true;
		array[0].IsGetNullable = false;
		array[0].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).brandPrimary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).brandPrimary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "brandPrimary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("brandPrimary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = jsonPropertyInfoValues;
		array[1] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues3);
		array[1].IsRequired = true;
		array[1].IsGetNullable = false;
		array[1].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).brandSecondary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).brandSecondary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "brandSecondary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("brandSecondary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues;
		array[2] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues4);
		array[2].IsRequired = true;
		array[2].IsGetNullable = false;
		array[2].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).brandTertiary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).brandTertiary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "brandTertiary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("brandTertiary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = jsonPropertyInfoValues;
		array[3] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues5);
		array[3].IsRequired = true;
		array[3].IsGetNullable = false;
		array[3].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).textPrimary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).textPrimary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "textPrimary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("textPrimary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues;
		array[4] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues6);
		array[4].IsRequired = true;
		array[4].IsGetNullable = false;
		array[4].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).textSecondary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).textSecondary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "textSecondary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("textSecondary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = jsonPropertyInfoValues;
		array[5] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues7);
		array[5].IsRequired = true;
		array[5].IsGetNullable = false;
		array[5].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).textTertiary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).textTertiary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "textTertiary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("textTertiary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues;
		array[6] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues8);
		array[6].IsRequired = true;
		array[6].IsGetNullable = false;
		array[6].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).textWhite;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).textWhite = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "textWhite";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("textWhite", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = jsonPropertyInfoValues;
		array[7] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues9);
		array[7].IsRequired = true;
		array[7].IsGetNullable = false;
		array[7].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).backgroundPrimary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).backgroundPrimary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "backgroundPrimary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("backgroundPrimary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues;
		array[8] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues10);
		array[8].IsRequired = true;
		array[8].IsGetNullable = false;
		array[8].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).backgroundSecondary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).backgroundSecondary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "backgroundSecondary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("backgroundSecondary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = jsonPropertyInfoValues;
		array[9] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues11);
		array[9].IsRequired = true;
		array[9].IsGetNullable = false;
		array[9].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).backgroundTertiary;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).backgroundTertiary = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "backgroundTertiary";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("backgroundTertiary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues;
		array[10] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues12);
		array[10].IsRequired = true;
		array[10].IsGetNullable = false;
		array[10].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).input;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).input = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "input";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("input", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = jsonPropertyInfoValues;
		array[11] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues13);
		array[11].IsRequired = true;
		array[11].IsGetNullable = false;
		array[11].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).border;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).border = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "border";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("border", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues;
		array[12] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues14);
		array[12].IsRequired = true;
		array[12].IsGetNullable = false;
		array[12].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).highlightLight;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).highlightLight = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "highlightLight";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("highlightLight", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = jsonPropertyInfoValues;
		array[13] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues15);
		array[13].IsRequired = true;
		array[13].IsGetNullable = false;
		array[13].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).highlightNormal;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).highlightNormal = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "highlightNormal";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("highlightNormal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues;
		array[14] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues16);
		array[14].IsRequired = true;
		array[14].IsGetNullable = false;
		array[14].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).highlightStrong;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).highlightStrong = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "highlightStrong";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("highlightStrong", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues17 = jsonPropertyInfoValues;
		array[15] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues17);
		array[15].IsRequired = true;
		array[15].IsGetNullable = false;
		array[15].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).info;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).info = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "info";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("info", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues18 = jsonPropertyInfoValues;
		array[16] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues18);
		array[16].IsRequired = true;
		array[16].IsGetNullable = false;
		array[16].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).warning;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).warning = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "warning";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("warning", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues19 = jsonPropertyInfoValues;
		array[17] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues19);
		array[17].IsRequired = true;
		array[17].IsGetNullable = false;
		array[17].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).error;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).error = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "error";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("error", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues20 = jsonPropertyInfoValues;
		array[18] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues20);
		array[18].IsRequired = true;
		array[18].IsGetNullable = false;
		array[18].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).muted;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).muted = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "muted";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("muted", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues21 = jsonPropertyInfoValues;
		array[19] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues21);
		array[19].IsRequired = true;
		array[19].IsGetNullable = false;
		array[19].IsSetNullable = false;
		jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(RootThemeCss);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((RootThemeCss)obj).link;
		jsonPropertyInfoValues.Setter = delegate(object obj, string? value)
		{
			((RootThemeCss)obj).link = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "link";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RootThemeCss).GetProperty("link", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string> jsonPropertyInfoValues22 = jsonPropertyInfoValues;
		array[20] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues22);
		array[20].IsRequired = true;
		array[20].IsGetNullable = false;
		array[20].IsSetNullable = false;
		return array;
	}

	private void RootThemeCssSerializeHandler(Utf8JsonWriter writer, RootThemeCss? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WriteString(PropName_themeBase, value.themeBase);
		writer.WriteString(PropName_brandPrimary, value.brandPrimary);
		writer.WriteString(PropName_brandSecondary, value.brandSecondary);
		writer.WriteString(PropName_brandTertiary, value.brandTertiary);
		writer.WriteString(PropName_textPrimary, value.textPrimary);
		writer.WriteString(PropName_textSecondary, value.textSecondary);
		writer.WriteString(PropName_textTertiary, value.textTertiary);
		writer.WriteString(PropName_textWhite, value.textWhite);
		writer.WriteString(PropName_backgroundPrimary, value.backgroundPrimary);
		writer.WriteString(PropName_backgroundSecondary, value.backgroundSecondary);
		writer.WriteString(PropName_backgroundTertiary, value.backgroundTertiary);
		writer.WriteString(PropName_input, value.input);
		writer.WriteString(PropName_border, value.border);
		writer.WriteString(PropName_highlightLight, value.highlightLight);
		writer.WriteString(PropName_highlightNormal, value.highlightNormal);
		writer.WriteString(PropName_highlightStrong, value.highlightStrong);
		writer.WriteString(PropName_info, value.info);
		writer.WriteString(PropName_warning, value.warning);
		writer.WriteString(PropName_error, value.error);
		writer.WriteString(PropName_muted, value.muted);
		writer.WriteString(PropName_link, value.link);
		writer.WriteEndObject();
	}

	private static JsonParameterInfoValues[] RootThemeCssCtorParamInit()
	{
		return new JsonParameterInfoValues[21]
		{
			new JsonParameterInfoValues
			{
				Name = "themeBase",
				ParameterType = typeof(string),
				Position = 0,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "brandPrimary",
				ParameterType = typeof(string),
				Position = 1,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "brandSecondary",
				ParameterType = typeof(string),
				Position = 2,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "brandTertiary",
				ParameterType = typeof(string),
				Position = 3,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "textPrimary",
				ParameterType = typeof(string),
				Position = 4,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "textSecondary",
				ParameterType = typeof(string),
				Position = 5,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "textTertiary",
				ParameterType = typeof(string),
				Position = 6,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "textWhite",
				ParameterType = typeof(string),
				Position = 7,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "backgroundPrimary",
				ParameterType = typeof(string),
				Position = 8,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "backgroundSecondary",
				ParameterType = typeof(string),
				Position = 9,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "backgroundTertiary",
				ParameterType = typeof(string),
				Position = 10,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "input",
				ParameterType = typeof(string),
				Position = 11,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "border",
				ParameterType = typeof(string),
				Position = 12,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "highlightLight",
				ParameterType = typeof(string),
				Position = 13,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "highlightNormal",
				ParameterType = typeof(string),
				Position = 14,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "highlightStrong",
				ParameterType = typeof(string),
				Position = 15,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "info",
				ParameterType = typeof(string),
				Position = 16,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "warning",
				ParameterType = typeof(string),
				Position = 17,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "error",
				ParameterType = typeof(string),
				Position = 18,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "muted",
				ParameterType = typeof(string),
				Position = 19,
				IsNullable = false,
				IsMemberInitializer = true
			},
			new JsonParameterInfoValues
			{
				Name = "link",
				ParameterType = typeof(string),
				Position = 20,
				IsNullable = false,
				IsMemberInitializer = true
			}
		};
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

	public RootThemeCssJsonContext(JsonSerializerOptions P_0)
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
		if (P_0 == typeof(RootThemeCss))
		{
			return Create_RootThemeCss(P_1);
		}
		if (P_0 == typeof(string))
		{
			return Create_String(P_1);
		}
		return null;
	}
}
