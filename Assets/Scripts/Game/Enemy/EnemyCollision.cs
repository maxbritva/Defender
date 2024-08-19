using Game.Health;
using Game.Interfaces;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out PlayerHealth planet))
            {
                planet.OnPlayerHit?.Invoke();
                planet.TakeDamage(1);
                GetComponent<EnemyHealth>().DestroyEnemy();
            }
            else if (other.gameObject.TryGetComponent(out IEnemyDestroyable destroy)) 
                GetComponent<EnemyHealth>().DestroyEnemy();
        }
    }
}