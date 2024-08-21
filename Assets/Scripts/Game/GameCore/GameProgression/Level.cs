using System;
using UnityEngine;

namespace Game.GameCore.GameProgression
{  
    [Serializable]
    public class Level
    {
       [SerializeField] private float _spawnIntervalAsteroids;
       [SerializeField] private float _spawnIntervalShips;
       [SerializeField] private float _spawnIntervalBoss;

       [SerializeField] private bool _asteroidIsActive;
       [SerializeField] private bool _shipIsActive;
       [SerializeField] private bool _bossISActive;

       public float SpawnIntervalAsteroids => _spawnIntervalAsteroids;

       public float SpawnIntervalShips => _spawnIntervalShips;

       public float SpawnIntervalBoss => _spawnIntervalBoss;

       public bool AsteroidIsActive => _asteroidIsActive;

       public bool ShipIsActive => _shipIsActive;

       public bool BossIsActive => _bossISActive;
    }
}