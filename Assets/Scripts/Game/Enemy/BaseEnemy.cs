using Game.Health;
using Game.Interfaces;
using Game.Score;
using Game.Weapons;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private ScoreCollector _scoreCollector;
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out PlayerHealth planet))
            {
                planet.OnPlayerHit?.Invoke();
                planet.TakeDamage(_damage);
                GetComponent<EnemyHealth>().DestroyEnemy();
            }
            else if (other.gameObject.TryGetComponent(out IEnemyDestroyable destroy))
                GetComponent<EnemyHealth>().DestroyEnemy();
        }

    }
}