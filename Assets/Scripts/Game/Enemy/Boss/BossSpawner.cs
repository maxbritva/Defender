using System.Collections;
using System.Collections.Generic;
using Game.GameCore.GameStates;
using Game.GameCore.Pause;
using Game.Health;
using Game.Interfaces;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Boss
{
    public class BossSpawner : MonoBehaviour, IPause, IActivatable
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private GameObject _bossPrefab;
        [SerializeField] private Pool _minionPool;
        private List<GameObject> _minions = new List<GameObject>();
        private PauseHandler _pauseHandler;
        private Coroutine _minionsCoroutine;
        private Boss _boss;
        private GameManager _gameManager;
        private bool _isPaused;
        private float _time;
        
        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        public List<Transform> SpawnPoints => _spawnPoints;

        public void ChangeBossHealth(int value) => _boss.GetComponent<BossEnemyHealth>().SetMaxHealth(value);

        public void StartSpawnMinions() => _minionsCoroutine = StartCoroutine(SpawnMinions());

        public void StopSpawnMinions()
        {
            if(_minionsCoroutine != null )
                StopCoroutine(_minionsCoroutine);
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        public void Activate()
        {
            _gameManager.OnBossLevelStarted?.Invoke();
            _boss.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            _boss.gameObject.SetActive(true);
        }

        public void Deactivate() => HideAllMinions();

        public void RemoveMinionFromList(GameObject targetMinion) => _minions.Remove(targetMinion);

        private void HideAllMinions()
        {
            if (_minions.Count <= 0) return;
            for (int i = 0; i < _minions.Count; i++)
            {
                if(_minions[i].activeInHierarchy)
                    _minions[i].GetComponent<EnemyHealth>().DestroyEnemy();
            }
            _minions.Clear();
        }
        
        private IEnumerator SpawnMinions()
        {
            _time = 0;
            while (true)
            {
                while (_time < 1f)
                {
                    if (_isPaused == false) 
                        _time += Time.deltaTime;
                    yield return null;
                }
                var minion = _minionPool.GetFromPool();
                _minions.Add(minion);
                minion.transform.SetParent(transform);
                _time = 0;
                yield return null;
            }
        }
        [Inject] private void Construct(PauseHandler pauseHandler, GameManager gameManager, Boss boss)
        {
            _pauseHandler = pauseHandler;
            _gameManager = gameManager;
            _boss = boss;
        }
    }
}