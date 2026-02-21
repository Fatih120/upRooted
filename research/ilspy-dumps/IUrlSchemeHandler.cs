using DotNetBrowser.Net.Handlers;

namespace RootApp.Browser.Handlers;

public interface IUrlSchemeHandler
{
	bool CanHandle(string P_0);

	InterceptRequestResponse Handle(InterceptRequestParameters P_0);
}
