using System.Collections.Generic;

namespace Modules.Inventory.API
{
    public interface IInventoryReadData
    {
        IReadOnlyList<ItemDefinition> items { get; }
        ItemDefinition EquippedItem { get; }
    }

    public interface IInventory : IInventoryReadData
    {
        void Add(ItemDefinition item);
        void Remove(ItemDefinition item);
        void Equip (ItemDefinition item);
    }
}