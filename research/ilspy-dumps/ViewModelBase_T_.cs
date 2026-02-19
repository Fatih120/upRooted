// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.ViewModelBase<T>
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using RootApp.Client.Avalonia;

public class ViewModelBase<T> : ObservableObject, IViewModelBase, INotifyDataErrorInfo, IDisposable where T : class
{
	private readonly IValidator<T>? _validator;

	private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsTopMostViewModel
	{
		get
		{
			return _003CIsTopMostViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsTopMostViewModel_003Ek__BackingField, value))
			{
				_003CIsTopMostViewModel_003Ek__BackingField = value;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsTopMostViewModel);
			}
		}
	}

	public bool HasErrors => _errors.Count != 0;

	public bool HasNoErrors => !HasErrors;

	public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

	protected ViewModelBase(IValidator<T>? validator = null)
	{
		_validator = validator;
	}

	public IEnumerable GetErrors(string? propertyName)
	{
		if (!string.IsNullOrWhiteSpace(propertyName) && _errors.TryGetValue(propertyName, out List<string> value))
		{
			return value;
		}
		return Enumerable.Empty<string>();
	}

	public void ValidateProperty(string propertyName)
	{
		if (_validator == null)
		{
			return;
		}
		ClearErrors(propertyName);
		ValidationResult validationResult = _validator.Validate(this as T, delegate(ValidationStrategy<T> options)
		{
			options.IncludeProperties(propertyName);
		});
		foreach (ValidationFailure error in validationResult.Errors)
		{
			AddError(propertyName, error.ErrorMessage);
		}
	}

	private void AddError(string propertyName, string error)
	{
		if (!_errors.ContainsKey(propertyName))
		{
			_errors[propertyName] = new List<string>();
		}
		_errors[propertyName].Add(error);
		OnErrorsChanged(propertyName);
	}

	private void ClearErrors(string propertyName)
	{
		if (_errors.Remove(propertyName))
		{
			OnErrorsChanged(propertyName);
		}
	}

	private void OnErrorsChanged(string propertyName)
	{
		this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
	}

	public virtual void Dispose()
	{
		WeakReferenceMessenger.Default.UnregisterAll(this);
	}
}
