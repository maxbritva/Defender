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
        [SerializeField] private ObjectPool _projectilePool;
        private InputHandler _inputHandler;
        private PauseHandler _pauseHandler;
        private IWeapon _weapon;
        private float _fireRate = 0.7f; // playerData
        private float _timeBetweenAttack;
        private bool _isPaused;

        [Inject] private void Construct(InputHandler inputHandler, IWeapon weapon, PauseHandler pauseHandler) 
        {
            _inputHandler = inputHandler;
            _weapon = weapon;
            _pauseHandler = pauseHandler;
        }
        private void Update()
        {
            if (_inputHandler.IsFire() == false) return;
            _timeBetweenAttack += Time.deltaTime;
            if (_timeBetweenAttack > _fireRate)
                Shot();
        }
        
        private void Shot() {
            _weapon.Shot(_projectilePool);
            _timeBetweenAttack = 0;
        }
        public void SetPause(bool isPaused) => _isPaused = isPaused;
    }
}