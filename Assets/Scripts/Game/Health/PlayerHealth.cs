using System;
using Game.GameCore.GameStates;
using Player;
using Zenject;

namespace Game.Health
{
    public class PlayerHealth : ObjectHealth
    {
        public Action OnPlayerHit;
        public Action OnPlayerHeal;
        
        private GameManager _gameManager;
        private UpgradesHandler _upgradesHandler;

        protected override void OnEnable()
        {
            _maxHealth = (int)_upgradesHandler.LivesCurrentLevel.Value;
            base.OnEnable();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            OnPlayerHit?.Invoke();
            if (_currentHealth <= 0)
                _gameManager.OnGameEnded?.Invoke();
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
        [Inject] private void Construct(UpgradesHandler upgradesHandler, GameManager gameManager)
        {
            _upgradesHandler = upgradesHandler;
            _gameManager = gameManager;
        }
    }
}