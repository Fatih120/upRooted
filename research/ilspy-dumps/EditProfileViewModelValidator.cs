// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.EditProfileViewModelValidator
using FluentValidation;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.WebApi.Shared;

public class EditProfileViewModelValidator : AbstractValidator<EditProfileViewModel>
{
	public EditProfileViewModelValidator()
	{
		RuleFor((EditProfileViewModel vm) => vm.Username).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods")
			.MinimumLength(3)
			.WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods")
			.MaximumLength(20)
			.WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods")
			.Matches(GlobalConstants.UserAuthentication.USERNAME_REGEX())
			.WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods");
		RuleFor((EditProfileViewModel vm) => vm.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("A valid email is required")
			.EmailAddress()
			.WithMessage("A valid email is required");
	}
}

