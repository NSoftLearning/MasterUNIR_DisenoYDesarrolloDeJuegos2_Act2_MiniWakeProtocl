using System;
using Modules.Interaction.API;
using Modules.Inventory.API;
using Unity.Profiling.LowLevel;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Modules.Inventory.Private
{
    public class ItemSocket :
        MonoBehaviour,
        IInteractableReadData,
        IItemInstallation
    {
        [SerializeField] private string prompt = "Instalar cartucho";
        [SerializeField] private string installedPrompt = "Cartucho instalado";
        [SerializeField] private Transform modelAnchor;
        [SerializeField] private UnityEvent itemInstalled;
        private IInventory _inventory;
        private IInteractable _interactable;

        public bool IsInstalled { get; private set; }

        public string InteractionPrompt => CalculatePrompt();

        public event Action Installed;

        [Inject]
        private void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }

        string CalculatePrompt()
        {
            return IsInstalled ? installedPrompt : prompt;
        }

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
        }

        private void OnEnable()
        {
            _interactable.InteractionStarted += Install;
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= Install;
        }

        private void Install()
        {
            if (IsInstalled)
                return;

            ItemDefinition item = _inventory.EquippedItem;

            GameObject model = Instantiate(
                item.ModelPrefab,
                modelAnchor,
                false);

            model.transform.SetLocalPositionAndRotation(
                Vector3.zero,
                Quaternion.identity);

            foreach (Collider collider in model.GetComponentsInChildren<Collider>(true))
                collider.enabled = false;

            _inventory.Remove(item);

            IsInstalled = true;
            Installed?.Invoke();
            itemInstalled.Invoke();
        }
    }
}