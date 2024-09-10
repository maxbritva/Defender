using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private PauseHandler _pauseHandler;
        private CancellationTokenSource _cancellationToken;
        private bool _isPaused;

        private async void OnEnable()
        {
            _cancellationToken = new CancellationTokenSource();
            _pauseHandler.Add(this);
            try
            {
                await Move();
            }
            catch (OperationCanceledException) { }
        }
        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            _cancellationToken.Cancel();
        }

        public void SetPause(bool isPaused) => _isPaused = isPaused;
        
        private async UniTask Move()
        {
            while (_cancellationToken.IsCancellationRequested == false)
            {
                if (_isPaused == false)
                    transform.position += transform.forward * (_speed * Time.deltaTime);
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken.Token);
            }
            //_cancellationToken.Cancel();
        }

        [Inject] private void Construct(PauseHandler pauseHandler) => 
           _pauseHandler = pauseHandler;
    }
}