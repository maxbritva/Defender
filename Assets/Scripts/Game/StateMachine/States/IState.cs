namespace Game.StateMachine.States
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void OnEnable();
    }
}