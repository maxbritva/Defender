
namespace Game.Enemy.Ship
{
    public class Ship : BaseEnemy
    {

        private States.StateMachine _stateMachine;

        private void Awake() => _stateMachine = new States.StateMachine(this);

        private void Update() => _stateMachine.Update();

        private void OnEnable() => _stateMachine.OnEnable();


    }
}