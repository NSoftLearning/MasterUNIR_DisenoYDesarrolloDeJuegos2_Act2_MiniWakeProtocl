using UnityEngine;
using Zenject;

namespace PrototypesAndTests.Infra.EventBusTest
{
    [CreateAssetMenu(fileName = "TestInstaller", menuName = EventTestConstants.modulePath )]
    public class EventTestInstaller : ScriptableObjectInstaller<EventTestInstaller>
    {
        public override void InstallBindings()
        {
            //   Container.BindInterfacesAndSelfTo<EventBusTestEventRegisterer>().AsSingle();

        }
    }


    public class EventTestConstants
    {
        public const string modulePath = "Tools/Config/PrototypesAndTests/EventSystemTest";
    }
}