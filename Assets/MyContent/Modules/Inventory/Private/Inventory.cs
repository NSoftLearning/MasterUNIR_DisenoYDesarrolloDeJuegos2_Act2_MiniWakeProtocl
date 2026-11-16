using System.Collections.Generic;
using Modules.Inventory.API;

namespace Modules.Inventory.Private
{
    public class Inventory : IInventory
    {
        private readonly List<ItemDefinition> _items = new();
        
        public IReadOnlyList<ItemDefinition> items { get; }
        public ItemDefinition EquippedItem { get; private set; }

        public Inventory ()
        {
            items = _items.AsReadOnly();
        }
        
        public void Add(ItemDefinition item)
        {
            if (item == null)
                throw new InventoryException("Cannot add a null item");
            if (_items.Contains(item))
                throw new InventoryException("Item already exists");
            
            _items.Add(item);
        }

        public void Remove(ItemDefinition item)
        {
            if (!_items.Remove(item))
                throw new InventoryException("Item does not exist");

            if (EquippedItem == item)
                EquippedItem = null;
        }

        public void Equip(ItemDefinition item)
        {
            if (item != null && !_items.Contains(item))
                throw new InventoryException("Item does not exist");

            EquippedItem = item;
        }
    }
}