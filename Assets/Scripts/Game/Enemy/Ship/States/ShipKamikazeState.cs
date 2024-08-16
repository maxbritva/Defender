using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipKamikazeState : ShipState
    {
        public ShipKamikazeState(IStateSwitcher stateSwitcher, ShipData data, Ship ship) : base(stateSwitcher, data, ship)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Data.Speed = 23f;
        }

        public override void Update()
        {
            base.Update();
            MoveShip(Vector3.zero,Data.Speed);
        }
    }
}