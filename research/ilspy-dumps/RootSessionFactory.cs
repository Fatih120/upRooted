// RootApp.Client.CoreDomain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.CoreDomain.RootSessionFactory
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.WebApi.Client.Shared;

public class RootSessionFactory(IHostApplicationLifetime P_0, ILoggerFactory P_1, UserInfoServiceFactory P_2, UserBlockServiceFactory P_3, FriendServiceFactory P_4, TabServiceFactory P_5, CommunityServiceFactory P_6, IGlobalUserCacheService P_7, FileUploadServiceFactory P_8, NotificationServiceFactory P_9, DirectMessageServiceFactory P_10, IActiveMediaRoomService P_11, IConnectionStatusService P_12, SupportServiceFactory P_13, AllNotificationCountServiceFactory P_14, AssetServiceFactory P_15, LinkServiceFactory P_16, IAnalyticsService P_17)
{
	public RootSession Create(SessionUser P_0, IRootService P_1)
	{
		return new RootSession(P_0, P_1, P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, P_9, P_10, P_11, P_12, P_13, P_14, P_15, P_16, P_17);
	}
}
