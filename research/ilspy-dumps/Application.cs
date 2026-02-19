// Avalonia.Controls, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Application
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Input.Raw;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Threading;

public class Application : AvaloniaObject, IDataContextProvider, IGlobalDataTemplates, IDataTemplateHost, IGlobalStyles, IStyleHost, IThemeVariantHost, IResourceHost, IResourceNode, IResourceHost2, IApplicationPlatformEvents, IOptionalFeatureProvider
{
	private DataTemplates? _dataTemplates;

	private Styles? _styles;

	private IResourceDictionary? _resources;

	private Action<IReadOnlyList<IStyle>>? _stylesAdded;

	private Action<IReadOnlyList<IStyle>>? _stylesRemoved;

	private IApplicationLifetime? _applicationLifetime;

	private bool _setupCompleted;

	private EventHandler<ResourcesChangedToken>? _resourcesChanged2;

	public static readonly StyledProperty<object?> DataContextProperty = StyledElement.DataContextProperty.AddOwner<Application>();

	public static readonly StyledProperty<ThemeVariant> ActualThemeVariantProperty = ThemeVariantScope.ActualThemeVariantProperty.AddOwner<Application>();

	public static readonly StyledProperty<ThemeVariant?> RequestedThemeVariantProperty = ThemeVariantScope.RequestedThemeVariantProperty.AddOwner<Application>();

	[CompilerGenerated]
	private EventHandler<ResourcesChangedEventArgs>? m_ResourcesChanged;

	[CompilerGenerated]
	private EventHandler<UrlOpenedEventArgs>? UrlsOpened;

	[CompilerGenerated]
	private EventHandler? m_ActualThemeVariantChanged;

	[CompilerGenerated]
	private InputManager? _003CInputManager_003Ek__BackingField;

	private string? _name;

	public static readonly DirectProperty<Application, string?> NameProperty = AvaloniaProperty.RegisterDirect("Name", (Application o) => o.Name, delegate(Application o, string? v)
	{
		o.Name = v;
	});

	public object? DataContext
	{
		get
		{
			return GetValue(DataContextProperty);
		}
		set
		{
			SetValue(DataContextProperty, value2);
		}
	}

	public ThemeVariant? RequestedThemeVariant
	{
		get
		{
			return GetValue(RequestedThemeVariantProperty);
		}
		set
		{
			SetValue(RequestedThemeVariantProperty, value2);
		}
	}

	public ThemeVariant ActualThemeVariant => GetValue(ActualThemeVariantProperty);

	public static Application? Current => AvaloniaLocator.Current.GetService<Application>();

	public DataTemplates DataTemplates => _dataTemplates ?? (_dataTemplates = new DataTemplates());

	internal InputManager? InputManager
	{
		[CompilerGenerated]
		get
		{
			return _003CInputManager_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CInputManager_003Ek__BackingField = inputManager;
		}
	}

	public IResourceDictionary Resources
	{
		get
		{
			return _resources ?? (_resources = new ResourceDictionary(this));
		}
		set
		{
			resourceDictionary = resourceDictionary ?? throw new ArgumentNullException("value");
			_resources?.RemoveOwner(this);
			_resources = resourceDictionary;
			_resources.AddOwner(this);
		}
	}

	public Styles Styles => _styles ?? (_styles = new Styles(this));

	bool IDataTemplateHost.IsDataTemplatesInitialized => _dataTemplates != null;

	bool IResourceNode.HasResources
	{
		get
		{
			IResourceDictionary? resources = _resources;
			if (resources == null || !resources.HasResources)
			{
				return ((IResourceNode)_styles)?.HasResources ?? false;
			}
			return true;
		}
	}

	IStyleHost? IStyleHost.StylingParent => null;

	bool IStyleHost.IsStylesInitialized => _styles != null;

	public IApplicationLifetime? ApplicationLifetime
	{
		get
		{
			return _applicationLifetime;
		}
		set
		{
			if (_setupCompleted)
			{
				throw new InvalidOperationException("It's not possible to change ApplicationLifetime after Application was initialized.");
			}
			_applicationLifetime = applicationLifetime;
		}
	}

	public IPlatformSettings? PlatformSettings => this.TryGetFeature<IPlatformSettings>();

	public string? Name
	{
		get
		{
			return _name;
		}
		set
		{
			SetAndRaise(NameProperty, ref _name, value2);
		}
	}

	public event EventHandler<ResourcesChangedEventArgs>? ResourcesChanged
	{
		[CompilerGenerated]
		add
		{
			EventHandler<ResourcesChangedEventArgs> eventHandler = this.m_ResourcesChanged;
			EventHandler<ResourcesChangedEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<ResourcesChangedEventArgs> eventHandler3 = (EventHandler<ResourcesChangedEventArgs>)Delegate.Combine(eventHandler2, b);
				eventHandler = Interlocked.CompareExchange(ref this.m_ResourcesChanged, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<ResourcesChangedEventArgs> eventHandler = this.m_ResourcesChanged;
			EventHandler<ResourcesChangedEventArgs> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<ResourcesChangedEventArgs> eventHandler3 = (EventHandler<ResourcesChangedEventArgs>)Delegate.Remove(eventHandler2, value2);
				eventHandler = Interlocked.CompareExchange(ref this.m_ResourcesChanged, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	event EventHandler<ResourcesChangedToken>? IResourceHost2.ResourcesChanged2
	{
		add
		{
			_resourcesChanged2 = (EventHandler<ResourcesChangedToken>)Delegate.Combine(_resourcesChanged2, b);
		}
		remove
		{
			_resourcesChanged2 = (EventHandler<ResourcesChangedToken>)Delegate.Remove(_resourcesChanged2, value2);
		}
	}

	public event EventHandler? ActualThemeVariantChanged
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = this.m_ActualThemeVariantChanged;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, b);
				eventHandler = Interlocked.CompareExchange(ref this.m_ActualThemeVariantChanged, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = this.m_ActualThemeVariantChanged;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value2);
				eventHandler = Interlocked.CompareExchange(ref this.m_ActualThemeVariantChanged, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	event Action<IReadOnlyList<IStyle>>? IGlobalStyles.GlobalStylesAdded
	{
		add
		{
			_stylesAdded = (Action<IReadOnlyList<IStyle>>)Delegate.Combine(_stylesAdded, b);
		}
		remove
		{
			_stylesAdded = (Action<IReadOnlyList<IStyle>>)Delegate.Remove(_stylesAdded, value2);
		}
	}

	event Action<IReadOnlyList<IStyle>>? IGlobalStyles.GlobalStylesRemoved
	{
		add
		{
			_stylesRemoved = (Action<IReadOnlyList<IStyle>>)Delegate.Combine(_stylesRemoved, b);
		}
		remove
		{
			_stylesRemoved = (Action<IReadOnlyList<IStyle>>)Delegate.Remove(_stylesRemoved, value2);
		}
	}

	public Application()
	{
		Name = "Avalonia Application";
	}

	public virtual void Initialize()
	{
	}

	public bool TryGetResource(object P_0, ThemeVariant? P_1, out object? P_2)
	{
		P_2 = null;
		IResourceDictionary? resources = _resources;
		if (resources == null || !resources.TryGetResource(P_0, P_1, out P_2))
		{
			return Styles.TryGetResource(P_0, P_1, out P_2);
		}
		return true;
	}

	void IResourceHost.NotifyHostedResourcesChanged(ResourcesChangedEventArgs P_0)
	{
		_resourcesChanged2?.Invoke(this, ResourcesChangedToken.Create());
		this.ResourcesChanged?.Invoke(this, P_0);
	}

	void IResourceHost2.NotifyHostedResourcesChanged(ResourcesChangedToken P_0)
	{
		_resourcesChanged2?.Invoke(this, P_0);
		this.ResourcesChanged?.Invoke(this, ResourcesChangedEventArgs.Empty);
	}

	void IStyleHost.StylesAdded(IReadOnlyList<IStyle> P_0)
	{
		_stylesAdded?.Invoke(P_0);
	}

	void IStyleHost.StylesRemoved(IReadOnlyList<IStyle> P_0)
	{
		_stylesRemoved?.Invoke(P_0);
	}

	public virtual void RegisterServices()
	{
		AvaloniaSynchronizationContext.InstallIfNeeded();
		FocusManager focusManager = new FocusManager();
		InputManager = new InputManager();
		IPlatformSettings platformSettings = PlatformSettings;
		if (platformSettings != null)
		{
			platformSettings.ColorValuesChanged += OnColorValuesChanged;
			OnColorValuesChanged(platformSettings, platformSettings.GetColorValues());
		}
		AvaloniaLocator.CurrentMutable.Bind<IAccessKeyHandler>().ToTransient<AccessKeyHandler>().Bind<IGlobalDataTemplates>()
			.ToConstant(this)
			.Bind<IGlobalStyles>()
			.ToConstant(this)
			.Bind<IThemeVariantHost>()
			.ToConstant(this)
			.Bind<IFocusManager>()
			.ToConstant(focusManager)
			.Bind<IInputManager>()
			.ToConstant(InputManager)
			.Bind<IToolTipService>()
			.ToConstant(new ToolTipService(InputManager))
			.Bind<IKeyboardNavigationHandler>()
			.ToTransient<KeyboardNavigationHandler>()
			.Bind<IDragDropDevice>()
			.ToConstant(DragDropDevice.Instance);
		if (AvaloniaLocator.Current.GetService<IPlatformDragSource>() == null)
		{
			AvaloniaLocator.CurrentMutable.Bind<IPlatformDragSource>().ToTransient<InProcessDragSource>();
		}
		AvaloniaLocator.CurrentMutable.Bind<IGlobalClock>().ToConstant(MediaContext.Instance.Clock);
		_setupCompleted = true;
	}

	public virtual void OnFrameworkInitializationCompleted()
	{
	}

	void IApplicationPlatformEvents.RaiseUrlsOpened(string[] P_0)
	{
		UrlsOpened?.Invoke(this, new UrlOpenedEventArgs(P_0));
	}

	public object? TryGetFeature(Type P_0)
	{
		if (P_0 == typeof(IPlatformSettings))
		{
			return AvaloniaLocator.Current.GetService<IPlatformSettings>();
		}
		if (P_0 == typeof(IActivatableLifetime))
		{
			return AvaloniaLocator.Current.GetService<IActivatableLifetime>();
		}
		return null;
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == RequestedThemeVariantProperty)
		{
			ThemeVariant newValue = P_0.GetNewValue<ThemeVariant>();
			if ((object)newValue != null && newValue != ThemeVariant.Default)
			{
				SetValue(ActualThemeVariantProperty, newValue);
			}
			else
			{
				ClearValue(ActualThemeVariantProperty);
			}
		}
		else if (P_0.Property == ActualThemeVariantProperty)
		{
			this.ActualThemeVariantChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	private void OnColorValuesChanged(object? sender, PlatformColorValues e)
	{
		SetValue(ActualThemeVariantProperty, (ThemeVariant)e.ThemeVariant, BindingPriority.Template);
	}
}
