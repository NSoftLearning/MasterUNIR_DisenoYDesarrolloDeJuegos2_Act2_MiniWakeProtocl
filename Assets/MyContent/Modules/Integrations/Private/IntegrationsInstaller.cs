using UnityEngine;
using Zenject;

namespace Modules.Integrations.Private
{
    [CreateAssetMenu(
        fileName = "IntegrationsInstaller",
        menuName = "Tools/Config/Modules/Integrations/Installer")]
    public class IntegrationsInstaller :
        ScriptableObjectInstaller<IntegrationsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<InventoryToPlayerControlIntegration>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<InventoryToInteractionIntegration>()
                .AsSingle()
                .NonLazy();
        }
    }
}