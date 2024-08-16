using Game.StateMachine;
using UnityEngine;
using IState = Game.StateMachine.States.IState;

namespace Game.Enemy.Ship.States
{
    public abstract class ShipState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected readonly ShipData Data;
        protected Ship _ship;

        public ShipState(IStateSwitcher stateSwitcher, ShipData data, Ship ship)
        {
            StateSwitcher = stateSwitcher;
            Data = data;
            _ship = ship;
        }

        public virtual void Enter() => Debug.Log(GetType());

        public virtual void Exit() { }
        
        public virtual void OnEnable() => _ship.transform.position = Random.insideUnitCircle.normalized * 30f;

        public virtual void Update() => _ship.transform.LookAt(Vector3.zero, Vector3.forward * -1);
        
      

        protected void MoveShip(Vector3 target, float speed)
        {
            Debug.Log(222);
            _ship.transform.position = Vector3.MoveTowards(_ship.transform.position, target, speed * Time.deltaTime);
        }
    }
}