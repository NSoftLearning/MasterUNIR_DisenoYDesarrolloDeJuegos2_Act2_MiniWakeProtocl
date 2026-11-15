using System;
using Modules.EventHandling;
using Modules.EventHandling.API;

namespace PrototypesAndTests.Infra.EventBusTest
{
    public class EventBusTestEventRegisterer : IDisposable
    {
        private readonly DomainEventBus _domainEventbus;
        private readonly IDomainEventHandler<EventBusGeneralTestEvent> _handler;

        EventBusTestEventRegisterer (
            DomainEventBus domainEventBus,
            IDomainEventHandler<EventBusGeneralTestEvent> handler)
        {
            _domainEventbus = domainEventBus;
            _handler = handler;

            Register();
        }


        void Register ()
        {
            _domainEventbus.RegisterHandler<EventBusGeneralTestEvent>(_handler);
        }
        public void Dispose()
        {
            _domainEventbus.UnregisterHandler<EventBusGeneralTestEvent>(_handler);
        }
    }
}
