using UnityEngine.EventSystems;
using Zenject;

namespace Player.Input
{
    public class MobileInput : IInput
    {
        private Joystick _joystick;
        private FireButton _fireButton;

        public float ReadRotationInput() => _joystick.Horizontal;
        
        public bool IsFireButtonActive() => _fireButton.isFireButtonOn();
        
        
        [Inject] private void Construct(Joystick joystick, FireButton fireButton)
        {
            _joystick = joystick;
            _fireButton = fireButton;
        }
    }
}