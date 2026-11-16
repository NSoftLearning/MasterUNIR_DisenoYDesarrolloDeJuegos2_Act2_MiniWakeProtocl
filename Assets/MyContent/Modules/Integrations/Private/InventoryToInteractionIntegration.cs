using System;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using Modules.Inventory.API;
using Zenject;

namespace Modules.Integrations.Private
{
    public class InventoryToInteractionIntegration :
        IInitializable,
        IDisposable,
        IDomainEventHandler<InventoryOpened>,
        IDomainEventHandler<InventoryClosed>
    {
        private readonly DomainEventBus _eventBus;
        private readonly IInteractionControl _interactionControl;

        public InventoryToInteractionIntegration(
            DomainEventBus eventBus,
            IInteractionControl interactionControl)
        {
            _eventBus = eventBus;
            _interactionControl = interactionControl;
        }

        public void Initialize()
        {
            _eventBus.RegisterHandler<InventoryOpened>(this);
            _eventBus.RegisterHandler<InventoryClosed>(this);
        }

        public void Handle(InventoryOpened domainEvent)
        {
            _interactionControl.DisableInteraction();
        }

        public void Handle(InventoryClosed domainEvent)
        {
            _interactionControl.EnableInteraction();
        }

        public void Dispose()
        {
            _eventBus.UnregisterHandler<InventoryOpened>(this);
            _eventBus.UnregisterHandler<InventoryClosed>(this);
        }
    }
}