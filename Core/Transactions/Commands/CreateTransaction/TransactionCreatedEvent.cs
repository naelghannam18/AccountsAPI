using Domain.Abstractions.Events;

namespace Application.Transactions.Commands.CreateTransaction;
public record TransactionCreatedEvent : IEventMessage
{
    public decimal Amount { get; set; }

    public string SenderAccountId { get; set; } = string.Empty;

    public string ReceiverAccountId { get; set; } = string.Empty;
}
