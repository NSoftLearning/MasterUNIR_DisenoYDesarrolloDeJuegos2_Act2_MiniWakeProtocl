using Modules.EventHandling.API;

namespace Modules.Inventory.API
{
    public class InventoryClosed : DomainEventBase
    {
        public InventoryClosed(object source) : base(source)
        {
        }
    }
}