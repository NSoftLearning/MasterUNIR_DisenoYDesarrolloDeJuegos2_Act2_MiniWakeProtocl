using System;

namespace Modules.Inventory.API
{
    public class InventoryException : Exception
    {
        public InventoryException(string message)
            : base(message)
        {
        }
    }
}