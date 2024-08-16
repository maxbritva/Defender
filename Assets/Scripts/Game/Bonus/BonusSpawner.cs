using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Bonus
{
    public class BonusSpawner : MonoBehaviour, IActivatable
    {
        [SerializeField] private List<BonusBase> _bonusPrefabs = new List<BonusBase>();
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        private List<GameObject> _bonuses = new List<GameObject>();
        private readonly WaitForSeconds _waitBetweenSpawn = new WaitForSeconds(10f);
        private readonly WaitForSeconds _moveTick = new WaitForSeconds(3f);
        private Coroutine _bonusCoroutine;
        private DiContainer _diContainer;

        private void Start()
        {
           InitializeBonuses();
           Activate();
        }
        public void Activate() => _bonusCoroutine = StartCoroutine(BonusSpawn());

        public void Deactivate() {
          if(_bonusCoroutine != null)
            StopCoroutine(BonusSpawn());
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
            while (true) {
                GameObject bonus = GetRandomBonus();
                bonus.transform.position = RandomSpawn().position;
                bonus.SetActive(true);
                for ( int x = 0; x < 3; x++) {
                    bonus.gameObject.transform.position = RandomSpawn().position;
                    yield return _moveTick;	
                }
                bonus.SetActive(false);
                yield return _waitBetweenSpawn;
            }
        }
		
        private GameObject GetRandomBonus() {
            GameObject bonus = _bonuses[Random.Range(0,_bonuses.Count)].gameObject;
            bonus.SetActive(false);
            return bonus;
        }
        private Transform RandomSpawn() => _spawnPoints[Random.Range(0, _spawnPoints.Count)];
       [Inject] private void Construct(DiContainer diContainer) => _diContainer = diContainer;
       
    }
}