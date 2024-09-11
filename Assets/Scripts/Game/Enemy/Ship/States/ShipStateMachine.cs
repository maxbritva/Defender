using System.Collections.Generic;
using System.Linq;
using Game.Health;
using Game.StateMachine;
using Game.StateMachine.States;
using Game.Weapons;

namespace Game.Enemy.Ship.States
{
    public class ShipStateMachine: IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public ShipStateMachine(Ship ship, EnemyHealth health, GunSingle gunSingle)
        {
            ShipData data = new ShipData();
            _states = new List<IState>()
            {
                new ShipFollowState(this, data, ship),
                new ShipAttackState(this, data, ship, gunSingle, health),
                new ShipKamikazeState(this, data, ship)
                
            };
            _currentState = _states[0];
            _currentState.Enter();
        }
        
        public void ExitCurrentState() => _currentState.Exit();

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
        
    }
}