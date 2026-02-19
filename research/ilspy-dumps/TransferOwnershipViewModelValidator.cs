// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.TransferOwnershipViewModelValidator
using FluentValidation;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class TransferOwnershipViewModelValidator : AbstractValidator<TransferOwnershipViewModel>
{
	public TransferOwnershipViewModelValidator()
	{
		RuleFor((TransferOwnershipViewModel vm) => vm.ConfirmationTextBoxText).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(Resources.PasswordRequiredToTransferOwnership);
	}
}

