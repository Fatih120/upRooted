using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Browser.Handlers;
using DotNetBrowser.Engine;
using DotNetBrowser.Handlers;
using DotNetBrowser.Js;
using DotNetBrowser.Navigation;
using Microsoft.Extensions.Logging;
using RootApp.Core.Identifiers;

namespace RootApp.Browser.Turnstile;

public sealed class TurnstileBrowser : IRootBrowser, IDisposable
{
	private readonly string _challengeUrl;

	private readonly IEngine _engine;

	private readonly ILogger<TurnstileBrowser> _logger;

	private readonly ILoggerFactory _loggerFactory;

	private bool _disposed;

	[CompilerGenerated]
	private TurnstileToNativeBridge? _003CBridge_003Ek__BackingField;

	[CompilerGenerated]
	private Action? m_InteractionRequired;

	public TurnstileToNativeBridge? Bridge
	{
		[CompilerGenerated]
		get
		{
			return _003CBridge_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CBridge_003Ek__BackingField = turnstileToNativeBridge;
		}
	}

	public RootGuid Id { get; }

	public IBrowser DotNetBrowser { get; }

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

	public TurnstileBrowser(RootGuid P_0, IBrowser P_1, IEngine P_2, string P_3, ILoggerFactory P_4)
	{
		_challengeUrl = P_3;
		_engine = P_2;
		_logger = P_4.CreateLogger<TurnstileBrowser>();
		_loggerFactory = P_4;
		Id = P_0;
		DotNetBrowser = P_1;
		base._002Ector();
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
			Bridge?.Dispose();
			DotNetBrowser.Dispose();
			_engine.Dispose();
			LogBrowserDisposed();
		}
	}

	public async Task InitializeAsync(CancellationToken P_0)
	{
		LogLoadingChallengePage(_challengeUrl);
		Bridge = new TurnstileToNativeBridge(_loggerFactory.CreateLogger<TurnstileToNativeBridge>(), P_0);
		Bridge.InteractionRequired += delegate
		{
			this.InteractionRequired?.Invoke();
		};
		DotNetBrowser.InjectJsHandler = new Handler<InjectJsParameters>(delegate(InjectJsParameters p)
		{
			if (p.Frame.IsMain)
			{
				IJsObject result2 = p.Frame.ExecuteJavaScript<IJsObject>("window").Result;
				Bridge.AttachToFrame(p.Frame);
				result2.Properties["TurnstileBridge"] = Bridge.JsObject;
				p.Frame.ExecuteJavaScript("\r\n                    (function() {\r\n                        const bridge = window.TurnstileBridge;\r\n                        if (!bridge || !bridge.postMessage) return;\r\n                        const orig = { log: console.log, warn: console.warn, error: console.error };\r\n                        function hook(level) {\r\n                            return function() {\r\n                                try {\r\n                                    bridge.postMessage(JSON.stringify({\r\n                                        type: 'console',\r\n                                        level: level,\r\n                                        message: Array.prototype.join.call(arguments, ' ')\r\n                                    }));\r\n                                } catch(e) {}\r\n                                return orig[level].apply(console, arguments);\r\n                            };\r\n                        }\r\n                        console.log = hook('log');\r\n                        console.warn = hook('warn');\r\n                        console.error = hook('error');\r\n                    })();\r\n                ");
				LogBridgeInjected();
			}
		});
		NavigationResult result = await DotNetBrowser.Navigation.LoadUrl(_challengeUrl);
		if (result.LoadResult != LoadResult.Completed)
		{
			LogFailedToLoadChallengePage(result.LoadResult);
			throw new TurnstileException($"Failed to load Turnstile challenge page: {result.LoadResult}");
		}
		LogBrowserInitialized();
	}

	public async Task<string> WaitForTokenAsync(TimeSpan P_0, CancellationToken P_1 = default(CancellationToken))
	{
		if (Bridge == null)
		{
			throw new InvalidOperationException("Browser not initialized. Call InitializeAsync first.");
		}
		using CancellationTokenSource timeoutCts = new CancellationTokenSource(P_0);
		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(P_1, timeoutCts.Token);
		try
		{
			return await Bridge.TokenTask.WaitAsync(linkedCts.Token);
		}
		catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
		{
			throw new TurnstileTimeoutException($"Turnstile challenge timed out after {P_0.TotalSeconds} seconds");
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Loading Turnstile challenge page: {Url}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogLoadingChallengePage(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Loading Turnstile challenge page: {Url}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1449978793, "LogLoadingChallengePage"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(34, 1, invariantCulture);
				handler.AppendLiteral("Loading Turnstile challenge page: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Failed to load Turnstile challenge page. Result: {Result}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogFailedToLoadChallengePage(LoadResult P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Failed to load Turnstile challenge page. Result: {Result}");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Result", P_0);
		_logger.Log(LogLevel.Error, new EventId(1481392171, "LogFailedToLoadChallengePage"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object value = s.TagArray[0].Value;
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(49, 1, invariantCulture);
			handler.AppendLiteral("Failed to load Turnstile challenge page. Result: ");
			handler.AppendFormatted<object>(value);
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile browser initialized successfully")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogBrowserInitialized()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile browser initialized successfully");
			_logger.Log(LogLevel.Debug, new EventId(558483295, "LogBrowserInitialized"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile browser initialized successfully");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile bridge injected into page")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogBridgeInjected()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile bridge injected into page");
			_logger.Log(LogLevel.Debug, new EventId(1377251772, "LogBridgeInjected"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile bridge injected into page");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Turnstile browser disposed")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogBrowserDisposed()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Turnstile browser disposed");
			_logger.Log(LogLevel.Debug, new EventId(1845426920, "LogBrowserDisposed"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Turnstile browser disposed");
			threadLocalState.Clear();
		}
	}
}
