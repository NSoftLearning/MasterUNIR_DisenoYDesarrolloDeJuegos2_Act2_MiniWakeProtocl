using Modules.EventHandling.API;
using Modules.NarrativePlayback.API;
using UnityEngine;
using Zenject;

namespace Modules.NarrativePlayback.Private
{
    public class NarrativeStarter : MonoBehaviour
    {
        [SerializeField] private NarrativeSequence sequence;

        private DomainEventBus _eventBus;

        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void StartNarrative()
        {
            _eventBus.Publish(new NarrativeRequested(sequence));
        }
    }
}