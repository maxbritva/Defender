using Game.Bonus;
using Game.FX;
using Game.GameCore.GameProgression;
using Game.GameCore.GameStates;
using Game.ObjectPool;
using Game.Score;
using Game.UI;
using Game.Weapons.Bonus;
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
        [SerializeField] private Shield _shield;
        [SerializeField] private Bomb _bomb;
        [SerializeField] private Pool _shipProjectilePool;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private LevelsHandler _levelsHandler;
        [SerializeField] private BonusSpawner _bonusSpawner;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameStartAnimation _gameStartAnimation;
        [SerializeField] private EndGame _endGame;
        [SerializeField] private EndGameUI _endGameUI;
        [SerializeField] private EndGameAnimation _endGameAnimation;
        public override void InstallBindings()
        {
            Inputs();
                // Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<ScoreCollector>().FromNew().AsSingle().NonLazy();
            Container.Bind<ShakeCamera>().FromInstance(_shakeCamera).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<DestroyEffectSpawner>().FromInstance(_destroyEffectSpawner).AsSingle().NonLazy();
            Container.Bind<Shield>().FromInstance(_shield).AsSingle().NonLazy();
            Container.Bind<Bomb>().FromInstance(_bomb).AsSingle().NonLazy();
            Container.Bind<Pool>().FromInstance(_shipProjectilePool).AsSingle().NonLazy();
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
            Container.Bind<BonusSpawner>().FromInstance(_bonusSpawner).AsSingle().NonLazy();
            Container.Bind<GameStartAnimation>().FromInstance(_gameStartAnimation).AsSingle().NonLazy();
            LevelSystem();
            EndGame();
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

        private void LevelSystem()
        {
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy(); 
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<LevelsHandler>().FromInstance(_levelsHandler).AsSingle().NonLazy(); 
        }

        private void EndGame()
        {
            Container.Bind<EndGame>().FromNew().AsSingle().NonLazy();
            Container.Bind<EndGameUI>().FromInstance(_endGameUI).AsSingle().NonLazy(); 
            Container.Bind<EndGameAnimation>().FromInstance(_endGameAnimation).AsSingle().NonLazy(); 
        }
    }
    
 
}