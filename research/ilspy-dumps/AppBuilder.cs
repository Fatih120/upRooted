// Avalonia.Controls, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.AppBuilder
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Platform;

public sealed class AppBuilder
{
	private static bool s_setupWasAlreadyCalled;

	private Action? _optionsInitializers;

	private Func<Application>? _appFactory;

	private IApplicationLifetime? _lifetime;

	[CompilerGenerated]
	private Action? _003CRuntimePlatformServicesInitializer_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CRuntimePlatformServicesName_003Ek__BackingField;

	[CompilerGenerated]
	private Application? _003CInstance_003Ek__BackingField;

	[CompilerGenerated]
	private Type? _003CApplicationType_003Ek__BackingField;

	[CompilerGenerated]
	private Action? _003CWindowingSubsystemInitializer_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CWindowingSubsystemName_003Ek__BackingField;

	[CompilerGenerated]
	private Action? _003CRenderingSubsystemInitializer_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CRenderingSubsystemName_003Ek__BackingField;

	[CompilerGenerated]
	private Action<AppBuilder> _003CAfterSetupCallback_003Ek__BackingField;

	[CompilerGenerated]
	private Action<AppBuilder> _003CAfterPlatformServicesSetupCallback_003Ek__BackingField;

	public Action? RuntimePlatformServicesInitializer
	{
		[CompilerGenerated]
		get
		{
			return _003CRuntimePlatformServicesInitializer_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CRuntimePlatformServicesInitializer_003Ek__BackingField = action;
		}
	}

	private string? RuntimePlatformServicesName
	{
		[CompilerGenerated]
		set
		{
			_003CRuntimePlatformServicesName_003Ek__BackingField = text;
		}
	}

	public Application? Instance
	{
		[CompilerGenerated]
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CInstance_003Ek__BackingField = application;
		}
	}

	public Type? ApplicationType
	{
		[CompilerGenerated]
		get
		{
			return _003CApplicationType_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CApplicationType_003Ek__BackingField = type;
		}
	}

	public Action? WindowingSubsystemInitializer
	{
		[CompilerGenerated]
		get
		{
			return _003CWindowingSubsystemInitializer_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CWindowingSubsystemInitializer_003Ek__BackingField = action;
		}
	}

	private string? WindowingSubsystemName
	{
		[CompilerGenerated]
		set
		{
			_003CWindowingSubsystemName_003Ek__BackingField = text;
		}
	}

	public Action? RenderingSubsystemInitializer
	{
		[CompilerGenerated]
		get
		{
			return _003CRenderingSubsystemInitializer_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CRenderingSubsystemInitializer_003Ek__BackingField = action;
		}
	}

	private string? RenderingSubsystemName
	{
		[CompilerGenerated]
		set
		{
			_003CRenderingSubsystemName_003Ek__BackingField = text;
		}
	}

	public Action<AppBuilder> AfterSetupCallback
	{
		[CompilerGenerated]
		get
		{
			return _003CAfterSetupCallback_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CAfterSetupCallback_003Ek__BackingField = action;
		}
	}

	private Action<AppBuilder> AfterApplicationSetupCallback { get; }

	public Action<AppBuilder> AfterPlatformServicesSetupCallback
	{
		[CompilerGenerated]
		get
		{
			return _003CAfterPlatformServicesSetupCallback_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CAfterPlatformServicesSetupCallback_003Ek__BackingField = action;
		}
	}

	private AppBuilder Self => this;

	private AppBuilder()
	{
		AfterSetupCallback = delegate
		{
		};
		AfterApplicationSetupCallback = delegate
		{
		};
		AfterPlatformServicesSetupCallback = delegate
		{
		};
		base._002Ector();
	}

	public static AppBuilder Configure<TApp>(Func<TApp> P_0) where TApp : Application
	{
		return new AppBuilder
		{
			ApplicationType = typeof(TApp),
			_appFactory = P_0
		};
	}

	internal static AppBuilder Configure([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] Type P_0)
	{
		if (P_0.GetMethod("BuildAvaloniaApp", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, null, Array.Empty<Type>(), null)?.Invoke(null, Array.Empty<object>()) is AppBuilder result)
		{
			return result;
		}
		if (typeof(Application).IsAssignableFrom(P_0))
		{
			return Configure(() => (Application)Activator.CreateInstance(P_0));
		}
		throw new InvalidOperationException("Unable to create AppBuilder from type \"" + P_0.FullName + "\". Input type either needs to have BuildAvaloniaApp -> AppBuilder method or inherit Application type.");
	}

	public AppBuilder AfterSetup(Action<AppBuilder> P_0)
	{
		AfterSetupCallback = (Action<AppBuilder>)Delegate.Combine(AfterSetupCallback, P_0);
		return Self;
	}

	public AppBuilder AfterPlatformServicesSetup(Action<AppBuilder> P_0)
	{
		AfterPlatformServicesSetupCallback = (Action<AppBuilder>)Delegate.Combine(AfterPlatformServicesSetupCallback, P_0);
		return Self;
	}

	public AppBuilder SetupWithoutStarting()
	{
		Setup();
		return Self;
	}

	public AppBuilder SetupWithLifetime(IApplicationLifetime P_0)
	{
		_lifetime = P_0;
		Setup();
		return Self;
	}

	public AppBuilder UseWindowingSubsystem(Action P_0, string P_1 = "")
	{
		WindowingSubsystemInitializer = P_0;
		WindowingSubsystemName = P_1;
		return Self;
	}

	public AppBuilder UseRenderingSubsystem(Action P_0, string P_1 = "")
	{
		RenderingSubsystemInitializer = P_0;
		RenderingSubsystemName = P_1;
		return Self;
	}

	public AppBuilder UseStandardRuntimePlatformSubsystem()
	{
		RuntimePlatformServicesInitializer = delegate
		{
			StandardRuntimePlatformServices.Register(ApplicationType?.Assembly);
		};
		RuntimePlatformServicesName = "StandardRuntimePlatform";
		return Self;
	}

	public AppBuilder With<T>(T P_0)
	{
		_optionsInitializers = (Action)Delegate.Combine(_optionsInitializers, (Action)delegate
		{
			AvaloniaLocator.CurrentMutable.Bind<T>().ToConstant(P_0);
		});
		return Self;
	}

	public AppBuilder ConfigureFonts(Action<FontManager> P_0)
	{
		return AfterSetup(delegate
		{
			P_0?.Invoke(FontManager.Current);
		});
	}

	private void Setup()
	{
		if (RuntimePlatformServicesInitializer == null)
		{
			throw new InvalidOperationException("No runtime platform services configured.");
		}
		if (WindowingSubsystemInitializer == null)
		{
			throw new InvalidOperationException("No windowing system configured.");
		}
		if (RenderingSubsystemInitializer == null)
		{
			throw new InvalidOperationException("No rendering system configured.");
		}
		if (_appFactory == null)
		{
			throw new InvalidOperationException("No Application factory configured.");
		}
		if (s_setupWasAlreadyCalled)
		{
			throw new InvalidOperationException("Setup was already called on one of AppBuilder instances");
		}
		s_setupWasAlreadyCalled = true;
		SetupUnsafe();
	}

	internal void SetupUnsafe()
	{
		_optionsInitializers?.Invoke();
		RuntimePlatformServicesInitializer?.Invoke();
		RenderingSubsystemInitializer?.Invoke();
		WindowingSubsystemInitializer?.Invoke();
		AfterPlatformServicesSetupCallback?.Invoke(Self);
		Instance = _appFactory();
		Instance.ApplicationLifetime = _lifetime;
		AvaloniaLocator.CurrentMutable.BindToSelf(Instance);
		Instance.RegisterServices();
		Instance.Initialize();
		AfterApplicationSetupCallback?.Invoke(Self);
		AfterSetupCallback?.Invoke(Self);
		Instance.OnFrameworkInitializationCompleted();
	}
}
