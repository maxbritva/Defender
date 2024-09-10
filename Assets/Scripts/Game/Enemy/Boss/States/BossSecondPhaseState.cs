using System.Threading;
using Cysharp.Threading.Tasks;
using Game.StateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy.Boss.States
{
    public class BossSecondPhaseState: BossState
    {
        private BossSpawner _bossSpawner;
        private BossShield _bossShield;
        private float _currentTime;
        private const float PhaseTimer = 4f; 
        private CancellationTokenSource _cts;
        
        public BossSecondPhaseState(IStateSwitcher stateSwitcher, Boss boss, BossSpawner bossSpawner, 
            BossShield bossShield) : base(stateSwitcher, boss)
        {
            _bossShield = bossShield;
            _bossSpawner = bossSpawner;
        }
        
        public async override void Enter()
        {
            base.Enter();
            _cts = new CancellationTokenSource();
            _bossShield.SetShield(true);
            _bossSpawner.StartSpawnMinions();
            _currentTime = 0;
            await SaveState();
        }

        public override void Exit()
        {
            base.Exit();
            _bossShield.SetShield(false);
            _bossSpawner.StopSpawnMinions();
        }
        
        private async UniTask SaveState()
        {
            BossAim();
            while (_boss.gameObject.activeInHierarchy)
            {
                if (_boss.IsPaused == false)
                {
                    _currentTime += Time.deltaTime;
                    if (_currentTime >= PhaseTimer)
                    {
                        StateSwitcher.SwitchState<BossThirdPhaseState>();
                        _cts.Cancel();
                    }
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }

        public override void Update()
        {
            // base.Update();
            // _currentTime += Time.deltaTime;
            // if (_currentTime >= PhaseTimer)
            // {
            //     StateSwitcher.SwitchState<BossThirdPhaseState>();
            //     _currentTime = 0;
            // }
        }
    }
}