using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Boss.States
{
    public class BossSecondPhaseState: BossState
    {
        private BossSpawner _bossSpawner;
        private BossShield _bossShield;
        private float _currentTime;
        private const float PhaseTimer = 4f; 
        
        public BossSecondPhaseState(IStateSwitcher stateSwitcher, Boss boss, BossSpawner bossSpawner, BossShield bossShield) : base(stateSwitcher, boss)
        {
            _bossShield = bossShield;
            _bossSpawner = bossSpawner;
        }
        
        public override void Enter()
        {
            base.Enter();
            _bossShield.SetShield(true);
            _bossSpawner.StartSpawnMinions();
            _currentTime = 0;
        }

        public override void Exit()
        {
            base.Exit();
            _bossShield.SetShield(false);
            _bossSpawner.StopSpawnMinions();
        }

        public override void Update()
        {
            base.Update();
            _currentTime += Time.deltaTime;
            if (_currentTime >= PhaseTimer)
            {
                StateSwitcher.SwitchState<BossThirdPhaseState>();
                _currentTime = 0;
            }
        }
    }
}