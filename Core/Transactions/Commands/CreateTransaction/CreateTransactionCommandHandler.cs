using System.Net;
using Domain.Abstractions.Events;
using Domain.Contracts.Infrastructure;
using Domain.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<string>>
{
    #region Private Readonly Fields
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly IAccountsRepository AccountsRepository;
    private readonly IEventBus EventBus;
    #endregion

    #region Constructor
    public CreateTransactionCommandHandler(ITransactionsRepository transactionsRepository, IAccountsRepository accountsRepository, IEventBus eventBus)
    {
        TransactionsRepository = transactionsRepository;
        AccountsRepository = accountsRepository;
        EventBus = eventBus;
    }
    #endregion

    #region Command Handler
    public async Task<Response<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ModelValidationException(new() { "Please Provide Missing Values" });

        var sender = await AccountsRepository.GetById(request.request.SenderId);
        var receiver = await AccountsRepository.GetById(request.request.ReceiverId);

        if (sender is null || receiver is null)
        {
            throw new AccountDoesNotExistException();
        }

        if (sender.Balance < request.request.Amount)
        {
            throw new AccountInsufficientFundsException(sender.Balance);
        }

        var entity = new Transaction()
        {
            Amount = request.request.Amount,
            SenderId = request.request.SenderId,
            ReceiverId = request.request.ReceiverId,
        };
        var result = await TransactionsRepository.Create(entity);

        await EventBus.PublishAsync(
            new TransactionCreatedEvent()
            {
                Amount = entity.Amount,
                SenderAccountId = entity.SenderId,
                ReceiverAccountId = entity.ReceiverId
            },
            cancellationToken);

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };
    } 
    #endregion
}
