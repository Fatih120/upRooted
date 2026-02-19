// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CTextBlockAutomationPeer
using Avalonia.Automation.Peers;
using RootApp.Client.Avalonia.Markdown.Components;

public class CTextBlockAutomationPeer : ControlAutomationPeer
{
	public new CTextBlock Owner => (CTextBlock)base.Owner;

	public CTextBlockAutomationPeer(CTextBlock P_0)
		: base(P_0)
	{
	}

	protected override AutomationControlType GetAutomationControlTypeCore()
	{
		return AutomationControlType.Text;
	}

	protected override string? GetNameCore()
	{
		return Owner.Text;
	}

	protected override bool IsControlElementCore()
	{
		return Owner.TemplatedParent == null && base.IsControlElementCore();
	}
}

