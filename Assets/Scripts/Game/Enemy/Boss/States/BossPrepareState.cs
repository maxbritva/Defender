using Game.FX;
using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Boss.States
{
    public class BossPrepareState : BossState
    {
        private const float PrepareWaitTimer = 4f;
        private float _currentTime;
        private BossLevelStartFX _bossLevelStartFX;
        private BossShield _bossShield;


        public BossPrepareState(IStateSwitcher stateSwitcher, Boss boss, BossLevelStartFX bossLevelStartFX, BossShield bossShield) : base(stateSwitcher, boss)
        {
            _bossLevelStartFX = bossLevelStartFX;
            _bossShield = bossShield;
        }

        public override void Enter()
        {
            base.Enter();
            _bossLevelStartFX.StartBossLevelFX(_boss.transform);
            _currentTime = 0f;
            _bossShield.SetCollider(true);
        }

        public override void Exit()
        {
            base.Exit();
            _bossLevelStartFX.EndBossLevelFX();
            _bossShield.SetCollider(false);
        }

        public override void Update()
        {
            base.Update();
            _currentTime += Time.deltaTime;
            if(_currentTime >= PrepareWaitTimer)
                StateSwitcher.SwitchState<BossFirstPhaseState>();
        }
    }
}