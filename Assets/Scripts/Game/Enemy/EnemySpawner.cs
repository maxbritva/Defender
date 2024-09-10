using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private CancellationTokenSource _cts;
        private bool _isPaused;
        private bool _isActive;
        private float _time;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            _cts?.Cancel();
        }


        public async void Activate()
        {
            _cts = new CancellationTokenSource();
            await Spawn().SuppressCancellationThrow();
        }

        public void Deactivate() => _cts?.Cancel();

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

        private async UniTask Spawn()
        {
            _time = 0;
            while (true)
            {
                if (_isPaused == false) 
                        _time += Time.deltaTime;
                if(_time >= _spawnInterval)
                {
                    var enemy = _gameObjectPool.GetFromPool(_prefab);
                    _enemyList.Add(enemy);
                    _time = 0;
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            } 
        }

        [Inject]  private void Construct(PauseHandler pauseHandler, GameObjectPool gameObjectPool)
      {
          _gameObjectPool = gameObjectPool;
          _pauseHandler = pauseHandler;
      }
    }
}