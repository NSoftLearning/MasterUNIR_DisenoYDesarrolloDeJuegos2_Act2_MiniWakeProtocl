using Modules.PlayerControl.API;
using UnityEngine;
using Zenject;

namespace Modules.PlayerControl.Private
{
    [CreateAssetMenu(
        fileName = "PlayerControlInstaller",
        menuName = "Tools/Config/Modules/PlayerControl/Installer")]
    public class PlayerControllerInstaller :
        ScriptableObjectInstaller<PlayerControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerControl>()
                .To<StarterAssetsPlayerControl>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.Bind<IPlayerControlBlocker>()
                .To<PlayerControlBlocker>()
                .AsSingle();
        }
    }
}