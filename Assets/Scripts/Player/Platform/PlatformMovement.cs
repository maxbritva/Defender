using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameCore.Pause;
using Game.Interfaces;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlatformMovement : MonoBehaviour, IMovable, IPause {
    
        [SerializeField] private Rigidbody _platformRigidbody;
        private InputHandler _inputHandler; 
        private PauseHandler _pauseHandler;
        private CancellationTokenSource _cts;
        private float _stunTimer;
        private bool _isPaused;
        private bool _isStunned;
        private float _turn;

        private void OnEnable()
        {
            _pauseHandler.Add(this);
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            _cts.Cancel();
        }

        private void FixedUpdate()
        {
            if(_isStunned == false) 
                Move(_inputHandler.Rotate());
        }

        public void Move(float value)
        {
            if (_isPaused) return;
            _turn = value;
            _platformRigidbody.AddTorque(Vector3.forward * (_turn * 3f * -1 * (90f * Time.deltaTime)), ForceMode.Impulse);
        }

        public bool IsMoving() => _turn != 0;
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        public async UniTask StunPlatform() => await Stunning();

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