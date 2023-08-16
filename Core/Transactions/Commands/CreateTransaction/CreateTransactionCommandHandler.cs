using Domain.Exceptions;
using System.Net;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using Infrastructure.Repositories.Implementations;
using MediatR;

namespace Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<string>>
{
    private readonly ITransactionsRepository TransactionsRepository;
    private readonly IAccountsRepository AccountsRepository;

    public CreateTransactionCommandHandler(ITransactionsRepository transactionRepository, IAccountsRepository accountsRepository)
    {
        TransactionsRepository = transactionRepository;
        AccountsRepository = accountsRepository;
    }

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

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };
    }
}
