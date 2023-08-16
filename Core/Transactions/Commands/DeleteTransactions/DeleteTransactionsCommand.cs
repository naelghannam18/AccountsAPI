using MediatR;

namespace Application.Transactions.Commands.DeleteTransactions;

public sealed record DeleteTransactionsCommand(params string[] ids) : IRequest
{
}
