using Modules.EventHandling.API;
using Modules.Inventory.API;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Modules.Inventory.Private
{
    public class InventoryView : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private TMP_Text itemName;

        [Header("Input")] [SerializeField] private InputActionReference toggleInventory;
        [SerializeField] private InputActionReference previousItem;
        [SerializeField] private InputActionReference nextItem;
        [SerializeField] private InputActionReference confirm;
        [SerializeField] private InputActionReference cancel;

        private IInventory _inventory;
        private int _selectedIndex;
        private bool _isOpened;
        private DomainEventBus _eventBus;
        private GameObject _previewModel;
        private InventoryPreviewRigView _previewRig;
        private ItemDefinition SelectedItem => CalculateSelectedItem();

        [Inject]
        private void Construct(
            IInventory inventory,
            DomainEventBus eventBus,
            InventoryPreviewRigView previewRig)
        {
            _inventory = inventory;
            _eventBus = eventBus;
            _previewRig = previewRig;
        }

        private ItemDefinition CalculateSelectedItem()
        {
            if (_selectedIndex == 0)
                return null;
            return _inventory.items[_selectedIndex - 1];
        }

        private void Awake()
        {
            inventoryPanel.SetActive(false);
        }


        private void Cancel(InputAction.CallbackContext obj)
        {
            Close();
        }

        private void Confirm(InputAction.CallbackContext obj)
        {
            if (!_isOpened)
                return;
            
            _inventory.Equip(SelectedItem);
            Close();
        }

        private void NextItem(InputAction.CallbackContext obj)
        {
            Navigate(1);
        }

        private void PreviousItem(InputAction.CallbackContext obj)
        {
            Navigate(-1);
        }

        private void Navigate(int direction)
        {
            int optionCount = _inventory.items.Count + 1;

            _selectedIndex =
                (_selectedIndex + direction + optionCount) % optionCount;

            RefreshView();
        }

        private void ToggleInventory(InputAction.CallbackContext obj)
        {
            if (_isOpened)
                Close();
            else
                Open();
        }

        private void Open()
        {
            _isOpened = true;

            RefreshView();
            inventoryPanel.SetActive(true);

            _eventBus.Publish(new InventoryOpened());
            EnableNavigation();
        }

        private void RefreshView()
        {
            ItemDefinition item = SelectedItem;

            itemName.text = item == null
                ? "Mano vacía"
                : item.DisplayName;

            RefreshPreview(item);
        }

        private void Close()
        {
            if (!_isOpened)
                return;

            ResetView();

            _eventBus.Publish(new InventoryClosed());
        }

        private void ResetView()
        {
            _isOpened = false;
            DisableNavigation();
            inventoryPanel.SetActive(false);

            ClearPreview();
        }

        private void EnableNavigation()
        {
            previousItem.action.Enable();
            nextItem.action.Enable();
            confirm.action.Enable();
            cancel.action.Enable();
        }

        private void DisableNavigation()
        {
            previousItem.action.Disable();
            nextItem.action.Disable();
            confirm.action.Disable();
            cancel.action.Disable();
        }

        private void ClearPreview()
        {
            if (_previewModel == null)
                return;

            Destroy(_previewModel);
            _previewModel = null;
        }

        private void RefreshPreview(ItemDefinition item)
        {
            ClearPreview();
            if (item == null)
                return;
            _previewModel = Instantiate(item.ModelPrefab, _previewRig.ModelAnchor, false);
            _previewModel.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(60, 0, 0));

            SetPreviewLayer(_previewModel.transform);

        }

        private void SetPreviewLayer(Transform target)
        {
            int layer = _previewRig.ModelAnchor.gameObject.layer;

            foreach (Transform child in target.GetComponentsInChildren<Transform>(true))
                child.gameObject.layer = layer;
        }

        private void OnEnable()
        {
            toggleInventory.action.performed += ToggleInventory;
            previousItem.action.performed += PreviousItem;
            nextItem.action.performed += NextItem;
            confirm.action.canceled += Confirm;
            cancel.action.performed += Cancel;

            toggleInventory.action.Enable();
        }

        private void OnDisable()
        {
            toggleInventory.action.performed -= ToggleInventory;
            previousItem.action.performed -= PreviousItem;
            nextItem.action.performed -= NextItem;
            confirm.action.canceled -= Confirm;
            cancel.action.performed -= Cancel;

            toggleInventory.action.Disable();
            ResetView();
        }
    }
}