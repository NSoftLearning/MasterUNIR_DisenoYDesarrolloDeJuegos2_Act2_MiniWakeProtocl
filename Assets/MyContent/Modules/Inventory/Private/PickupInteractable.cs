using Modules.Interaction.API;
using Modules.Inventory.API;
using UnityEngine;
using Zenject;

namespace Modules.Inventory.Private
{
    public class PickupInteractable : MonoBehaviour, IInteractable
    {

        [SerializeField] private ItemDefinition item;

        private IInventory _inventory;
        public string InteractionPrompt => $"Recoger {item.DisplayName}";
    
        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }
    
        public void Select()
        {
        }

        public void Unselect()
        {
        }

        public void StartInteraction()
        {
            _inventory.Add(item);
            Debug.Log(
                $"Picked:  {item.DisplayName} Objects in inventory {_inventory.items.Count}", this);

            gameObject.SetActive(false);
        }

        public void StopInteration()
        {
     
        }
    }
}
