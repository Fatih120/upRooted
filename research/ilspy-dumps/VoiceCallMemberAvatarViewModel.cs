// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.VoiceCallMemberAvatarViewModel
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain.Models.Media;

public class VoiceCallMemberAvatarViewModel : ViewModelBase<VoiceCallMemberAvatarViewModel>
{
	private readonly BitmapCache _bitmapCache;

	public MediaMember Member { get; }

	public int Index { get; }

	public bool IsFirst => Index == 0;

	public Thickness AvatarMargin => IsFirst ? new Thickness(1.0, 0.0, 0.0, 0.0) : new Thickness(-3.0, 0.0, 0.0, 0.0);

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Member.GlobalUser.ProfilePictureUri, null, 60);

	public VoiceCallMemberAvatarViewModel(MediaMember P_0, BitmapCache P_1, int P_2)
		: base((IValidator<VoiceCallMemberAvatarViewModel>?)null)
	{
		Member = P_0;
		_bitmapCache = P_1;
		Index = P_2;
		Member.GlobalUser.PropertyChanged += onGlobalUserPropertyChanged;
	}

	private void onGlobalUserPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ProfilePictureUri")
			{
				OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
			}
		});
	}

	public override void Dispose()
	{
		Member.GlobalUser.PropertyChanged -= onGlobalUserPropertyChanged;
		base.Dispose();
	}
}

