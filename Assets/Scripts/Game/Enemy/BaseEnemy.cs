using Player;
using Player.Platform;
using UnityEngine;

namespace Game.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out PlayerHealth planet))
            {
                planet.OnPlayerHit?.Invoke();
                planet.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
              
            else if (other.gameObject.TryGetComponent(out PlatformSizeChanger platform)) {
                gameObject.SetActive(false);
            }
        }
        protected void Rotation() {
            transform.LookAt(Vector3.zero,Vector3.forward * -1);
        }
    }
}