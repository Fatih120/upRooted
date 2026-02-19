// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.CreateCommunityViewModelValidator
using FluentValidation;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.WebApi.Shared;

public class CreateCommunityViewModelValidator : AbstractValidator<CreateCommunityViewModel>
{
	public CreateCommunityViewModelValidator()
	{
		RuleFor((CreateCommunityViewModel vm) => vm.CommunityName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Must only contain letters, numbers, spaces, apostrophes, and be 1 to 256 characters long")
			.MinimumLength(1)
			.WithMessage("Must only contain letters, numbers, spaces, apostrophes, and be 1 to 256 characters long")
			.MaximumLength(256)
			.WithMessage("Must only contain letters, numbers, spaces, apostrophes, and be 1 to 256 characters long")
			.Matches(GlobalConstants.CreateCommunity.NAME_REGEX())
			.WithMessage("Must only contain letters, numbers, spaces, apostrophes, and be 1 to 256 characters long");
	}
}

