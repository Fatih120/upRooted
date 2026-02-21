using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Immutable;
using Avalonia.Threading;
using DotNetBrowser.Browser;
using DotNetBrowser.Ui;

namespace RootApp.Browser;

public sealed class BrowserAppearanceHelper
{
	public void SetBrowserBackgroundColor(IBrowser P_0)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (!P_0.IsDisposed)
			{
				ImmutableSolidColorBrush immutableSolidColorBrush = (ImmutableSolidColorBrush)Application.Current.FindResource(Application.Current.ActualThemeVariant, "BackgroundPrimary");
				double num = (double)(int)immutableSolidColorBrush.Color.R / 255.0;
				double num2 = (double)(int)immutableSolidColorBrush.Color.G / 255.0;
				double num3 = (double)(int)immutableSolidColorBrush.Color.B / 255.0;
				double num4 = (double)(int)immutableSolidColorBrush.Color.A / 255.0;
				P_0.Settings.DefaultBackgroundColor = new Color((float)num, (float)num2, (float)num3, (float)num4);
			}
		});
	}
}
