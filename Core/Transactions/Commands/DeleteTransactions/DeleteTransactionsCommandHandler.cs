using Domain.Contracts.Infrastructure;
using MediatR;

namespace Application.Transactions.Commands.DeleteTransactions;

public class DeleteTransactionsCommandHandler : IRequestHandler<DeleteTransactionsCommand>
{
    #region Private Readonly Fields
    private readonly ITransactionsRepository TransactionsRepository;
    #endregion

    #region Constructor
    public DeleteTransactionsCommandHandler(ITransactionsRepository transactionsRepository)
    {
        TransactionsRepository = transactionsRepository;
    }
    #endregion

    #region Command Handler
    public async Task Handle(DeleteTransactionsCommand request, CancellationToken cancellationToken)
    {
        await TransactionsRepository.Delete(ids: request.ids);
    } 
    #endregion
}
