using System.Collections;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] [Min(0)] private float _speed;
        [SerializeField] [Min(0)] private float _selfDestroyTimer;
        private WaitForSeconds _delay;

        private void OnEnable()
        {
            _delay = new WaitForSeconds(_selfDestroyTimer);
            StartCoroutine(SelfDestroy());
        }

        private void FixedUpdate() => _rigidbody.linearVelocity = transform.forward * (_speed);

        private IEnumerator SelfDestroy()
        {
            yield return _delay;
            gameObject.SetActive(false);
        }
    }
}