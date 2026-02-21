namespace RootApp.Browser.Handlers;

public class DevicePermissionHandlerFactory
{
	public DevicePermissionHandler Create(bool P_0)
	{
		return new DevicePermissionHandler(P_0);
	}
}
