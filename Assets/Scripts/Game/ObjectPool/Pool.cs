using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.ObjectPool
{
    public class Pool : MonoBehaviour, IFactory<GameObject>, IEnemyProjectilePool
    {
        [SerializeField] private GameObject _prefab;
        private List<GameObject> _objectPool = new List<GameObject>();
        private DiContainer _diContainer;

        private void Awake()
        {
          //  for (int i = 0; i < 3; i++) Create();
        }

        public GameObject Create()
        {
            var newObject = _diContainer.InstantiatePrefab(_prefab);
            newObject.SetActive(false);
            newObject.transform.SetParent(transform);
            _objectPool.Add(newObject);
            return newObject;
        }
        
        public GameObject GetFromPool()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                if(_objectPool[i].gameObject.activeInHierarchy) continue;
                SetActive(_objectPool[i].gameObject,true);
                return _objectPool[i];
            }
            GameObject newObject = Create();
            SetActive(newObject.gameObject,true);
            return newObject;
        }

        private void SetActive(GameObject target, bool value) => target.SetActive(value);
        [Inject] private void Construct(DiContainer diContainer) => _diContainer = diContainer;
    }
}