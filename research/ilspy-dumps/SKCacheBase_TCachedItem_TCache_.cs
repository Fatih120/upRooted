// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKCacheBase<TCachedItem,TCache>
using System;
using System.Collections.Concurrent;

internal abstract class SKCacheBase<TCachedItem, TCache> where TCachedItem : IDisposable, new() where TCache : new()
{
	protected readonly ConcurrentBag<TCachedItem> Cache;

	public static readonly TCache Shared = new TCache();

	protected SKCacheBase()
	{
		Cache = new ConcurrentBag<TCachedItem>();
	}

	public TCachedItem Get()
	{
		if (!Cache.TryTake(out var result))
		{
			return new TCachedItem();
		}
		return result;
	}

	public void Return(TCachedItem P_0)
	{
		Cache.Add(P_0);
	}
}

