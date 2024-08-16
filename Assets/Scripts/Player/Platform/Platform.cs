using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class Platform : MonoBehaviour, IEnemyDestroyable
    {
        private PlatformMovement _platformMovement;
        private const float MaxSize = 1.5f;
        private const float Size = 1f;
        private float _currentSize = 1f;
		
        private void Update() {
            transform.localScale = new Vector3(1, _currentSize, 1);
            SetSize(_platformMovement.IsMoving() ? MaxSize : Size);
        }

        private void SetSize(float sizeTo) => _currentSize = Mathf.Lerp(_currentSize, sizeTo, Time.deltaTime * 4f);

        [Inject] private void Construct(PlatformMovement platformMovement) => _platformMovement = platformMovement;
    }
}