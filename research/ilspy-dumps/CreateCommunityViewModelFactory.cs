// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.CreateCommunityViewModelFactory
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Helpers.DiscordTemplates;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;

public class CreateCommunityViewModelFactory(IRootSessionAccessor P_0, ImageUploaderViewModelFactory P_1, DiscordTemplateService P_2)
{
	public CreateCommunityViewModel Create()
	{
		return new CreateCommunityViewModel(P_0, P_1, P_2);
	}
}

