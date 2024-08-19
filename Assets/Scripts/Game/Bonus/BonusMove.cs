using System.Collections;
using System.Collections.Generic;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Bonus
{
    public class BonusMove : MonoBehaviour, IPause, IActivatable
    {
        private List<Vector3> _spawnPoints = new List<Vector3>()
        {
            new Vector3(20,0,0),
            new Vector3(20,11,0),
            new Vector3(20,-11,0),
            new Vector3(-20,0,0),
            new Vector3(-20,11,0),
            new Vector3(-20,-11,0),
            new Vector3(-7,11,0),
            new Vector3(7,11,0),
            new Vector3(7,-11,0),
            new Vector3(-7,-11,0),
        };
        private PauseHandler _pauseHandler;
        private bool _isPaused;
        private readonly float _timeBetweenMove = 3f;
        private Coroutine _moveCoroutine;
        private void OnEnable()
        {
            _pauseHandler.Add(this);
            transform.position = RandomSpawnPoint();
            Activate();
        }

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            Deactivate();
        }

        public void Activate() => _moveCoroutine = StartCoroutine(Move());

        public void Deactivate()
        {
            if(_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);
            gameObject.SetActive(false);
        }
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private IEnumerator Move()
        {
            float time = 0;
            for (int x = 0; x < 3; x++)
            {
                while (time < _timeBetweenMove)
                {
                    if (_isPaused == false)
                        time += Time.deltaTime;
                    yield return null;
                }
                gameObject.transform.position = RandomSpawnPoint();
                time = 0;
            }
            gameObject.SetActive(false);
        }
        private Vector3 RandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        [Inject] private void Construct(PauseHandler pauseHandler) =>
            _pauseHandler = pauseHandler;
        
    }
}