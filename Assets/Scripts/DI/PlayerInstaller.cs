using Game.ObjectPool;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlatformMovement _platformMovement;
        public override void InstallBindings()
        {
            Container.Bind<PlatformMovement>().FromInstance(_platformMovement).AsSingle().NonLazy();
        }
    }
}