using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class Asteroid : BaseEnemy, IMovable
    {
        [SerializeField] private List<Mesh> _modelsList;
        private Coroutine _moveCoroutine;
        private MeshFilter _model;
        private readonly Vector3 _startedSize = new Vector3(1, 1, 1);
        private readonly float _minSpeed = 1f;
        private readonly float _maxSpeed = 3.5f;
        private float _speed;

        private void Awake() => _model = GetComponent<MeshFilter>();

        private void OnEnable()
        {
            _model.mesh = _modelsList[Random.Range(0, _modelsList.Count)];
            transform.localScale = _startedSize * Random.Range(0.6f, 1.7f);
            _speed = Random.Range(_minSpeed, _maxSpeed);
            transform.position = Random.insideUnitCircle.normalized * 30f;
           Move(_speed);
        }
        private void OnDisable() => StopCoroutine(_moveCoroutine);

        public void Move(float value) => _moveCoroutine = StartCoroutine(UpdatePosition(value));
        
        protected void Rotation() { //убрать
            transform.LookAt(Vector3.zero,Vector3.forward * -1);
        }

        private IEnumerator UpdatePosition(float value) {
            while (true) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition,Vector3.zero, value * Time.deltaTime);
                yield return null; } }

       
    }
}