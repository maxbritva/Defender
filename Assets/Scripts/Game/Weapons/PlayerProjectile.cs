using Game.Interfaces;
using Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Weapons
{
    public class PlayerProjectile : Projectile
    {
        private int _initialDamage;
        private float _critChance;
        private UpgradesHandler _upgradesHandler;
        private void Start()
        {
            _initialDamage = (int) _upgradesHandler.DamageCurrentLevel.Value;
            _critChance = _upgradesHandler.CritCurrentLevel.Value;
        }

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

        [Inject] private void Construct(UpgradesHandler upgradesHandler) => _upgradesHandler = upgradesHandler;
    }
}