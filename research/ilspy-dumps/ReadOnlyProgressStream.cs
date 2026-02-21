using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RootApp.WebApi.Client.Shared;

public class ReadOnlyProgressStream : Stream
{
	private static readonly long _updateInterval = Stopwatch.Frequency / 30;

	private readonly long _length;

	private readonly IProgress<int> _progress;

	private readonly Stream _streamImplementation;

	private readonly double _toPercent;

	private long _lastPosition;

	private long _lastUpdate;

	public override bool CanRead => _streamImplementation.CanRead;

	public override bool CanSeek => _streamImplementation.CanSeek;

	public override bool CanWrite => false;

	public override long Length => _streamImplementation.Length;

	public override long Position
	{
		get
		{
			return _streamImplementation.Position;
		}
		set
		{
			_streamImplementation.Position = value;
		}
	}

	public ReadOnlyProgressStream(Stream streamImplementation, IProgress<int> progress)
	{
		_streamImplementation = streamImplementation;
		_progress = progress;
		_length = streamImplementation.Length;
		_toPercent = ((_length > 0) ? (100.0 / (double)_length) : 0.0);
	}

	public override void Flush()
	{
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		int result = _streamImplementation.Read(buffer, offset, count);
		ReportProgress();
		return result;
	}

	public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
	{
		return ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
	}

	public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
	{
		int read = await _streamImplementation.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
		ReportProgress();
		return read;
	}

	private void ReportProgress()
	{
		long position = Position;
		if (_lastPosition == position)
		{
			return;
		}
		_lastPosition = position;
		if (position != _length)
		{
			long timestamp = Stopwatch.GetTimestamp();
			if (timestamp - _lastUpdate < _updateInterval)
			{
				return;
			}
			_lastUpdate = timestamp;
		}
		_progress.Report(checked((int)Math.Round(_toPercent * (double)position)));
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		return _streamImplementation.Seek(offset, origin);
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException();
	}
}
