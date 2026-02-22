// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TransformGroup
using System;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Metadata;

public sealed class TransformGroup : Transform
{
	public static readonly StyledProperty<Transforms> ChildrenProperty = AvaloniaProperty.Register<TransformGroup, Transforms>("Children");

	private IDisposable? _childrenNotificationSubscription;

	private readonly EventHandler _childTransformChangedHandler;

	private Matrix? _lastMatrix;

	[Content]
	public Transforms Children
	{
		get
		{
			return GetValue(ChildrenProperty);
		}
		set
		{
			SetValue(ChildrenProperty, value2);
		}
	}

	public override Matrix Value
	{
		get
		{
			Matrix? lastMatrix = _lastMatrix;
			if (!lastMatrix.HasValue)
			{
				Matrix identity = Matrix.Identity;
				foreach (Transform child in Children)
				{
					identity *= child.Value;
				}
				_lastMatrix = identity;
			}
			return _lastMatrix.Value;
		}
	}

	public TransformGroup()
	{
		_childTransformChangedHandler = delegate
		{
			OnTransformInvalidated();
		};
		Children = new Transforms();
	}

	private void OnTransformInvalidated()
	{
		_lastMatrix = null;
		RaiseChanged();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (!(P_0.Property == ChildrenProperty))
		{
			return;
		}
		_childrenNotificationSubscription?.Dispose();
		if (P_0.OldValue is Transforms transforms)
		{
			foreach (Transform item in transforms)
			{
				item.Changed -= _childTransformChangedHandler;
			}
		}
		if (P_0.NewValue is Transforms transforms2)
		{
			transforms2.ResetBehavior = ResetBehavior.Remove;
			_childrenNotificationSubscription = transforms2.ForEachItem(delegate(Transform tr)
			{
				tr.Changed += _childTransformChangedHandler;
				OnTransformInvalidated();
			}, delegate(Transform tr)
			{
				tr.Changed -= _childTransformChangedHandler;
				OnTransformInvalidated();
			}, delegate
			{
			});
		}
		OnTransformInvalidated();
	}
}

