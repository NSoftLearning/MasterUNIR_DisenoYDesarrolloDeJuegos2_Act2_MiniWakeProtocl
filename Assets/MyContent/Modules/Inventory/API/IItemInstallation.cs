using System;

namespace Modules.Inventory.API
{
    public interface IItemInstallation
    {
        bool IsInstalled { get; }

        event Action Installed;
    }
}