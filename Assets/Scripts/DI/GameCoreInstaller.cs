using Player;
using Player.Input;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameCoreInstaller: MonoInstaller
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private FireButton _fireButton;
        [SerializeField] private PlatformVisualChanger _platformVisualChanger;
        public override void InstallBindings()
        {
            Inputs();
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlatformVisualChanger>().FromInstance(_platformVisualChanger).AsSingle().NonLazy();
            Container.Bind<PlatformInitialize>().FromNew().AsSingle().NonLazy();
           
        }
        private void Inputs()
        {
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle().NonLazy();
            Container.Bind<FireButton>().FromInstance(_fireButton).AsSingle().NonLazy();
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.Bind<IInput>().To<MobileInput>().FromNew().AsSingle().NonLazy();
                Debug.Log("mobile");
            }
            else
                Container.Bind<IInput>().To<DesktopInput>().FromNew().AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromNew().AsSingle().NonLazy();
        }
    }
    
 
}