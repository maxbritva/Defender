using Game.Health;
using Game.ObjectPool;
using Game.StateMachine;
using Game.Weapons;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipAttackState: ShipState
    {
        public ShipAttackState(IStateSwitcher stateSwitcher, ShipData data, Ship ship, 
            Pool pool, ShipGun shipGun, EnemyHealth enemyHealth) : base(stateSwitcher, data, ship)
        {
            _pool = pool;
            _shipGun = shipGun;
            _enemyHealth = enemyHealth;
        }
        private Pool _pool;
        private ShipGun _shipGun;
        private EnemyHealth _enemyHealth;
        private float _timeBetweenAttack;
        private float _timeBetweenMove;
        private bool _needToChangePosition;
        private Vector3 _position;
        
        public override void Enter()
        {
            base.Enter();
            _position = NewPosition();
        }

        public override void Exit() => Data.WayPoints.Clear();

        public override void Update()
        {
            base.Update();
            Attack();
            MoveShip(_position,5f);
            if (CalculateKamikazeHealthRemain() <= 30) 
                StateSwitcher.SwitchState<ShipKamikazeState>();
        }
        
        private void Attack()
        {
            _timeBetweenAttack += Time.deltaTime;
            if (_timeBetweenAttack > 3f)
            {
                _shipGun.Shot(_pool);
                _position = NewPosition();
                _timeBetweenAttack = 0;
            }
        }
        private float CalculateKamikazeHealthRemain() => 
            _enemyHealth.CurrentHealth / _enemyHealth.MAXHealth * 100f;
  
        private Vector3 NewPosition() => _ship.Waypoints[Random.Range(0, 2)].position;
    }
}