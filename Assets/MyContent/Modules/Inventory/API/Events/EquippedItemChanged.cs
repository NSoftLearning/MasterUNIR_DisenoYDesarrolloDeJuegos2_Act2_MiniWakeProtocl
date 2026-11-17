using Modules.EventHandling.API;

namespace Modules.Inventory.API
{
    public class EquippedItemChanged : DomainEventBase
    {
        public ItemDefinition Item { get; }

        public EquippedItemChanged(ItemDefinition item)
        {
            Item = item;
        }
    }
}