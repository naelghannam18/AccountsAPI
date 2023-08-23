using Domain.Contracts.Infrastructure;
using Domain.Models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Accounts.Commands.CreateAccount;
public sealed class AccountCreatedEventConsumer : IConsumer<AccountCreatedEvent>
{
    #region Private Readonly Fields
    private readonly ILogger<AccountCreatedEventConsumer> Logger;
    private readonly ITransactionsRepository TransactionsRepository;
    #endregion

    #region Constructor
    public AccountCreatedEventConsumer(ILogger<AccountCreatedEventConsumer> logger, ITransactionsRepository transactionsRepository)
    {
        Logger = logger;
        TransactionsRepository = transactionsRepository;
    }
    #endregion

    #region Consume Method
    public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
    {
        Logger.LogInformation("Account Created: {0}", context.Message);

        if (context.Message.InitialCredit > 0)
        {
            Transaction transaction = new()
            {
                Amount = context.Message.InitialCredit,
                ReceiverId = context.Message.AccountId,
            };
            await TransactionsRepository.Create(transaction);
        }
    } 
    #endregion
}
