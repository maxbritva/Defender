using Game.Interfaces;
using Game.ObjectPool;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformShoot : MonoBehaviour
    {
        [SerializeField] private ObjectPool _projectilePool;
        private InputHandler _inputHandler;
        private IWeapon _weapon;
        private float _fireRate = 0.5f; // playerData
        private float _timer;

        [Inject] private void Construct(InputHandler inputHandler, IWeapon weapon) 
        {
            _inputHandler = inputHandler;
            _weapon = weapon;
        }
        private void Update()
        {
            if (_inputHandler.IsFire() == false) return;
            _timer += Time.deltaTime;
            if (_timer > _fireRate)
                Shot();
        }
        
        private void Shot() {
            _weapon.Shot(_projectilePool);
            _timer = 0;
        }
    }
}