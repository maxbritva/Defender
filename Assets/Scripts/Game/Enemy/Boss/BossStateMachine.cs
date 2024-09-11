using System.Collections.Generic;
using System.Linq;
using Game.Enemy.Boss.States;
using Game.FX;
using Game.ObjectPool;
using Game.StateMachine;
using Game.StateMachine.States;
using Game.Weapons;

namespace Game.Enemy.Boss
{
    public class BossStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private BossLevelStartFX _bossLevelStartFX;
        private GameObjectPool _enemyProjectilePool;
        private BossSpawner _bossSpawner;
        private GunMultiply _gunMultiply;
        private Boss _boss;
        private BossShield _bossShield;
        private GunSingle _bossGunSingle;

        public BossStateMachine(Boss boss, GameObjectPool pool, BossLevelStartFX bossLevelStartFX, BossSpawner bossSpawner, 
            GunMultiply gunMultiply, GunSingle bossBigGun, BossBigProjectile bigProjectile, BossShield bossShield)
        {
            _bossLevelStartFX = bossLevelStartFX;
            _enemyProjectilePool = pool;
            _bossSpawner = bossSpawner;
            _gunMultiply = gunMultiply;
            _boss = boss;
            _bossShield = bossShield;
            _bossGunSingle = bossBigGun;
            _states = new List<IState>()
            {
                new BossPrepareState(this, _boss, _bossLevelStartFX, _bossShield),
                new BossFirstPhaseState(this, _boss, _bossSpawner, _gunMultiply,_enemyProjectilePool),
                new BossSecondPhaseState(this,_boss, _bossSpawner, _bossShield),
                new BossThirdPhaseState(this,_boss, _bossGunSingle)
            };
            _currentState = _states[0];
            _currentState.Enter();
        }
        
        public void ExitCurrentState() => _currentState.Exit();

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}