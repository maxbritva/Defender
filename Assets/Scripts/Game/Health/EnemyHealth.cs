using System;
using Game.FX;
using Zenject;

namespace Game.Health
{
    public class EnemyHealth : ObjectHealth
    {
        public Action OnEnemyDead;
        private DamageTextSpawner _damageTextSpawner;
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _damageTextSpawner.SpawnDamageText(transform, damage);
            if (_currentHealth > 0) return;
            gameObject.SetActive(false);
            OnEnemyDead?.Invoke();
        }

        [Inject] private void Construct(DamageTextSpawner damageTextSpawner) => _damageTextSpawner = damageTextSpawner;
    }
}