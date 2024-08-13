using System;

namespace Game.Health
{
    public class PlayerHealth : ObjectHealth
    {
        public Action OnPlayerHit;
        public Action OnPlayerHeal;
        public Action OnPlayerDead;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            OnPlayerHit?.Invoke();
            if (_currentHealth <= 0)
                OnPlayerDead?.Invoke();
        }

        public void TakeHeal(int value)
        {
            if(value <=0) 
                throw new ArgumentOutOfRangeException(nameof(value));
            _currentHealth += value;
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
            OnPlayerHeal?.Invoke();
        }
    }
}