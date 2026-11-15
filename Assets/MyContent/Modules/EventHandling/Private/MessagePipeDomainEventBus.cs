using System;
using System.Collections.Generic;
using MessagePipe;

public class MessagePipeDomainEventBus : DomainEventBus
{
    private readonly IPublisher<IDomainEvent> _publisher;
    private readonly ISubscriber<IDomainEvent> _subscriber;

    private readonly Dictionary<(Type, object), IDisposable> _subscriptions
        = new Dictionary<(Type, object), IDisposable>();

    public MessagePipeDomainEventBus(
        IPublisher<IDomainEvent> publisher,
        ISubscriber<IDomainEvent> subscriber)
    {
        _publisher = publisher;
        _subscriber = subscriber;
    }

    public void Publish(IDomainEvent domainEvent)
    {
        _publisher.Publish(domainEvent);
    }

    public void RegisterHandler<TEvent>(
        IDomainEventHandler<TEvent> handler)
        where TEvent : IDomainEvent
    {
        var key = (typeof(TEvent), (object)handler);

        
        if (_subscriptions.ContainsKey(key))
            return;

        var subscription = _subscriber.Subscribe(domainEvent =>
        {
            if (domainEvent is TEvent typedEvent)
                handler.Handle(typedEvent);
        });

        _subscriptions.Add(key, subscription);
    }

    public void UnregisterHandler<TEvent>(
        IDomainEventHandler<TEvent> handler)
        where TEvent : IDomainEvent
    {
        var key = (typeof(TEvent), (object)handler);

        if (!_subscriptions.TryGetValue(key, out var subscription))
            return;

        subscription.Dispose();
        _subscriptions.Remove(key);
    }
}