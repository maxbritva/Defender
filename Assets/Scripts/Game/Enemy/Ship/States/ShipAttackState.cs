using System.Collections;
using Game.Health;
using Game.StateMachine;
using Game.Weapons;

namespace Game.Enemy.Ship.States
{
    public class ShipAttackState: ShipState
    {
        public ShipAttackState(IStateSwitcher stateSwitcher, ShipData data, Ship ship) : base(stateSwitcher, data, ship)
        {
        }
        private ObjectPool.ObjectPool _objectPool;
        private GunSingle _gun;
        private EnemyHealth _enemyHealth;
        
        
        private IEnumerator AttackStateMove() {
          
            _gun.Shot(_objectPool);
            while (true) {
                //MoveShip(_positionToGo,_speed,4f);
                if (CalculateKamikazeHealthRemain() <= 30) 
                    StateSwitcher.SwitchState<ShipKamikazeState>();
                yield return null;
            }
        }
        private float CalculateKamikazeHealthRemain() => (_enemyHealth.CurrentHealth / _enemyHealth.MAXHealth) * 100f;
    }
}