using Game.GameCore.Pause;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace DI
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _Asteroid;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
        }
    }
}