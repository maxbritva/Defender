using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.FX;
using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Boss.States
{
    public class BossPrepareState : BossState, IDisposable
    {
        private const float PrepareWaitTimer = 2f;
        private float _currentTime;
        private BossLevelStartFX _bossLevelStartFX;
        private BossShield _bossShield;
        private CancellationTokenSource _cts;


        public BossPrepareState(IStateSwitcher stateSwitcher, Boss boss, BossLevelStartFX bossLevelStartFX, BossShield bossShield) : base(stateSwitcher, boss)
        {
            _bossLevelStartFX = bossLevelStartFX;
            _bossShield = bossShield;
        }

        public override async void Enter()
        {
            base.Enter();
            _cts = new CancellationTokenSource();
            _bossLevelStartFX.StartBossLevelFX(_boss.transform);
            _currentTime = 0f;
            _bossShield.SetCollider(true);
            await PrepareState().SuppressCancellationThrow();
        }

        public override void Exit()
        {
            base.Exit();
            _bossLevelStartFX.EndBossLevelFX();
            _bossShield.SetCollider(false);
            _cts.Cancel();
        }
        
        private async UniTask PrepareState()
        {
            BossAim();
            while (_boss.gameObject.activeInHierarchy)
            {
                if (_boss.IsPaused == false)
                {
                    _currentTime += Time.deltaTime;
                    if (_currentTime >= PrepareWaitTimer)
                    {
                        StateSwitcher.SwitchState<BossFirstPhaseState>();
                        _cts.Cancel();
                    }
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }

        public void Dispose() => _cts?.Dispose();
    }
}