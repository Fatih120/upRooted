using System.Globalization;
using System.Threading.Tasks;
using DotNetBrowser.Js;

namespace RootApp.Browser.WebRtc.Services;

public class SoundPoolService
{
	private readonly IJsObject _window;

	private readonly int _poolSize;

	public SoundPoolService(IJsObject P_0, int P_1 = 1)
	{
		_window = P_0;
		_poolSize = P_1;
	}

	public async Task InitializeAsync()
	{
		string js = "\r\n(function() {\r\n  const HAS_SINK = typeof HTMLMediaElement !== 'undefined' && 'setSinkId' in HTMLMediaElement.prototype;\r\n\r\n  // Globals\r\n  window.__soundPools     = window.__soundPools     || {};\r\n  window.__soundPoolIdx   = window.__soundPoolIdx   || {};\r\n  window.__soundSinkId    = window.__soundSinkId    || 'default'; // current desired sink for sounds\r\n  window.__soundElements  = window.__soundElements  || new Set(); // every pooled element for retargeting\r\n\r\n  // Helper: apply sink to an HTMLMediaElement if supported/needed\r\n  async function applySink(el) {\r\n    if (!HAS_SINK) return;\r\n    try {\r\n      if (el.sinkId !== window.__soundSinkId) {\r\n        await el.setSinkId(window.__soundSinkId);\r\n      }\r\n    } catch (e) {\r\n      console.warn('setSinkId failed', e);\r\n    }\r\n  }\r\n\r\n  // Public: set the output device for all pooled sounds (and remember it for future ones)\r\n  window.__setOutputDevice = async function(deviceId) {\r\n    window.__soundSinkId = deviceId || 'default';\r\n    await Promise.all(Array.from(window.__soundElements, el => applySink(el)));\r\n  };\r\n\r\n  // Public: play from pool with volume\r\n  window.__playSoundFromPool = async function(url, volume) {\r\n    const key = url;\r\n    if (!window.__soundPools[key]) {\r\n      window.__soundPools[key] = [];\r\n      window.__soundPoolIdx[key] = 0;\r\n      for (let i = 0; i < " + _poolSize + "; i++) {\r\n        const a = new Audio(key);\r\n        a.preload = 'auto';\r\n        window.__soundPools[key].push(a);\r\n        window.__soundElements.add(a);\r\n        await applySink(a);\r\n      }\r\n    }\r\n\r\n    const pool = window.__soundPools[key];\r\n    const idx  = window.__soundPoolIdx[key] || 0;\r\n    const a    = pool[idx];\r\n\r\n    a.volume = (typeof volume === 'number') ? volume : 1.0;\r\n    a.currentTime = 0;\r\n\r\n    await applySink(a);\r\n    a.play().catch(e => console.warn('SoundPool play failed', e));\r\n\r\n    window.__soundPoolIdx[key] = (idx + 1) % pool.length;\r\n  };\r\n\r\n  // Public: stop all sounds\r\n  window.__stopAllSounds = function() {\r\n    for (const el of window.__soundElements) {\r\n      try {\r\n        el.pause();\r\n        el.currentTime = 0;\r\n      } catch (e) {\r\n        console.warn('StopAll failed', e);\r\n      }\r\n    }\r\n  };\r\n})();";
		await _window.Frame.ExecuteJavaScript<IJsPromise>(js, userGesture: true);
	}

	public async Task PlayAsync(string P_0, double P_1 = 0.2)
	{
		string escapedUrl = P_0.Replace("'", "\\'");
		string volString = P_1.ToString(CultureInfo.InvariantCulture);
		string js = $"window.{"__playSoundFromPool"}('{escapedUrl}', {volString});";
		await _window.Frame.ExecuteJavaScript<IJsPromise>(js, userGesture: true);
	}

	public async Task StopAllAsync()
	{
		string js = "window.__stopAllSounds();";
		await _window.Frame.ExecuteJavaScript<IJsPromise>(js, userGesture: true);
	}
}
