using DotNetBrowser.Handlers;
using DotNetBrowser.Permissions.Handlers;

namespace RootApp.Browser.Handlers;

public class DevicePermissionHandler : IHandler<RequestPermissionParameters, RequestPermissionResponse>
{
	private readonly bool _grant;

	public DevicePermissionHandler(bool P_0)
	{
		_grant = P_0;
	}

	public RequestPermissionResponse Handle(RequestPermissionParameters P_0)
	{
		if (_grant)
		{
			return RequestPermissionResponse.Grant();
		}
		return RequestPermissionResponse.Deny();
	}
}
