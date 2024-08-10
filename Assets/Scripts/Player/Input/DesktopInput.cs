using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    
    public class DesktopInput : IInput, IDisposable
    {
        private PlayerInput _playerInput;
        private bool _isFire;
        public DesktopInput()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Gameplay.Fire.performed += OnPressedFire;
            _playerInput.Gameplay.Fire.canceled += OnCanceledFire;
        }
        public void Dispose()
        {
            _playerInput.Dispose();
            _playerInput.Gameplay.Fire.performed -= OnPressedFire;
            _playerInput.Gameplay.Fire.canceled -= OnCanceledFire;
        }

        public float ReadRotationInput() => _playerInput.Gameplay.Rotate.ReadValue<Vector2>().x;
        public bool IsFireButtonActive() => _isFire;
        

        private void OnPressedFire(InputAction.CallbackContext context) => _isFire = true;
        private void OnCanceledFire(InputAction.CallbackContext context) => _isFire = false;
    }
}