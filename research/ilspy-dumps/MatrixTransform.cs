// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.MatrixTransform
using Avalonia;
using Avalonia.Media;
using Avalonia.Reactive;

public sealed class MatrixTransform : Transform
{
	public static readonly StyledProperty<Matrix> MatrixProperty = AvaloniaProperty.Register<MatrixTransform, Matrix>("Matrix", Matrix.Identity);

	public Matrix Matrix
	{
		get
		{
			return GetValue(MatrixProperty);
		}
		set
		{
			SetValue(MatrixProperty, value2);
		}
	}

	public override Matrix Value => Matrix;

	public MatrixTransform()
	{
		this.GetObservable(MatrixProperty).Subscribe(delegate
		{
			RaiseChanged();
		});
	}

	public MatrixTransform(Matrix P_0)
		: this()
	{
		Matrix = P_0;
	}
}

