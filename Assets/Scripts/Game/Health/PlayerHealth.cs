using System;
using Game.GameCore.GameStates;
using Game.Interfaces;
using Player;
using Zenject;

namespace Game.Health
{
    public class PlayerHealth : ObjectHealth, IHealable
    {
        public Action OnPlayerHit;
        public Action OnPlayerHeal;
        
        private GameManager _gameManager;
        private UpgradesHandler _upgradesHandler;

        protected override void OnEnable()
        {
            _maxHealth = (int) _upgradesHandler.GetCurrentUpgradeValue("Lives");
            base.OnEnable();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            OnPlayerHit?.Invoke();
            if (_currentHealth <= 0)
                _gameManager.OnGameEnded?.Invoke();
        }

        public override void TakeHeal(int value)
        {
            base.TakeHeal(value);
            OnPlayerHeal?.Invoke();
        }

        [Inject] private void Construct(UpgradesHandler upgradesHandler, GameManager gameManager)
        {
            _upgradesHandler = upgradesHandler;
            _gameManager = gameManager;
        }
    }
}