using MiniControllers;
using Modules.Interaction.API;
using UnityEngine;

namespace Modules.Integrations.Private
{
    public class InteractionToSequence : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour interactable;
        [SerializeField] private SequenceController sequenceController;

        private IInteractable _interactable;

        private void Awake()
        {
            _interactable = (IInteractable)interactable;
        }

        private void OnEnable()
        {
            _interactable.InteractionStarted += PlaySequence;
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= PlaySequence;
        }

        private void PlaySequence()
        {
            sequenceController.PlaySequence();
        }
    }
}