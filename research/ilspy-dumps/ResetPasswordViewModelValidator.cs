using FluentValidation;
using RootApp.WebApi.Shared.Policies;

namespace RootApp.Client.Avalonia.UI.Login;

public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
{
	public ResetPasswordViewModelValidator()
	{
		RuleFor((ResetPasswordViewModel vm) => vm.NewPassword).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Must be at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")
			.Must(PasswordPolicy.IsValid)
			.WithMessage("Must be at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");
		RuleFor((ResetPasswordViewModel vm) => vm.ConfirmNewPassword).Cascade(CascadeMode.Stop).Equal<ResetPasswordViewModel>((ResetPasswordViewModel vm) => vm.NewPassword).WithMessage("Passwords must match");
		RuleFor((ResetPasswordViewModel vm) => vm.VerificationCode).Cascade(CascadeMode.Stop).Length(16).WithMessage("A valid verification code is required")
			.NotEmpty()
			.WithMessage("A valid verification code is required");
	}
}
