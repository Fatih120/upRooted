using System.Threading;
using System.Threading.Tasks;
using RootApp.WebApi.Client.Shared;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.WebApi.Client;

public interface IApiConnectionFactory
{
	Task SignUpAsync(string emailAddress, string userName, string password, string? accessToken, CancellationToken cancellationToken = default(CancellationToken));

	Task<IApiConnection?> SignInAsync(string username, string password, bool persistLogin, CancellationToken cancellationToken = default(CancellationToken));

	Task<IApiConnection?> GetConnectionAsync(CancellationToken cancellationToken = default(CancellationToken));

	Task<UserForgotPasswordResponse> ForgotPasswordAsync(UserForgotPasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));

	Task<UserResetPasswordResponse> ResetPasswordAsync(UserResetPasswordRequest request, CancellationToken cancellationToken);

	Task<UserForgotUsernameResponse> ForgotUsernameAsync(UserForgotUsernameRequest request, CancellationToken cancellationToken = default(CancellationToken));

	ValueTask UpdateBearerTokenAsync(IApiConnection connection, string bearerToken, bool persistLogin, CancellationToken cancellationToken);
}
