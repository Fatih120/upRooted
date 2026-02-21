using System;
using DotNetBrowser.Handlers;
using DotNetBrowser.Navigation.Handlers;
using Microsoft.Extensions.Logging;

namespace RootApp.Browser.Handlers;

public sealed class NavigationGuardHandler : IHandler<StartNavigationParameters, StartNavigationResponse>
{
	private static readonly string[] AllowedSchemes = new string[6] { "rootapp://", "root://", "avares://", "about:", "data:", "blob:" };

	private readonly ILogger<NavigationGuardHandler> _logger;

	public NavigationGuardHandler(ILoggerFactory P_0)
	{
		_logger = P_0.CreateLogger<NavigationGuardHandler>();
	}

	public StartNavigationResponse Handle(StartNavigationParameters P_0)
	{
		string url = P_0.Url;
		string[] allowedSchemes = AllowedSchemes;
		foreach (string value in allowedSchemes)
		{
			if (url.StartsWith(value, StringComparison.OrdinalIgnoreCase))
			{
				return StartNavigationResponse.Start();
			}
		}
		_logger.LogWarning("Navigation blocked by security guard. URL: {Url}", url);
		return StartNavigationResponse.Ignore();
	}
}
