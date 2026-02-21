using FluentValidation;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared;
using RootApp.WebApi.Shared.Policies;

namespace RootApp.Client.Avalonia.UI.Register;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
	public RegisterViewModelValidator(bool P_0)
	{
		RuleFor((RegisterViewModel vm) => vm.Username).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods")
			.MinimumLength(3)
			.WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods")
			.MaximumLength(20)
			.WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods")
			.Matches(GlobalConstants.UserAuthentication.USERNAME_REGEX())
			.WithMessage("Must be between 3-20 characters and may only contain letters, numbers, underscores and/or periods");
		RuleFor((RegisterViewModel vm) => vm.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("A valid email is required")
			.EmailAddress()
			.WithMessage("A valid email is required");
		RuleFor((RegisterViewModel vm) => vm.Password).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Must be at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character")
			.Must(PasswordPolicy.IsValid)
			.WithMessage("Must be at least eight characters, at least one uppercase letter, one lowercase letter, one number and one special character");
		if (P_0)
		{
			return;
		}
		RuleFor((RegisterViewModel vm) => vm.AccessToken).Cascade(CascadeMode.Stop).Must(delegate(string value)
		{
			try
			{
				return !string.IsNullOrEmpty(value) && RootGuid.Parse<RootGuid>(value) != null;
			}
			catch
			{
				return false;
			}
		}).WithMessage(RootApp.Client.Avalonia.Resources.Strings.Resources.InvalidAccessToken);
	}
}
