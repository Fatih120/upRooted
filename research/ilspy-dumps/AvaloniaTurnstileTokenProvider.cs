using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Turnstile;
using RootApp.WebApi.Client.Shared;

namespace RootApp.Browser.Turnstile;

public sealed class AvaloniaTurnstileTokenProvider : ITurnstileTokenProvider
{
	private readonly BrowserService _browserService;

	private readonly ILogger<AvaloniaTurnstileTokenProvider> _logger;

	private readonly TurnstileVerificationViewModelFactory _turnstileFactory;

	public AvaloniaTurnstileTokenProvider(BrowserService P_0, TurnstileVerificationViewModelFactory P_1, ILogger<AvaloniaTurnstileTokenProvider> P_2)
	{
		_browserService = P_0;
		_logger = P_2;
		_turnstileFactory = P_1;
		base._002Ector();
	}

	public async Task<string?> GetTokenAsync(string P_0, string P_1, CancellationToken P_2)
	{
		LogTurnstileChallengeRequested(P_1);
		TurnstileBrowser browser = null;
		TurnstileVerificationViewModel overlayVm = null;
		try
		{
			string separator = (P_0.Contains('?') ? "&" : "?");
			string fullChallengeUrl = P_0 + separator + "bridge=desktop";
			LogStartingSilentVerification(P_1);
			browser = await _browserService.CreateTurnstileBrowserAsync(fullChallengeUrl, P_2);
			if (browser == null)
			{
				LogFailedToCreateBrowser(P_1);
				return null;
			}
			TaskCompletionSource interactionRequiredTcs = new TaskCompletionSource();
			browser.InteractionRequired += delegate
			{
				LogInteractionRequired(P_1);
				interactionRequiredTcs.TrySetResult();
			};
			LogWaitingForSilentOrInteraction(P_1);
			Task<string> tokenTask = browser.WaitForTokenAsync(TimeSpan.FromMinutes(5L), P_2);
			Task interactionTask = interactionRequiredTcs.Task;
			if (await Task.WhenAny(tokenTask, interactionTask) == tokenTask)
			{
				if (tokenTask.IsFaulted)
				{
					LogSilentVerificationFaulted(tokenTask.Exception?.InnerException ?? tokenTask.Exception, P_1);
					throw tokenTask.Exception.InnerException ?? tokenTask.Exception;
				}
				string token = await tokenTask;
				LogSilentVerificationSucceeded(P_1);
				return token;
			}
			LogInteractionWon(P_1);
			LogShowingOverlay(P_1);
			TaskCompletionSource<string?> resultSource = new TaskCompletionSource<string>();
			overlayVm = _turnstileFactory.CreateWithBrowser(browser, resultSource);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(overlayVm));
			string result2;
			await using (P_2.Register(delegate
			{
				LogTurnstileChallengeCancelledViaToken(P_1);
				overlayVm.Cancel();
			}))
			{
				string result = await resultSource.Task;
				if (result != null)
				{
					LogTurnstileTokenAcquired(P_1);
				}
				else
				{
					LogTurnstileChallengeReturnedNull(P_1);
				}
				result2 = result;
			}
			return result2;
		}
		catch (OperationCanceledException)
		{
			LogTurnstileChallengeCancelled(P_1);
			return null;
		}
		catch (TurnstileException ex2)
		{
			TurnstileException ex3 = ex2;
			LogTurnstileChallengeError(ex3, P_1);
			return null;
		}
		catch (Exception ex4)
		{
			Exception ex5 = ex4;
			LogTurnstileChallengeError(ex5, P_1);
			return null;
		}
		finally
		{
			if (browser != null && overlayVm == null)
			{
				_browserService.RemoveTurnstileBrowser(browser.Id);
			}
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge requested for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileChallengeRequested(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge requested for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1274767420, "LogTurnstileChallengeRequested"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(42, 1, invariantCulture);
				handler.AppendLiteral("Turnstile challenge requested for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Starting silent Turnstile verification for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogStartingSilentVerification(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Starting silent Turnstile verification for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1446012833, "LogStartingSilentVerification"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(51, 1, invariantCulture);
				handler.AppendLiteral("Starting silent Turnstile verification for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Failed to create Turnstile browser for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogFailedToCreateBrowser(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Failed to create Turnstile browser for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Warning, new EventId(1283129149, "LogFailedToCreateBrowser"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(47, 1, invariantCulture);
				handler.AppendLiteral("Failed to create Turnstile browser for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Waiting for silent token or interaction required for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogWaitingForSilentOrInteraction(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Waiting for silent token or interaction required for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(941183067, "LogWaitingForSilentOrInteraction"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(61, 1, invariantCulture);
				handler.AppendLiteral("Waiting for silent token or interaction required for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile requires user interaction for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInteractionRequired(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile requires user interaction for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1803702186, "LogInteractionRequired"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(48, 1, invariantCulture);
				handler.AppendLiteral("Turnstile requires user interaction for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Silent Turnstile verification succeeded for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogSilentVerificationSucceeded(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Silent Turnstile verification succeeded for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(898561616, "LogSilentVerificationSucceeded"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(52, 1, invariantCulture);
				handler.AppendLiteral("Silent Turnstile verification succeeded for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Silent Turnstile verification faulted for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogSilentVerificationFaulted(Exception P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Silent Turnstile verification faulted for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_1);
			_logger.Log(LogLevel.Warning, new EventId(101326356, "LogSilentVerificationFaulted"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(50, 1, invariantCulture);
				handler.AppendLiteral("Silent Turnstile verification faulted for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Interaction required won the race for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInteractionWon(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Interaction required won the race for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1964363647, "LogInteractionWon"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(46, 1, invariantCulture);
				handler.AppendLiteral("Interaction required won the race for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Showing Turnstile overlay for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogShowingOverlay(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Showing Turnstile overlay for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(817234464, "LogShowingOverlay"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(38, 1, invariantCulture);
				handler.AppendLiteral("Showing Turnstile overlay for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge cancelled via token for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileChallengeCancelledViaToken(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge cancelled via token for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1483211962, "LogTurnstileChallengeCancelledViaToken"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(52, 1, invariantCulture);
				handler.AppendLiteral("Turnstile challenge cancelled via token for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile token acquired for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileTokenAcquired(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile token acquired for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(385816480, "LogTurnstileTokenAcquired"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(37, 1, invariantCulture);
				handler.AppendLiteral("Turnstile token acquired for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge returned null for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileChallengeReturnedNull(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge returned null for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(101910742, "LogTurnstileChallengeReturnedNull"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(46, 1, invariantCulture);
				handler.AppendLiteral("Turnstile challenge returned null for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge cancelled for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileChallengeCancelled(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge cancelled for action: {Action}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1522405369, "LogTurnstileChallengeCancelled"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(42, 1, invariantCulture);
				handler.AppendLiteral("Turnstile challenge cancelled for action: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Unexpected error during Turnstile challenge for action: {Action}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTurnstileChallengeError(Exception P_0, string P_1)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Unexpected error during Turnstile challenge for action: {Action}");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Action", P_1);
		_logger.Log(LogLevel.Error, new EventId(1362416314, "LogTurnstileChallengeError"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object obj = s.TagArray[0].Value ?? "(null)";
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(56, 1, invariantCulture);
			handler.AppendLiteral("Unexpected error during Turnstile challenge for action: ");
			handler.AppendFormatted<object>(obj);
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}
}
