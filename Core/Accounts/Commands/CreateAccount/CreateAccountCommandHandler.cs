using Domain.Exceptions;
using System.Net;
using Domain.Models;
using Infrastructure.Repositories.Contracts;
using MediatR;

namespace Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<string>>
{
    private readonly IAccountsRepository AccountsRepository;
    private readonly ICustomersRepository CustomersRepository;

    public CreateAccountCommandHandler(IAccountsRepository accountsRepository, ICustomersRepository customersRepository)
    {
        AccountsRepository = accountsRepository;
        CustomersRepository = customersRepository;
    }

    public async Task<Response<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ModelValidationException(new() { "Please Provide Missing Values" });

        var customer = await CustomersRepository.GetById(request.request.CustomerId)
                       ??
                       throw new CustomerDoesNotExistException(request.request.CustomerId);

        var entity = new Account()
        {
            Balance = 0 + request.request.InitialCredit,
            CustomerId = customer.Id,
            Customer = customer,
            SentTransactions = new(),
            ReceivedTransactions = new()
        };

        if (request.request.InitialCredit > 0)
        {
            var transaction = new Transaction()
            {
                Amount = request.request.InitialCredit,
            };
            entity.ReceivedTransactions.Add(transaction);
        }
        var result = await AccountsRepository.Create(entity);

        return new()
        {
            Status = HttpStatusCode.Created,
            Data = result.Id
        };
    }
}
