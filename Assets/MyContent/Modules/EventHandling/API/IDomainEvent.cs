using System;

public interface IDomainEvent
{
    string Identifier { get; }
    float Timestamp { get; }
}
