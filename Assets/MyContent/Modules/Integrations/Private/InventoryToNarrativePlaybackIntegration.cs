using System;
using Modules.EventHandling.API;
using Modules.Inventory.API;
using Modules.NarrativePlayback.API;
using Zenject;

namespace Modules.Integrations.Private
{
    public class InventoryToNarrativePlaybackIntegration :
        IInitializable,
        IDisposable,
        IDomainEventHandler<InventoryOpened>,
        IDomainEventHandler<InventoryClosed>
    {
        private readonly DomainEventBus _eventBus;
        private readonly INarrativePlaybackControl _playback;

        public InventoryToNarrativePlaybackIntegration(
            DomainEventBus eventBus,
            INarrativePlaybackControl playback)
        {
            _eventBus = eventBus;
            _playback = playback;
        }

        public void Initialize()
        {
            _eventBus.RegisterHandler<InventoryOpened>(this);
            _eventBus.RegisterHandler<InventoryClosed>(this);
        }

        public void Handle(InventoryOpened domainEvent)
        {
            _playback.SuspendPlayback();
        }

        public void Handle(InventoryClosed domainEvent)
        {
            _playback.ResumePlayback();
        }

        public void Dispose()
        {
            _eventBus.UnregisterHandler<InventoryOpened>(this);
            _eventBus.UnregisterHandler<InventoryClosed>(this);
        }
    }
}