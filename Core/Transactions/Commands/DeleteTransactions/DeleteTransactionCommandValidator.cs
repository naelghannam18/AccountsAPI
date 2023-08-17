using FluentValidation;

namespace Application.Transactions.Commands.DeleteTransactions;

public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionsCommand>
{
    public DeleteTransactionCommandValidator()
    {
        RuleFor(x => x.ids).NotNull().NotEmpty();
    }
}
