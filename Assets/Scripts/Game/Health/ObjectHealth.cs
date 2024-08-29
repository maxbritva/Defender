using System;
using Game.Interfaces;
using UnityEngine;

namespace Game.Health
{
    public abstract class ObjectHealth: MonoBehaviour, IDamageable, IHealable
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

        public virtual void TakeHeal(int value)
        {
            if(value <=0) 
                throw new ArgumentOutOfRangeException(nameof(value));
            _currentHealth += value;
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
          
        }
    }
}