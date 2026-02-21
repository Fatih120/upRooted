using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DotNetBrowser.Js;

namespace RootApp.Browser.Models;

public sealed class JsPromise
{
	public enum ResultState
	{
		Fulfilled,
		Rejected
	}

	public class Result
	{
		[CompilerGenerated]
		private readonly ResultState _003CState_003Ek__BackingField;

		public object Data { get; }

		internal Result(ResultState P_0, object P_1)
		{
			_003CState_003Ek__BackingField = P_0;
			Data = P_1;
		}
	}

	private readonly IJsObject _jsObject;

	private JsPromise(IJsObject P_0)
	{
		_jsObject = P_0;
	}

	public static JsPromise? AsPromise(IJsObject P_0)
	{
		return (!IsPromise(P_0)) ? null : new JsPromise(P_0);
	}

	public Task<Result> ResolveAsync()
	{
		TaskCompletionSource<Result> promiseTcs = new TaskCompletionSource<Result>();
		Then(delegate(object obj)
		{
			promiseTcs.TrySetResult(Fulfilled(obj));
		}, delegate(object obj)
		{
			promiseTcs.TrySetResult(Rejected(obj));
		});
		return promiseTcs.Task;
	}

	public void Then(Action<object> P_0, Action<object>? P_1 = null)
	{
		_jsObject.Invoke("then", P_0, P_1);
	}

	private Result Fulfilled(object P_0)
	{
		return new Result(ResultState.Fulfilled, P_0);
	}

	private static bool IsPromise(IJsObject P_0)
	{
		if (P_0 == null || P_0.IsDisposed)
		{
			return false;
		}
		IJsObject result = P_0.Frame.ExecuteJavaScript<IJsObject>("Promise.prototype").Result;
		return result.Invoke<bool>("isPrototypeOf", new object[1] { P_0 });
	}

	private Result Rejected(object P_0)
	{
		return new Result(ResultState.Rejected, P_0);
	}
}
