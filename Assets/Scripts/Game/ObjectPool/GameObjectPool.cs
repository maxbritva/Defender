using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.ObjectPool
{
    public class GameObjectPool
    {
        private Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();
        private DiContainer _diContainer;

        private GameObject Create(GameObject prefab, Transform parent)
        {
            var newObject = _diContainer.InstantiatePrefab(prefab);
            newObject.SetActive(false);
            newObject.transform.SetParent(parent);
            return newObject;
        }
        
        public GameObject GetFromPool(GameObject prefab, Transform parent)
        {
            if (_pool.ContainsKey(prefab.name) == false)
                _pool[prefab.name] = new List<GameObject>();
            
            for (int i = 0; i < _pool[prefab.name].Count ; i++)
            {
                var gameObject = _pool[prefab.name][i];
                if(gameObject.activeInHierarchy) continue;
                gameObject.SetActive(true);
                return gameObject;
            }
            var newObject = Create(prefab, parent);
            newObject.SetActive(true);
            return newObject;
        }

        [Inject] private void Construct(DiContainer diContainer) => _diContainer = diContainer;
    }
}