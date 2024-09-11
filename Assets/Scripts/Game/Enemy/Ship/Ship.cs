using System.Collections.Generic;
using Game.Enemy.Ship.States;
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
        private ShipStateMachine _shipStateMachine;
        private PauseHandler _pauseHandler;
        private EnemyHealth _enemyHealth;
        private GunSingle _gunSingle;
        public bool IsPaused { get; private set; }
        
        public List<Transform> Waypoints => _waypoints;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _gunSingle = GetComponent<GunSingle>();
            _shipStateMachine = new ShipStateMachine(this, _enemyHealth, _gunSingle);
        }

        private void OnEnable()
        {
            _pauseHandler.Add(this);
            transform.position = Random.insideUnitCircle.normalized * 39f;
            _shipStateMachine.SwitchState<ShipFollowState>();
        }

        private void OnDisable()
        {
            _shipStateMachine.ExitCurrentState();
            _pauseHandler.Remove(this);
        }

        public void SetPause(bool isPaused) => IsPaused = isPaused;

        [Inject] private void Construct(PauseHandler pauseHandler) => _pauseHandler = pauseHandler;
    }
}