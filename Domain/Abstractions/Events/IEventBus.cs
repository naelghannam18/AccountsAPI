﻿namespace Domain.Abstractions.Events;
public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class, IEventMessage;
}
