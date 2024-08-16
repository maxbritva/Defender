using Game.StateMachine.States;

namespace Game.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}