using System.Collections.Generic;
using Game.Enemy;
using Game.Enemy.Boss;
using Game.GameCore.GameStates;
using UnityEngine;
using Zenject;

namespace Game.GameCore.GameProgression
{
    public class LevelsHandler: MonoBehaviour
    {
        [SerializeField] private List<Level> _levels = new List<Level>();

        [SerializeField]private EnemySpawner _asteroidSpawner;
        [SerializeField]private EnemySpawner _shipSpawner;
        [SerializeField]private BossSpawner _bossSpawner;
        private GameManager _gameManager;

        public void Activate(int level)
        {
            if (_levels[level].AsteroidIsActive)
            {
                _asteroidSpawner.ChangeSpawnInterval(_levels[level].SpawnIntervalAsteroids);
                _asteroidSpawner.Activate();
            }

            if (_levels[level].ShipIsActive)
            {
                _shipSpawner.ChangeSpawnInterval(_levels[level].SpawnIntervalShips);
                _shipSpawner.Activate();
            }

            if (_levels[level].BossIsActive)
            {
                _shipSpawner.HideAllEnemy();
                _asteroidSpawner.HideAllEnemy();
                _bossSpawner.ChangeBossHealth(_levels[level].SetBossHealth);
                _bossSpawner.Activate();
                _gameManager.OnBossLevelStarted?.Invoke();
            }
        }

        public void Deactivate()
        {
            _asteroidSpawner.Deactivate();
            _shipSpawner.Deactivate();
            _bossSpawner.Deactivate();
        }

        [Inject] private void Construct(GameManager gameManager) => _gameManager = gameManager;
    }
}