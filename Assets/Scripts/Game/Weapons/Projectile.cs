using System.Collections;
using Game.Interfaces;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] protected int _damage;
        [SerializeField] [Min(0)] private float _speed;
        [SerializeField] [Min(0)] private float _selfDestroyTimer;
        private WaitForSeconds _delay;

        private void OnEnable()
        {
            _delay = new WaitForSeconds(_selfDestroyTimer);
            StartCoroutine(SelfDestroy());
        }

        private void FixedUpdate() => _rigidbody.linearVelocity = transform.forward * (_speed);
        
        protected virtual void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);
        }

        private IEnumerator SelfDestroy()
        {
            yield return _delay;
            gameObject.SetActive(false);
        }
    }
}