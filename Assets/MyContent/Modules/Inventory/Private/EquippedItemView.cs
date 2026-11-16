    using System;
using Modules.EventHandling.API;
using Modules.Inventory.API;
using UnityEngine;
using Zenject;

namespace Modules.Inventory.Private
{
    public class EquippedItemView : MonoBehaviour,
        IDomainEventHandler<EquippedItemChanged>
    {
        private DomainEventBus _eventBus;
        private IInventoryReadData _inventory;
        private GameObject _model;

        [Inject]
        private void Construct(
            DomainEventBus eventBus,
            IInventoryReadData inventory)
        {
            _eventBus = eventBus;
            _inventory = inventory;
        }

        public void Handle(EquippedItemChanged domainEvent)
        {
            ShowItem(domainEvent.Item);
        }

        private void ShowItem(ItemDefinition item)
        {
            ClearModel();
            if (item == null)
                return;
            _model = Instantiate(item.ModelPrefab, transform, false);
            _model.transform.SetLocalPositionAndRotation(
                item.EquippedPositionOffset, 
                Quaternion.Euler(item.EquippedRotationOffset));
        }

        private void ClearModel ()
        {
            if (_model == null)
                return;
            Destroy(_model);
            _model = null;
        }

        private void OnEnable()
        {
            _eventBus.RegisterHandler<EquippedItemChanged>(this);
            ShowItem(_inventory.EquippedItem);
        }

        private void OnDisable()
        {
            _eventBus.UnregisterHandler<EquippedItemChanged>(this);
            ClearModel();
        }
    }
}