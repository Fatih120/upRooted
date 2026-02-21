// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GeometryCollection
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Collections;
using Avalonia.Media;

public sealed class GeometryCollection : AvaloniaList<Geometry>
{
	[CompilerGenerated]
	private GeometryGroup? _003CParent_003Ek__BackingField;

	public GeometryGroup? Parent
	{
		[CompilerGenerated]
		get
		{
			return _003CParent_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CParent_003Ek__BackingField = geometryGroup;
		}
	}

	public GeometryCollection()
	{
		base.ResetBehavior = ResetBehavior.Remove;
		((IAvaloniaReadOnlyList<Geometry>)this).ForEachItem((Action<Geometry>)delegate
		{
			Parent?.Invalidate();
		}, (Action<Geometry>)delegate
		{
			Parent?.Invalidate();
		}, (Action)delegate
		{
			throw new NotSupportedException();
		}, false);
	}

	public GeometryCollection(IEnumerable<Geometry> P_0)
		: base(P_0)
	{
		base.ResetBehavior = ResetBehavior.Remove;
		((IAvaloniaReadOnlyList<Geometry>)this).ForEachItem((Action<Geometry>)delegate
		{
			Parent?.Invalidate();
		}, (Action<Geometry>)delegate
		{
			Parent?.Invalidate();
		}, (Action)delegate
		{
			throw new NotSupportedException();
		}, false);
	}
}

