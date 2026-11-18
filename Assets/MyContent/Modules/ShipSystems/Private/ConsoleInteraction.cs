using Modules.EventHandling.API;
using Modules.Interaction.API;
using Modules.NarrativePlayback.API;
using UnityEngine;
using Zenject;

namespace Modules.ShipSystems.Private
{
    public class ConsoleInteraction : MonoBehaviour, IInteractableReadData
    {
        [SerializeField] private string prompt = "Consultar consola";
        [SerializeField] private NarrativeSequence sequence;

        private IInteractable _interactable;
        private DomainEventBus _eventBus;

        public string InteractionPrompt => prompt;

        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
        }

        private void OnEnable()
        {
            _interactable.InteractionStarted += RequestNarrative;
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= RequestNarrative;
        }

        private void RequestNarrative()
        {
            _eventBus.Publish(new NarrativeRequested(sequence));
        }

        public void SetSequence(NarrativeSequence newSequence)
        {
            sequence = newSequence;
        }
    }
}