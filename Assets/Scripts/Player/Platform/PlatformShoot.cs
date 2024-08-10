using Game.ObjectPool;
using Game.Weapons;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformShoot : MonoBehaviour
    {
        [SerializeField] private ObjectPool _projectilePool;
        [SerializeField] private GunMultiply _weapon; /// <summary>
                                                      /// нужна абстракция
                                                      /// </summary>
        private InputHandler _inputHandler;
        private float _fireRate = 0.5f;
        private float _timer;
        
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

        [Inject] private void Construct(InputHandler inputHandler) => _inputHandler = inputHandler;
    }
}