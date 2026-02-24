using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RootApp.Client.Avalonia.Controls;

public class RootSplitView : SplitView
{
	protected override void OnPaneOpened(RoutedEventArgs P_0)
	{
		RaiseEvent(P_0);
	}
}
