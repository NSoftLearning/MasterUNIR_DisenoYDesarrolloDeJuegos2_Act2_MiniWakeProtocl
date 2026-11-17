namespace Modules.Inventory.API
{
    public interface IInventory : IInventoryReadData
    {
        void Add(ItemDefinition item);
        void Remove(ItemDefinition item);
        void Equip (ItemDefinition item);
    }
}