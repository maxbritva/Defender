using System.Collections.Generic;
using System.Linq;
using Game.GameCore.GameStates.States;
using Game.StateMachine;
using Game.StateMachine.States;
using Player;
using Zenject;

namespace Game.GameCore.GameStates
{
    public class GameStateMachine: IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private PlayerData _playerData;

        public GameStateMachine()
        {
            _states = new List<IState>()
            {
                new GamePrepareState(),
                new GameplayState(),
                new EndGameState()
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

        public void OnEnable()
        {
            _currentState = _states[0];
            _currentState.OnEnable();
        }

        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}