using System;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using Modules.PlayerControl.API;
using Zenject;

namespace Modules.Integrations.Private
{
    public class PlayerControlToInteractionIntegration :
        IInitializable,
        IDisposable,
        IDomainEventHandler<PlayerControlBlocked>,
        IDomainEventHandler<PlayerControlRestored>
    {
        private readonly DomainEventBus _eventBus;
        private readonly IInteractionControl _interactionControl;

        public PlayerControlToInteractionIntegration(
            DomainEventBus eventBus,
            IInteractionControl interactionControl)
        {
            _eventBus = eventBus;
            _interactionControl = interactionControl;
        }

        public void Initialize()
        {
            _eventBus.RegisterHandler<PlayerControlBlocked>(this);
            _eventBus.RegisterHandler<PlayerControlRestored>(this);
        }

        public void Handle(PlayerControlBlocked domainEvent)
        {
            _interactionControl.DisableInteraction();
        }

        public void Handle(PlayerControlRestored domainEvent)
        {
            _interactionControl.EnableInteraction();
        }

        public void Dispose()
        {
            _eventBus.UnregisterHandler<PlayerControlBlocked>(this);
            _eventBus.UnregisterHandler<PlayerControlRestored>(this);
        }
    }
}