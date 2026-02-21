using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace RootApp.Browser.RootApps.Models;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(FileUploadResponse))]
[GeneratedCode("System.Text.Json.SourceGeneration", "10.0.14.7603")]
public class FileUploadResponseJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
{
	private JsonTypeInfo<FileUploadResponse>? _FileUploadResponse;

	private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

	private static readonly JsonEncodedText PropName_tokens = JsonEncodedText.Encode("tokens");

	public JsonTypeInfo<FileUploadResponse> FileUploadResponse => _FileUploadResponse ?? (_FileUploadResponse = (JsonTypeInfo<FileUploadResponse>)base.Options.GetTypeInfo(typeof(FileUploadResponse)));

	public static FileUploadResponseJsonContext Default { get; } = new FileUploadResponseJsonContext(new JsonSerializerOptions(s_defaultOptions));

	protected override JsonSerializerOptions? GeneratedSerializerOptions { get; } = s_defaultOptions;

	private JsonTypeInfo<FileUploadResponse> Create_FileUploadResponse(JsonSerializerOptions P_0)
	{
		if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<FileUploadResponse> jsonTypeInfo))
		{
			JsonObjectInfoValues<FileUploadResponse> jsonObjectInfoValues = new JsonObjectInfoValues<FileUploadResponse>();
			jsonObjectInfoValues.ObjectCreator = null;
			jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new FileUploadResponse
			{
				tokens = (string[])args[0]
			};
			jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => FileUploadResponsePropInit(P_0);
			jsonObjectInfoValues.ConstructorParameterMetadataInitializer = FileUploadResponseCtorParamInit;
			jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(FileUploadResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
			jsonObjectInfoValues.SerializeHandler = FileUploadResponseSerializeHandler;
			JsonObjectInfoValues<FileUploadResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
			jsonTypeInfo = JsonMetadataServices.CreateObjectInfo(P_0, jsonObjectInfoValues2);
			jsonTypeInfo.NumberHandling = null;
		}
		jsonTypeInfo.OriginatingResolver = this;
		return jsonTypeInfo;
	}

	private static JsonPropertyInfo[] FileUploadResponsePropInit(JsonSerializerOptions P_0)
	{
		JsonPropertyInfo[] array = new JsonPropertyInfo[1];
		JsonPropertyInfoValues<string[]> jsonPropertyInfoValues = new JsonPropertyInfoValues<string[]>();
		jsonPropertyInfoValues.IsProperty = true;
		jsonPropertyInfoValues.IsPublic = true;
		jsonPropertyInfoValues.IsVirtual = false;
		jsonPropertyInfoValues.DeclaringType = typeof(FileUploadResponse);
		jsonPropertyInfoValues.Converter = null;
		jsonPropertyInfoValues.Getter = (object obj) => ((FileUploadResponse)obj).tokens;
		jsonPropertyInfoValues.Setter = delegate(object obj, string[]? value)
		{
			((FileUploadResponse)obj).tokens = value;
		};
		jsonPropertyInfoValues.IgnoreCondition = null;
		jsonPropertyInfoValues.HasJsonInclude = false;
		jsonPropertyInfoValues.IsExtensionData = false;
		jsonPropertyInfoValues.NumberHandling = null;
		jsonPropertyInfoValues.PropertyName = "tokens";
		jsonPropertyInfoValues.JsonPropertyName = null;
		jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(FileUploadResponse).GetProperty("tokens", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string[]), Array.Empty<Type>(), null);
		JsonPropertyInfoValues<string[]> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
		array[0] = JsonMetadataServices.CreatePropertyInfo(P_0, jsonPropertyInfoValues2);
		array[0].IsRequired = true;
		array[0].IsGetNullable = false;
		array[0].IsSetNullable = false;
		return array;
	}

	private void FileUploadResponseSerializeHandler(Utf8JsonWriter writer, FileUploadResponse? value)
	{
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName(PropName_tokens);
		StringArraySerializeHandler(writer, value.tokens);
		writer.WriteEndObject();
	}

	private static JsonParameterInfoValues[] FileUploadResponseCtorParamInit()
	{
		return new JsonParameterInfoValues[1]
		{
			new JsonParameterInfoValues
			{
				Name = "tokens",
				ParameterType = typeof(string[]),
				Position = 0,
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

	public FileUploadResponseJsonContext(JsonSerializerOptions P_0)
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
		if (P_0 == typeof(FileUploadResponse))
		{
			return Create_FileUploadResponse(P_1);
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
