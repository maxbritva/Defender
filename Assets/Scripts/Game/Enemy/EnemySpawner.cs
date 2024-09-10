using System.Collections;
using System.Collections.Generic;
using Game.GameCore.Pause;
using Game.Health;
using Game.Interfaces;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour, IPause, IActivatable
    {
        [SerializeField] private float _spawnInterval;
        [SerializeField] private GameObject _prefab;
        private GameObjectPool _gameObjectPool;
        private List<GameObject> _enemyList = new List<GameObject>();
        private PauseHandler _pauseHandler;
        private Coroutine _spawnCoroutine;
        private bool _isPaused;
        private bool _isActive;
        private float _time;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

      
        public void Activate() => _spawnCoroutine = StartCoroutine(Spawn());

        public void Deactivate()
        {
            if(_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        public void ChangeSpawnInterval(float value) => _spawnInterval = value;
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        public void HideAllEnemy()
        {
            if (_enemyList.Count <= 0) return;
            for (int i = 0; i < _enemyList.Count; i++)
            {
                if(_enemyList[i].activeInHierarchy)
                    _enemyList[i].GetComponent<EnemyHealth>().DestroyEnemy();
            }
            _enemyList.Clear();
        }

        private IEnumerator Spawn()
        {
            _time = 0;
            while (true)
            {
                while (_time < _spawnInterval)
                {
                    if (_isPaused == false) 
                        _time += Time.deltaTime;
                    yield return null;
                }

                var enemy = _gameObjectPool.GetFromPool(_prefab, transform);
                _enemyList.Add(enemy);
                _time = 0;
                yield return null;
            }
        }
        
      [Inject]  private void Construct(PauseHandler pauseHandler, GameObjectPool gameObjectPool)
      {
          _gameObjectPool = gameObjectPool;
          _pauseHandler = pauseHandler;
      }
    }
}