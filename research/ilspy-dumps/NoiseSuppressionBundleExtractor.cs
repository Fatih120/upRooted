using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;

namespace RootApp.Browser.WebRtc;

public class NoiseSuppressionBundleExtractor(IRootApplicationDirectories)
{
	public string? CachedTempFolder;

	public async Task EnsureExtractedAsync()
	{
		Uri assetBaseUri = new Uri("avares://RootApp.Client.Avalonia/DotNetBrowser/WebRtc/SuppressionBundle/assets/");
		List<string> assetFiles = new List<string> { "atsvb-web.js", "ort-wasm.wasm", "ort-wasm-simd.wasm", "models/audio-model-1.0.1.tsvb", "models/audio-model-2.5.0.tsvb", "models/audio-model-3.3.2.wasm" };
		await extractAssetBundleToTempFolderAsync(assetBaseUri, assetFiles);
	}

	private async Task<string> extractAssetBundleToTempFolderAsync(Uri P_0, IEnumerable<string> P_1)
	{
		if (!string.IsNullOrEmpty(CachedTempFolder) && Directory.Exists(CachedTempFolder))
		{
			return CachedTempFolder;
		}
		CachedTempFolder = Path.Combine(P_0.LocalDirectory.FullName, "WebRtcSuppressionBundle");
		if (Directory.Exists(CachedTempFolder))
		{
			Directory.Delete(CachedTempFolder, true);
		}
		Directory.CreateDirectory(CachedTempFolder);
		foreach (string relativePath in P_1)
		{
			Uri assetUri = new Uri(P_0, relativePath);
			try
			{
				await using Stream assetStream = AssetLoader.Open(assetUri);
				string targetFilePath = Path.Combine(CachedTempFolder, relativePath.Replace('/', Path.DirectorySeparatorChar));
				Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
				await using FileStream fileStream = File.Create(targetFilePath);
				await assetStream.CopyToAsync(fileStream);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to extract asset {assetUri}", ex);
			}
		}
		return CachedTempFolder;
	}
}
