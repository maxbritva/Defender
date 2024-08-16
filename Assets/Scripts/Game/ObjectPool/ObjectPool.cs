using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.ObjectPool
{
    public class ObjectPool : MonoBehaviour, IFactory<GameObject>
    {
        [SerializeField] protected GameObject _prefab;
        private List<GameObject> _objectPool = new List<GameObject>();
        [Inject] private DiContainer _diContainer;

        public virtual GameObject GetFromPool()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                if(_objectPool[i].activeInHierarchy) continue;
                SetActive(_objectPool[i],true);
                return _objectPool[i];
            }
            GameObject newObject = Create();
            SetActive(newObject,true);
            return newObject;
        }
        
        public GameObject Create()
        {
            GameObject newObject = _diContainer.InstantiatePrefab(_prefab);
            _diContainer.Inject(newObject);
            SetActive(newObject,false);
            _objectPool.Add(newObject);
            return newObject;
        }
        private void SetActive(GameObject target ,bool value) => target.SetActive(value);
    }
}