using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum AppDeploymentStatus
{
	[OriginalName("APP_DEPLOYMENT_STATUS_UNSPECIFIED")]
	Unspecified,
	[OriginalName("APP_DEPLOYMENT_STATUS_RUNNING")]
	Running,
	[OriginalName("APP_DEPLOYMENT_STATUS_STOPPED")]
	Stopped,
	[OriginalName("APP_DEPLOYMENT_STATUS_STARTING")]
	Starting,
	[OriginalName("APP_DEPLOYMENT_STATUS_STOPPING")]
	Stopping,
	[OriginalName("APP_DEPLOYMENT_STATUS_SUSPENDED")]
	Suspended
}
