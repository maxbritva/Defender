using Game.Interfaces;
using UnityEngine;

namespace Game.Weapons
{
    public class GunSingle : GunBase, IWeapon
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _muzzleParticles;
        
        public void Shot(ObjectPool.Pool targetPool)
        {
            GameObject bulletFromPool = GetBulletFromPool(targetPool);
            bulletFromPool.transform.SetParent(transform);
            bulletFromPool.transform.position = _shootPoint.position;
            bulletFromPool.transform.rotation = _shootPoint.rotation;
           // _muzzleParticles.Play();
        }
        
    }
}