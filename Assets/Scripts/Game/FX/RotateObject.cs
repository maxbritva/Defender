using System.Collections;
using UnityEngine;

namespace Game.FX
{
    public class RotateObject: MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;

        private void OnEnable() => StartCoroutine(StartRotation());

        private IEnumerator StartRotation() {
            while (true) {
                transform.Rotate(_rotation * Time.deltaTime);
                yield return null;
            }}
    }
}