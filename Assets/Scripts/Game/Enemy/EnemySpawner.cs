using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour, IPause, IActivatable
    {
        [SerializeField] private Pool _enemyPool;
        [Inject] private DiContainer _diContainer;
        [SerializeField] private float _spawnInterval;
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
                _enemyPool.GetFromPool();
                _time = 0;
                yield return null;
            }
        }
        

      [Inject]  private void Construct(PauseHandler pauseHandler) => _pauseHandler = pauseHandler;

    }
}