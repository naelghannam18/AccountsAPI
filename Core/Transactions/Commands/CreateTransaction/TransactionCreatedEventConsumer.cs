using Domain.Contracts.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Transactions.Commands.CreateTransaction;
public sealed class TransactionCreatedEventConsumer : IConsumer<TransactionCreatedEvent>
{
    #region Private Readonly Fields
    private ILogger<TransactionCreatedEventConsumer> Logger;
    private IAccountsRepository AccountsRepository;
    #endregion

    #region Constructor
    public TransactionCreatedEventConsumer(ILogger<TransactionCreatedEventConsumer> logger, IAccountsRepository accountsRepository)
    {
        Logger = logger;
        AccountsRepository = accountsRepository;
    }
    #endregion

    #region Consume Method
    public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
    {
        Logger.LogInformation("Transaction Created: {0}", context.Message);

        var sender = await AccountsRepository.GetById(context.Message.SenderAccountId);
        var receiver = await AccountsRepository.GetById(context.Message.ReceiverAccountId);

        sender.Balance -= context.Message.Amount;
        receiver.Balance += context.Message.Amount;

        await AccountsRepository.Update(sender);
        await AccountsRepository.Update(receiver);
    } 
    #endregion
}
