using UnityEngine;
using Zenject;

public class DomainEventTest_Subscriber_MB : MonoBehaviour, IDomainEventHandler<EventBusGeneralTestEvent>
{

    [SerializeField] GameObject cube;
    DomainEventBus _domainEventBus;

    [Inject]
    public void Construct(DomainEventBus domainEventBus)
    {
        _domainEventBus = domainEventBus;
    }

    public void Start()
    {
        _domainEventBus.RegisterHandler<EventBusGeneralTestEvent>(this);
    }

    public void OnDestroy()
    {
        _domainEventBus.UnregisterHandler<EventBusGeneralTestEvent>(this);
    }

    public void Handle(EventBusGeneralTestEvent DomainEvent)
    {
        cube.SetActive(false);
    }
}
