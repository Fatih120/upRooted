// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.CachedViewModelBase
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Messages;

public class CachedViewModelBase : ViewModelBase<CachedViewModelBase>
{
	public override void Dispose()
	{
		base.Dispose();
		WeakReferenceMessenger.Default.Send(new RemoveViewFromCacheMessage(this));
	}

	public CachedViewModelBase()
		: base((IValidator<CachedViewModelBase>?)null)
	{
	}
}

