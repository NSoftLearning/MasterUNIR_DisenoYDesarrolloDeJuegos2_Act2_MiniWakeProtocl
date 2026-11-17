using System;
using Modules.EventHandling.API;
using Modules.NarrativePlayback.API;
using Modules.PlayerControl.API;
using Zenject;

namespace Modules.Integrations.Private
{
    public class NarrativeToPlayerControlIntegration :
        IInitializable,
        IDisposable,
        IDomainEventHandler<NarrativeStarted>,
        IDomainEventHandler<NarrativeFinished>
    {
        private readonly DomainEventBus _eventBus;
        private readonly IPlayerControlBlocker _controlBlocker;

        public NarrativeToPlayerControlIntegration(
            DomainEventBus eventBus,
            IPlayerControlBlocker controlBlocker)
        {
            _eventBus = eventBus;
            _controlBlocker = controlBlocker;
        }

        public void Initialize()
        {
            _eventBus.RegisterHandler<NarrativeStarted>(this);
            _eventBus.RegisterHandler<NarrativeFinished>(this);
        }

        public void Handle(NarrativeStarted domainEvent)
        {
            if (domainEvent.Sequence.BlockPlayerControl)
                _controlBlocker.BlockControl(domainEvent.Source);
        }

        public void Handle(NarrativeFinished domainEvent)
        {
            _controlBlocker.ReleaseControl(domainEvent.Source);
        }

        public void Dispose()
        {
            _eventBus.UnregisterHandler<NarrativeStarted>(this);
            _eventBus.UnregisterHandler<NarrativeFinished>(this);
        }
    }
}