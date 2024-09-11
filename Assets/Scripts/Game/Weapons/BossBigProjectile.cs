using Game.FX;
using Game.Interfaces;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    public class BossBigProjectile : EnemyProjectile
    {
        private PlatformMovement _platformMovement;
        private PlatformFreezeFX _platformFreezeFX;
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
                _playerHealth.OnPlayerHit?.Invoke();
                _CTS.Cancel();
                gameObject.SetActive(false);
            }
            if (other.gameObject.TryGetComponent(out Platform platform))
            {
                _platformMovement.StunPlatform();
                _platformFreezeFX.ShowFreezeFX();
                _CTS?.Cancel();
                gameObject.SetActive(false);
            }
        }

        [Inject] private void Construct(PlatformMovement platformMovement, PlatformFreezeFX platformFreezeFX)
        {
            _platformFreezeFX = platformFreezeFX;
            _platformMovement = platformMovement;
        }
    }
}