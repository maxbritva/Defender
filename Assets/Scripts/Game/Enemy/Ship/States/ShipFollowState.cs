using System.Threading;
using Cysharp.Threading.Tasks;
using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipFollowState : ShipState
    {
        public ShipFollowState(IStateSwitcher stateSwitcher, ShipData data, Ship ship) : base(stateSwitcher, data, ship) { }
        
        private const float DistanceToAttack = 16f;
        private bool _isNeedToMove;
        private CancellationTokenSource _cts;

        public override async void Enter()
        {
            base.Enter();
            Data.Speed = 5f;
            _cts = new CancellationTokenSource();
            await Follow().SuppressCancellationThrow();
        }

        public override void Exit()
        {
            base.Exit();
            _cts.Cancel();
        }

        private async UniTask Follow()
        {
            ShipAim();
            while (_ship.gameObject.activeInHierarchy)
            {
                if (_ship.IsPaused == false)
                {
                    ShipAim();
                    MoveShip(Vector3.zero, Data.Speed);
                    if(Vector3.Distance(_ship.transform.position, Vector3.zero) <= DistanceToAttack)
                    {
                        
                        StateSwitcher.SwitchState<ShipAttackState>();
                        _cts.Cancel();
                    }
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }
    }
}