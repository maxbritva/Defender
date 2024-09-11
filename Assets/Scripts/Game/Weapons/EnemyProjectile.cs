using Game.Health;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    public class EnemyProjectile: Projectile
    {
        protected PlayerHealth _playerHealth;
        
        protected virtual void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                _playerHealth.OnPlayerHit?.Invoke();
            }
            _CTS.Cancel();
            gameObject.SetActive(false);
        }

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}