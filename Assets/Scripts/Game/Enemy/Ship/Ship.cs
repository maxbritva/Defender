using System.Collections.Generic;
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
        private States.StateMachine _stateMachine;
        private Pool _pool;
        private PauseHandler _pauseHandler;
        private EnemyHealth _enemyHealth;
        private GunSingle _gunSingle;
        private bool _isPaused;
        
        public List<Transform> Waypoints => _waypoints;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _gunSingle = GetComponent<GunSingle>();
            _stateMachine = new States.StateMachine(this, _pool, _enemyHealth, _gunSingle);
        }

        private void Update()
        {
            if(_isPaused)
                return;
            _stateMachine.Update();
        }

        private void OnEnable()
        {
            _stateMachine.OnEnable();
            _pauseHandler.Add(this);
        }
        private void OnDisable() => _pauseHandler.Remove(this);

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        [Inject] private void Construct(ObjectPool.Pool pool,PauseHandler pauseHandler)
        {
            _pool = pool;
            _pauseHandler = pauseHandler;
        }
    }
}