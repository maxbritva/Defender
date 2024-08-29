using Game.ObjectPool;
using UnityEngine;

namespace Game.Weapons
{
    public class ShipGun : MonoBehaviour, IEnemyWeapon
    {
        [SerializeField] private Transform _shootPoint;

        protected GameObject GetBulletFromPool(IEnemyProjectilePool targetPool) => targetPool.GetFromPool();
        public void Shot(IEnemyProjectilePool targetPool)
        {
            GameObject bulletFromPool = GetBulletFromPool(targetPool);
            bulletFromPool.transform.SetParent(transform);
            bulletFromPool.transform.position = _shootPoint.position;
            bulletFromPool.transform.rotation = _shootPoint.rotation;
        }
        
    }
}