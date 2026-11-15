using MessagePipe;
using UnityEngine;
using Zenject;

[CreateAssetMenu(
    fileName = "EventBusInstaller",
    menuName = "Tools/Config/Modules/EventHandling")]
public class EventBusInstaller :
    ScriptableObjectInstaller<EventBusInstaller>
{
    public override void InstallBindings()
    {
        var options = Container.BindMessagePipe();

        Container.BindMessageBroker<IDomainEvent>(options);

        Container.Bind<DomainEventBus>()
            .To<MessagePipeDomainEventBus>()
            .AsSingle();
    }
}