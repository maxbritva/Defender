using Game.FX;
using Game.Score;
using Player;
using Player.Input;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameCoreInstaller: MonoInstaller
    {
        [SerializeField] private FireButton _fireButton;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Canvas _mobileUI;
        [SerializeField] private ShakeCamera _shakeCamera;
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        [SerializeField] private DestroyEffectSpawner _destroyEffectSpawner;
        public override void InstallBindings()
        {
            Inputs();
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<ScoreCollector>().FromNew().AsSingle().NonLazy();
            Container.Bind<ShakeCamera>().FromInstance(_shakeCamera).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<DestroyEffectSpawner>().FromInstance(_destroyEffectSpawner).AsSingle().NonLazy();
        }
        private void Inputs()
        {
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle().NonLazy();
            Container.Bind<FireButton>().FromInstance(_fireButton).AsSingle().NonLazy();
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                _mobileUI.gameObject.SetActive(true);
                Container.Bind<IInput>().To<MobileInput>().FromNew().AsSingle().NonLazy();
                Debug.Log("mobile");
            }
            else
                Container.Bind<IInput>().To<DesktopInput>().FromNew().AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromNew().AsSingle().NonLazy();
        }
    }
    
 
}