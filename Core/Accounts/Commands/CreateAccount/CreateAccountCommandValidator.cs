using FluentValidation;

namespace Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.request.CustomerId).NotNull();
        RuleFor(x => x.request.InitialCredit).GreaterThanOrEqualTo(0);
    }
}
