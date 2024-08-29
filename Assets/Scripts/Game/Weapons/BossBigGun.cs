using UnityEngine;

namespace Game.Weapons
{
    public class BossBigGun : MonoBehaviour, IBigGunWeapon
    {
        [SerializeField] private Transform _shootPoint;
        public void Shot(GameObject gameObject)
        {
            GameObject bulletFromPool = gameObject;
            bulletFromPool.SetActive(true);
            bulletFromPool.transform.position = _shootPoint.position;
            bulletFromPool.transform.rotation = _shootPoint.rotation;
        }
    }
}