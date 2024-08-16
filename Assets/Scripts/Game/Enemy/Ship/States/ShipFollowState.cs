using Game.StateMachine;
using UnityEngine;

namespace Game.Enemy.Ship.States
{
    public class ShipFollowState : ShipState
    {
        public ShipFollowState(IStateSwitcher stateSwitcher, ShipData data, Ship ship) : base(stateSwitcher, data, ship) { }
        private const float DistanceToAttack = 13f;

        public override void Enter()
        {
            base.Enter();
            Data.Speed = 5f;
        }
        
        public override void Update()
        {
            base.Update();
            MoveShip(Vector3.zero, Data.Speed);
            if(DistanceToPlanet() <= DistanceToAttack)
                StateSwitcher.SwitchState<ShipAttackState>();
        }
        
        private float DistanceToPlanet() => Vector3.Distance(_ship.transform.position, Vector3.zero);
        
    }
}