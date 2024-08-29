
using Game.ObjectPool;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IWeapon
    {
        public void Shot(Pool pool);
    }
}