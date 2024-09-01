using System.Collections;
using System.Collections.Generic;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Bonus
{
    public class BonusSpawner : MonoBehaviour, IActivatable, IPause
    {
        [SerializeField] private List<BonusBase> _bonusPrefabs = new List<BonusBase>();
        private List<GameObject> _bonuses = new List<GameObject>();
        private Coroutine _bonusCoroutine;
        private DiContainer _diContainer;
        private PauseHandler _pauseHandler;
        private bool _isPaused;
        private float _timeBetweenSpawn = 12f;
        private float _timer;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        public void Activate()
        {
            InitializeBonuses();
            _bonusCoroutine = StartCoroutine(BonusSpawn());
        }

        public void Deactivate() {
          if(_bonusCoroutine != null)
            StopCoroutine(_bonusCoroutine);
        }
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
                bonus.transform.SetParent(transform);
                bonus.gameObject.SetActive(false);
                _bonuses.Add(bonus);
                _diContainer.Inject(bonus);
            }
        }

        private IEnumerator BonusSpawn() {
            _timer = 0;
            while (true) {
                while (_timer < _timeBetweenSpawn)
                {
                    if(_isPaused == false)
                        _timer += Time.deltaTime;
                    yield return null;
                }
                GameObject bonus = GetRandomBonus();
                bonus.SetActive(true);
                _timer = 0;
                yield return null;
            }
        }
        private GameObject GetRandomBonus() => _bonuses[Random.Range(0,_bonuses.Count)].gameObject;
        
       [Inject] private void Construct(DiContainer diContainer, PauseHandler pauseHandler)
       {
           _pauseHandler = pauseHandler;
           _diContainer = diContainer;
       }
    }
}