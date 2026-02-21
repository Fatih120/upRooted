using System.Threading.Tasks;
using DotNetBrowser.Browser.Handlers;

namespace RootApp.Browser.Handlers;

public class ContextMenuHandler
{
	public static Task<ShowContextMenuResponse> ShowContextMenuAsync(ShowContextMenuParameters parameters)
	{
		return Task.FromResult(ShowContextMenuResponse.Close());
	}
}
