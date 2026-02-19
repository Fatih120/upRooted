// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.EnterVerificationCodeViewModelValidator
using FluentValidation;
using RootApp.Client.Avalonia.UI.Home;

public class EnterVerificationCodeViewModelValidator : AbstractValidator<EnterVerificationCodeViewModel>
{
	public EnterVerificationCodeViewModelValidator()
	{
		RuleFor((EnterVerificationCodeViewModel vm) => vm.ConfirmationTextBoxText).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("A valid verification code is required")
			.Length(6)
			.WithMessage("A valid verification code is required");
	}
}

