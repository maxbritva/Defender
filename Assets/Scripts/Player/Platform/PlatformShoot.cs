using Game.GameCore.Pause;
using Game.Interfaces;
using Game.ObjectPool;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformShoot : MonoBehaviour, IPause
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Pool _projectilePool;
        private UpgradesHandler _upgradesHandler; 
        private InputHandler _inputHandler;
        private PauseHandler _pauseHandler;
        private IWeapon _weapon;
        private float _fireRate = 2f;
        private float _timeBetweenAttack;
        private bool _isPaused;

        private void Start() => _fireRate = _upgradesHandler.ShootRateCurrentLevel.Value;

        private void OnEnable() => _pauseHandler.Add(this);
        private void OnDisable() => _pauseHandler.Remove(this);
        private void Update() => Shot();

        public void SetPause(bool isPaused) => _isPaused = isPaused;
        private void Shot() {
            if(_isPaused) return;
            _timeBetweenAttack += Time.deltaTime;
            if (_timeBetweenAttack > _fireRate == false 
                || _inputHandler.IsFire() == false) return;
            _weapon.Shot(_projectilePool);
            _timeBetweenAttack = 0;
        }
        
        [Inject] private void Construct(InputHandler inputHandler, IWeapon weapon, PauseHandler pauseHandler, UpgradesHandler upgradesHandler) 
        {
            _inputHandler = inputHandler;
            _weapon = weapon;
            _upgradesHandler = upgradesHandler;
            _pauseHandler = pauseHandler;
        }
    }
}