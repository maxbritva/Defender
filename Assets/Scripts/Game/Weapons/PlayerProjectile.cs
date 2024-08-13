using Game.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Weapons
{
    public class PlayerProjectile : Projectile
    {
        private int _initialDamage;
        private void Start() => _initialDamage = _damage;

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(CalculateDamage());
            gameObject.SetActive(false);
        }

        private int CalculateDamage() {
            _damage = (int)Random.Range(_initialDamage / 2f, _initialDamage * 1.5f);
            if (Random.value < 0.1f) 
                _damage *= 3;
            return _damage;
        }
    }
}