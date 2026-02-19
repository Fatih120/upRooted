// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.DeleteCommunityViewModelValidator
using FluentValidation;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class DeleteCommunityViewModelValidator : AbstractValidator<DeleteCommunityViewModel>
{
	public DeleteCommunityViewModelValidator()
	{
		RuleFor((DeleteCommunityViewModel vm) => vm.ConfirmationTextBoxText).Cascade(CascadeMode.Stop).Equal<DeleteCommunityViewModel>((DeleteCommunityViewModel vm) => vm.CommunityName).WithMessage(Resources.DeleteConfirmationErrorMessage);
	}
}

