using FluentValidation;

namespace RootApp.Client.Avalonia.UI.Login;

public class ForgotUsernameViewModelValidator : AbstractValidator<ForgotUsernameViewModel>
{
	public ForgotUsernameViewModelValidator()
	{
		RuleFor((ForgotUsernameViewModel vm) => vm.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("A valid email is required")
			.EmailAddress()
			.WithMessage("A valid email is required");
	}
}
