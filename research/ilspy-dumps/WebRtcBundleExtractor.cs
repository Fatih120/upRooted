using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Logging;
using Avalonia.Platform;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;

namespace RootApp.Browser.WebRtc;

public class WebRtcBundleExtractor(IRootApplicationDirectories)
{
	private string? _cachedTempFolder;

	public async Task<string> GetMainFileUrlAsync()
	{
		Uri assetBaseUri = new Uri("avares://RootApp.Client.Avalonia/DotNetBrowser/WebRtc/Bundle/");
		List<string> assetFiles = new List<string> { "index.html", "rootapp-desktop-webrtc.css", "rootapp-desktop-webrtc.js", "assets/CircularStd-Bold.ttf", "assets/CircularStd-Book.ttf", "assets/CircularStd-Medium.ttf", "assets/CircularXXTT-Thin.ttf" };
		List<string> optionalFiles = new List<string> { "rootapp-desktop-webrtc.js.map" };
		return Path.Combine(await extractAssetBundleToTempFolderAsync(assetBaseUri, assetFiles, optionalFiles), "index.html");
	}

	private async Task<string> extractAssetBundleToTempFolderAsync(Uri P_0, IEnumerable<string> P_1, IEnumerable<string>? P_2 = null)
	{
		if (!string.IsNullOrEmpty(_cachedTempFolder) && Directory.Exists(_cachedTempFolder))
		{
			return _cachedTempFolder;
		}
		_cachedTempFolder = Path.Combine(P_0.LocalDirectory.FullName, "WebRtcBundle");
		if (Directory.Exists(_cachedTempFolder))
		{
			Directory.Delete(_cachedTempFolder, true);
		}
		Directory.CreateDirectory(_cachedTempFolder);
		foreach (string relativePath in P_1)
		{
			await extractAssetAsync(P_0, relativePath, true);
		}
		if (P_2 != null)
		{
			foreach (string relativePath2 in P_2)
			{
				await extractAssetAsync(P_0, relativePath2, false);
			}
		}
		return _cachedTempFolder;
	}

	private async Task extractAssetAsync(Uri P_0, string P_1, bool P_2)
	{
		Uri assetUri = new Uri(P_0, P_1);
		try
		{
			await using Stream assetStream = AssetLoader.Open(assetUri);
			string targetFilePath = Path.Combine(_cachedTempFolder, P_1.Replace('/', Path.DirectorySeparatorChar));
			Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
			await using FileStream fileStream = File.Create(targetFilePath);
			await assetStream.CopyToAsync(fileStream);
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			if (P_2)
			{
				throw new Exception($"Failed to extract asset {assetUri}", ex2);
			}
			Logger.TryGet(LogEventLevel.Warning, "WebRtcBundleExtractor")?.Log(this, "Optional asset not found: {Asset}", assetUri);
		}
	}
}
