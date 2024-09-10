using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Health;
using Game.StateMachine;
using Game.Weapons;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipAttackState: ShipState
    {
        public ShipAttackState(IStateSwitcher stateSwitcher, ShipData data, Ship ship, 
            ShipGun shipGun, EnemyHealth enemyHealth) : base(stateSwitcher, data, ship)
        {
            _shipGun = shipGun;
            _enemyHealth = enemyHealth;
        }
        private ShipGun _shipGun;
        private EnemyHealth _enemyHealth;
        private float _timeBetweenAttack;
        private float _timeBetweenMove;
        private bool _needToChangePosition;
        private Vector3 _position;
        private CancellationTokenSource _cts;
        
        public override async void Enter()
        {
            base.Enter();
            _position = NewPosition();
            _cts = new CancellationTokenSource();
            await Attack().SuppressCancellationThrow();
        }

        public override void Exit()
        {
            Data.WayPoints.Clear();
            _cts.Cancel();
        }

        private async UniTask Attack()
        {
            ShipAim();
            while (_ship.gameObject.activeInHierarchy)
            {
                if (_ship.IsPaused == false)
                {
                    MoveShip(_position,5f);
                    ShipAim();
                    _timeBetweenAttack += Time.deltaTime;
                    if (_timeBetweenAttack > 3f)
                    {
                        _shipGun.Shot();
                        _position = NewPosition();
                        _timeBetweenAttack = 0;
                    }
                }

                if (CalculateKamikazeHealthRemain() <= 30)
                {
                    StateSwitcher.SwitchState<ShipKamikazeState>();
                    _cts.Cancel();
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }
        
        private float CalculateKamikazeHealthRemain() => 
            _enemyHealth.CurrentHealth / _enemyHealth.MAXHealth * 100f;
  
        private Vector3 NewPosition() => _ship.Waypoints[Random.Range(0, 2)].position;
    }
}