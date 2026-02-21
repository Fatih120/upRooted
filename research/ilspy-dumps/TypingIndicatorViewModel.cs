using System.Threading.Tasks;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.User;

namespace RootApp.Client.Avalonia.UI.Messages;

public class TypingIndicatorViewModel : ViewModelBase<TypingIndicatorViewModel>
{
	private readonly BitmapCache _bitmapCache;

	public IMessageContainerMember? MessageContainerMember { get; }

	public GlobalUser GlobalUser { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(GlobalUser.ProfilePictureUri, null, 120);

	public TypingIndicatorViewModel(IMessageContainerMember P_0, BitmapCache P_1)
		: base((IValidator<TypingIndicatorViewModel>?)null)
	{
		_bitmapCache = P_1;
		MessageContainerMember = P_0;
		GlobalUser = MessageContainerMember.GlobalUser;
	}

	public TypingIndicatorViewModel(GlobalUser P_0, BitmapCache P_1)
		: base((IValidator<TypingIndicatorViewModel>?)null)
	{
		_bitmapCache = P_1;
		GlobalUser = P_0;
	}
}
