// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootSplitView
using Avalonia.Controls;
using Avalonia.Interactivity;

public class RootSplitView : SplitView
{
	protected override void OnPaneOpened(RoutedEventArgs P_0)
	{
		RaiseEvent(P_0);
	}
}

