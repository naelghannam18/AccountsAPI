using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Transactions.Commands.DeleteTransactions;

public class DeleteTransactionsCommandHandler : IRequestHandler<DeleteTransactionsCommand>
{
    private readonly ITransactionsRepository TransactionsRepository;

    public DeleteTransactionsCommandHandler(ITransactionsRepository transactionsRepository)
    {
        TransactionsRepository = transactionsRepository;
    }

    public async Task Handle(DeleteTransactionsCommand request, CancellationToken cancellationToken)
    {
        await TransactionsRepository.Delete(ids: request.ids);
    }
}
