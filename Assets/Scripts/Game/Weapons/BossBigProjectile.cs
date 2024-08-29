using Game.Interfaces;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    public class BossBigProjectile : EnemyProjectile
    {
        private PlatformMovement _platformMovement;
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                _playerHealth.OnPlayerHit?.Invoke();
                gameObject.SetActive(false);
            }
            if (other.gameObject.TryGetComponent(out Platform platform))
            {
                Debug.Log(333);
                _platformMovement.StunPlatform();
                gameObject.SetActive(false);
            }
        }

        [Inject] private void Construct(PlatformMovement platformMovement) => _platformMovement = platformMovement;
    }
}