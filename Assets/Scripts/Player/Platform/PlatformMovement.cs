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
        private bool _isPaused;
        private float _turn;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        private void FixedUpdate() => Move(_inputHandler.Rotate());

        public void Move(float value)
        {
            if (_isPaused) return;
            _turn = value;
            _platformRigidbody.AddTorque(Vector3.forward * (_turn * 3f * -1 * (90f * Time.deltaTime)), ForceMode.Impulse);
        }

        public bool IsMoving() => _turn != 0;
        public void SetPause(bool isPaused) => _isPaused = isPaused;
        
        [Inject] private void Construct(InputHandler inputHandler, PauseHandler pauseHandler)
        {
            _inputHandler = inputHandler;
            _pauseHandler = pauseHandler;
        }
    }
}