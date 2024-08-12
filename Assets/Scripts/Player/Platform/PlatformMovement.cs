using Game.Interfaces;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlatformMovement : MonoBehaviour, IMovable {
    
        [SerializeField] private Rigidbody _platformRigidbody;
        private InputHandler _inputHandler; 
        private float _turn;
        
        private void FixedUpdate() => Move(_inputHandler.Rotate());

        public void Move(float value)
        {
            _turn = value;
            _platformRigidbody.AddTorque(Vector3.forward * (_turn * 3f * -1 * (90f * Time.deltaTime)), ForceMode.Impulse);
        }

        public bool IsMoving() => _turn != 0;
        [Inject] private void Construct(InputHandler inputHandler) => _inputHandler = inputHandler;
        public void Collision()
        {
            
        }
    }
}