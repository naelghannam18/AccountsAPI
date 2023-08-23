using Domain.Abstractions.Events;

namespace Application.Accounts.Commands.CreateAccount;
public record AccountCreatedEvent : IEventMessage
{
    public string AccountId { get; set; }

    public decimal InitialCredit { get; set; }
}
