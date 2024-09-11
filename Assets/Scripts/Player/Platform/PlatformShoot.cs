using Cysharp.Threading.Tasks;
using Game.GameCore.Pause;
using Game.Interfaces;
using Game.ObjectPool;
using Game.Weapons;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformShoot : MonoBehaviour, IPause
    {
        private GameObjectPool _gameObjectPool;
        private UpgradesHandler _upgradesHandler; 
        private InputHandler _inputHandler;
        private PauseHandler _pauseHandler;
        private GunMultiply _weapon;
        private float _fireRate = 2f;
        private float _timeBetweenAttack;
        private bool _isPaused;

        private void Start() => _fireRate = _upgradesHandler.GetCurrentUpgradeValue("ShootRate");

        private async void OnEnable()
        {
            _pauseHandler.Add(this);
            _timeBetweenAttack = 0;  
            await Shoot().SuppressCancellationThrow();
        }

        private void OnDisable() => _pauseHandler.Remove(this);

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private async UniTask Shoot()
        {
            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                if (_isPaused == false)
                {
                    _timeBetweenAttack += Time.deltaTime;
                    if (_timeBetweenAttack > _fireRate && _inputHandler.IsFire())
                    {
                        _weapon.Shot(_gameObjectPool);
                        _timeBetweenAttack = 0;  
                    }
                }
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }

        [Inject] private void Construct(InputHandler inputHandler, GunMultiply weapon, PauseHandler pauseHandler, 
            UpgradesHandler upgradesHandler, GameObjectPool gameObjectPool) 
        {
            _inputHandler = inputHandler;
            _weapon = weapon;
            _upgradesHandler = upgradesHandler;
            _pauseHandler = pauseHandler;
            _gameObjectPool = gameObjectPool;
        }
    }
}