using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;

namespace Game.GameCore.GameProgression
{
    public class LevelsHandler: MonoBehaviour
    {

        [SerializeField] private List<Level> _levels = new List<Level>();

        [SerializeField]private EnemySpawner _asteroidSpawner;
        [SerializeField]private EnemySpawner _shipSpawner;
        [SerializeField]private EnemySpawner _bossSpawner;

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
                _bossSpawner.ChangeSpawnInterval(_levels[0].SpawnIntervalBoss);
                _bossSpawner.Activate();
            }
        }

        public void Deactivate()
        {
            _asteroidSpawner.Deactivate();
            _shipSpawner.Deactivate();
            _bossSpawner.Deactivate();
        }
    }
}