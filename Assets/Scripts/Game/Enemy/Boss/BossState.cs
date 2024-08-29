using Game.StateMachine;
using UnityEngine;
using IState = Game.StateMachine.States.IState;

namespace Game.Enemy.Boss
{
    public class BossState : IState
    {
        protected readonly IStateSwitcher StateSwitcher;
        protected Boss _boss;

        public BossState(IStateSwitcher stateSwitcher, Boss boss)
        {
            StateSwitcher = stateSwitcher;
            _boss = boss;
        }

        public virtual void Enter() => Debug.Log(GetType());

        public virtual void Exit() { }

        public virtual void Update()  => _boss.transform.LookAt(Vector3.zero, Vector3.forward * -1);

        public virtual void OnEnable() { }
    }
}