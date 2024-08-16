using System.Collections.Generic;
using Game.Health;
using Game.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Ship
{
    public class Ship : BaseEnemy
    {
        [SerializeField] private List<Transform> _waypoints;
        private States.StateMachine _stateMachine;
        private EnemyHealth _enemyHealth;
        private GunSingle _gunSingle;
        private ObjectPool.ObjectPool _objectPool;
        public List<Transform> Waypoints => _waypoints;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _gunSingle = GetComponent<GunSingle>();
            _stateMachine = new States.StateMachine(this, _objectPool, _enemyHealth, _gunSingle);
        }

        private void Update() => _stateMachine.Update();

        private void OnEnable() => _stateMachine.OnEnable();


        [Inject] private void Construct(ObjectPool.ObjectPool pool) => 
            _objectPool = pool;
    }
}