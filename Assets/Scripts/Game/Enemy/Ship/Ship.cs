using System.Collections.Generic;
using Game.Enemy.Ship.States;
using Game.GameCore.Pause;
using Game.Health;
using Game.Interfaces;
using Game.ObjectPool;
using Game.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Ship
{
    public class Ship : MonoBehaviour, IPause
    {
        [SerializeField] private List<Transform> _waypoints;
        [SerializeField] private GameObject _shipProjectile;
        private ShipStateMachine _shipStateMachine;
        private IEnemyProjectilePool _pool;
        private PauseHandler _pauseHandler;
        private EnemyHealth _enemyHealth;
        private ShipGun _shipGun;
        private bool _isPaused;
        
        public List<Transform> Waypoints => _waypoints;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _shipGun = GetComponent<ShipGun>();
            _shipStateMachine = new ShipStateMachine(this, _pool, _enemyHealth, _shipGun);
        }

        private void Update()
        {
            if(_isPaused)
                return;
            _shipStateMachine.Update();
        }

        private void OnEnable()
        {
            _shipStateMachine.OnEnable();
            _pauseHandler.Add(this);
        }
        private void OnDisable() => _pauseHandler.Remove(this);

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        [Inject] private void Construct(IEnemyProjectilePool pool,PauseHandler pauseHandler)
        {
            _pool = pool;
            _pauseHandler = pauseHandler;
        }
    }
}