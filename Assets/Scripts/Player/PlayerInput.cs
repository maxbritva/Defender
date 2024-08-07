using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private CharacterController _characterController;
        
    }
}
