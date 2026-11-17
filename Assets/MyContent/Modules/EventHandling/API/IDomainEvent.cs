namespace Modules.EventHandling.API
{
    public interface IDomainEvent
    {
        string Identifier { get; }
        float Timestamp { get; }
        object Source { get; }
    }
}
