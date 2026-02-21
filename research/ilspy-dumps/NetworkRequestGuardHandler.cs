using System;
using DotNetBrowser.Handlers;
using DotNetBrowser.Net.Handlers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RootApp.Core;

namespace RootApp.Browser.Handlers;

public sealed class NetworkRequestGuardHandler : IHandler<SendUrlRequestParameters, SendUrlRequestResponse>
{
	private readonly string[] _allowedAuthorities;

	private static readonly string[] KnownChromiumDomains = new string[6] { "chrome.cloudflare-dns.com", "dns.google", "clients1.google.com", "clients2.google.com", "update.googleapis.com", "www.gstatic.com" };

	private readonly ILogger<NetworkRequestGuardHandler> _logger;

	public NetworkRequestGuardHandler(IOptions<RootWebApiConfig> P_0, ILoggerFactory P_1)
	{
		_logger = P_1.CreateLogger<NetworkRequestGuardHandler>();
		string authority = P_0.Value.WebApiUrl.Authority;
		_allowedAuthorities = new string[2] { authority, "o4509469920133120.ingest.us.sentry.io" };
	}

	public SendUrlRequestResponse Handle(SendUrlRequestParameters P_0)
	{
		string url = P_0.UrlRequest.Url;
		if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
		{
			return SendUrlRequestResponse.Continue();
		}
		bool flag;
		switch (result.Scheme.ToLowerInvariant())
		{
		case "http":
		case "https":
		case "ws":
		case "wss":
		case "ftp":
			flag = true;
			break;
		default:
			flag = false;
			break;
		}
		if (!flag)
		{
			return SendUrlRequestResponse.Continue();
		}
		string[] allowedAuthorities = _allowedAuthorities;
		foreach (string b in allowedAuthorities)
		{
			if (string.Equals(result.Authority, b, StringComparison.OrdinalIgnoreCase))
			{
				return SendUrlRequestResponse.Continue();
			}
		}
		if (IsKnownChromiumDomain(result.Host))
		{
			_logger.LogDebug("Blocked Chromium-internal request: {Url}", url);
		}
		else
		{
			_logger.LogWarning("Blocked external network request: {Url}", url);
		}
		return SendUrlRequestResponse.Cancel();
	}

	private static bool IsKnownChromiumDomain(string P_0)
	{
		string[] knownChromiumDomains = KnownChromiumDomains;
		foreach (string b in knownChromiumDomains)
		{
			if (string.Equals(P_0, b, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}
}
