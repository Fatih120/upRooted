// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChangePasswordViewModelValidator
using FluentValidation;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.WebApi.Shared.Policies;

public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
{
	public ChangePasswordViewModelValidator()
	{
		RuleFor((ChangePasswordViewModel vm) => vm.NewPassword).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Must be at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")
			.Must(PasswordPolicy.IsValid)
			.WithMessage("Must be at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");
		RuleFor((ChangePasswordViewModel vm) => vm.ConfirmNewPassword).Cascade(CascadeMode.Stop).Equal<ChangePasswordViewModel>((ChangePasswordViewModel vm) => vm.NewPassword).WithMessage("Passwords must match");
	}
}

