using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RootApp.WebApi.Client.Shared;
using RootApp.WebApi.Shared.Grpc.Services;

namespace RootApp.WebApi.Client;

internal sealed class NullApiConnection : IApiConnection, IAsyncDisposable
{
	private readonly InvalidOperationException _exception = new InvalidOperationException("ApiConnection not established.");

	public static IApiConnection Instance { get; } = new NullApiConnection();

	public ClientToken ClientToken
	{
		get
		{
			throw _exception;
		}
	}

	public Uri? HubServerUrl
	{
		get
		{
			throw _exception;
		}
	}

	public bool IsHubConnected
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityLogGrpcService.CommunityLogGrpcServiceClient CommunityLog
	{
		get
		{
			throw _exception;
		}
	}

	public UserGrpcService.UserGrpcServiceClient User
	{
		get
		{
			throw _exception;
		}
	}

	public AccessRuleGrpcService.AccessRuleGrpcServiceClient AccessRule
	{
		get
		{
			throw _exception;
		}
	}

	public AppStoreGrpcService.AppStoreGrpcServiceClient AppStore
	{
		get
		{
			throw _exception;
		}
	}

	public AssetGrpcService.AssetGrpcServiceClient Asset
	{
		get
		{
			throw _exception;
		}
	}

	public ChannelGrpcService.ChannelGrpcServiceClient Channel
	{
		get
		{
			throw _exception;
		}
	}

	public ChannelGroupGrpcService.ChannelGroupGrpcServiceClient ChannelGroup
	{
		get
		{
			throw _exception;
		}
	}

	public MessageGrpcService.MessageGrpcServiceClient Message
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityGrpcService.CommunityGrpcServiceClient Community
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityAppGrpcService.CommunityAppGrpcServiceClient CommunityApp
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityAppLogGrpcService.CommunityAppLogGrpcServiceClient CommunityAppLog
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityMemberBanGrpcService.CommunityMemberBanGrpcServiceClient CommunityMemberBan
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityMemberInviteGrpcService.CommunityMemberInviteGrpcServiceClient CommunityMemberInvite
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityRoleGrpcService.CommunityRoleGrpcServiceClient CommunityRole
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityMemberGrpcService.CommunityMemberGrpcServiceClient CommunityMember
	{
		get
		{
			throw _exception;
		}
	}

	public CommunityMemberRoleGrpcService.CommunityMemberRoleGrpcServiceClient CommunityMemberRole
	{
		get
		{
			throw _exception;
		}
	}

	public DirectMessageGrpcService.DirectMessageGrpcServiceClient DirectMessage
	{
		get
		{
			throw _exception;
		}
	}

	public DirectoryGrpcService.DirectoryGrpcServiceClient Directory
	{
		get
		{
			throw _exception;
		}
	}

	public FileGrpcService.FileGrpcServiceClient File
	{
		get
		{
			throw _exception;
		}
	}

	public FriendshipGrpcService.FriendshipGrpcServiceClient Friendship
	{
		get
		{
			throw _exception;
		}
	}

	public FriendshipGroupGrpcService.FriendshipGroupGrpcServiceClient FriendshipGroup
	{
		get
		{
			throw _exception;
		}
	}

	public FriendshipInviteGrpcService.FriendshipInviteGrpcServiceClient FriendshipInvite
	{
		get
		{
			throw _exception;
		}
	}

	public LinkGrpcService.LinkGrpcServiceClient Link
	{
		get
		{
			throw _exception;
		}
	}

	public NotificationGrpcService.NotificationGrpcServiceClient Notification
	{
		get
		{
			throw _exception;
		}
	}

	public SupportGrpcService.SupportGrpcServiceClient Support
	{
		get
		{
			throw _exception;
		}
	}

	public WebRtcGrpcService.WebRtcGrpcServiceClient WebRtc
	{
		get
		{
			throw _exception;
		}
	}

	public ValueTask DisposeAsync()
	{
		return ValueTask.CompletedTask;
	}

	public Task<Uri> UploadFileAsync(FileInfo P_0, IProgress<int>? P_1 = null, CancellationToken P_2 = default(CancellationToken))
	{
		throw _exception;
	}

	public Task<HttpResponseMessage> DownloadAssetAsync(Uri P_0, string? P_1, DateTimeOffset? P_2, CancellationToken P_3)
	{
		throw _exception;
	}

	public IAsyncEnumerable<ClientHubPacket> ReadPacketsAsync(CancellationToken P_0)
	{
		throw _exception;
	}

	public ValueTask WaitForHubConnectionAsync(CancellationToken P_0 = default(CancellationToken))
	{
		throw _exception;
	}
}
