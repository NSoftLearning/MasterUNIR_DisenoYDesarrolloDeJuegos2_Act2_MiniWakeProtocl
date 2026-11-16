using System.Collections.Generic;

namespace Modules.Inventory.API
{
    public interface IInventoryReadData
    {
        IReadOnlyList<ItemDefinition> items { get; }
        ItemDefinition EquippedItem { get; }
    }
}