using System;
using Player.Inputs;
using UnityEngine;

namespace Player.Input
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public event Action<Vector2> Rotate;
        public event Action Fire;
        
        private PlayerInput _playerInput;

        private void Awake() => _playerInput = new PlayerInput();

        private void OnEnable() => _playerInput.Enable();

        private void OnDisable() => _playerInput.Disable();
        
        public float ReadInput() => _playerInput.Gameplay.Rotate.ReadValue<Vector2>().x;
    }
}