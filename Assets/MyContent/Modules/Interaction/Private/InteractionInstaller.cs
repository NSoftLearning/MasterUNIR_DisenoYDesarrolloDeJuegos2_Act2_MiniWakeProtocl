using Modules.Interaction.API;
using UnityEngine;
using Zenject;

namespace Modules.Interaction.Private
{
    [CreateAssetMenu(
        fileName = "InteractionInstaller",
        menuName = "Tools/Config/Modules/Interaction/Installer")]
    public class InteractionInstaller :
        ScriptableObjectInstaller<InteractionInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInteractionControl>()
                .To<PlayerInteractor>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}