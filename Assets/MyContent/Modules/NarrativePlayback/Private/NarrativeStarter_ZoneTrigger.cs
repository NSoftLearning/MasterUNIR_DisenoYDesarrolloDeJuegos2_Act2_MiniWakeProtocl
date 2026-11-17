using Modules.EventHandling.API;
using Modules.NarrativePlayback.API;
using Modules.PlayerControl.API;
using UnityEngine;
using Zenject;

namespace Modules.NarrativePlayback.Private
{
    public class NarrativeStarter_ZoneTrigger : MonoBehaviour
    {
        [SerializeField] private NarrativeSequence sequence;
        [SerializeField] private bool playOnce = true;

        private DomainEventBus _eventBus;
        private bool _requested;

        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerEntity>() == null)
                return;

            if (playOnce && _requested)
                return;

            _requested = true;

            _eventBus.Publish(new NarrativeRequested(sequence));
        }
    }
}