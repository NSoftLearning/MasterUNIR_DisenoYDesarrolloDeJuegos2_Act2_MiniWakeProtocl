using Modules.NarrativePlayback.API;
using UnityEngine;
using Zenject;

namespace Modules.NarrativePlayback.Private
{
    [CreateAssetMenu(
        fileName = "NarrativePlaybackInstaller",
        menuName = "Tools/Config/Modules/NarrativePlayback/Installer")]
    public class NarrativePlaybackInstaller :
        ScriptableObjectInstaller<NarrativePlaybackInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(
                    typeof(INarrativePlaybackControl),
                    typeof(IManualNarrativePlaybackControl))
                .To<NarrativeView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}