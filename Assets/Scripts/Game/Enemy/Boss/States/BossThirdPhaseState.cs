using System.Threading;
using Cysharp.Threading.Tasks;
using Game.StateMachine;
using Game.Weapons;
using UnityEngine;

namespace Game.Enemy.Boss.States
{
    public class BossThirdPhaseState : BossState
    {
        private BossBigGun _bossBigGun;
        private BossBigProjectile _bigProjectile;
        private float _currentTime;
        private const float PhaseTimer = 1.5f; 
        private CancellationTokenSource _cts;
        
        public BossThirdPhaseState(IStateSwitcher stateSwitcher, Boss boss, BossBigGun bossBigGun, BossBigProjectile bossBigProjectile) : base(stateSwitcher, boss)
        {
            _bossBigGun = bossBigGun;
            _bigProjectile = bossBigProjectile;
        }
        
        public override async void Enter()
        {
            base.Enter();
            _cts = new CancellationTokenSource();
            await ShotState().SuppressCancellationThrow();
        }
        private async UniTask ShotState()
        {
            BossAim();
            _bossBigGun.Shot(_bigProjectile.gameObject);
            while (_boss.gameObject.activeInHierarchy)
            {
                if (_boss.IsPaused == false)
                {
                    _currentTime += Time.deltaTime;
                    if (_currentTime >= PhaseTimer)
                    {
                        StateSwitcher.SwitchState<BossFirstPhaseState>();
                        _cts.Cancel();
                    }
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }
    }
}