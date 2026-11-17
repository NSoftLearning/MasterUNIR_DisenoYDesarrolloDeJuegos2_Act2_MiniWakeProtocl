using System;
using Modules.EventHandling.API;
using Modules.Inventory.API;
using Modules.PlayerControl.API;
using Zenject;

namespace Modules.Integrations.Private
{
    public class InventoryToPlayerControlIntegration :
        IInitializable,
        IDisposable,
        IDomainEventHandler<InventoryOpened>,
        IDomainEventHandler<InventoryClosed>
    {
        private readonly DomainEventBus _eventBus;
        private readonly IPlayerControlBlocker _controlBlocker;

        public InventoryToPlayerControlIntegration(
            DomainEventBus eventBus,
            IPlayerControlBlocker controlBlocker)
        {
            _eventBus = eventBus;
            _controlBlocker = controlBlocker;
        }

        public void Initialize()
        {
            _eventBus.RegisterHandler<InventoryOpened>(this);
            _eventBus.RegisterHandler<InventoryClosed>(this);
        }

        public void Handle(InventoryOpened domainEvent)
        {
            _controlBlocker.BlockControl(domainEvent.Source);
        }

        public void Handle(InventoryClosed domainEvent)
        {
            _controlBlocker.ReleaseControl(domainEvent.Source);
        }

        public void Dispose()
        {
            _eventBus.UnregisterHandler<InventoryOpened>(this);
            _eventBus.UnregisterHandler<InventoryClosed>(this);
        }
    }
}