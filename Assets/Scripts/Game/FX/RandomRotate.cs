using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.FX
{
    public class RandomRotate : MonoBehaviour
    {
        private readonly float _minSpeed = 5f;
        private readonly float _maxSpeed = 80f;
        private readonly float[] _random = new float[3];
        private readonly float[] _randomSide = new float[3];
        private void OnEnable() {
            Setup();
            StartCoroutine(Rotate());
        }

        private void OnDisable() => StopCoroutine(Rotate());

        private void Setup() {
            for (int i = 0; i < 3; i++) {
                _random[i] = Random.Range(_minSpeed, _maxSpeed);
                _randomSide[i] = Random.Range(0, 2);
                if (Math.Abs(_randomSide[i]) < 1) {
                    _randomSide[i] = -1;
                }
            }
        }

        private IEnumerator Rotate() {
            while (true) {
                transform.Rotate(new Vector3(
                    _random[0] * _randomSide[0],
                    _random[1] * _randomSide[1],
                    _random[2] * _randomSide[2]) * Time.deltaTime);
                yield return null;
            }
        }
    }
}