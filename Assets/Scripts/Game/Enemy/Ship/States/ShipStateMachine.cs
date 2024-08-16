using System.Collections.Generic;
using System.Linq;
using Game.StateMachine;
using Game.StateMachine.States;

namespace Game.Enemy.Ship.States
{
    public class StateMachine: IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public StateMachine(Ship ship)
        {
            ShipData data = new ShipData();
            _states = new List<IState>()
            {
                new ShipFollowState(this, data, ship),
                new ShipAttackState(this, data, ship),
                new ShipKamikazeState(this, data, ship)
                
            };
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();

        public void OnEnable() => _currentState.OnEnable();
    }
}