using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameCore.Pause;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class SelfHideFX : MonoBehaviour, IPause
    {
        [SerializeField] private float _timer;
        private CancellationTokenSource _cancellationToken;
        private PauseHandler _pauseHandler;
        private bool _isPaused;

        private async void OnEnable()
        {
            _cancellationToken = new CancellationTokenSource();
            try
            {
                await TimerToHide();
            }
            catch (OperationCanceledException) { }
            _pauseHandler.Add(this);
        }

        private void OnDisable() => _pauseHandler.Remove(this);
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private void OnDestroy() => _cancellationToken.Cancel();

        private async UniTask TimerToHide()
        {
            float currentTime = 0;
            while (currentTime < _timer)
            {
                if(_isPaused == false)
                    currentTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken.Token);
            }
            gameObject.SetActive(false);
            _cancellationToken.Cancel();
        }
        [Inject] private void Construct(PauseHandler pauseHandler) => _pauseHandler = pauseHandler;
    }
}