using System;
using Game.Enemy.Boss;
using Game.GameCore.GameStates;
using Game.GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Game.Health
{
    public class BossEnemyHealth : EnemyHealth
    {
        private GameManager _gameManager;
        public void SetMaxHealth(int value)
        {
            if(value <=0)
                throw new ArgumentOutOfRangeException();
            _maxHealth = value;
            _currentHealth = _maxHealth;
        }


        public override void DestroyEnemy()
        {
            base.DestroyEnemy();
            _gameManager.OnBossLevelEnded?.Invoke();
        }
        
        [Inject] private void Construct(GameManager gameManager) => _gameManager = gameManager;
    }
}