using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlatformMovement : MonoBehaviour {
    
        [SerializeField] private Transform _platform;

        private void Update() => Rotate(3f);

        public void Rotate(float value) => _platform.Rotate(0, 0, value * Time.deltaTime);
    }
}