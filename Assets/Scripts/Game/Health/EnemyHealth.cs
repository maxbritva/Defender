using System;
using Game.FX;
using Game.Score;
using UnityEngine;
using Zenject;

namespace Game.Health
{
    public class EnemyHealth : ObjectHealth
    {
        [SerializeField] private int _scoreForDestroy;
        public Action OnEnemyDead;
        private ScoreCollector _scoreCollector;
        private DamageTextSpawner _damageTextSpawner;
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _damageTextSpawner.SpawnDamageText(transform, damage);
            if (_currentHealth > 0) return;
            DestroyEnemy();
        }

        public void DestroyEnemy()
        {
            _scoreCollector.AddScore(_scoreForDestroy);
            OnEnemyDead?.Invoke();
            gameObject.SetActive(false);
        }
        [Inject] private void Construct(DamageTextSpawner damageTextSpawner, 
            ScoreCollector scoreCollector)
        {
            _damageTextSpawner = damageTextSpawner;
            _scoreCollector = scoreCollector;
        }
    }
}