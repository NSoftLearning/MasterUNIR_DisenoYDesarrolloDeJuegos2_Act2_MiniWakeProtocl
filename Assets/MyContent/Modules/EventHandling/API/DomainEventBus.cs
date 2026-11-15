namespace Modules.EventHandling.API
{
    public interface DomainEventBus
    {
        void Publish (IDomainEvent domainEvent);

        void RegisterHandler<TEvent>(IDomainEventHandler<TEvent> handler) where TEvent : IDomainEvent;
        void UnregisterHandler<TEvent>(IDomainEventHandler<TEvent> handler) where TEvent : IDomainEvent;
   
    }
}
