using Game.GameCore.Pause;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace DI
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
        }
    }
}