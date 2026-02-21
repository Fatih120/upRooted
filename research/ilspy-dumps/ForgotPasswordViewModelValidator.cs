using FluentValidation;

namespace RootApp.Client.Avalonia.UI.Login;

public class ForgotPasswordViewModelValidator : AbstractValidator<ForgotPasswordViewModel>
{
	public ForgotPasswordViewModelValidator()
	{
		RuleFor((ForgotPasswordViewModel vm) => vm.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("A valid email is required")
			.EmailAddress()
			.WithMessage("A valid email is required");
	}
}
