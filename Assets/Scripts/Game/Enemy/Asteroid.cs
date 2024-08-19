using System.Collections;
using System.Collections.Generic;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class Asteroid : MonoBehaviour, IMovable, IPause
    {
        [SerializeField] private List<Mesh> _modelsList;
        private PauseHandler _pauseHandler;
        private Coroutine _moveCoroutine;
        private MeshFilter _model;
        private readonly Vector3 _startedSize = new Vector3(1, 1, 1);
        private readonly float _minSpeed = 1f;
        private readonly float _maxSpeed = 3.5f;
        private float _speed;
        private bool _isPaused;

        private void Awake() => _model = GetComponent<MeshFilter>();

        private void OnEnable()
        {
            _pauseHandler.Add(this);
            _model.mesh = _modelsList[Random.Range(0, _modelsList.Count)];
            transform.localScale = _startedSize * Random.Range(0.6f, 1.7f);
            _speed = Random.Range(_minSpeed, _maxSpeed);
            transform.position = Random.insideUnitCircle.normalized * 30f;
           Move(_speed);
        }
        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            StopCoroutine(_moveCoroutine);
        }
        public void SetPause(bool isPaused) => _isPaused = isPaused;
        public void Move(float value) => _moveCoroutine = StartCoroutine(UpdatePosition(value));

        private IEnumerator UpdatePosition(float value) {
            while (true) {
                if(_isPaused == false)
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition,Vector3.zero, value * Time.deltaTime);
                yield return null; } }

        [Inject] private void Construct(PauseHandler pauseHandler) => 
            _pauseHandler = pauseHandler;
    }
}