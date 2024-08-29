using Game.ObjectPool;

namespace Game.Weapons
{
    public interface IEnemyWeapon
    {
        public void Shot(IEnemyProjectilePool targetPool);
    }
}