using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameCore.Pause;
using Game.Interfaces;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformMovement : MonoBehaviour, IPause 
    {
        private InputHandler _inputHandler; 
        private PauseHandler _pauseHandler;
        private CancellationTokenSource _cts;
        private float _stunTimer;
        private bool _isPaused;
        private bool _isStunned;
        private float _turn;

        private async void OnEnable()
        {
            _pauseHandler.Add(this);
            _cts = new CancellationTokenSource();
            await Move().SuppressCancellationThrow();
        }

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            _cts.Cancel();
        }

        public bool IsMoving() => _turn != 0;
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        public async void StunPlatform() => await Stunning();
        
        private async UniTask Move()
        {
            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                _turn = _inputHandler.Rotate();
                if (_isStunned == false && _isPaused == false)
                    transform.Rotate(Vector3.forward * (_turn * 1.5f * -1 * (90f * Time.deltaTime)));
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }

        private async UniTask Stunning()
        {
            _isStunned = true;
            _stunTimer = 0;
            while (_stunTimer <= 3f)
            {
                if(_isPaused == false)
                    _stunTimer += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
            _isStunned = false;
            _cts.Cancel();
        }

        [Inject] private void Construct(InputHandler inputHandler, PauseHandler pauseHandler)
        {
            _inputHandler = inputHandler;
            _pauseHandler = pauseHandler;
        }
    }
}