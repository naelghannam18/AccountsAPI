using Domain.Abstractions.Events;
using MassTransit;

namespace Infrastructure.EventBus;
public sealed class EventBus : IEventBus
{
    #region Private Readonly Fields
    private readonly IBus Bus;
    #endregion

    #region Constructor
    public EventBus(IBus bus)
    {
        Bus = bus;
    }
    #endregion

    #region Publish Message
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken)
where T : class, IEventMessage
    {
        Bus.Publish(message, cancellationToken);

        return Task.CompletedTask;
    } 
    #endregion
}
