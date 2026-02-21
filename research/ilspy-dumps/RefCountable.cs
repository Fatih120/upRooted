// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.RefCountable
using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using Avalonia.Utilities;

internal static class RefCountable
{
	private class RefCounter
	{
		private IDisposable? _item;

		private volatile int _refs;

		public RefCounter(IDisposable P_0)
		{
			_item = P_0;
			_refs = 1;
		}

		public void AddRef()
		{
			int num = _refs;
			while (true)
			{
				if (num == 0)
				{
					throw new ObjectDisposedException("Cannot add a reference to a nonreferenced item");
				}
				int num2 = Interlocked.CompareExchange(ref _refs, num + 1, num);
				if (num2 != num)
				{
					num = num2;
					continue;
				}
				break;
			}
		}

		public void Release()
		{
			int num = _refs;
			while (true)
			{
				int num2 = Interlocked.CompareExchange(ref _refs, num - 1, num);
				if (num2 == num)
				{
					break;
				}
				num = num2;
			}
			if (num == 1)
			{
				_item?.Dispose();
				_item = null;
			}
		}
	}

	private class Ref<T> : CriticalFinalizerObject, IRef<T>, IDisposable where T : class
	{
		private T? _item;

		private readonly RefCounter _counter;

		private readonly object _lock = new object();

		public T Item
		{
			get
			{
				lock (_lock)
				{
					return _item;
				}
			}
		}

		public Ref(T P_0, RefCounter P_1)
		{
			_item = P_0;
			_counter = P_1;
		}

		public void Dispose()
		{
			lock (_lock)
			{
				if (_item != null)
				{
					_counter.Release();
					_item = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		~Ref()
		{
			Dispose();
		}

		public IRef<T> Clone()
		{
			lock (_lock)
			{
				if (_item != null)
				{
					Ref<T> result = new Ref<T>(_item, _counter);
					_counter.AddRef();
					return result;
				}
				throw new ObjectDisposedException("Ref<" + typeof(T)?.ToString() + ">");
			}
		}
	}

	public static IRef<T> Create<T>(T P_0) where T : class, IDisposable
	{
		return new Ref<T>(P_0, new RefCounter(P_0));
	}
}

