// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.StreamGeometry
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class StreamGeometry : Geometry
{
	private IStreamGeometryImpl? _impl;

	public StreamGeometry()
	{
	}

	private StreamGeometry(IStreamGeometryImpl P_0)
	{
		_impl = P_0;
	}

	public new static StreamGeometry Parse(string P_0)
	{
		StreamGeometry streamGeometry = new StreamGeometry();
		using StreamGeometryContext streamGeometryContext = streamGeometry.Open();
		using PathMarkupParser pathMarkupParser = new PathMarkupParser(streamGeometryContext);
		pathMarkupParser.Parse(P_0);
		return streamGeometry;
	}

	public override Geometry Clone()
	{
		return new StreamGeometry(((IStreamGeometryImpl)base.PlatformImpl).Clone());
	}

	public StreamGeometryContext Open()
	{
		return new StreamGeometryContext(((IStreamGeometryImpl)base.PlatformImpl).Open());
	}

	private protected override IGeometryImpl? CreateDefiningGeometry()
	{
		if (_impl == null)
		{
			IPlatformRenderInterface requiredService = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>();
			_impl = requiredService.CreateStreamGeometry();
		}
		return _impl;
	}
}

