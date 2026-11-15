namespace Modules.EventHandling.API
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        void Handle (T DomainEvent);
    }
}