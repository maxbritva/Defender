using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Weapons
{
    public abstract class Projectile : MonoBehaviour, IPause
    {
        [SerializeField] protected int _damage;
        [SerializeField] [Min(0)] private float _speed;
        [SerializeField] private float _timerToHide;
        private PauseHandler _pauseHandler;

        private bool _isPaused;

        private void OnEnable()
        {
            _pauseHandler.Add(this);
            StartCoroutine(Moving());
        }
        private void OnDisable() => _pauseHandler.Remove(this);
        
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private IEnumerator Moving()
        {
            float currentTime = 0;
            while (currentTime < _timerToHide)
            {
                if (_isPaused == false)
                {
                    currentTime += Time.deltaTime;
                    transform.position += transform.forward * (_speed * Time.deltaTime);
                }
                yield return null;
            }
            gameObject.SetActive(false);
        }
        
        [Inject] private void Construct(PauseHandler pauseHandler) => 
           _pauseHandler = pauseHandler;
    }
}