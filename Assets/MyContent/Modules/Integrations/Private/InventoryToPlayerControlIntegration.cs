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
        private readonly IPlayerControl _playerControl;

        public InventoryToPlayerControlIntegration(
            DomainEventBus eventBus,
            IPlayerControl playerControl)
        {
            _eventBus = eventBus;
            _playerControl = playerControl;
        }

        public void Initialize()
        {
            _eventBus.RegisterHandler<InventoryOpened>(this);
            _eventBus.RegisterHandler<InventoryClosed>(this);
        }

        public void Handle(InventoryOpened domainEvent)
        {
            _playerControl.DisableControl();
        }

        public void Handle(InventoryClosed domainEvent)
        {
            _playerControl.EnableControl();
        }

        public void Dispose()
        {
            _eventBus.UnregisterHandler<InventoryOpened>(this);
            _eventBus.UnregisterHandler<InventoryClosed>(this);
        }
    }
}