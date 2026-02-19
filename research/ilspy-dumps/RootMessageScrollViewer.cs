// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootMessageScrollViewer
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.UI.Community.Content;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Core.Identifiers;

public class RootMessageScrollViewer : RootScrollViewer
{
	public static readonly StyledProperty<ICommand> DownloadNewerMessagesCommandProperty = AvaloniaProperty.Register<RootMessageScrollViewer, ICommand>("DownloadNewerMessagesCommand");

	public static readonly StyledProperty<ICommand> DownloadOlderMessagesCommandProperty = AvaloniaProperty.Register<RootMessageScrollViewer, ICommand>("DownloadOlderMessagesCommand");

	public static readonly StyledProperty<ICommand> SetNewMessagesBannerStatusCommandProperty = AvaloniaProperty.Register<RootMessageScrollViewer, ICommand>("SetNewMessagesBannerStatusCommand");

	public static readonly StyledProperty<ICommand> SetAutoScrollStatusCommandProperty = AvaloniaProperty.Register<RootMessageScrollViewer, ICommand>("SetAutoScrollStatusCommand");

	public static readonly StyledProperty<ICommand> SetShowJumpToPresentCommandProperty = AvaloniaProperty.Register<RootMessageScrollViewer, ICommand>("SetShowJumpToPresentCommand");

	public static readonly StyledProperty<ICommand> MessagesBeganRenderingCommandProperty = AvaloniaProperty.Register<RootMessageScrollViewer, ICommand>("MessagesBeganRenderingCommand");

	public static readonly StyledProperty<int> PrefetchCooldownMsProperty = AvaloniaProperty.Register<RootMessageScrollViewer, int>("PrefetchCooldownMs", 100);

	private bool _hasBeenLoaded;

	private ContentPresenter? _lockedContentPresenter;

	private readonly DispatcherTimer _messageLockTimer;

	private IMessageContainerViewModel? _messageContainerViewModel;

	private object? _bottomMostControl;

	private bool _isAnimationRunning;

	private DateTimeOffset _lastOlderFetch = DateTimeOffset.MinValue;

	private DateTimeOffset _lastNewerFetch = DateTimeOffset.MinValue;

	private bool _showJumpToPresent;

	private DispatcherTimer? _jumpToPresentDelayTimer;

	private bool _pendingShowJumpToPresent;

	private bool _isExpandingToFillScreen;

	[CompilerGenerated]
	private RootMessageItemsControl? _003CItemsControl_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CAutoScroll_003Ek__BackingField;

	private bool _blockSelection;

	public ICommand DownloadNewerMessagesCommand => GetValue(DownloadNewerMessagesCommandProperty);

	public ICommand DownloadOlderMessagesCommand => GetValue(DownloadOlderMessagesCommandProperty);

	public ICommand SetNewMessagesBannerStatusCommand => GetValue(SetNewMessagesBannerStatusCommandProperty);

	public ICommand SetAutoScrollStatusCommand => GetValue(SetAutoScrollStatusCommandProperty);

	public ICommand SetShowJumpToPresentCommand => GetValue(SetShowJumpToPresentCommandProperty);

	public ICommand MessagesBeganRenderingCommand => GetValue(MessagesBeganRenderingCommandProperty);

	public int PrefetchCooldownMs => GetValue(PrefetchCooldownMsProperty);

	public RootMessageItemsControl? ItemsControl
	{
		[CompilerGenerated]
		get
		{
			return _003CItemsControl_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CItemsControl_003Ek__BackingField = rootMessageItemsControl;
		}
	}

	protected override Type StyleKeyOverride => typeof(RootScrollViewer);

	public bool AutoScroll
	{
		[CompilerGenerated]
		get
		{
			return _003CAutoScroll_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAutoScroll_003Ek__BackingField = flag;
		}
	}

	public RootMessageScrollViewer()
	{
		base.Focusable = true;
		_messageLockTimer = new DispatcherTimer
		{
			Interval = TimeSpan.FromMilliseconds(500L)
		};
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		_messageLockTimer.Tick += OnMessageLockTimerTick;
		if (!_hasBeenLoaded)
		{
			_hasBeenLoaded = true;
			if (ItemsControl == null)
			{
				RootMessageItemsControl rootMessageItemsControl = (ItemsControl = this.FindDescendantOfType<RootMessageItemsControl>());
			}
			if (base.DataContext is IMessageContainerViewModel messageContainerViewModel)
			{
				_messageContainerViewModel = messageContainerViewModel;
			}
		}
		if (ItemsControl != null)
		{
			ItemsControl.Items.CollectionChanged += OnItemsCollectionChanged;
			if (ItemsControl.Items.Count > 0)
			{
				ItemsControl.Items.CollectionChanged -= OnItemsCollectionChanged;
				InitialFocusAsync();
			}
		}
		if (_messageContainerViewModel != null)
		{
			_messageContainerViewModel.FocusMessageRequested += OnFocusMessageRequested;
			_messageContainerViewModel.ExpandMessageAreaRequested += OnExpandMessageAreaRequested;
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		_messageLockTimer.Stop();
		_messageLockTimer.Tick -= OnMessageLockTimerTick;
		if (_jumpToPresentDelayTimer != null)
		{
			_jumpToPresentDelayTimer.Stop();
			_jumpToPresentDelayTimer.Tick -= OnJumpToPresentDelayTimerTick;
		}
		if (ItemsControl != null)
		{
			ItemsControl.Items.CollectionChanged -= OnItemsCollectionChanged;
		}
		if (_messageContainerViewModel != null)
		{
			_messageContainerViewModel.FocusMessageRequested -= OnFocusMessageRequested;
			_messageContainerViewModel.ExpandMessageAreaRequested -= OnExpandMessageAreaRequested;
		}
	}

	private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		RootMessageItemsControl? itemsControl = ItemsControl;
		if (itemsControl != null && itemsControl.Items.Count > 0)
		{
			ItemsControl.Items.CollectionChanged -= OnItemsCollectionChanged;
			InitialFocusAsync();
		}
	}

	private async Task InitialFocusAsync()
	{
		bool canShowNewMessageBanner = true;
		await ExpandVirtualizationWindowToFillScreenAsync();
		if (_messageContainerViewModel.StartupMessageId != null)
		{
			ContentPresenter p2 = await GetMessageByIdAsync(_messageContainerViewModel.StartupMessageId.Value, 15, 50);
			if (p2 != null)
			{
				LockAndJumpToMessageAsync(p2, true);
			}
			else
			{
				if (_lockedContentPresenter == null)
				{
					ScrollToEnd();
				}
				DetermineAutoScrollValue();
			}
			_messageContainerViewModel.StartupMessageId = null;
			canShowNewMessageBanner = false;
		}
		else if (_messageContainerViewModel.MessageContainer.Messages.HasMessageWithNewIndicator)
		{
			ContentPresenter newPresenter = await GetMessageByNewStatusAsync(15, 50);
			if (newPresenter != null)
			{
				LockAndJumpToMessageAsync(newPresenter);
			}
			else
			{
				if (_lockedContentPresenter == null)
				{
					ScrollToEnd();
				}
				DetermineAutoScrollValue();
			}
		}
		else
		{
			if (_lockedContentPresenter == null)
			{
				ScrollToEnd();
			}
			DetermineAutoScrollValue();
		}
		if (MessagesBeganRenderingCommand.CanExecute(null))
		{
			MessagesBeganRenderingCommand.Execute(null);
		}
		if (!CanScroll() || Math.Abs(GetScrollBarPositionPercentage() - 100.0) < 0.1)
		{
			await _messageContainerViewModel.MessageContainer.Messages.SetViewTimeAsync();
		}
		if (canShowNewMessageBanner)
		{
			determineNewMessageBanner();
		}
	}

	private async Task ExpandVirtualizationWindowToFillScreenAsync()
	{
		if (_isExpandingToFillScreen)
		{
			return;
		}
		_isExpandingToFillScreen = true;
		try
		{
			await Task.Delay(50);
			int iterations = 0;
			while (!CanScroll() && iterations++ < 50)
			{
				if (!_messageContainerViewModel.MessageContainer.Messages.IncreaseVirtualizationWindow())
				{
					if (TryDownloadMoreOlder())
					{
						TouchMessageLockTimer();
						await Task.Delay(100);
						continue;
					}
					if (!TryDownloadMoreNewer())
					{
						break;
					}
					TouchMessageLockTimer();
					await Task.Delay(100);
				}
				else
				{
					await Task.Delay(100);
				}
			}
		}
		finally
		{
			_isExpandingToFillScreen = false;
		}
	}

	private bool TryDownloadMoreOlder()
	{
		if (_messageContainerViewModel.MessageContainer.Messages.HasMoreOlderMessages && DownloadOlderMessagesCommand.CanExecute(null))
		{
			DownloadOlderMessagesCommand.Execute(null);
			_lastOlderFetch = DateTimeOffset.Now;
			return true;
		}
		return false;
	}

	private bool TryDownloadMoreNewer()
	{
		if (_messageContainerViewModel.MessageContainer.Messages.HasMoreNewerMessages && DownloadNewerMessagesCommand.CanExecute(null))
		{
			DownloadNewerMessagesCommand.Execute(null);
			_lastNewerFetch = DateTimeOffset.Now;
			return true;
		}
		return false;
	}

	protected override void OnScrollChanged(ScrollChangedEventArgs P_0)
	{
		base.OnScrollChanged(P_0);
		if (_messageContainerViewModel == null)
		{
			return;
		}
		RootMessageItemsControl? itemsControl = ItemsControl;
		object obj = ((itemsControl != null && itemsControl.ItemCount > 0) ? ItemsControl.Items[ItemsControl.ItemCount - 1] : null);
		bool flag = _bottomMostControl != null && _bottomMostControl != obj;
		_bottomMostControl = obj;
		if (_lockedContentPresenter != null)
		{
			TouchMessageLockTimer();
			LockAndJumpToMessageAsync(_lockedContentPresenter);
		}
		bool autoScroll = AutoScroll;
		DetermineAutoScrollValue();
		if (autoScroll && P_0.ExtentDelta.Y != 0.0)
		{
			ScrollToEnd();
			setAutoScroll(true);
		}
		bool flag2 = autoScroll != AutoScroll;
		if (AutoScroll)
		{
			ScrollToEnd();
			if (flag2 && CanScroll())
			{
				_messageContainerViewModel.MessageContainer.Messages.ShrinkVirtualizationWindow();
			}
			else if (!CanScroll())
			{
				ExpandVirtualizationWindowToFillScreenAsync();
			}
			if (_messageContainerViewModel.MessageContainer.HasActivity && !_messageContainerViewModel.MessageContainer.Messages.HasMoreNewerMessages)
			{
				_messageContainerViewModel.MessageContainer.Messages.SetViewTimeAsync().Forget();
				if (SetNewMessagesBannerStatusCommand.CanExecute(false))
				{
					SetNewMessagesBannerStatusCommand.Execute(false);
				}
			}
		}
		else if (P_0.ExtentDelta.Y != 0.0 && !flag)
		{
			base.Offset = new Vector(base.Offset.X, base.Offset.Y + P_0.ExtentDelta.Y);
		}
		else if (P_0.ExtentDelta.Y != 0.0)
		{
			base.Offset = new Vector(base.Offset.X, base.Offset.Y);
		}
		double scrollBarPositionPercentage = GetScrollBarPositionPercentage();
		TimeSpan timeSpan = TimeSpan.FromMilliseconds(Math.Max(0, PrefetchCooldownMs));
		bool flag3 = _messageContainerViewModel.StartupMessageId == null && _lockedContentPresenter == null;
		if (flag3 && scrollBarPositionPercentage == 0.0 && _messageContainerViewModel.MessageContainer.Messages.HasMoreOlderMessages && DateTimeOffset.Now - _lastOlderFetch >= timeSpan && DownloadOlderMessagesCommand.CanExecute(null))
		{
			_lastOlderFetch = DateTimeOffset.Now;
			DownloadOlderMessagesCommand.Execute(null);
		}
		if (flag3 && scrollBarPositionPercentage == 100.0 && _messageContainerViewModel.MessageContainer.Messages.HasMoreNewerMessages && DateTimeOffset.Now - _lastNewerFetch >= timeSpan && DownloadNewerMessagesCommand.CanExecute(null))
		{
			_lastNewerFetch = DateTimeOffset.Now;
			DownloadNewerMessagesCommand.Execute(null);
		}
	}

	protected override void OnSizeChanged(SizeChangedEventArgs P_0)
	{
		base.OnSizeChanged(P_0);
		if (AutoScroll)
		{
			ScrollToEnd();
		}
	}

	private double GetScrollBarPositionPercentage()
	{
		double num = base.Extent.Height - base.Viewport.Height;
		if (num <= 0.0)
		{
			return 100.0;
		}
		double num2 = base.Offset.Y / num * 100.0;
		if (num2 < 0.0)
		{
			num2 = 0.0;
		}
		if (num2 > 100.0)
		{
			num2 = 100.0;
		}
		return num2;
	}

	private bool CanScroll()
	{
		return base.Extent.Height > base.Viewport.Height;
	}

	private void DetermineAutoScrollValue()
	{
		double scrollBarPositionPercentage = GetScrollBarPositionPercentage();
		setAutoScroll(!CanScroll() || (!_messageContainerViewModel.MessageContainer.Messages.HasMoreNewerMessages && Math.Abs(scrollBarPositionPercentage - 100.0) < 0.1));
		bool virtualizationLockMode = !_messageContainerViewModel.MessageContainer.Messages.HasMoreNewerMessages && Math.Abs(scrollBarPositionPercentage - 100.0) < 0.1 && CanScroll();
		_messageContainerViewModel.MessageContainer.Messages.SetVirtualizationLockMode(virtualizationLockMode);
		double num = base.Extent.Height - base.Viewport.Height - base.Offset.Y;
		bool hasMoreNewerMessages = _messageContainerViewModel.MessageContainer.Messages.HasMoreNewerMessages;
		bool flag = !CanScroll() || (!hasMoreNewerMessages && Math.Abs(scrollBarPositionPercentage - 100.0) < 0.1);
		bool showJumpToPresent = hasMoreNewerMessages || (CanScroll() && scrollBarPositionPercentage < 85.0 && num >= 4000.0);
		if (_showJumpToPresent && !flag)
		{
			showJumpToPresent = true;
		}
		setShowJumpToPresent(showJumpToPresent);
	}

	private void determineNewMessageBanner()
	{
		bool flag = false;
		if (_messageContainerViewModel.MessageContainer.Messages.NewMessagesCount > 0 && CanScroll())
		{
			double scrollBarPositionPercentage = GetScrollBarPositionPercentage();
			flag = scrollBarPositionPercentage > 0.0 && scrollBarPositionPercentage < 100.0;
		}
		if (SetNewMessagesBannerStatusCommand.CanExecute(flag))
		{
			SetNewMessagesBannerStatusCommand.Execute(flag);
		}
	}

	public async Task<ContentPresenter?> GetMessageByIdAsync(MessageGuid P_0, int P_1 = 5, int P_2 = 100)
	{
		for (int attempt = 0; attempt < P_1; attempt++)
		{
			ContentPresenter presenter = await Dispatcher.UIThread.InvokeAsync(delegate
			{
				MessageViewModel messageViewModel = ItemsControl?.Items.OfType<MessageViewModel>().FirstOrDefault((MessageViewModel messageViewModel2) => messageViewModel2.Message.MessageId == P_0);
				return (messageViewModel != null) ? (ItemsControl?.ContainerFromItem(messageViewModel) as ContentPresenter) : null;
			});
			if (presenter != null)
			{
				return presenter;
			}
			await Task.Delay(P_2);
		}
		return null;
	}

	public async Task<ContentPresenter?> GetMessageByNewStatusAsync(int P_0 = 5, int P_1 = 100)
	{
		for (int attempt = 0; attempt < P_0; attempt++)
		{
			ContentPresenter presenter = await Dispatcher.UIThread.InvokeAsync(delegate
			{
				MessageViewModel messageViewModel = ItemsControl?.Items.OfType<MessageViewModel>().FirstOrDefault((MessageViewModel messageViewModel2) => messageViewModel2.Message.ShowNewDivider);
				return (messageViewModel != null) ? (ItemsControl?.ContainerFromItem(messageViewModel) as ContentPresenter) : null;
			});
			if (presenter != null)
			{
				return presenter;
			}
			await Task.Delay(P_1);
		}
		return null;
	}

	private async Task LockAndJumpToMessageAsync(ContentPresenter P_0, bool P_1 = false)
	{
		_lockedContentPresenter = P_0;
		TouchMessageLockTimer();
		base.Offset = new Vector(base.Offset.X, P_0.Bounds.Top - 56.0);
		if (P_1 && !_isAnimationRunning)
		{
			Animation animation = new Animation
			{
				Duration = TimeSpan.FromSeconds(1L),
				Easing = new SineEaseInOut(),
				IterationCount = new IterationCount(3uL),
				Children = 
				{
					new KeyFrame
					{
						Cue = new Cue(0.0),
						Setters = { (IAnimationSetter)new Setter(ContentPresenter.BackgroundProperty, Brushes.Transparent) }
					},
					new KeyFrame
					{
						Cue = new Cue(0.5),
						Setters = { (IAnimationSetter)new Setter(ContentPresenter.BackgroundProperty, Application.Current.FindResource(Application.Current.ActualThemeVariant, "HighlightStrong") as IBrush) }
					},
					new KeyFrame
					{
						Cue = new Cue(1.0),
						Setters = { (IAnimationSetter)new Setter(ContentPresenter.BackgroundProperty, Brushes.Transparent) }
					}
				}
			};
			_isAnimationRunning = true;
			try
			{
				await animation.RunAsync(P_0);
			}
			finally
			{
				_isAnimationRunning = false;
			}
		}
	}

	private void OnFocusMessageRequested(MessageGuid _)
	{
		if (_hasBeenLoaded)
		{
			InitialFocusAsync().Forget();
		}
	}

	private void OnExpandMessageAreaRequested()
	{
		if (_hasBeenLoaded)
		{
			ExpandVirtualizationWindowToFillScreenAsync().Forget();
		}
	}

	private void OnMessageLockTimerTick(object? sender, EventArgs e)
	{
		ContentPresenter lockedContentPresenter = _lockedContentPresenter;
		_lockedContentPresenter = null;
		_messageLockTimer.Stop();
		if (lockedContentPresenter != null)
		{
			base.Offset = new Vector(base.Offset.X, lockedContentPresenter.Bounds.Top - 56.0);
		}
		DetermineAutoScrollValue();
	}

	private void TouchMessageLockTimer()
	{
		if (_lockedContentPresenter != null)
		{
			if (_messageLockTimer.IsEnabled)
			{
				_messageLockTimer.Stop();
			}
			_messageLockTimer.Start();
		}
	}

	private void setAutoScroll(bool P_0)
	{
		AutoScroll = P_0;
		if (SetAutoScrollStatusCommand.CanExecute(P_0))
		{
			SetAutoScrollStatusCommand.Execute(P_0);
		}
	}

	private void setShowJumpToPresent(bool P_0)
	{
		if (!P_0)
		{
			_pendingShowJumpToPresent = false;
			_jumpToPresentDelayTimer?.Stop();
			if (_showJumpToPresent)
			{
				_showJumpToPresent = false;
				ICommand setShowJumpToPresentCommand = SetShowJumpToPresentCommand;
				if (setShowJumpToPresentCommand != null && setShowJumpToPresentCommand.CanExecute(false))
				{
					SetShowJumpToPresentCommand.Execute(false);
				}
			}
		}
		else if (!_showJumpToPresent && !_pendingShowJumpToPresent)
		{
			_pendingShowJumpToPresent = true;
			if (_jumpToPresentDelayTimer == null)
			{
				_jumpToPresentDelayTimer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(1000L)
				};
			}
			_jumpToPresentDelayTimer.Tick -= OnJumpToPresentDelayTimerTick;
			_jumpToPresentDelayTimer.Tick += OnJumpToPresentDelayTimerTick;
			_jumpToPresentDelayTimer.Start();
		}
	}

	private void OnJumpToPresentDelayTimerTick(object? sender, EventArgs e)
	{
		_jumpToPresentDelayTimer?.Stop();
		if (_pendingShowJumpToPresent && !_showJumpToPresent)
		{
			_showJumpToPresent = true;
			ICommand setShowJumpToPresentCommand = SetShowJumpToPresentCommand;
			if (setShowJumpToPresentCommand != null && setShowJumpToPresentCommand.CanExecute(true))
			{
				SetShowJumpToPresentCommand.Execute(true);
			}
		}
		_pendingShowJumpToPresent = false;
	}

	public void SetBlockSelection(bool P_0)
	{
		_blockSelection = P_0;
	}

	protected override void OnLostFocus(RoutedEventArgs P_0)
	{
		base.OnLostFocus(P_0);
		if (!_blockSelection)
		{
			ItemsControl?.UnSelect();
		}
	}

	protected override void OnKeyDown(KeyEventArgs P_0)
	{
		base.OnKeyDown(P_0);
		if (P_0.Key == Key.C && P_0.KeyModifiers == KeyModifiers.Control)
		{
			string text = ItemsControl?.GetSelectedText();
			if (text != null)
			{
				TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(text).Forget();
			}
		}
	}
}

