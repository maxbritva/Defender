using UnityEngine;

namespace Game.ObjectPool
{
    public interface IEnemyProjectilePool
    {
        public GameObject GetFromPool();
    }
}