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
        private const float PhaseTimer = 2f; 
        
        public BossThirdPhaseState(IStateSwitcher stateSwitcher, Boss boss, BossBigGun bossBigGun, BossBigProjectile bossBigProjectile) : base(stateSwitcher, boss)
        {
            _bossBigGun = bossBigGun;
            _bigProjectile = bossBigProjectile;
        }
        
        public override void Enter()
        {
            base.Enter();
            _bossBigGun.Shot(_bigProjectile.gameObject);
        }

        public override void Update()
        {
            base.Update();
            _currentTime += Time.deltaTime;
            if (_currentTime >= PhaseTimer)
            {
                StateSwitcher.SwitchState<BossFirstPhaseState>();
                _currentTime = 0;
            }
        }
    }
}