// RootApp.Client.Domain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Domain.Helpers.Store.SecureStorageImplementation
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Client.Domain.SecureStorage;

public class SecureStorageImplementation(ILogger<SecureStorageImplementation> P_0, IRootApplicationDirectories P_1) : ISecureStorage
{
	private static readonly byte[] _hmacKey = Convert.FromHexString("2ff42d1c0fed2e26e2d90dfcac5ca587f9c83d4d8399a8dc853c050daf6996273f182c3d8af870bb07798e33ed6c87cd1d1d0f9e2cf6e083d29029fb878c1e2b5b611bff6d3fe537304d076bba503fde");

	private readonly DirectoryInfo _dir = Directory.CreateDirectory(Path.Combine(P_1.LocalDirectory.FullName, "Store"));

	private readonly ILogger<SecureStorageImplementation> _logger = P_0;

	public virtual async ValueTask<string?> GetAsync(string P_0)
	{
		string path = GetFilePath(P_0);
		if (!File.Exists(path))
		{
			return null;
		}
		byte[] bytes = await File.ReadAllBytesAsync(path).ConfigureAwait(continueOnCapturedContext: false);
		if (1 == 0)
		{
		}
		byte[] unprotectedBytes = ProtectedData.Unprotect(bytes, GetEntropy(P_0), DataProtectionScope.CurrentUser);
		return Encoding.UTF8.GetString(unprotectedBytes);
	}

	public virtual async ValueTask SetAsync(string P_0, string? P_1)
	{
		string filePath = GetFilePath(P_0);
		string tempFilePath = filePath + ".tmp";
		try
		{
			if (P_1 == null)
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(P_1);
			if (1 == 0)
			{
			}
			byte[] protectedBytes = ProtectedData.Protect(bytes, GetEntropy(P_0), DataProtectionScope.CurrentUser);
			await File.WriteAllBytesAsync(tempFilePath, protectedBytes).ConfigureAwait(continueOnCapturedContext: false);
			File.Move(tempFilePath, filePath, true);
			return;
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2, "Unable to save key {key} to {path}", P_0, filePath);
		}
		if (File.Exists(tempFilePath))
		{
			File.Delete(tempFilePath);
		}
	}

	public virtual void Delete(string P_0)
	{
		string filePath = GetFilePath(P_0);
		File.Delete(filePath);
	}

	private string GetFilePath(string P_0)
	{
		return Path.GetFullPath(P_0, _dir.FullName);
	}

	private byte[] GetEntropy(string P_0)
	{
		byte[] bytes = Encoding.Unicode.GetBytes(P_0.Normalize());
		return HMACSHA384.HashData(_hmacKey, bytes);
	}
}
