using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class Asteroid : MonoBehaviour, IPause, IDisposable
    {
        [SerializeField] private List<Mesh> _modelsList;
        private PauseHandler _pauseHandler;
        private CancellationTokenSource _cancellationToken;
        private MeshFilter _model;
        private readonly Vector3 _startedSize = new Vector3(1, 1, 1);
        private readonly float _minSpeed = 1f;
        private readonly float _maxSpeed = 3.5f;
        private float _speed;
        private bool _isPaused;

        private void Awake()
        {
            _model = GetComponent<MeshFilter>();
            _cancellationToken = new CancellationTokenSource();
        }

        private async void OnEnable()
        {
            _pauseHandler.Add(this);
            _model.mesh = _modelsList[Random.Range(0, _modelsList.Count)];
            transform.localScale = _startedSize * Random.Range(0.6f, 1.7f);
            _speed = Random.Range(_minSpeed, _maxSpeed);
            transform.position = Random.insideUnitCircle.normalized * 30f;
            try
            {
                await Move();
            }
            catch (OperationCanceledException) { }
        }
        private void OnDisable() => _pauseHandler.Remove(this);

        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private async UniTask Move()
        {
            while (gameObject.activeInHierarchy)
            {
                if (_isPaused == false)
                    transform.localPosition =
                        Vector3.MoveTowards(transform.localPosition, Vector3.zero, _speed * Time.deltaTime);
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }

        [Inject] private void Construct(PauseHandler pauseHandler) => 
            _pauseHandler = pauseHandler;

        public void Dispose() => _cancellationToken?.Dispose();
    }
}