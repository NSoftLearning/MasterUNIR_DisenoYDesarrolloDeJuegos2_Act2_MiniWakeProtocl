using Modules.Core.API;
using Modules.Core.API.Events;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using Modules.NarrativePlayback.API;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Modules.ShipSystems.Private
{
    public class PressureSystemController : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour holdInteraction;

        [SerializeField] private ConsoleInteraction console;
        [SerializeField] private NarrativeSequence equalizedSequence;

        private IHoldToCompleteInteraction _holdInteraction;
        [SerializeField] private UnityEvent pressureEqualized;
        private DomainEventBus _eventBus;

        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        private void Awake()
        {
            _holdInteraction =
                (IHoldToCompleteInteraction)holdInteraction;
            
        }
        private void CompleteEqualization()
        {
            console.SetSequence(equalizedSequence);
            pressureEqualized.Invoke();

            _eventBus.Publish(
                new FeedbackRequested(
                    "Presión igualada. Puerta desbloqueada."));
        }
        private void OnEnable()
        {
            _holdInteraction.Completed += CompleteEqualization;
        }

        private void OnDisable()
        {
            _holdInteraction.Completed -= CompleteEqualization;
        }
        
    }
}