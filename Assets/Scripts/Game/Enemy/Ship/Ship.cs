using System.Collections.Generic;
using Game.GameCore.Pause;
using Game.Health;
using Game.Interfaces;
using Game.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Ship
{
    public class Ship : MonoBehaviour, IPause
    {
        [SerializeField] private List<Transform> _waypoints;
        private States.StateMachine _stateMachine;
        private ObjectPool.ObjectPool _objectPool;
        private PauseHandler _pauseHandler;
        private EnemyHealth _enemyHealth;
        private GunSingle _gunSingle;
        private bool _isPaused;
        
        public List<Transform> Waypoints => _waypoints;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _gunSingle = GetComponent<GunSingle>();
            _stateMachine = new States.StateMachine(this, _objectPool, _enemyHealth, _gunSingle);
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

        [Inject] private void Construct(ObjectPool.ObjectPool pool,PauseHandler pauseHandler)
        {
            _objectPool = pool;
            _pauseHandler = pauseHandler;
        }
    }
}