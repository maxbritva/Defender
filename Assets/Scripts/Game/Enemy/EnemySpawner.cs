using System;
using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour, IPause
    {
        [SerializeField] private ObjectPool.ObjectPool _objectPool;
        [SerializeField] private float _spawnInterval;
        private PauseHandler _pauseHandler;
        private Coroutine _spawnCoroutine;
        private bool _isPaused = false;

        private void Awake()
        {
            for (int i = 0; i < 3; i++)
            {
                var newEnemy = _objectPool.Create();
                newEnemy.transform.SetParent(transform);
            }
        }

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        public void Activate(float value)
        {
            if(value <=0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _spawnInterval = value;
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public void Deactivate()
        {
            if(_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }
        
        private IEnumerator Spawn()
        {
            float time = 0;
            while (true)
            {
                while (time < _spawnInterval)
                {
                    if (_isPaused == false) 
                        time += Time.deltaTime;
                    yield return null;
                }
                GameObject newEnemy = _objectPool.GetFromPool();
                newEnemy.transform.SetParent(transform);
                time = 0;
            }
        }

      [Inject]  private void Construct(PauseHandler pauseHandler)
      {
          _pauseHandler = pauseHandler;
          pauseHandler.Add(this);
      }

      public void SetPause(bool isPaused) => _isPaused = isPaused;
    }
}