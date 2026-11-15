using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Zenject;

public class DomainEventTest_Publisher_MB : MonoBehaviour
{
    DomainEventBus _domainEventBus;


    [Inject]
    public void Construct(DomainEventBus domainEventBus)
    {
        _domainEventBus = domainEventBus;
    }



    [ContextMenu ("publish test event")]
    public void PublishEventTest () 
    {
        EventBusGeneralTestEvent testEvent = new EventBusGeneralTestEvent("test event"); 
        _domainEventBus.Publish(testEvent); }
}
