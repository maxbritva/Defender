using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameCore.GameStates;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Bonus
{
    public class BonusSpawner : IActivatable, IPause, IDisposable, IInitializable
    {
        private List<BonusBase> _bonusPrefabs = new List<BonusBase>();
        private List<GameObject> _bonuses = new List<GameObject>();
        
        private DiContainer _diContainer;
        private PauseHandler _pauseHandler;
        private GameManager _gameManager;
        
        private readonly float _timeBetweenSpawn = 12f;
        private CancellationTokenSource _cts;
        private bool _isPaused;
        private float _timerForSpawn;
        private float _timerForMove;

        private BonusSpawner() => _bonusPrefabs = Resources.Load<BonusesConfiguration>("Config/BonusesConfiguration").BonusPrefabs;
        private List<Vector3> _spawnPoints = Resources.Load<BonusesConfiguration>("Config/BonusesConfiguration").SpawnPoints;

        public async void Activate()
        {
            _cts = new CancellationTokenSource();
            await Spawn().SuppressCancellationThrow();
        }

        public void Initialize()
        {
            _pauseHandler.Add(this);
            InitializeBonuses();
        }

        public void Dispose()
        {
            _cts?.Dispose();
            _pauseHandler.Remove(this);
        }
        
        public void Deactivate() => _cts.Cancel();
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        public void HideAllBonuses()
        {
            for (int i = 0; i < _bonuses.Count; i++) 
                _bonuses[i].SetActive(false);
        }
        
        private void InitializeBonuses()
        {
            for (int i = 0; i < _bonusPrefabs.Count; i++)
            {
                var bonus = _diContainer.InstantiatePrefab(_bonusPrefabs[i]);
                bonus.transform.SetParent(_gameManager.transform);
                bonus.gameObject.SetActive(false);
                _bonuses.Add(bonus);
                _diContainer.Inject(bonus);
            }
        }

        private async UniTask Spawn()
        {
            _timerForSpawn = 0;
            while (_cts.IsCancellationRequested == false)
            {
                if(_isPaused == false)
                    _timerForSpawn += Time.deltaTime;
                if (_timerForSpawn >= _timeBetweenSpawn)
                {
                    GameObject bonus = GetRandomBonus();
                    bonus.SetActive(true);
                    bonus.transform.position = RandomSpawnPoint();
                    _timerForSpawn = 0;
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
            }
        }
        
        private GameObject GetRandomBonus() => _bonuses[Random.Range(0,_bonuses.Count)].gameObject;
        
        private Vector3 RandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        
       [Inject] private void Construct(DiContainer diContainer, PauseHandler pauseHandler, GameManager gameManager)
       {
           _pauseHandler = pauseHandler;
           _gameManager = gameManager;
           _diContainer = diContainer;
       }
       
    }
}