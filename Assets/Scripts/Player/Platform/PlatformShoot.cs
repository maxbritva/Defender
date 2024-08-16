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
        private float _timeBetweenAttack;

        [Inject] private void Construct(InputHandler inputHandler, IWeapon weapon) 
        {
            _inputHandler = inputHandler;
            _weapon = weapon;
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
    }
}