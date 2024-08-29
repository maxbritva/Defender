using System;
using UnityEngine;

namespace Game.GameCore.GameProgression
{  
    [Serializable]
    public class Level
    {
       [SerializeField] private float _spawnIntervalAsteroids;
       [SerializeField] private float _spawnIntervalShips;
       [SerializeField] private int _setBossHealth;

       [SerializeField] private bool _asteroidIsActive;
       [SerializeField] private bool _shipIsActive;
       [SerializeField] private bool _bossIsActive;

       public float SpawnIntervalAsteroids => _spawnIntervalAsteroids;

       public float SpawnIntervalShips => _spawnIntervalShips;

       public int SetBossHealth => _setBossHealth;

       public bool AsteroidIsActive => _asteroidIsActive;

       public bool ShipIsActive => _shipIsActive;

       public bool BossIsActive => _bossIsActive;
    }
}