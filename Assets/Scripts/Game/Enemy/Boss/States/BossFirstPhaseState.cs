using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.ObjectPool;
using Game.StateMachine;
using Game.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy.Boss.States
{
    public class BossFirstPhaseState: BossState, IDisposable
    {
        private GameObjectPool _enemyProjectilePool;
        private BossSpawner _bossSpawner;
        private GunMultiply _gunMultiply;
        private CancellationTokenSource _cts;
        private int _attackCounter;
        private float _currentTime;

        public BossFirstPhaseState(IStateSwitcher stateSwitcher, Boss boss, BossSpawner bossSpawner, GunMultiply gunMultiply, 
            GameObjectPool pool) : base(stateSwitcher, boss)
        {
            _bossSpawner = bossSpawner;
            _gunMultiply = gunMultiply;
            _enemyProjectilePool = pool;
        }

        public override async void Enter()
        {
            base.Enter();
            _cts = new CancellationTokenSource();
            
            _currentTime = 0;
            _attackCounter = 0;
            await AttackState().SuppressCancellationThrow();
        }

        public override void Exit() => _cts.Cancel();

        private async UniTask AttackState()
        {
            BossAim();
            while (_attackCounter < 3 && _boss.gameObject.activeInHierarchy)
            {
                if (_boss.IsPaused == false)
                {
                    _currentTime += Time.deltaTime;
                    if (_currentTime >= 4f)
                    {
                        _boss.transform.position = _bossSpawner.SpawnPoints[Random.Range(0, _bossSpawner.SpawnPoints.Count)].position;
                        BossAim();
                        Debug.Log("Shot");
                        _gunMultiply.Shot(_enemyProjectilePool);
                        _currentTime = 0;
                        _attackCounter++;
                    }
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
            StateSwitcher.SwitchState<BossSecondPhaseState>();
        }

        public void Dispose() => _cts?.Dispose();
    }
}