using System;
using Game.Interfaces;
using UnityEngine;

namespace Game.Health
{
    public abstract class ObjectHealth: MonoBehaviour, IDamageable
    {
        [SerializeField] protected int _currentHealth;
        [SerializeField] protected int _maxHealth;
        public float CurrentHealth => _currentHealth;
        public float MAXHealth => _maxHealth;

        protected  virtual void OnEnable() => _currentHealth = _maxHealth;

        public virtual void TakeDamage(int damage)
        {
            if(damage <=0) 
                throw new ArgumentOutOfRangeException(nameof(damage));
            _currentHealth -= damage;
        }
    }
}