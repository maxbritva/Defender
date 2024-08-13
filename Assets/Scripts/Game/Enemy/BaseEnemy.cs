using Game.Health;
using Game.Score;
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
            else if (other.gameObject.TryGetComponent(out PlatformSizeChanger platform)) 
                GetComponent<EnemyHealth>().DestroyEnemy();
        }
        protected void Rotation() {
            transform.LookAt(Vector3.zero,Vector3.forward * -1);
        }
        
    }
}