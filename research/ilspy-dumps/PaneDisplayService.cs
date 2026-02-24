using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace RootApp.Client.Avalonia.Helpers.Panes;

public class PaneDisplayService : ObservableObject
{
	[CompilerGenerated]
	private SplitViewDisplayMode <GlobalPaneDisplayMode>k__BackingField;

	[CompilerGenerated]
	private SplitViewDisplayMode <CommunityPaneDisplayMode>k__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public SplitViewDisplayMode GlobalPaneDisplayMode
	{
		get
		{
			return <GlobalPaneDisplayMode>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<SplitViewDisplayMode>.Default.Equals(<GlobalPaneDisplayMode>k__BackingField, splitViewDisplayMode))
			{
				<GlobalPaneDisplayMode>k__BackingField = splitViewDisplayMode;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.GlobalPaneDisplayMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public SplitViewDisplayMode CommunityPaneDisplayMode
	{
		get
		{
			return <CommunityPaneDisplayMode>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<SplitViewDisplayMode>.Default.Equals(<CommunityPaneDisplayMode>k__BackingField, splitViewDisplayMode))
			{
				<CommunityPaneDisplayMode>k__BackingField = splitViewDisplayMode;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityPaneDisplayMode);
			}
		}
	}

	public void ReportCommunityPaneWidth(double P_0, bool P_1)
	{
		SplitViewDisplayMode splitViewDisplayMode = CommunityPaneDisplayMode;
		if (P_0 < 320.0)
		{
			splitViewDisplayMode = SplitViewDisplayMode.Overlay;
		}
		else if (P_0 >= 735.0)
		{
			splitViewDisplayMode = SplitViewDisplayMode.Inline;
		}
		if (CommunityPaneDisplayMode != splitViewDisplayMode)
		{
			CommunityPaneDisplayMode = splitViewDisplayMode;
		}
		updateGlobalPaneDisplayMode(P_0, P_1);
	}

	private void updateGlobalPaneDisplayMode(double P_0, bool P_1)
	{
		SplitViewDisplayMode splitViewDisplayMode = GlobalPaneDisplayMode;
		if (P_0 < 320.0)
		{
			splitViewDisplayMode = SplitViewDisplayMode.Overlay;
		}
		else if (P_0 >= 735.0)
		{
			splitViewDisplayMode = SplitViewDisplayMode.Inline;
		}
		if (P_1 && CommunityPaneDisplayMode == SplitViewDisplayMode.Overlay)
		{
			splitViewDisplayMode = SplitViewDisplayMode.Inline;
		}
		if (GlobalPaneDisplayMode != splitViewDisplayMode)
		{
			GlobalPaneDisplayMode = splitViewDisplayMode;
		}
	}

	public void SetGlobalPaneDisplayMode(SplitViewDisplayMode P_0)
	{
		GlobalPaneDisplayMode = P_0;
	}
}
