using Player.Input;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    public abstract class GunBase : MonoBehaviour
    {
        protected GameObject GetBulletFromPool(ObjectPool.Pool targetPool) => targetPool.GetFromPool();
    }
}