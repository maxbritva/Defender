using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour, IPause
    {
        [SerializeField] protected int _damage;
        [SerializeField] [Min(0)] private float _speed;
        [SerializeField] [Min(0)] private float _selfDestroyTimer;
        private PauseHandler _pauseHandler;
        private bool _isPaused;

        private void OnEnable()
        {
            _pauseHandler.Add(this);
            StartCoroutine(MoveAndSelfDestroy());
        }
        private void OnDisable() => _pauseHandler.Remove(this);
        
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private IEnumerator MoveAndSelfDestroy()
        {
            float time = 0;
            while (true)
            {
                if (time < _selfDestroyTimer)
                {
                    if (_isPaused == false)
                    {
                        time += Time.deltaTime;
                        transform.position += transform.forward * (_speed * Time.deltaTime);
                    }
                }
                else
                    gameObject.SetActive(false);
                yield return null;
            }
        }

       [Inject] private void Construct(PauseHandler pauseHandler) => 
           _pauseHandler = pauseHandler;
    }
}