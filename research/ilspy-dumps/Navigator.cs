// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Helpers.Navigation.Navigator
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Resources.Enums;

public class Navigator : ObservableObject
{
	private Stack<IViewModelBase> _navigationStack;

	[CompilerGenerated]
	private bool _003CHasPendingChanges_003Ek__BackingField = false;

	[CompilerGenerated]
	private bool _003CCanSave_003Ek__BackingField = true;

	[CompilerGenerated]
	private bool _003CPromptToDiscardChanges_003Ek__BackingField;

	[CompilerGenerated]
	private Action? m_SaveChangesRequested;

	[CompilerGenerated]
	private Action? m_RevertChangesRequested;

	[CompilerGenerated]
	private Action? m_DiscardChangesAndGoBackRequested;

	[CompilerGenerated]
	private IViewModelBase? _003CCurrentViewModel_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CCanGoBack_003Ek__BackingField;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? CurrentViewModel
	{
		get
		{
			return _003CCurrentViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(_003CCurrentViewModel_003Ek__BackingField, viewModelBase))
			{
				_003CCurrentViewModel_003Ek__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CurrentViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasPendingChanges
	{
		get
		{
			return _003CHasPendingChanges_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHasPendingChanges_003Ek__BackingField, flag))
			{
				_003CHasPendingChanges_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasPendingChanges);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool CanSave
	{
		get
		{
			return _003CCanSave_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CCanSave_003Ek__BackingField, flag))
			{
				_003CCanSave_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CanSave);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool CanGoBack
	{
		get
		{
			return _003CCanGoBack_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CCanGoBack_003Ek__BackingField, flag))
			{
				_003CCanGoBack_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CanGoBack);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WebApiStatus WebApiStatus
	{
		get
		{
			return _003CWebApiStatus_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(_003CWebApiStatus_003Ek__BackingField, webApiStatus))
			{
				_003CWebApiStatus_003Ek__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	public IViewModelBase? FirstViewModel => _navigationStack.FirstOrDefault();

	public bool PromptToDiscardChanges
	{
		[CompilerGenerated]
		get
		{
			return _003CPromptToDiscardChanges_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPromptToDiscardChanges_003Ek__BackingField = flag;
		}
	}

	public int Count => _navigationStack.Count;

	public event Action? SaveChangesRequested
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_SaveChangesRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_SaveChangesRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_SaveChangesRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_SaveChangesRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action? RevertChangesRequested
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_RevertChangesRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_RevertChangesRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_RevertChangesRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_RevertChangesRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action? DiscardChangesAndGoBackRequested
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_DiscardChangesAndGoBackRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_DiscardChangesAndGoBackRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_DiscardChangesAndGoBackRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_DiscardChangesAndGoBackRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public Navigator()
	{
		_navigationStack = new Stack<IViewModelBase>();
	}

	public void Push(IViewModelBase P_0)
	{
		_navigationStack.Push(P_0);
		CurrentViewModel = P_0;
		determineCanGoBack();
	}

	public void Pop()
	{
		if (PromptToDiscardChanges && HasPendingChanges)
		{
			this.DiscardChangesAndGoBackRequested?.Invoke();
			return;
		}
		_navigationStack.Pop().Dispose();
		CurrentViewModel = _navigationStack.Peek();
		determineCanGoBack();
	}

	private void determineCanGoBack()
	{
		CanGoBack = _navigationStack.Count > 1;
	}

	public void SaveChanges()
	{
		this.SaveChangesRequested?.Invoke();
	}

	public void RevertChanges()
	{
		this.RevertChangesRequested?.Invoke();
	}
}
