using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using Microsoft.Extensions.Logging;

namespace RootApp.Browser.Turnstile;

public sealed class TurnstileToNativeBridge : IDisposable
{
	private readonly CancellationTokenRegistration _cancellationRegistration;

	private readonly ILogger _logger;

	private readonly TaskCompletionSource<string> _tokenSource;

	private readonly TaskCompletionSource _interactionRequiredSource;

	private bool _disposed;

	[CompilerGenerated]
	private IJsObject? <JsObject>k__BackingField;

	[CompilerGenerated]
	private Action? m_InteractionRequired;

	[CompilerGenerated]
	private Action? InteractionCompleted;

	public IJsObject? JsObject
	{
		[CompilerGenerated]
		get
		{
			return <JsObject>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<JsObject>k__BackingField = jsObject;
		}
	}

	public Task<string> TokenTask => _tokenSource.Task;

	public event Action? InteractionRequired
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_InteractionRequired;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_InteractionRequired, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_InteractionRequired;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_InteractionRequired, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public TurnstileToNativeBridge(ILogger P_0, CancellationToken P_1 = default(CancellationToken))
	{
		TurnstileToNativeBridge turnstileToNativeBridge = this;
		_logger = P_0;
		_tokenSource = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
		_interactionRequiredSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
		_cancellationRegistration = P_1.Register(delegate
		{
			turnstileToNativeBridge._tokenSource.TrySetCanceled(P_1);
			turnstileToNativeBridge._interactionRequiredSource.TrySetCanceled(P_1);
		});
	}

	public void AttachToFrame(IFrame P_0)
	{
		if (JsObject == null)
		{
			JsObject = P_0.ParseJsonString<IJsObject>("{}");
			JsObject.Properties["postMessage"] = new Action<string>(OnMessage);
		}
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
			_cancellationRegistration.Dispose();
			_tokenSource.TrySetCanceled();
			_interactionRequiredSource.TrySetCanceled();
		}
	}

	private void OnMessage(string json)
	{
		if (_disposed)
		{
			return;
		}
		try
		{
			using JsonDocument jsonDocument = JsonDocument.Parse(json);
			JsonElement rootElement = jsonDocument.RootElement;
			if (!rootElement.TryGetProperty("type", out var jsonElement))
			{
				_logger.LogWarning("Turnstile bridge message missing 'type' field: {Json}", json);
				return;
			}
			string text = jsonElement.GetString();
			switch (text)
			{
			case "success":
			{
				string text5 = rootElement.GetProperty("token").GetString();
				LogChallengeSucceeded();
				_tokenSource.TrySetResult(text5);
				break;
			}
			case "error":
			{
				JsonElement jsonElement4;
				string text4 = (rootElement.TryGetProperty("errorCode", out jsonElement4) ? (jsonElement4.GetString() ?? "unknown") : "unknown");
				LogChallengeFailed(text4);
				_tokenSource.TrySetException(new TurnstileException("Turnstile verification failed: " + text4));
				break;
			}
			case "expired":
				LogChallengeExpired();
				_tokenSource.TrySetException(new TurnstileExpiredException("Turnstile verification expired"));
				break;
			case "timeout":
				LogChallengeTimedOut();
				_tokenSource.TrySetException(new TurnstileTimeoutException("Turnstile verification timed out"));
				break;
			case "pageReady":
				LogPageReady();
				break;
			case "beforeInteractive":
				LogBeforeInteractive();
				_interactionRequiredSource.TrySetResult();
				this.InteractionRequired?.Invoke();
				break;
			case "afterInteractive":
				LogAfterInteractive();
				InteractionCompleted?.Invoke();
				break;
			case "console":
			{
				JsonElement jsonElement2;
				string text2 = (rootElement.TryGetProperty("level", out jsonElement2) ? (jsonElement2.GetString() ?? "log") : "log");
				JsonElement jsonElement3;
				string text3 = (rootElement.TryGetProperty("message", out jsonElement3) ? (jsonElement3.GetString() ?? "") : "");
				LogJsConsoleMessage(text2, text3);
				break;
			}
			default:
				_logger.LogWarning("Unknown Turnstile message type: {Type}", text);
				break;
			}
		}
		catch (JsonException ex)
		{
			_logger.LogError(ex, "Failed to parse Turnstile bridge message: {Json}", json);
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge succeeded")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogChallengeSucceeded()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge succeeded");
			_logger.Log(LogLevel.Debug, new EventId(1655880303, "LogChallengeSucceeded"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile challenge succeeded");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Turnstile challenge failed with error: {ErrorCode}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogChallengeFailed(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge failed with error: {ErrorCode}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("ErrorCode", P_0);
			_logger.Log(LogLevel.Warning, new EventId(191993165, "LogChallengeFailed"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(39, 1, invariantCulture);
				handler.AppendLiteral("Turnstile challenge failed with error: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Turnstile challenge expired")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogChallengeExpired()
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge expired");
			_logger.Log(LogLevel.Warning, new EventId(803816349, "LogChallengeExpired"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile challenge expired");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Turnstile challenge timed out")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogChallengeTimedOut()
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge timed out");
			_logger.Log(LogLevel.Warning, new EventId(2069012609, "LogChallengeTimedOut"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile challenge timed out");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge page ready")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPageReady()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge page ready");
			_logger.Log(LogLevel.Debug, new EventId(1769070631, "LogPageReady"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile challenge page ready");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile challenge requires user interaction")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogBeforeInteractive()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile challenge requires user interaction");
			_logger.Log(LogLevel.Debug, new EventId(1218750998, "LogBeforeInteractive"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile challenge requires user interaction");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile user interaction completed, awaiting result")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogAfterInteractive()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile user interaction completed, awaiting result");
			_logger.Log(LogLevel.Debug, new EventId(1495185445, "LogAfterInteractive"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile user interaction completed, awaiting result");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile JS console [{Level}]: {Message}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogJsConsoleMessage(string P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile JS console [{Level}]: {Message}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Level", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Message", P_1);
			_logger.Log(LogLevel.Debug, new EventId(1666960782, "LogJsConsoleMessage"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(25, 2, invariantCulture);
				handler.AppendLiteral("Turnstile JS console [");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("]: ");
				handler.AppendFormatted<object>(obj2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
