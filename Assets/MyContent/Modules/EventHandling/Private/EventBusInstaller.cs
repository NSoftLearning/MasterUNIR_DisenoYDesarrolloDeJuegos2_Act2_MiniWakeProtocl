using MessagePipe;
using Modules.EventHandling.API;
using UnityEngine;
using Zenject;

namespace Modules.EventHandling.Private
{
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
}