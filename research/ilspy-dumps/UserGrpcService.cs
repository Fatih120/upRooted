using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Google.Protobuf;
using Grpc.Core;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Shared.Grpc.Services;

public static class UserGrpcService
{
	[GeneratedCode("grpc_csharp_plugin", null)]
	private static class __Helper_MessageCache<T>
	{
		public static readonly bool IsBufferMessage = typeof(IBufferMessage).GetTypeInfo().IsAssignableFrom(typeof(T));
	}

	public class UserGrpcServiceClient : ClientBase<UserGrpcServiceClient>
	{
		[GeneratedCode("grpc_csharp_plugin", null)]
		public UserGrpcServiceClient(ChannelBase channel)
			: base(channel)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public UserGrpcServiceClient(CallInvoker callInvoker)
			: base(callInvoker)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected UserGrpcServiceClient(ClientBaseConfiguration P_0)
			: base(P_0)
		{
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSelfResponse GetSelf(UserGetSelfRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetSelf(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSelfResponse GetSelf(UserGetSelfRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetSelf, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSelfResponse> GetSelfAsync(UserGetSelfRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetSelfAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSelfResponse> GetSelfAsync(UserGetSelfRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetSelf, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserListResponse GetExtendedUsersById(UserGetExtendedUsersByIdRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetExtendedUsersById(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserListResponse GetExtendedUsersById(UserGetExtendedUsersByIdRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetExtendedUsersById, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserListResponse> GetExtendedUsersByIdAsync(UserGetExtendedUsersByIdRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetExtendedUsersByIdAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserListResponse> GetExtendedUsersByIdAsync(UserGetExtendedUsersByIdRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetExtendedUsersById, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserListResponse GetExtendedUsersByUsername(UserGetExtendedUsersByUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetExtendedUsersByUsername(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserListResponse GetExtendedUsersByUsername(UserGetExtendedUsersByUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetExtendedUsersByUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserListResponse> GetExtendedUsersByUsernameAsync(UserGetExtendedUsersByUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetExtendedUsersByUsernameAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserListResponse> GetExtendedUsersByUsernameAsync(UserGetExtendedUsersByUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetExtendedUsersByUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserTokenResponse SignIn(UserSignInRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SignIn(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserTokenResponse SignIn(UserSignInRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SignIn, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserTokenResponse> SignInAsync(UserSignInRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SignInAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserTokenResponse> SignInAsync(UserSignInRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SignIn, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetMaxOnlineStatusResponse SetMaxOnlineStatus(UserSetMaxOnlineStatusRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMaxOnlineStatus(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetMaxOnlineStatusResponse SetMaxOnlineStatus(UserSetMaxOnlineStatusRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetMaxOnlineStatus, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetMaxOnlineStatusResponse> SetMaxOnlineStatusAsync(UserSetMaxOnlineStatusRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMaxOnlineStatusAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetMaxOnlineStatusResponse> SetMaxOnlineStatusAsync(UserSetMaxOnlineStatusRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetMaxOnlineStatus, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetDeviceOnlineStatusResponse SetDeviceOnlineStatus(UserSetDeviceOnlineStatusRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetDeviceOnlineStatus(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetDeviceOnlineStatusResponse SetDeviceOnlineStatus(UserSetDeviceOnlineStatusRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetDeviceOnlineStatus, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetDeviceOnlineStatusResponse> SetDeviceOnlineStatusAsync(UserSetDeviceOnlineStatusRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetDeviceOnlineStatusAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetDeviceOnlineStatusResponse> SetDeviceOnlineStatusAsync(UserSetDeviceOnlineStatusRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetDeviceOnlineStatus, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual HubServerEndpointResponse GetNewHubserverEndpoint(UserGetNewHubserverRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetNewHubserverEndpoint(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual HubServerEndpointResponse GetNewHubserverEndpoint(UserGetNewHubserverRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetNewHubserverEndpoint, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<HubServerEndpointResponse> GetNewHubserverEndpointAsync(UserGetNewHubserverRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetNewHubserverEndpointAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<HubServerEndpointResponse> GetNewHubserverEndpointAsync(UserGetNewHubserverRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetNewHubserverEndpoint, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSignUpResponse SignUp(UserSignUpRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SignUp(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSignUpResponse SignUp(UserSignUpRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SignUp, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSignUpResponse> SignUpAsync(UserSignUpRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SignUpAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSignUpResponse> SignUpAsync(UserSignUpRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SignUp, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSignOutResponse SignOut(UserSignOutRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SignOut(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSignOutResponse SignOut(UserSignOutRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SignOut, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSignOutResponse> SignOutAsync(UserSignOutRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SignOutAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSignOutResponse> SignOutAsync(UserSignOutRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SignOut, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetPasswordResponse SetPassword(UserSetPasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetPassword(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetPasswordResponse SetPassword(UserSetPasswordRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetPassword, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetPasswordResponse> SetPasswordAsync(UserSetPasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetPasswordAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetPasswordResponse> SetPasswordAsync(UserSetPasswordRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetPassword, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserForgotPasswordResponse ForgotPassword(UserForgotPasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ForgotPassword(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserForgotPasswordResponse ForgotPassword(UserForgotPasswordRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ForgotPassword, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserForgotPasswordResponse> ForgotPasswordAsync(UserForgotPasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ForgotPasswordAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserForgotPasswordResponse> ForgotPasswordAsync(UserForgotPasswordRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ForgotPassword, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserForgotUsernameResponse ForgotUsername(UserForgotUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ForgotUsername(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserForgotUsernameResponse ForgotUsername(UserForgotUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ForgotUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserForgotUsernameResponse> ForgotUsernameAsync(UserForgotUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ForgotUsernameAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserForgotUsernameResponse> ForgotUsernameAsync(UserForgotUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ForgotUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserResetPasswordResponse ResetPassword(UserResetPasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ResetPassword(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserResetPasswordResponse ResetPassword(UserResetPasswordRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ResetPassword, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserResetPasswordResponse> ResetPasswordAsync(UserResetPasswordRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ResetPasswordAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserResetPasswordResponse> ResetPasswordAsync(UserResetPasswordRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ResetPassword, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetEmailVerificationCodeResponse SetEmailVerificationCode(UserSetEmailVerificationCodeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetEmailVerificationCode(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetEmailVerificationCodeResponse SetEmailVerificationCode(UserSetEmailVerificationCodeRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetEmailVerificationCode, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetEmailVerificationCodeResponse> SetEmailVerificationCodeAsync(UserSetEmailVerificationCodeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetEmailVerificationCodeAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetEmailVerificationCodeResponse> SetEmailVerificationCodeAsync(UserSetEmailVerificationCodeRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetEmailVerificationCode, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserResendEmailVerificationCodeResponse ResendEmailVerificationCode(UserResendEmailVerificationCodeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ResendEmailVerificationCode(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserResendEmailVerificationCodeResponse ResendEmailVerificationCode(UserResendEmailVerificationCodeRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_ResendEmailVerificationCode, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserResendEmailVerificationCodeResponse> ResendEmailVerificationCodeAsync(UserResendEmailVerificationCodeRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return ResendEmailVerificationCodeAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserResendEmailVerificationCodeResponse> ResendEmailVerificationCodeAsync(UserResendEmailVerificationCodeRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_ResendEmailVerificationCode, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetUsernameResponse SetUsername(UserSetUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetUsername(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetUsernameResponse SetUsername(UserSetUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetUsernameResponse> SetUsernameAsync(UserSetUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetUsernameAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetUsernameResponse> SetUsernameAsync(UserSetUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetProfilePictureResponse SetProfilePicture(UserSetProfilePictureRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetProfilePicture(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetProfilePictureResponse SetProfilePicture(UserSetProfilePictureRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetProfilePicture, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetProfilePictureResponse> SetProfilePictureAsync(UserSetProfilePictureRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetProfilePictureAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetProfilePictureResponse> SetProfilePictureAsync(UserSetProfilePictureRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetProfilePicture, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetEmailResponse SetEmail(UserSetEmailRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetEmail(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetEmailResponse SetEmail(UserSetEmailRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetEmail, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetEmailResponse> SetEmailAsync(UserSetEmailRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetEmailAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetEmailResponse> SetEmailAsync(UserSetEmailRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetEmail, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetDirectMessageInviteRequirementResponse SetDirectMessageInviteRequirement(UserSetDirectMessageInviteRequirementRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetDirectMessageInviteRequirement(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetDirectMessageInviteRequirementResponse SetDirectMessageInviteRequirement(UserSetDirectMessageInviteRequirementRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetDirectMessageInviteRequirement, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetDirectMessageInviteRequirementResponse> SetDirectMessageInviteRequirementAsync(UserSetDirectMessageInviteRequirementRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetDirectMessageInviteRequirementAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetDirectMessageInviteRequirementResponse> SetDirectMessageInviteRequirementAsync(UserSetDirectMessageInviteRequirementRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetDirectMessageInviteRequirement, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetFriendshipInviteRequirementResponse SetFriendshipInviteRequirement(UserSetFriendshipInviteRequirementRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetFriendshipInviteRequirement(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetFriendshipInviteRequirementResponse SetFriendshipInviteRequirement(UserSetFriendshipInviteRequirementRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetFriendshipInviteRequirement, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetFriendshipInviteRequirementResponse> SetFriendshipInviteRequirementAsync(UserSetFriendshipInviteRequirementRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetFriendshipInviteRequirementAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetFriendshipInviteRequirementResponse> SetFriendshipInviteRequirementAsync(UserSetFriendshipInviteRequirementRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetFriendshipInviteRequirement, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserShortListResponse FindByUsername(UserFindByUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return FindByUsername(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserShortListResponse FindByUsername(UserFindByUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_FindByUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserShortListResponse> FindByUsernameAsync(UserFindByUsernameRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return FindByUsernameAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserShortListResponse> FindByUsernameAsync(UserFindByUsernameRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_FindByUsername, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserConnectionResponse GetConnectionBetween(UserConnectionBetweenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetConnectionBetween(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserConnectionResponse GetConnectionBetween(UserConnectionBetweenRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetConnectionBetween, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserConnectionResponse> GetConnectionBetweenAsync(UserConnectionBetweenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetConnectionBetweenAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserConnectionResponse> GetConnectionBetweenAsync(UserConnectionBetweenRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetConnectionBetween, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserNoteResponse GetUserNote(UserGetNoteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetUserNote(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserNoteResponse GetUserNote(UserGetNoteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_GetUserNote, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserNoteResponse> GetUserNoteAsync(UserGetNoteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetUserNoteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserNoteResponse> GetUserNoteAsync(UserGetNoteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_GetUserNote, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetNoteResponse SetUserNote(UserSetNoteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetUserNote(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetNoteResponse SetUserNote(UserSetNoteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetUserNote, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetNoteResponse> SetUserNoteAsync(UserSetNoteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetUserNoteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetNoteResponse> SetUserNoteAsync(UserSetNoteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetUserNote, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetMobileNotificationTokenResponse SetMobileNotificationToken(UserSetMobileNotificationTokenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMobileNotificationToken(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserSetMobileNotificationTokenResponse SetMobileNotificationToken(UserSetMobileNotificationTokenRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_SetMobileNotificationToken, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetMobileNotificationTokenResponse> SetMobileNotificationTokenAsync(UserSetMobileNotificationTokenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return SetMobileNotificationTokenAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserSetMobileNotificationTokenResponse> SetMobileNotificationTokenAsync(UserSetMobileNotificationTokenRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_SetMobileNotificationToken, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserDeleteResponse Delete(UserDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Delete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserDeleteResponse Delete(UserDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserDeleteResponse> DeleteAsync(UserDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserDeleteResponse> DeleteAsync(UserDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserBlockListResponse BlockList(UserBlockListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return BlockList(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserBlockListResponse BlockList(UserBlockListRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_BlockList, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserBlockListResponse> BlockListAsync(UserBlockListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return BlockListAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserBlockListResponse> BlockListAsync(UserBlockListRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_BlockList, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserBlockCreateResponse BlockCreate(UserBlockCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return BlockCreate(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserBlockCreateResponse BlockCreate(UserBlockCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_BlockCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserBlockCreateResponse> BlockCreateAsync(UserBlockCreateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return BlockCreateAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserBlockCreateResponse> BlockCreateAsync(UserBlockCreateRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_BlockCreate, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserBlockDeleteResponse BlockDelete(UserBlockDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return BlockDelete(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserBlockDeleteResponse BlockDelete(UserBlockDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_BlockDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserBlockDeleteResponse> BlockDeleteAsync(UserBlockDeleteRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return BlockDeleteAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserBlockDeleteResponse> BlockDeleteAsync(UserBlockDeleteRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_BlockDelete, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserFlagResponse Flag(UserFlagRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Flag(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserFlagResponse Flag(UserFlagRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_Flag, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserFlagResponse> FlagAsync(UserFlagRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return FlagAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserFlagResponse> FlagAsync(UserFlagRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_Flag, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserRefreshTokenResponse RefreshToken(UserRefreshTokenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RefreshToken(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual UserRefreshTokenResponse RefreshToken(UserRefreshTokenRequest request, CallOptions options)
		{
			return base.CallInvoker.BlockingUnaryCall(__Method_RefreshToken, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserRefreshTokenResponse> RefreshTokenAsync(UserRefreshTokenRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return RefreshTokenAsync(request, new CallOptions(headers, deadline, cancellationToken));
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		public virtual AsyncUnaryCall<UserRefreshTokenResponse> RefreshTokenAsync(UserRefreshTokenRequest request, CallOptions options)
		{
			return base.CallInvoker.AsyncUnaryCall(__Method_RefreshToken, null, options, request);
		}

		[GeneratedCode("grpc_csharp_plugin", null)]
		protected override UserGrpcServiceClient NewInstance(ClientBaseConfiguration P_0)
		{
			return new UserGrpcServiceClient(P_0);
		}
	}

	private static readonly string __ServiceName = "root.UserGrpcService";

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserGetSelfRequest> __Marshaller_root_UserGetSelfRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserGetSelfRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSelfResponse> __Marshaller_root_UserSelfResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSelfResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserGetExtendedUsersByIdRequest> __Marshaller_root_UserGetExtendedUsersByIdRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserGetExtendedUsersByIdRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserListResponse> __Marshaller_root_UserListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserGetExtendedUsersByUsernameRequest> __Marshaller_root_UserGetExtendedUsersByUsernameRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserGetExtendedUsersByUsernameRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSignInRequest> __Marshaller_root_UserSignInRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSignInRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserTokenResponse> __Marshaller_root_UserTokenResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserTokenResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetMaxOnlineStatusRequest> __Marshaller_root_UserSetMaxOnlineStatusRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetMaxOnlineStatusRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetMaxOnlineStatusResponse> __Marshaller_root_UserSetMaxOnlineStatusResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetMaxOnlineStatusResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetDeviceOnlineStatusRequest> __Marshaller_root_UserSetDeviceOnlineStatusRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetDeviceOnlineStatusRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetDeviceOnlineStatusResponse> __Marshaller_root_UserSetDeviceOnlineStatusResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetDeviceOnlineStatusResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserGetNewHubserverRequest> __Marshaller_root_UserGetNewHubserverRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserGetNewHubserverRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<HubServerEndpointResponse> __Marshaller_root_HubServerEndpointResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, HubServerEndpointResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSignUpRequest> __Marshaller_root_UserSignUpRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSignUpRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSignUpResponse> __Marshaller_root_UserSignUpResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSignUpResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSignOutRequest> __Marshaller_root_UserSignOutRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSignOutRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSignOutResponse> __Marshaller_root_UserSignOutResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSignOutResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetPasswordRequest> __Marshaller_root_UserSetPasswordRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetPasswordRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetPasswordResponse> __Marshaller_root_UserSetPasswordResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetPasswordResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserForgotPasswordRequest> __Marshaller_root_UserForgotPasswordRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserForgotPasswordRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserForgotPasswordResponse> __Marshaller_root_UserForgotPasswordResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserForgotPasswordResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserForgotUsernameRequest> __Marshaller_root_UserForgotUsernameRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserForgotUsernameRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserForgotUsernameResponse> __Marshaller_root_UserForgotUsernameResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserForgotUsernameResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserResetPasswordRequest> __Marshaller_root_UserResetPasswordRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserResetPasswordRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserResetPasswordResponse> __Marshaller_root_UserResetPasswordResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserResetPasswordResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetEmailVerificationCodeRequest> __Marshaller_root_UserSetEmailVerificationCodeRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetEmailVerificationCodeRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetEmailVerificationCodeResponse> __Marshaller_root_UserSetEmailVerificationCodeResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetEmailVerificationCodeResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserResendEmailVerificationCodeRequest> __Marshaller_root_UserResendEmailVerificationCodeRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserResendEmailVerificationCodeRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserResendEmailVerificationCodeResponse> __Marshaller_root_UserResendEmailVerificationCodeResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserResendEmailVerificationCodeResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetUsernameRequest> __Marshaller_root_UserSetUsernameRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetUsernameRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetUsernameResponse> __Marshaller_root_UserSetUsernameResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetUsernameResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetProfilePictureRequest> __Marshaller_root_UserSetProfilePictureRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetProfilePictureRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetProfilePictureResponse> __Marshaller_root_UserSetProfilePictureResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetProfilePictureResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetEmailRequest> __Marshaller_root_UserSetEmailRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetEmailRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetEmailResponse> __Marshaller_root_UserSetEmailResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetEmailResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetDirectMessageInviteRequirementRequest> __Marshaller_root_UserSetDirectMessageInviteRequirementRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetDirectMessageInviteRequirementRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetDirectMessageInviteRequirementResponse> __Marshaller_root_UserSetDirectMessageInviteRequirementResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetDirectMessageInviteRequirementResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetFriendshipInviteRequirementRequest> __Marshaller_root_UserSetFriendshipInviteRequirementRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetFriendshipInviteRequirementRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetFriendshipInviteRequirementResponse> __Marshaller_root_UserSetFriendshipInviteRequirementResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetFriendshipInviteRequirementResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserFindByUsernameRequest> __Marshaller_root_UserFindByUsernameRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserFindByUsernameRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserShortListResponse> __Marshaller_root_UserShortListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserShortListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserConnectionBetweenRequest> __Marshaller_root_UserConnectionBetweenRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserConnectionBetweenRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserConnectionResponse> __Marshaller_root_UserConnectionResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserConnectionResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserGetNoteRequest> __Marshaller_root_UserGetNoteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserGetNoteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserNoteResponse> __Marshaller_root_UserNoteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserNoteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetNoteRequest> __Marshaller_root_UserSetNoteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetNoteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetNoteResponse> __Marshaller_root_UserSetNoteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetNoteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetMobileNotificationTokenRequest> __Marshaller_root_UserSetMobileNotificationTokenRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetMobileNotificationTokenRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserSetMobileNotificationTokenResponse> __Marshaller_root_UserSetMobileNotificationTokenResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserSetMobileNotificationTokenResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserDeleteRequest> __Marshaller_root_UserDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserDeleteResponse> __Marshaller_root_UserDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserBlockListRequest> __Marshaller_root_UserBlockListRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserBlockListRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserBlockListResponse> __Marshaller_root_UserBlockListResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserBlockListResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserBlockCreateRequest> __Marshaller_root_UserBlockCreateRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserBlockCreateRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserBlockCreateResponse> __Marshaller_root_UserBlockCreateResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserBlockCreateResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserBlockDeleteRequest> __Marshaller_root_UserBlockDeleteRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserBlockDeleteRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserBlockDeleteResponse> __Marshaller_root_UserBlockDeleteResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserBlockDeleteResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserFlagRequest> __Marshaller_root_UserFlagRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserFlagRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserFlagResponse> __Marshaller_root_UserFlagResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserFlagResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserRefreshTokenRequest> __Marshaller_root_UserRefreshTokenRequest = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserRefreshTokenRequest.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Marshaller<UserRefreshTokenResponse> __Marshaller_root_UserRefreshTokenResponse = Marshallers.Create(__Helper_SerializeMessage, (DeserializationContext context) => __Helper_DeserializeMessage(context, UserRefreshTokenResponse.Parser));

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserGetSelfRequest, UserSelfResponse> __Method_GetSelf = new Method<UserGetSelfRequest, UserSelfResponse>(MethodType.Unary, __ServiceName, "GetSelf", __Marshaller_root_UserGetSelfRequest, __Marshaller_root_UserSelfResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserGetExtendedUsersByIdRequest, UserListResponse> __Method_GetExtendedUsersById = new Method<UserGetExtendedUsersByIdRequest, UserListResponse>(MethodType.Unary, __ServiceName, "GetExtendedUsersById", __Marshaller_root_UserGetExtendedUsersByIdRequest, __Marshaller_root_UserListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserGetExtendedUsersByUsernameRequest, UserListResponse> __Method_GetExtendedUsersByUsername = new Method<UserGetExtendedUsersByUsernameRequest, UserListResponse>(MethodType.Unary, __ServiceName, "GetExtendedUsersByUsername", __Marshaller_root_UserGetExtendedUsersByUsernameRequest, __Marshaller_root_UserListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSignInRequest, UserTokenResponse> __Method_SignIn = new Method<UserSignInRequest, UserTokenResponse>(MethodType.Unary, __ServiceName, "SignIn", __Marshaller_root_UserSignInRequest, __Marshaller_root_UserTokenResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetMaxOnlineStatusRequest, UserSetMaxOnlineStatusResponse> __Method_SetMaxOnlineStatus = new Method<UserSetMaxOnlineStatusRequest, UserSetMaxOnlineStatusResponse>(MethodType.Unary, __ServiceName, "SetMaxOnlineStatus", __Marshaller_root_UserSetMaxOnlineStatusRequest, __Marshaller_root_UserSetMaxOnlineStatusResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetDeviceOnlineStatusRequest, UserSetDeviceOnlineStatusResponse> __Method_SetDeviceOnlineStatus = new Method<UserSetDeviceOnlineStatusRequest, UserSetDeviceOnlineStatusResponse>(MethodType.Unary, __ServiceName, "SetDeviceOnlineStatus", __Marshaller_root_UserSetDeviceOnlineStatusRequest, __Marshaller_root_UserSetDeviceOnlineStatusResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserGetNewHubserverRequest, HubServerEndpointResponse> __Method_GetNewHubserverEndpoint = new Method<UserGetNewHubserverRequest, HubServerEndpointResponse>(MethodType.Unary, __ServiceName, "GetNewHubserverEndpoint", __Marshaller_root_UserGetNewHubserverRequest, __Marshaller_root_HubServerEndpointResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSignUpRequest, UserSignUpResponse> __Method_SignUp = new Method<UserSignUpRequest, UserSignUpResponse>(MethodType.Unary, __ServiceName, "SignUp", __Marshaller_root_UserSignUpRequest, __Marshaller_root_UserSignUpResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSignOutRequest, UserSignOutResponse> __Method_SignOut = new Method<UserSignOutRequest, UserSignOutResponse>(MethodType.Unary, __ServiceName, "SignOut", __Marshaller_root_UserSignOutRequest, __Marshaller_root_UserSignOutResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetPasswordRequest, UserSetPasswordResponse> __Method_SetPassword = new Method<UserSetPasswordRequest, UserSetPasswordResponse>(MethodType.Unary, __ServiceName, "SetPassword", __Marshaller_root_UserSetPasswordRequest, __Marshaller_root_UserSetPasswordResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserForgotPasswordRequest, UserForgotPasswordResponse> __Method_ForgotPassword = new Method<UserForgotPasswordRequest, UserForgotPasswordResponse>(MethodType.Unary, __ServiceName, "ForgotPassword", __Marshaller_root_UserForgotPasswordRequest, __Marshaller_root_UserForgotPasswordResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserForgotUsernameRequest, UserForgotUsernameResponse> __Method_ForgotUsername = new Method<UserForgotUsernameRequest, UserForgotUsernameResponse>(MethodType.Unary, __ServiceName, "ForgotUsername", __Marshaller_root_UserForgotUsernameRequest, __Marshaller_root_UserForgotUsernameResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserResetPasswordRequest, UserResetPasswordResponse> __Method_ResetPassword = new Method<UserResetPasswordRequest, UserResetPasswordResponse>(MethodType.Unary, __ServiceName, "ResetPassword", __Marshaller_root_UserResetPasswordRequest, __Marshaller_root_UserResetPasswordResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetEmailVerificationCodeRequest, UserSetEmailVerificationCodeResponse> __Method_SetEmailVerificationCode = new Method<UserSetEmailVerificationCodeRequest, UserSetEmailVerificationCodeResponse>(MethodType.Unary, __ServiceName, "SetEmailVerificationCode", __Marshaller_root_UserSetEmailVerificationCodeRequest, __Marshaller_root_UserSetEmailVerificationCodeResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserResendEmailVerificationCodeRequest, UserResendEmailVerificationCodeResponse> __Method_ResendEmailVerificationCode = new Method<UserResendEmailVerificationCodeRequest, UserResendEmailVerificationCodeResponse>(MethodType.Unary, __ServiceName, "ResendEmailVerificationCode", __Marshaller_root_UserResendEmailVerificationCodeRequest, __Marshaller_root_UserResendEmailVerificationCodeResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetUsernameRequest, UserSetUsernameResponse> __Method_SetUsername = new Method<UserSetUsernameRequest, UserSetUsernameResponse>(MethodType.Unary, __ServiceName, "SetUsername", __Marshaller_root_UserSetUsernameRequest, __Marshaller_root_UserSetUsernameResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetProfilePictureRequest, UserSetProfilePictureResponse> __Method_SetProfilePicture = new Method<UserSetProfilePictureRequest, UserSetProfilePictureResponse>(MethodType.Unary, __ServiceName, "SetProfilePicture", __Marshaller_root_UserSetProfilePictureRequest, __Marshaller_root_UserSetProfilePictureResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetEmailRequest, UserSetEmailResponse> __Method_SetEmail = new Method<UserSetEmailRequest, UserSetEmailResponse>(MethodType.Unary, __ServiceName, "SetEmail", __Marshaller_root_UserSetEmailRequest, __Marshaller_root_UserSetEmailResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetDirectMessageInviteRequirementRequest, UserSetDirectMessageInviteRequirementResponse> __Method_SetDirectMessageInviteRequirement = new Method<UserSetDirectMessageInviteRequirementRequest, UserSetDirectMessageInviteRequirementResponse>(MethodType.Unary, __ServiceName, "SetDirectMessageInviteRequirement", __Marshaller_root_UserSetDirectMessageInviteRequirementRequest, __Marshaller_root_UserSetDirectMessageInviteRequirementResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetFriendshipInviteRequirementRequest, UserSetFriendshipInviteRequirementResponse> __Method_SetFriendshipInviteRequirement = new Method<UserSetFriendshipInviteRequirementRequest, UserSetFriendshipInviteRequirementResponse>(MethodType.Unary, __ServiceName, "SetFriendshipInviteRequirement", __Marshaller_root_UserSetFriendshipInviteRequirementRequest, __Marshaller_root_UserSetFriendshipInviteRequirementResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserFindByUsernameRequest, UserShortListResponse> __Method_FindByUsername = new Method<UserFindByUsernameRequest, UserShortListResponse>(MethodType.Unary, __ServiceName, "FindByUsername", __Marshaller_root_UserFindByUsernameRequest, __Marshaller_root_UserShortListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserConnectionBetweenRequest, UserConnectionResponse> __Method_GetConnectionBetween = new Method<UserConnectionBetweenRequest, UserConnectionResponse>(MethodType.Unary, __ServiceName, "GetConnectionBetween", __Marshaller_root_UserConnectionBetweenRequest, __Marshaller_root_UserConnectionResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserGetNoteRequest, UserNoteResponse> __Method_GetUserNote = new Method<UserGetNoteRequest, UserNoteResponse>(MethodType.Unary, __ServiceName, "GetUserNote", __Marshaller_root_UserGetNoteRequest, __Marshaller_root_UserNoteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetNoteRequest, UserSetNoteResponse> __Method_SetUserNote = new Method<UserSetNoteRequest, UserSetNoteResponse>(MethodType.Unary, __ServiceName, "SetUserNote", __Marshaller_root_UserSetNoteRequest, __Marshaller_root_UserSetNoteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserSetMobileNotificationTokenRequest, UserSetMobileNotificationTokenResponse> __Method_SetMobileNotificationToken = new Method<UserSetMobileNotificationTokenRequest, UserSetMobileNotificationTokenResponse>(MethodType.Unary, __ServiceName, "SetMobileNotificationToken", __Marshaller_root_UserSetMobileNotificationTokenRequest, __Marshaller_root_UserSetMobileNotificationTokenResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserDeleteRequest, UserDeleteResponse> __Method_Delete = new Method<UserDeleteRequest, UserDeleteResponse>(MethodType.Unary, __ServiceName, "Delete", __Marshaller_root_UserDeleteRequest, __Marshaller_root_UserDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserBlockListRequest, UserBlockListResponse> __Method_BlockList = new Method<UserBlockListRequest, UserBlockListResponse>(MethodType.Unary, __ServiceName, "BlockList", __Marshaller_root_UserBlockListRequest, __Marshaller_root_UserBlockListResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserBlockCreateRequest, UserBlockCreateResponse> __Method_BlockCreate = new Method<UserBlockCreateRequest, UserBlockCreateResponse>(MethodType.Unary, __ServiceName, "BlockCreate", __Marshaller_root_UserBlockCreateRequest, __Marshaller_root_UserBlockCreateResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserBlockDeleteRequest, UserBlockDeleteResponse> __Method_BlockDelete = new Method<UserBlockDeleteRequest, UserBlockDeleteResponse>(MethodType.Unary, __ServiceName, "BlockDelete", __Marshaller_root_UserBlockDeleteRequest, __Marshaller_root_UserBlockDeleteResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserFlagRequest, UserFlagResponse> __Method_Flag = new Method<UserFlagRequest, UserFlagResponse>(MethodType.Unary, __ServiceName, "Flag", __Marshaller_root_UserFlagRequest, __Marshaller_root_UserFlagResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static readonly Method<UserRefreshTokenRequest, UserRefreshTokenResponse> __Method_RefreshToken = new Method<UserRefreshTokenRequest, UserRefreshTokenResponse>(MethodType.Unary, __ServiceName, "RefreshToken", __Marshaller_root_UserRefreshTokenRequest, __Marshaller_root_UserRefreshTokenResponse);

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static void __Helper_SerializeMessage(IMessage message, SerializationContext context)
	{
		if (message is IBufferMessage)
		{
			context.SetPayloadLength(message.CalculateSize());
			message.WriteTo(context.GetBufferWriter());
			context.Complete();
		}
		else
		{
			context.Complete(message.ToByteArray());
		}
	}

	[GeneratedCode("grpc_csharp_plugin", null)]
	private static T __Helper_DeserializeMessage<T>(DeserializationContext P_0, MessageParser<T> P_1) where T : IMessage<T>
	{
		if (__Helper_MessageCache<T>.IsBufferMessage)
		{
			return P_1.ParseFrom(P_0.PayloadAsReadOnlySequence());
		}
		return P_1.ParseFrom(P_0.PayloadAsNewBuffer());
	}
}
