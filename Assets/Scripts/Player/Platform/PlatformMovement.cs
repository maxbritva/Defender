using Game.Interfaces;
using Player.Input;
using Player.Inputs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Platform
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlatformMovement : MonoBehaviour, IMovable {
    
        [SerializeField] private Rigidbody _platformRigidbody;
        [FormerlySerializedAs("desktopInputHandler")] [FormerlySerializedAs("_playerInputHandler")] [SerializeField] private DesktopInput desktopInput;// заменить астракцией
        private float _turn;
        
        private void FixedUpdate() => Move(desktopInput.ReadInput());

        public void Move(float value)
        {
            _turn = value;
            _platformRigidbody.AddTorque(Vector3.forward * (_turn * 3f * -1 * (90f * Time.deltaTime)), ForceMode.Impulse);
        }
    }
}