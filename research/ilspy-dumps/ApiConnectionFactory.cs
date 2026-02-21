using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RootApp.Client.Domain.SecureStorage;
using RootApp.Core;
using RootApp.Utility;
using RootApp.Utility.HttpClientPolicy;
using RootApp.WebApi.Client.Shared.Turnstile;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Grpc.Services;

namespace RootApp.WebApi.Client.Shared.Implementations;

public sealed class ApiConnectionFactory : IApiConnectionFactory
{
	private static readonly ServiceConfig? _serviceConfig = new ServiceConfig
	{
		MethodConfigs = 
		{
			new MethodConfig
			{
				Names = { MethodName.Default },
				RetryPolicy = new RetryPolicy
				{
					MaxAttempts = 10,
					InitialBackoff = TimeSpan.FromMilliseconds(100L),
					MaxBackoff = TimeSpan.FromSeconds(5L),
					BackoffMultiplier = 1.5,
					RetryableStatusCodes = { StatusCode.Unavailable }
				}
			}
		}
	};

	private readonly IOptions<RootWebApiConfig> _apiOptions;

	private readonly CancellationToken _cancellationToken;

	private readonly IConnectionStatusService _connectionStatusService;

	private readonly IFireAndForgetHost _fireAndForgetHost;

	private readonly IRootHttpHandlers _httpHandlers;

	private readonly ILogger<ApiConnectionFactory> _logger;

	private readonly ILoggerFactory _loggerFactory;

	private readonly RootClientFactory _rootClientFactory;

	private readonly ISecureStorage _secureStorage;

	private readonly ITurnstileTokenProvider _turnstileTokenProvider;

	public ApiConnectionFactory(ISecureStorage secureStorage, IOptions<RootWebApiConfig> apiOptions, ILoggerFactory loggerFactory, RootClientFactory rootClientFactory, IRootHttpHandlers httpHandlers, IHostApplicationLifetime applicationLifetime, IConnectionStatusService connectionStatusService, IFireAndForgetHost fireAndForgetHost, ITurnstileTokenProvider turnstileTokenProvider)
	{
		_loggerFactory = loggerFactory;
		_rootClientFactory = rootClientFactory;
		_httpHandlers = httpHandlers;
		_connectionStatusService = connectionStatusService;
		_fireAndForgetHost = fireAndForgetHost;
		_logger = _loggerFactory.CreateLogger<ApiConnectionFactory>();
		_secureStorage = secureStorage;
		_apiOptions = apiOptions;
		_cancellationToken = applicationLifetime.ApplicationStopping;
		_turnstileTokenProvider = turnstileTokenProvider;
		LogUsingApiUrl(_apiOptions.Value.WebApiUrl);
	}

	public async Task<IApiConnection?> GetConnectionAsync(CancellationToken cancellationToken)
	{
		string authToken = await _secureStorage.GetAsync("AuthToken");
		return (!string.IsNullOrEmpty(authToken)) ? (await GetConnectionAsync(authToken, cancellationToken)) : null;
	}

	public async Task SignUpAsync(string emailAddress, string userName, string password, string? accessToken, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(emailAddress))
		{
			throw new ArgumentException("Value cannot be null or empty.", "emailAddress");
		}
		if (string.IsNullOrEmpty(userName))
		{
			throw new ArgumentException("Value cannot be null or empty.", "userName");
		}
		if (string.IsNullOrEmpty(password))
		{
			throw new ArgumentException("Value cannot be null or empty.", "password");
		}
		UserSignUpRequest request = new UserSignUpRequest
		{
			Username = userName,
			Password = password,
			Email = emailAddress,
			AccessToken = accessToken
		};
		await ExecuteWithTurnstileRetryAsync("signup", (UserGrpcService.UserGrpcServiceClient client) => client.SignUpAsync(request, null, null, cancellationToken), delegate(string token)
		{
			request.TurnstileToken = token;
		}, cancellationToken);
	}

	public async Task<IApiConnection?> SignInAsync(string username, string password, bool persistLogin, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(username))
		{
			throw new ArgumentException("Invalid username", "username");
		}
		UserSignInRequest request = CreateUserSignInRequest(username, password);
		if (persistLogin)
		{
			string authToken = await _secureStorage.GetAsync("AuthToken");
			if (!string.IsNullOrEmpty(authToken))
			{
				ClientToken token = ClientToken.Parse(authToken);
				if ((object)token != null)
				{
					request.DeviceId = token.DeviceId;
				}
			}
		}
		string lastChallengeUrl = null;
		RpcException lastRpcException = null;
		foreach (TimeSpan delay in TurnstileRetryPolicy.RetryDelays)
		{
			if (delay > TimeSpan.Zero)
			{
				await Task.Delay(delay, cancellationToken);
			}
			using GrpcChannel channel = CreateGrpcClient(null).grpcChannel;
			UserGrpcService.UserGrpcServiceClient client = new UserGrpcService.UserGrpcServiceClient(channel);
			HttpClient httpClient = null;
			AsyncUnaryCall<UserTokenResponse> signInCall = null;
			try
			{
				signInCall = client.SignInAsync(request, null, null, cancellationToken);
				UserTokenResponse tokenResponse = await signInCall;
				ClientToken clientToken = ClientToken.Parse(tokenResponse.ClientToken);
				if ((object)clientToken == null)
				{
					LogInvalidClientTokenReceivedFromServer();
					return null;
				}
				if (persistLogin)
				{
					await _secureStorage.SetAsync("AuthToken", clientToken.Token);
				}
				TokenResponse tResponse = new TokenResponse
				{
					ClientToken = clientToken,
					HubServerInfo = tokenResponse.HubServerInfo
				};
				httpClient = CreateHttpClient();
				SetAuthentication(httpClient, clientToken);
				var (grpcChannel, interceptor) = CreateGrpcClient(clientToken.Token);
				if (interceptor == null)
				{
					throw new InvalidOperationException("Interceptor is null after signing in.");
				}
				ApiConnection result = new ApiConnection(httpClient, grpcChannel, interceptor, tResponse, _loggerFactory, _cancellationToken);
				httpClient = null;
				return result;
			}
			catch (RpcException ex) when (signInCall != null && ex.Status.StatusCode == StatusCode.FailedPrecondition)
			{
				string challengeUrl = (await signInCall.ResponseHeadersAsync).GetValue("turnstile-challenge-url");
				if (string.IsNullOrEmpty(challengeUrl))
				{
					LogUnableToSignIn(ex);
					throw;
				}
				lastChallengeUrl = challengeUrl;
				lastRpcException = ex;
				LogTurnstileRequired("signin", challengeUrl);
				string turnstileToken = await AcquireTurnstileTokenAsync(challengeUrl, "signin", cancellationToken);
				if (string.IsNullOrEmpty(turnstileToken))
				{
					LogTurnstileTokenNotProvided("signin");
					throw new TurnstileRequiredException(challengeUrl, "signin", ex);
				}
				request.TurnstileToken = turnstileToken;
			}
			catch (Exception ex2)
			{
				Exception ex3 = ex2;
				LogUnableToSignIn(ex3);
				return null;
			}
			finally
			{
				httpClient?.Dispose();
			}
		}
		LogTurnstileMaxRetriesExceeded("signin");
		throw new TurnstileRequiredException(lastChallengeUrl, "signin", lastRpcException);
	}

	public Task<UserForgotPasswordResponse> ForgotPasswordAsync(UserForgotPasswordRequest request, CancellationToken cancellationToken)
	{
		return ExecuteWithTurnstileRetryAsync("forgot_password", (UserGrpcService.UserGrpcServiceClient client) => client.ForgotPasswordAsync(request, null, null, cancellationToken), delegate(string token)
		{
			request.TurnstileToken = token;
		}, cancellationToken);
	}

	public Task<UserResetPasswordResponse> ResetPasswordAsync(UserResetPasswordRequest request, CancellationToken cancellationToken)
	{
		return ExecuteWithTurnstileRetryAsync("reset_password", (UserGrpcService.UserGrpcServiceClient client) => client.ResetPasswordAsync(request, null, null, cancellationToken), delegate(string token)
		{
			request.TurnstileToken = token;
		}, cancellationToken);
	}

	public Task<UserForgotUsernameResponse> ForgotUsernameAsync(UserForgotUsernameRequest request, CancellationToken cancellationToken)
	{
		return ExecuteWithTurnstileRetryAsync("forgot_username", (UserGrpcService.UserGrpcServiceClient client) => client.ForgotUsernameAsync(request, null, null, cancellationToken), delegate(string token)
		{
			request.TurnstileToken = token;
		}, cancellationToken);
	}

	public async ValueTask UpdateBearerTokenAsync(IApiConnection connection, string bearerToken, bool persistLogin, CancellationToken cancellationToken)
	{
		ApiConnection apiConnection = (ApiConnection)connection;
		ClientToken clientToken = ClientToken.Parse(bearerToken);
		if ((object)clientToken == null)
		{
			throw new ArgumentException("Invalid bearer token", "bearerToken");
		}
		if (persistLogin)
		{
			await _secureStorage.SetAsync("AuthToken", clientToken.Token);
		}
		apiConnection.ApiAuthorization.Authorization = "Bearer " + clientToken.Token;
		HttpClient httpClient = apiConnection.HttpClient;
		SetAuthentication(httpClient, clientToken);
	}

	private async Task<TResponse> ExecuteWithTurnstileRetryAsync<TResponse>(string action, Func<UserGrpcService.UserGrpcServiceClient, AsyncUnaryCall<TResponse>> executeCall, Action<string> setTurnstileToken, CancellationToken cancellationToken)
	{
		string lastChallengeUrl = null;
		RpcException lastRpcException = null;
		foreach (TimeSpan delay in TurnstileRetryPolicy.RetryDelays)
		{
			if (delay > TimeSpan.Zero)
			{
				await Task.Delay(delay, cancellationToken);
			}
			using GrpcChannel grpcClient = CreateGrpcClient(null).grpcChannel;
			UserGrpcService.UserGrpcServiceClient client = new UserGrpcService.UserGrpcServiceClient(grpcClient);
			AsyncUnaryCall<TResponse> call = null;
			try
			{
				call = executeCall(client);
				return await call;
			}
			catch (RpcException ex) when (call != null && ex.Status.StatusCode == StatusCode.FailedPrecondition)
			{
				string challengeUrl = (await call.ResponseHeadersAsync).GetValue("turnstile-challenge-url");
				if (string.IsNullOrEmpty(challengeUrl))
				{
					throw;
				}
				lastChallengeUrl = challengeUrl;
				lastRpcException = ex;
				LogTurnstileRequired(action, challengeUrl);
				string turnstileToken = await AcquireTurnstileTokenAsync(challengeUrl, action, cancellationToken);
				if (string.IsNullOrEmpty(turnstileToken))
				{
					LogTurnstileTokenNotProvided(action);
					throw new TurnstileRequiredException(challengeUrl, action, ex);
				}
				setTurnstileToken(turnstileToken);
			}
		}
		LogTurnstileMaxRetriesExceeded(action);
		throw new TurnstileRequiredException(lastChallengeUrl, action, lastRpcException);
	}

	private async Task<string?> AcquireTurnstileTokenAsync(string challengeUrl, string action, CancellationToken cancellationToken)
	{
		LogTurnstileAcquiringToken(action);
		return await _turnstileTokenProvider.GetTokenAsync(challengeUrl, action, cancellationToken);
	}

	public static UserSignInRequest CreateUserSignInRequest(string username, string password)
	{
		return new UserSignInRequest
		{
			Username = username,
			Password = password,
			UserDeviceDescription = new UserDeviceDescription
			{
				Os = GetPlatformName(),
				OsVersion = RuntimeInformation.OSDescription,
				DeviceName = Environment.MachineName
			}
		};
	}

	private async Task<IApiConnection> GetConnectionAsync(string bearerToken, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(bearerToken, "bearerToken");
		ClientToken clientToken = ClientToken.Parse(bearerToken) ?? throw new ArgumentException("Invalid bearer token", "bearerToken");
		HttpClient httpClient = CreateHttpClient();
		SetAuthentication(httpClient, clientToken);
		var (grpcChannel, interceptor) = CreateGrpcClient(clientToken.Token);
		if (interceptor == null)
		{
			throw new InvalidOperationException("Interceptor is null after signing in.");
		}
		try
		{
			UserGrpcService.UserGrpcServiceClient userClient = new UserGrpcService.UserGrpcServiceClient(grpcChannel);
			HubServerEndpointResponse response = await userClient.GetNewHubserverEndpointAsync(new UserGetNewHubserverRequest(), null, DateTime.UtcNow.AddSeconds(10.0), cancellationToken);
			TokenResponse tokenResponse = new TokenResponse
			{
				ClientToken = clientToken,
				HubServerInfo = response.HubServerInfo
			};
			return new ApiConnection(httpClient, grpcChannel, interceptor, tokenResponse, _loggerFactory, _cancellationToken);
		}
		catch (Exception)
		{
			httpClient.Dispose();
			grpcChannel.Dispose();
			throw;
		}
	}

	private HttpClient CreateHttpClient()
	{
		return _rootClientFactory.CreateWebApiHttpClient<ApiConnection>();
	}

	private (GrpcChannel grpcChannel, ApiAuthorizationInterceptor? interceptor) CreateGrpcClient(string? authToken)
	{
		Uri webApiUrl = _apiOptions.Value.WebApiUrl;
		(GrpcChannelOptions options, ApiAuthorizationInterceptor? interceptor) tuple = CreateGrpcOptions(authToken);
		GrpcChannelOptions item = tuple.options;
		ApiAuthorizationInterceptor item2 = tuple.interceptor;
		GrpcChannel grpcChannel = GrpcChannel.ForAddress(webApiUrl, item);
		if (authToken != null)
		{
			_fireAndForgetHost.AddTask(Task.Run(() => ChannelMonitorAsync(grpcChannel, "AuthenticatedConnection"), _cancellationToken));
		}
		return (grpcChannel: grpcChannel, interceptor: item2);
	}

	private (GrpcChannelOptions options, ApiAuthorizationInterceptor? interceptor) CreateGrpcOptions(string? authToken)
	{
		GrpcChannelOptions grpcChannelOptions = new GrpcChannelOptions
		{
			ServiceConfig = _serviceConfig,
			HttpHandler = new GrpcWebHandler(_httpHandlers.GrpcHandler)
		};
		if (authToken == null)
		{
			return (options: grpcChannelOptions, interceptor: null);
		}
		ApiAuthorizationInterceptor apiAuthorizationInterceptor = new ApiAuthorizationInterceptor("Bearer " + authToken);
		grpcChannelOptions.Credentials = ChannelCredentials.Create(ChannelCredentials.SecureSsl, apiAuthorizationInterceptor.Credentials);
		return (options: grpcChannelOptions, interceptor: apiAuthorizationInterceptor);
	}

	private static void SetAuthentication(HttpClient httpClient, ClientToken token)
	{
		ArgumentNullException.ThrowIfNull(httpClient, "httpClient");
		ArgumentNullException.ThrowIfNull(token, "token");
		HttpRequestHeaders defaultRequestHeaders = httpClient.DefaultRequestHeaders;
		string text = "x-root-Device-Id";
		defaultRequestHeaders.Remove(text);
		defaultRequestHeaders.Add(text, token.DeviceId);
		defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
	}

	private async Task ChannelMonitorAsync(GrpcChannel channel, string? reason)
	{
		string tag = (string.IsNullOrWhiteSpace(reason) ? "" : (" [" + reason + "]"));
		ConnectivityState prev = channel.State;
		LogGrpcChannelState(tag, prev);
		while (!_cancellationToken.IsCancellationRequested && prev != ConnectivityState.Shutdown)
		{
			try
			{
				await channel.WaitForStateChangedAsync(prev, _cancellationToken);
				ConnectivityState current = channel.State;
				switch (current)
				{
				case ConnectivityState.TransientFailure:
					_connectionStatusService.SetGrpcServerConnectionStatus(isConnected: false);
					LogGrpcChannelDisconnected(tag, current);
					break;
				case ConnectivityState.Ready:
					_connectionStatusService.SetGrpcServerConnectionStatus(isConnected: true);
					LogGrpcChannelReconnected(tag, current);
					break;
				case ConnectivityState.Connecting:
					LogGrpcChannelConnecting(tag, current);
					break;
				case ConnectivityState.Idle:
					LogGrpcChannelIdle(tag, current);
					break;
				case ConnectivityState.Shutdown:
					_connectionStatusService.SetGrpcServerConnectionStatus(isConnected: false);
					LogGrpcChannelShutdown(tag, current);
					break;
				}
				prev = current;
			}
			catch (OperationCanceledException)
			{
				break;
			}
			catch (ObjectDisposedException)
			{
				break;
			}
			catch (Exception ex3)
			{
				LogErrorWhileMonitoringGrpcChannelState(ex3);
				try
				{
					await Task.Delay(500, _cancellationToken);
				}
				catch
				{
				}
			}
		}
	}

	private static string GetPlatformName()
	{
		if (OperatingSystem.IsWindows())
		{
			return "Windows";
		}
		if (OperatingSystem.IsLinux())
		{
			return "Linux";
		}
		if (OperatingSystem.IsMacOS())
		{
			return "macOS";
		}
		if (OperatingSystem.IsFreeBSD())
		{
			return "FreeBSD";
		}
		return "Unknown";
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Error while monitoring gRPC channel state")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogErrorWhileMonitoringGrpcChannelState(Exception ex)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Error while monitoring gRPC channel state");
		_logger.Log(LogLevel.Error, new EventId(696529968, "LogErrorWhileMonitoringGrpcChannelState"), threadLocalState, ex, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Error while monitoring gRPC channel state");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "gRPC channel connecting{Tag}: {State}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGrpcChannelConnecting(string tag, ConnectivityState state)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "gRPC channel connecting{Tag}: {State}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Tag", tag);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("State", state);
			_logger.Log(LogLevel.Information, new EventId(1954346954, "LogGrpcChannelConnecting"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(25, 2, invariantCulture);
				handler.AppendLiteral("gRPC channel connecting");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "gRPC channel DISCONNECTED{Tag}: {State}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGrpcChannelDisconnected(string tag, ConnectivityState state)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "gRPC channel DISCONNECTED{Tag}: {State}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Tag", tag);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("State", state);
			_logger.Log(LogLevel.Information, new EventId(999854139, "LogGrpcChannelDisconnected"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(27, 2, invariantCulture);
				handler.AppendLiteral("gRPC channel DISCONNECTED");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "gRPC channel idle{Tag}: {State}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGrpcChannelIdle(string tag, ConnectivityState state)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "gRPC channel idle{Tag}: {State}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Tag", tag);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("State", state);
			_logger.Log(LogLevel.Information, new EventId(505873410, "LogGrpcChannelIdle"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(19, 2, invariantCulture);
				handler.AppendLiteral("gRPC channel idle");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "gRPC channel reconnected{Tag}: {State}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGrpcChannelReconnected(string tag, ConnectivityState state)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "gRPC channel reconnected{Tag}: {State}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Tag", tag);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("State", state);
			_logger.Log(LogLevel.Information, new EventId(955560812, "LogGrpcChannelReconnected"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(26, 2, invariantCulture);
				handler.AppendLiteral("gRPC channel reconnected");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "gRPC channel shutdown{Tag}: {State}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGrpcChannelShutdown(string tag, ConnectivityState state)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "gRPC channel shutdown{Tag}: {State}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Tag", tag);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("State", state);
			_logger.Log(LogLevel.Information, new EventId(2053407322, "LogGrpcChannelShutdown"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(23, 2, invariantCulture);
				handler.AppendLiteral("gRPC channel shutdown");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "gRPC channel state{Tag}: {State}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGrpcChannelState(string tag, ConnectivityState state)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "gRPC channel state{Tag}: {State}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Tag", tag);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("State", state);
			_logger.Log(LogLevel.Information, new EventId(1305242821, "LogGrpcChannelState"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(20, 2, invariantCulture);
				handler.AppendLiteral("gRPC channel state");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Invalid client token received from server.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInvalidClientTokenReceivedFromServer()
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Invalid client token received from server.");
			_logger.Log(LogLevel.Warning, new EventId(1847226444, "LogInvalidClientTokenReceivedFromServer"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Invalid client token received from server.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Unable to sign in")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnableToSignIn(Exception ex)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Unable to sign in");
			_logger.Log(LogLevel.Warning, new EventId(1040215029, "LogUnableToSignIn"), threadLocalState, ex, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Unable to sign in");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Using API URL {Url}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUsingApiUrl(Uri url)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Using API URL {Url}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", url);
			_logger.Log(LogLevel.Information, new EventId(94390414, "LogUsingApiUrl"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(14, 1, invariantCulture);
				handler.AppendLiteral("Using API URL ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Turnstile challenge required for {Action}, URL: {ChallengeUrl}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileRequired(string action, string challengeUrl)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge required for {Action}, URL: {ChallengeUrl}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Action", action);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("ChallengeUrl", challengeUrl);
			_logger.Log(LogLevel.Information, new EventId(1392718302, "LogTurnstileRequired"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(40, 2, invariantCulture);
				handler.AppendLiteral("Turnstile challenge required for ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(", URL: ");
				handler.AppendFormatted<object>(obj2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Acquiring Turnstile token for {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileAcquiringToken(string action)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Acquiring Turnstile token for {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", action);
			_logger.Log(LogLevel.Debug, new EventId(1738565037, "LogTurnstileAcquiringToken"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(30, 1, invariantCulture);
				handler.AppendLiteral("Acquiring Turnstile token for ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Turnstile max retries exceeded for {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileMaxRetriesExceeded(string action)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile max retries exceeded for {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", action);
			_logger.Log(LogLevel.Warning, new EventId(32017062, "LogTurnstileMaxRetriesExceeded"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(35, 1, invariantCulture);
				handler.AppendLiteral("Turnstile max retries exceeded for ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Turnstile token not provided for {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileTokenNotProvided(string action)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile token not provided for {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", action);
			_logger.Log(LogLevel.Warning, new EventId(420624486, "LogTurnstileTokenNotProvided"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(33, 1, invariantCulture);
				handler.AppendLiteral("Turnstile token not provided for ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
