using Modules.EventHandling;
using Modules.EventHandling.API;
using UnityEngine;

namespace PrototypesAndTests.Infra.EventBusTest
{
    public class EventBusGeneralTestEventHandler : IDomainEventHandler<EventBusGeneralTestEvent>
    {
        public void Handle(EventBusGeneralTestEvent DomainEvent)
        {
            Debug.Log("EVENT HANDLED WITH PAYLOAD " + DomainEvent.SamplePayload);
        }
    }
}
