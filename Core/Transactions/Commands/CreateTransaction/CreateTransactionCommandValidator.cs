using FluentValidation;

namespace Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.request.Amount).NotNull().GreaterThan(0);
        RuleFor(x => x.request.ReceiverId).NotNull().NotEmpty();
        RuleFor(x => x.request.SenderId).NotNull().NotEmpty();
    }
}
