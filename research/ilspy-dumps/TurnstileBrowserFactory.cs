using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Engine;
using DotNetBrowser.Profile;
using Microsoft.Extensions.Logging;
using RootApp.Core.Identifiers;

namespace RootApp.Browser.Turnstile;

public sealed class TurnstileBrowserFactory(TurnstileEngineFactory, ILoggerFactory)
{
	private readonly TurnstileEngineFactory _engineFactory = P_0;

	private readonly ILoggerFactory _loggerFactory = P_1;

	public async Task<TurnstileBrowser> CreateWithEngineAsync(RootGuid P_0, string P_1)
	{
		IEngine engine = await P_0.CreateEngineAsync();
		IProfile profile = engine.Profiles.Default;
		IBrowser browser = profile.CreateBrowser();
		return new TurnstileBrowser(P_0, browser, engine, P_1, P_1);
	}
}
