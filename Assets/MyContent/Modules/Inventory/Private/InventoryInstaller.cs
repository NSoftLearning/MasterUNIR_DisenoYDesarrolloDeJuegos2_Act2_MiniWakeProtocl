using Modules.Inventory.API;
using UnityEngine;
using Zenject;

namespace Modules.Inventory.Private
{
    [CreateAssetMenu(
        fileName = "InventoryInstaller",
        menuName = "Tools/Config/Modules/Inventory/Installer")]
    public class InventoryInstaller :
        ScriptableObjectInstaller<InventoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInventory), typeof(IInventoryReadData))
                .To<Inventory>()
                .AsSingle();
            
            Container.Bind<InventoryPreviewRigView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}