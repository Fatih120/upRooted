// RootApp.Client.Domain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Domain.Helpers.Store.LocalDataStoreExtensions
using System;
using System.CodeDom.Compiler;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using RootApp.Client.Domain.Helpers.Store;

public static class LocalDataStoreExtensions
{
	[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
	[JsonSerializable(typeof(int))]
	[JsonSerializable(typeof(long))]
	[JsonSerializable(typeof(double))]
	[JsonSerializable(typeof(string))]
	[GeneratedCode("System.Text.Json.SourceGeneration", "10.0.14.7603")]
	public class SerializerContext : JsonSerializerContext, IJsonTypeInfoResolver
	{
		private JsonTypeInfo<double>? _Double;

		private JsonTypeInfo<int>? _Int32;

		private JsonTypeInfo<long>? _Int64;

		private JsonTypeInfo<string>? _String;

		private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

		public JsonTypeInfo<double> Double => _Double ?? (_Double = (JsonTypeInfo<double>)base.Options.GetTypeInfo(typeof(double)));

		public JsonTypeInfo<int> Int32 => _Int32 ?? (_Int32 = (JsonTypeInfo<int>)base.Options.GetTypeInfo(typeof(int)));

		public JsonTypeInfo<long> Int64 => _Int64 ?? (_Int64 = (JsonTypeInfo<long>)base.Options.GetTypeInfo(typeof(long)));

		public JsonTypeInfo<string> String => _String ?? (_String = (JsonTypeInfo<string>)base.Options.GetTypeInfo(typeof(string)));

		public static SerializerContext Default { get; } = new SerializerContext(new JsonSerializerOptions(s_defaultOptions));

		protected override JsonSerializerOptions? GeneratedSerializerOptions { get; } = s_defaultOptions;

		private JsonTypeInfo<double> Create_Double(JsonSerializerOptions P_0)
		{
			if (!TryGetTypeInfoForRuntimeCustomConverter(P_0, out JsonTypeInfo<double> jsonTypeInfo))
			{
				jsonTypeInfo = JsonMetadataServices.CreateValueInfo<double>(P_0, JsonMetadataServices.DoubleConverter);
			}
			jsonTypeInfo.OriginatingResolver = this;
			return jsonTypeInfo;
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

		public SerializerContext(JsonSerializerOptions P_0)
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
			if (P_0 == typeof(double))
			{
				return Create_Double(P_1);
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
			return null;
		}
	}

	public static void SetGlobal<T>(this ILocalDataStore P_0, DataStoreKeys P_1, T P_2, JsonTypeInfo<T> P_3)
	{
		P_0.Set(new ReadOnlySpan<string>(P_1.ToString()), P_2, P_3);
	}

	public static void SetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, int P_2)
	{
		P_0.Set(new ReadOnlySpan<string>(P_1.ToString()), P_2, SerializerContext.Default.Int32);
	}

	public static void SetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, double P_2)
	{
		P_0.Set(new ReadOnlySpan<string>(P_1.ToString()), P_2, SerializerContext.Default.Double);
	}

	public static void SetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, string? P_2)
	{
		P_0.Set(new ReadOnlySpan<string>(P_1.ToString()), P_2, SerializerContext.Default.String);
	}

	public static bool TryGetGlobal<T>(this ILocalDataStore P_0, DataStoreKeys P_1, out T P_2, JsonTypeInfo<T> P_3)
	{
		return P_0.TryGet<T>(new ReadOnlySpan<string>(P_1.ToString()), out P_2, P_3);
	}

	public static bool TryGetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, out int P_2)
	{
		return P_0.TryGet(new ReadOnlySpan<string>(P_1.ToString()), out P_2, SerializerContext.Default.Int32);
	}

	public static bool TryGetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, out double P_2)
	{
		return P_0.TryGet(new ReadOnlySpan<string>(P_1.ToString()), out P_2, SerializerContext.Default.Double);
	}

	public static bool TryGetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, out long P_2)
	{
		return P_0.TryGet(new ReadOnlySpan<string>(P_1.ToString()), out P_2, SerializerContext.Default.Int64);
	}

	public static bool TryGetGlobal(this ILocalDataStore P_0, DataStoreKeys P_1, out string? P_2)
	{
		return P_0.TryGet(new ReadOnlySpan<string>(P_1.ToString()), out P_2, SerializerContext.Default.String);
	}

	public static void SetWithPath<T>(this ILocalDataStore P_0, ReadOnlySpan<string> P_1, T P_2, JsonTypeInfo<T> P_3)
	{
		P_0.Set(P_1, P_2, P_3);
	}

	public static void SetWithPath(this ILocalDataStore P_0, ReadOnlySpan<string> P_1, int P_2)
	{
		P_0.Set(P_1, P_2, SerializerContext.Default.Int32);
	}

	public static void SetWithPath(this ILocalDataStore P_0, ReadOnlySpan<string> P_1, double P_2)
	{
		P_0.Set(P_1, P_2, SerializerContext.Default.Double);
	}

	public static bool TryGetWithPath<T>(this ILocalDataStore P_0, ReadOnlySpan<string> P_1, out T P_2, JsonTypeInfo<T> P_3)
	{
		return P_0.TryGet<T>(P_1, out P_2, P_3);
	}

	public static bool TryGetWithPath(this ILocalDataStore P_0, ReadOnlySpan<string> P_1, out int P_2)
	{
		return P_0.TryGet(P_1, out P_2, SerializerContext.Default.Int32);
	}

	public static bool TryGetWithPath(this ILocalDataStore P_0, ReadOnlySpan<string> P_1, out double P_2)
	{
		return P_0.TryGet(P_1, out P_2, SerializerContext.Default.Double);
	}
}

