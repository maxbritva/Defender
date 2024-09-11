using System.Threading;
using Cysharp.Threading.Tasks;
using Game.FX;
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
        protected CancellationTokenSource _CTS;
        private bool _isPaused;

        private async void OnEnable()
        {
            _CTS = new CancellationTokenSource();
            _pauseHandler.Add(this);
            await Move().SuppressCancellationThrow();
        }
        private void OnDisable()
        {
            _pauseHandler.Remove(this);
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;
        
        private async UniTask Move()
        {
            float currentTime = 0;
            while (currentTime < _timerToHide)
            {
                if (_isPaused == false)
                {
                    currentTime += Time.deltaTime;
                    transform.position += transform.forward * (_speed * Time.deltaTime);
                }
                await UniTask.Yield(PlayerLoopTiming.Update, _CTS.Token);
            }
            _CTS.Cancel();
            gameObject.SetActive(false);
        }
        
        private async UniTask TimerToHide()
        {
            float currentTime = 0;
            while (currentTime < _timerToHide)
            {
                if(_isPaused == false)
                    currentTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, _CTS.Token);
            }
            gameObject.SetActive(false);
            _CTS.Cancel();
        }

        [Inject] private void Construct(PauseHandler pauseHandler) => 
           _pauseHandler = pauseHandler;
    }
}