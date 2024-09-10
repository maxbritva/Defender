using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    public class ShipGun : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private GameObject _prefabProjectile;
        private GameObjectPool _gameObjectPool;
        
        public void Shot()
        {
            GameObject bulletFromPool = _gameObjectPool.GetFromPool(_prefabProjectile);
            bulletFromPool.transform.SetParent(transform);
            bulletFromPool.transform.position = _shootPoint.position;
            bulletFromPool.transform.rotation = _shootPoint.rotation;
        }

        [Inject] private void Construct(GameObjectPool gameObjectPool) => _gameObjectPool = gameObjectPool;
    }
}