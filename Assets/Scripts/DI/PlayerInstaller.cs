using Player.Input;
using Player.Inputs;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInput>().To<DesktopInput>().FromNew().AsSingle().NonLazy();
        }
    }
}