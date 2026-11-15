using UnityEngine;

public class EventBusGeneralTestEventHandler : IDomainEventHandler<EventBusGeneralTestEvent>
{
    public void Handle(EventBusGeneralTestEvent DomainEvent)
    {
        Debug.Log("EVENT HANDLED WITH PAYLOAD " + DomainEvent.SamplePayload);
    }
}
