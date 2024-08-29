using Game.ObjectPool;
using Game.StateMachine;
using Game.Weapons;
using UnityEngine;

namespace Game.Enemy.Boss.States
{
    public class BossFirstPhaseState: BossState
    {
        private Pool _enemyProjectilePool;
        private BossSpawner _bossSpawner;
        private GunMultiply _gunMultiply;
        private int _attackCounter;
        private float _currentTime;

        public BossFirstPhaseState(IStateSwitcher stateSwitcher, Boss boss, BossSpawner bossSpawner, GunMultiply gunMultiply, Pool pool) : base(stateSwitcher, boss)
        {
            _bossSpawner = bossSpawner;
            _gunMultiply = gunMultiply;
            _enemyProjectilePool = pool;
        }

        public override void Enter()
        {
            base.Enter();
            _currentTime = 0;
            _attackCounter = 0;
        }

        public override void Update()
        {
            base.Update();
            if(_attackCounter < 3)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= 5f)
                {
                    _boss.transform.position = _bossSpawner.SpawnPoints[Random.Range(0, _bossSpawner.SpawnPoints.Count)].position;
                    _gunMultiply.Shot(_enemyProjectilePool);
                    _currentTime = 0;
                    _attackCounter++;
                }
            }
            else
                StateSwitcher.SwitchState<BossSecondPhaseState>();
        }
    }
}