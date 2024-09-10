using System.Threading;
using Cysharp.Threading.Tasks;
using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipKamikazeState : ShipState
    {
        private CancellationTokenSource _cts;
        public ShipKamikazeState(IStateSwitcher stateSwitcher, ShipData data, Ship ship) : base(stateSwitcher, data, ship)
        { }

        public override async void Enter()
        {
            base.Enter();
            Data.Speed = 23f;
            _cts = new CancellationTokenSource();
            await KamikazeMove().SuppressCancellationThrow();
        }
        public override void Exit() => _cts.Cancel();

        private async UniTask KamikazeMove()
        {
            ShipAim();
            while (_ship.gameObject.activeInHierarchy)
            {
                if (_ship.IsPaused == false)
                {
                    ShipAim();
                    MoveShip(Vector3.zero, Data.Speed);
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
            _cts.Cancel();
        }
    }
}