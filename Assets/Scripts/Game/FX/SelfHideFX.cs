using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class SelfHideFX : MonoBehaviour, IPause
    {
        [SerializeField] private float _timer;
        private PauseHandler _pauseHandler;
        private Coroutine _coroutine;
        private bool _isPaused;

        private void OnEnable()
        {
            _coroutine = StartCoroutine(LifeTime());
            _pauseHandler.Add(this);
        }

        private void OnDisable()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _pauseHandler.Remove(this);
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;
        private IEnumerator LifeTime()
        {
            float currentTime = 0;
            while (currentTime <= _timer)
            {
                if(_isPaused == false)
                    currentTime += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
        [Inject] private void Construct(PauseHandler pauseHandler) => _pauseHandler = pauseHandler;
    }
}