using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RootApp.WebApi.Shared.Grpc.Services;

namespace RootApp.WebApi.Client.Shared;

public interface IApiConnection : IAsyncDisposable
{
	ClientToken ClientToken { get; }

	Uri? HubServerUrl { get; }

	bool IsHubConnected { get; }

	UserGrpcService.UserGrpcServiceClient User { get; }

	AccessRuleGrpcService.AccessRuleGrpcServiceClient AccessRule { get; }

	AppStoreGrpcService.AppStoreGrpcServiceClient AppStore { get; }

	AssetGrpcService.AssetGrpcServiceClient Asset { get; }

	ChannelGrpcService.ChannelGrpcServiceClient Channel { get; }

	ChannelGroupGrpcService.ChannelGroupGrpcServiceClient ChannelGroup { get; }

	CommunityGrpcService.CommunityGrpcServiceClient Community { get; }

	CommunityAppGrpcService.CommunityAppGrpcServiceClient CommunityApp { get; }

	CommunityAppLogGrpcService.CommunityAppLogGrpcServiceClient CommunityAppLog { get; }

	CommunityMemberBanGrpcService.CommunityMemberBanGrpcServiceClient CommunityMemberBan { get; }

	CommunityMemberInviteGrpcService.CommunityMemberInviteGrpcServiceClient CommunityMemberInvite { get; }

	CommunityRoleGrpcService.CommunityRoleGrpcServiceClient CommunityRole { get; }

	CommunityMemberGrpcService.CommunityMemberGrpcServiceClient CommunityMember { get; }

	CommunityMemberRoleGrpcService.CommunityMemberRoleGrpcServiceClient CommunityMemberRole { get; }

	DirectoryGrpcService.DirectoryGrpcServiceClient Directory { get; }

	DirectMessageGrpcService.DirectMessageGrpcServiceClient DirectMessage { get; }

	FileGrpcService.FileGrpcServiceClient File { get; }

	FriendshipGrpcService.FriendshipGrpcServiceClient Friendship { get; }

	FriendshipGroupGrpcService.FriendshipGroupGrpcServiceClient FriendshipGroup { get; }

	FriendshipInviteGrpcService.FriendshipInviteGrpcServiceClient FriendshipInvite { get; }

	MessageGrpcService.MessageGrpcServiceClient Message { get; }

	NotificationGrpcService.NotificationGrpcServiceClient Notification { get; }

	CommunityLogGrpcService.CommunityLogGrpcServiceClient CommunityLog { get; }

	SupportGrpcService.SupportGrpcServiceClient Support { get; }

	WebRtcGrpcService.WebRtcGrpcServiceClient WebRtc { get; }

	LinkGrpcService.LinkGrpcServiceClient Link { get; }

	Task<Uri> UploadFileAsync(FileInfo fileInfo, IProgress<int>? percentComplete = null, CancellationToken cancellationToken = default(CancellationToken));

	Task<HttpResponseMessage> DownloadAssetAsync(Uri assetUri, string? eTag, DateTimeOffset? lastModified, CancellationToken cancellationToken);

	IAsyncEnumerable<ClientHubPacket> ReadPacketsAsync(CancellationToken cancellationToken = default(CancellationToken));

	ValueTask WaitForHubConnectionAsync(CancellationToken cancellationToken = default(CancellationToken));
}
