using Game.FX;
using Game.GameCore.Pause;
using Game.Interfaces;
using Game.ObjectPool;
using Game.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Boss
{
    public class Boss : MonoBehaviour, IPause
    {
        private Pool _enemyProjectilePool;
        private BossStateMachine _bossStateMachine;
        private BossLevelStartFX _bossLevelStartFX;
        private BossBigProjectile _bigProjectile;
        private BossSpawner _bossSpawner;
        private GunMultiply _gunMultiply;
        private PauseHandler _pauseHandler;
        private BossShield _bossShield;
        private BossBigGun _bossBigGun;
        private Boss _boss;
        private bool _isPaused;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        private void Awake()
        {
            _gunMultiply = GetComponent<GunMultiply>();
            _bossBigGun = GetComponent<BossBigGun>();
            _bossShield = GetComponent<BossShield>();
            _bossStateMachine = new BossStateMachine(this, _enemyProjectilePool, _bossLevelStartFX, 
                _bossSpawner, _gunMultiply, _bossBigGun, _bigProjectile, _bossShield);
            
        }
        private void Update()
        {
            if(_isPaused)
                return;
            _bossStateMachine.Update();
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        [Inject] private void Construct(BossLevelStartFX bossLevelStartFX, Pool enemyProjectilePool, 
            BossSpawner bossSpawner, BossBigProjectile bossBigProjectile, PauseHandler pauseHandler)
        {
            _bossLevelStartFX = bossLevelStartFX;
            _enemyProjectilePool = enemyProjectilePool;
            _bossSpawner = bossSpawner;
            _bigProjectile = bossBigProjectile;
            _pauseHandler = pauseHandler;
        }
    }
}