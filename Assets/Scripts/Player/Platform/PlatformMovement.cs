using System.Collections;
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
        private Coroutine _stunCoroutine;
        private float _stunTimer;
        private bool _isPaused;
        private bool _isStunned;
        private float _turn;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
            if(_stunCoroutine != null)
                StopCoroutine(_stunCoroutine);
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

        public void StunPlatform() => _stunCoroutine = StartCoroutine(Stun());

        private IEnumerator Stun()
        {
            _isStunned = true;
            _stunTimer = 0;
            while (true)
            {
                if(_isPaused == false)
                    _stunTimer += Time.deltaTime;
                if (_stunTimer >= 2f) 
                    _isStunned = false;
                yield return null;
            }
        }
        
        [Inject] private void Construct(InputHandler inputHandler, PauseHandler pauseHandler)
        {
            _inputHandler = inputHandler;
            _pauseHandler = pauseHandler;
        }
    }
}