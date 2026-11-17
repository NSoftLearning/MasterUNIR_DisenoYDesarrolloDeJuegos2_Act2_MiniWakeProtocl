using System;
using Modules.Interaction.API;
using Modules.Inventory.API;
using UnityEngine;
using Zenject;

namespace Modules.Inventory.Private
{
    public class PickupInteraction : MonoBehaviour, IInteractableReadData
    {

        [SerializeField] private ItemDefinition item;

        private IInventory _inventory;
        private IInteractable _interactable;

        public string InteractionPrompt => $"Recoger {item.DisplayName}";

        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
        }

        private void OnEnable()
        {
            _interactable.InteractionStarted += PickUp;
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= PickUp;
        }

        private void PickUp()
        {
            _inventory.Add(item);
            gameObject.SetActive(false);
        }
    }
}
