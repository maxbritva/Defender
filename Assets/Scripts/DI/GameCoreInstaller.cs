using Game.Bonus;
using Game.Enemy.Boss;
using Game.FX;
using Game.GameCore.GameProgression;
using Game.GameCore.GameStates;
using Game.GameCore.GameStates.EndGame;
using Game.GameCore.Pause;
using Game.Health;
using Game.ObjectPool;
using Game.Score;
using Game.UI;
using Game.Weapons;
using Game.Weapons.Bonus;
using Player.Input;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameCoreInstaller: MonoInstaller
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private FireButton _fireButton;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Canvas _mobileUI;
        [SerializeField] private ShakeCamera _shakeCamera;
        [SerializeField] private Shield _shield;
        [SerializeField] private Bomb _bomb;
        [SerializeField] private Pool _shipProjectilePool;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private LevelsHandler _levelsHandler;
        [SerializeField] private BonusSpawner _bonusSpawner;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameStartAnimation _gameStartAnimation;
        [SerializeField] private EndGameUI _endGameUI;
        [SerializeField] private EndGameAnimation _endGameAnimation;
        [SerializeField] private BackgroundColorsChanger _backgroundColorsChanger;
        [SerializeField] private BossLevelStartFX _bossLevelStartFX;
        [SerializeField] private Boss _boss;
        [SerializeField] private BossSpawner _bossSpawner;
        [SerializeField] private BossBigProjectile _bossBigProjectile;
        [SerializeField] private PlatformFreezeFX _platformFreezeFX;
        
        public override void InstallBindings()
        {
            Container.Bind<GameObjectPool>().FromNew().AsSingle().NonLazy();
            Inputs();
            GameSystem();
            Player();
            EndGame();
            FX();
            Enemy();
        }
        private void Inputs()
        {
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle().NonLazy();
            Container.Bind<FireButton>().FromInstance(_fireButton).AsSingle().NonLazy();
            if (Application.isMobilePlatform)
            {
                _mobileUI.gameObject.SetActive(true);
                Container.Bind<IInput>().To<MobileInput>().FromNew().AsSingle().NonLazy();
                Debug.Log("mobile");
            }
            else
                Container.Bind<IInput>().To<DesktopInput>().FromNew().AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromNew().AsSingle().NonLazy();
        }

        private void GameSystem()
        {
            Container.Bind<ScoreCollector>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle();
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy(); 
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<LevelsHandler>().FromInstance(_levelsHandler).AsSingle().NonLazy(); 
        }

        private void EndGame()
        {
            Container.Bind<EndGameManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<EndGameUI>().FromInstance(_endGameUI).AsSingle().NonLazy(); 
            Container.Bind<EndGameAnimation>().FromInstance(_endGameAnimation).AsSingle().NonLazy(); 
        }

        private void FX()
        {
            Container.Bind<ShakeCamera>().FromInstance(_shakeCamera).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromNew().AsSingle().NonLazy();
            Container.Bind<DestroyEffectSpawner>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameStartAnimation>().FromInstance(_gameStartAnimation).AsSingle().NonLazy();
            Container.Bind<BackgroundColorsChanger>().FromInstance(_backgroundColorsChanger).AsSingle().NonLazy();
            Container.Bind<BossLevelStartFX>().FromInstance(_bossLevelStartFX).AsSingle().NonLazy();
            Container.Bind<PlatformFreezeFX>().FromInstance(_platformFreezeFX).AsSingle().NonLazy();
        }

        private void Player()
        {
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth);
            Container.Bind<BonusSpawner>().FromInstance(_bonusSpawner).AsSingle().NonLazy();
            Container.Bind<Shield>().FromInstance(_shield).AsSingle().NonLazy();
            Container.Bind<Bomb>().FromInstance(_bomb).AsSingle().NonLazy();
        }

        private void Enemy()
        {
            Container.Bind<Pool>().FromInstance(_shipProjectilePool).AsSingle().NonLazy();
            Container.Bind<Boss>().FromInstance(_boss).AsSingle().NonLazy();
            Container.Bind<BossSpawner>().FromInstance(_bossSpawner).AsSingle().NonLazy();
            Container.Bind<BossBigProjectile>().FromInstance(_bossBigProjectile).AsSingle().NonLazy();
        }
    }
    
 
}