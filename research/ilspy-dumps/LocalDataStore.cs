// RootApp.Client.Domain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Domain.Helpers.Store.LocalDataStore
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Logging;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;
using RootApp.Client.Domain.Helpers.Store;

public sealed class LocalDataStore : ILocalDataStore
{
	private static readonly byte[] _obfuscationKey = new byte[32]
	{
		138, 79, 46, 29, 156, 123, 106, 95, 62, 45,
		28, 11, 154, 143, 126, 109, 92, 75, 58, 41,
		24, 247, 230, 213, 196, 179, 162, 145, 128, 127,
		110, 93
	};

	private static readonly JsonWriterOptions _jsonWriterOptions = new JsonWriterOptions
	{
		Indented = false
	};

	private readonly JsonNode _data;

	private readonly string _filePath;

	private readonly string _tempFilePath;

	private readonly ILogger<LocalDataStore> _logger;

	private readonly object _sync = new object();

	public LocalDataStore(IRootApplicationDirectories P_0, ILogger<LocalDataStore> P_1)
	{
		_logger = P_1;
		_filePath = Path.GetFullPath(Path.Combine(P_0.ApplicationDirectory.FullName, "data.json"));
		_tempFilePath = Path.GetFullPath(Path.Combine(P_0.ApplicationDirectory.FullName, "data.tmp.json"));
		JsonNode jsonNode = null;
		bool flag = false;
		if (File.Exists(_filePath))
		{
			(jsonNode, flag) = LoadData();
		}
		try
		{
			if (File.Exists(_tempFilePath))
			{
				File.Delete(_tempFilePath);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unable to cleanup temp file {path}", _tempFilePath);
		}
		_data = jsonNode ?? new JsonObject();
		if (flag)
		{
			lock (_sync)
			{
				saveToFile();
			}
		}
	}

	private (JsonNode? data, bool needsMigration) LoadData()
	{
		try
		{
			byte[] array = File.ReadAllBytes(_filePath);
			if (array.Length == 0)
			{
				return (data: null, needsMigration: false);
			}
			if (IsLikelyPlainJson(array))
			{
				_logger.LogInformation("Migrating plain JSON data store to obfuscated format");
				return (data: JsonNode.Parse(array), needsMigration: true);
			}
			XorTransform(array);
			return (data: JsonNode.Parse(array), needsMigration: false);
		}
		catch (JsonException ex)
		{
			_logger.LogError(ex, "Unable to parse JSON from {path}", _filePath);
			QuarantineInvalidFile();
		}
		catch (Exception ex2)
		{
			_logger.LogError(ex2, "Unexpected error loading data from {path}", _filePath);
		}
		return (data: null, needsMigration: false);
	}

	private static bool IsLikelyPlainJson(byte[] P_0)
	{
		foreach (byte b in P_0)
		{
			if (b != 32 && b != 9 && b != 10 && b != 13)
			{
				return b == 123;
			}
		}
		return false;
	}

	private static void XorTransform(Span<byte> P_0)
	{
		byte[] obfuscationKey = _obfuscationKey;
		int num = obfuscationKey.Length;
		for (int i = 0; i < P_0.Length; i++)
		{
			P_0[i] ^= obfuscationKey[i % num];
		}
	}

	private void QuarantineInvalidFile()
	{
		try
		{
			byte[] array = File.ReadAllBytes(_filePath);
			int num = Math.Min(array.Length, 16);
			string text = BitConverter.ToString(array, 0, num);
			_logger.LogError("Corrupt data file {path} first {length} bytes: {preview}", _filePath, num, text);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to read corrupt data bytes for {path}", _filePath);
		}
		try
		{
			string text2 = Path.GetDirectoryName(_filePath) ?? string.Empty;
			string fileName = Path.GetFileName(_filePath);
			string text3 = Path.Combine(text2, "invalid-" + fileName);
			File.Move(_filePath, text3, true);
		}
		catch (Exception ex2)
		{
			_logger.LogError(ex2, "Unable to move invalid data file {path}", _filePath);
		}
	}

	public void Set<T>(ReadOnlySpan<string> P_0, T P_1, JsonTypeInfo<T> P_2)
	{
		if (P_0.Length == 0)
		{
			throw new ArgumentException("Path must have at least one segment.", "path");
		}
		lock (_sync)
		{
			JsonObject jsonObject = ensureParentObject(P_0.Slice(0, P_0.Length - 1));
			string text = P_0[P_0.Length - 1];
			jsonObject[text] = JsonSerializer.SerializeToNode(P_1, P_2);
			saveToFile();
		}
	}

	public bool TryGet<T>(ReadOnlySpan<string> P_0, [NotNullWhen(true)] out T? P_1, JsonTypeInfo<T> P_2)
	{
		P_1 = default(T);
		if (P_0.Length == 0)
		{
			return false;
		}
		lock (_sync)
		{
			JsonNode nodeAtPath = getNodeAtPath(P_0.Slice(0, P_0.Length - 1));
			if (!(nodeAtPath is JsonObject jsonObject))
			{
				return false;
			}
			string text = P_0[P_0.Length - 1];
			if (!jsonObject.TryGetPropertyValue(text, out JsonNode jsonNode) || jsonNode == null)
			{
				return false;
			}
			try
			{
				P_1 = jsonNode.Deserialize(P_2);
			}
			catch (JsonException ex)
			{
				_logger.LogError(ex, "Error deserializing JSON at path: {Path}", string.Join("/", P_0.ToArray()));
				return false;
			}
			return P_1 != null;
		}
	}

	private void saveToFile()
	{
		try
		{
			using MemoryStream memoryStream = new MemoryStream();
			using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, _jsonWriterOptions))
			{
				_data.WriteTo(utf8JsonWriter);
			}
			byte[] array = memoryStream.ToArray();
			XorTransform(array);
			File.WriteAllBytes(_tempFilePath, array);
			File.Move(_tempFilePath, _filePath, true);
			return;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unable to save {path}", _filePath);
		}
		try
		{
			if (File.Exists(_tempFilePath))
			{
				File.Delete(_tempFilePath);
			}
		}
		catch (Exception ex2)
		{
			_logger.LogError(ex2, "Unable to cleanup temp file {path}", _tempFilePath);
		}
	}

	private JsonObject ensureParentObject(ReadOnlySpan<string> P_0)
	{
		if (!(_data is JsonObject jsonObject))
		{
			throw new InvalidOperationException("Root JSON node must be a JsonObject.");
		}
		JsonObject jsonObject2 = jsonObject;
		ReadOnlySpan<string> readOnlySpan = P_0;
		JsonObject jsonObject3;
		for (int i = 0; i < readOnlySpan.Length; jsonObject2 = jsonObject3, i++)
		{
			string text = readOnlySpan[i];
			if (jsonObject2.TryGetPropertyValue(text, out JsonNode jsonNode))
			{
				jsonObject3 = jsonNode as JsonObject;
				if (jsonObject3 != null)
				{
					continue;
				}
			}
			jsonObject3 = (JsonObject)(jsonObject2[text] = new JsonObject());
		}
		return jsonObject2;
	}

	private JsonNode? getNodeAtPath(ReadOnlySpan<string> P_0)
	{
		JsonNode jsonNode = _data;
		ReadOnlySpan<string> readOnlySpan = P_0;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			string text = readOnlySpan[i];
			if (!(jsonNode is JsonObject jsonObject))
			{
				return null;
			}
			if (!jsonObject.TryGetPropertyValue(text, out JsonNode jsonNode2) || jsonNode2 == null)
			{
				return null;
			}
			jsonNode = jsonNode2;
		}
		return jsonNode;
	}
}

