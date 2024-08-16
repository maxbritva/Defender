using System.Collections;
using Game.Interfaces;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour, IActivatable
    {
        [SerializeField] private ObjectPool.ObjectPool _objectPool;
        [SerializeField] private float _spawnInterval;
        private WaitForSeconds _interval;
        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            for (int i = 0; i < 3; i++)
            {
                var newEnemy = _objectPool.Create();
                newEnemy.transform.SetParent(transform);
            }
            _interval = new WaitForSeconds(_spawnInterval);
            Activate();
        }
        
        public void Activate() => _spawnCoroutine = StartCoroutine(Spawn());

        public void Deactivate()
        {
            if(_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }
        
        private IEnumerator Spawn()
        {
            while (true)
            {
                GameObject newEnemy = _objectPool.GetFromPool();
                newEnemy.transform.SetParent(transform);
                yield return _interval;
            }
        }
    }
}