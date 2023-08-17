using FluentValidation;

namespace Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(x => x.ids).NotNull().NotEmpty();
    }
}
