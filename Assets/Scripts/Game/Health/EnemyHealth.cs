using Game.FX;
using Game.Score;
using UnityEngine;
using Zenject;

namespace Game.Health
{
    public class EnemyHealth : ObjectHealth
    {
        [SerializeField] private int _scoreForDestroy;
        private ScoreCollector _scoreCollector;
        private DamageTextSpawner _damageTextSpawner;
        private DestroyEffectSpawner _destroyEffectSpawner;
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _damageTextSpawner.SpawnDamageText(transform, damage);
            if (_currentHealth < 0)
                DestroyEnemy();
        }

        public void DestroyEnemy()
        {
            _scoreCollector.AddScore(_scoreForDestroy);
            _destroyEffectSpawner.Spawn(gameObject.transform);
            gameObject.SetActive(false);
        }
        [Inject] private void Construct(DamageTextSpawner damageTextSpawner, 
            ScoreCollector scoreCollector, DestroyEffectSpawner destroyEffectSpawner)
        {
            _damageTextSpawner = damageTextSpawner;
            _scoreCollector = scoreCollector;
            _destroyEffectSpawner = destroyEffectSpawner;
        }
    }
}