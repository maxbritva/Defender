using System;
using Game.Enemy;
using Game.Interfaces;
using UnityEngine;

namespace Game.GameCore.GameProgression
{
    [Serializable]
    public class GameLevel:IActivatable
    {
        [SerializeField] private EnemySpawner _asteroidSpawner;
        [SerializeField] private EnemySpawner _shipSpawner;
        [SerializeField] private EnemySpawner _bossSpawner;

        [Header("Asteroids")]
        [SerializeField][Min(0.1f)] private float _spawnIntervalAsteroids;
		
        [Header("Ship")]
        [SerializeField][Min(0.1f)] private float _spawnIntervalShips;
		
        [Header("Boss")]
        [SerializeField][Min(0.0f)] private float _spawnIntervalBoss;

        public void Activate()
        {
            if(_asteroidSpawner != null)
                _asteroidSpawner.Activate(_spawnIntervalAsteroids);
            if(_shipSpawner != null)
                _asteroidSpawner.Activate(_spawnIntervalShips);
            if(_bossSpawner != null)
                _asteroidSpawner.Activate(_spawnIntervalBoss);
        }

        public void Deactivate()
        {
            if(_asteroidSpawner != null)
                _asteroidSpawner.Deactivate();
            if(_shipSpawner != null)
                _asteroidSpawner.Deactivate();
            if(_bossSpawner != null)
                _asteroidSpawner.Deactivate();
        }
    }
}