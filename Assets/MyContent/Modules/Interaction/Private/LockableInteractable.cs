using System;
using Modules.Core.API;
using Modules.Core.Exceptions;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using UnityEngine;
using Zenject;

namespace Modules.Interaction.Private
{
    public class LockableInteractable :
        MonoBehaviour,
        IInteractable,
        ILockableInteraction
    {
        [SerializeField] private MonoBehaviour dataProvider;
        [SerializeField] private bool locked = true;

        [SerializeField] private string lockedPrompt;
        [SerializeField] private string lockedMessage;

        [SerializeField, Min(0.1f)] private float feedbackDuration = 3f;

        private IInteractableReadData _readData;
        private DomainEventBus _eventBus;
        private bool _interactionAccepted;

        public string InteractionPrompt => CalculatePrompt();
            

        public event Action InteractionStarted;
        public event Action InteractionStopped;

        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            if (dataProvider == this ||
                dataProvider is not IInteractableReadData)
            {
                throw new WrongPrefabConfigurationException(
                    "LockableInteractable requires an external data provider");
            }

            _readData = (IInteractableReadData)dataProvider;
        }

        public void Select()
        {
        }

        public void Unselect()
        {
        }

        public void StartInteraction()
        {
            if (locked)
            {
                _eventBus.Publish(
                    new FeedbackRequested(
                        lockedMessage,
                        feedbackDuration));

                return;
            }

            _interactionAccepted = true;
            InteractionStarted?.Invoke();
        }

        public void StopInteration()
        {
            if (!_interactionAccepted)
                return;

            _interactionAccepted = false;
            InteractionStopped?.Invoke();
        }

        [ContextMenu("Unlock")]
        public void Unlock()
        {
            locked = false;
        }

        string CalculatePrompt()
        {
            return locked ? lockedPrompt : _readData.InteractionPrompt;
        }
        
    }
}