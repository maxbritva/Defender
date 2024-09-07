using System.Threading;
using Cysharp.Threading.Tasks;
using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipFollowState : ShipState
    {
        public ShipFollowState(IStateSwitcher stateSwitcher, ShipData data, Ship ship) : base(stateSwitcher, data, ship) { }
        
        private const float DistanceToAttack = 13f;
        private bool _isNeedToMove;
        private CancellationTokenSource _cancellationToken;

        public override void Enter()
        {
            base.Enter();
            Data.Speed = 5f;
        }
        
        
        public override void Update()
        {
            base.Update();
            MoveShip(Vector3.zero, Data.Speed);
            if(Vector3.Distance(_ship.transform.position, Vector3.zero) <= DistanceToAttack)
                StateSwitcher.SwitchState<ShipAttackState>();
        }
    }
}