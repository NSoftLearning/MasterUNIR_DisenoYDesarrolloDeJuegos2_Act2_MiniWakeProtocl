using System;
using Modules.Core.API;
using Modules.Core.API.Events;
using Modules.Core.Exceptions;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using Modules.Inventory.API;
using UnityEngine;
using Zenject;

namespace Modules.Inventory.Private
{
    public class ItemRequiredInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private MonoBehaviour dataProvider;
        [SerializeField] private ItemDefinition requiredItem;

        [SerializeField] private string failureMessage =
            "Necesito el objeto correcto";

        [SerializeField, Min(0.1f)] private float feedbackDuration = 3f;

        private IInteractableReadData _readData;
        private IInventoryReadData _inventory;
        private DomainEventBus _eventBus;
        private bool _interactionAccepted;

        public string InteractionPrompt => _readData.InteractionPrompt;

        public event Action InteractionStarted;
        public event Action InteractionStopped;

        [Inject]
        private void Construct(
            IInventoryReadData inventory,
            DomainEventBus eventBus)
        {
            _inventory = inventory;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            if (requiredItem == null)
            {
                throw new WrongPrefabConfigurationException(
                    "ItemRequiredInteractable requires an item");
            }

            if (dataProvider == this ||
                dataProvider is not IInteractableReadData)
            {
                throw new WrongPrefabConfigurationException(
                    "ItemRequiredInteractable requires an external data provider");
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
            if (_inventory.EquippedItem != requiredItem)
            {
                _eventBus.Publish(
                    new FeedbackRequested(
                        failureMessage,
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
    }
}