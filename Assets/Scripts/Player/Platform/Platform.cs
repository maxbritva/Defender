using System;
using Cysharp.Threading.Tasks;
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

        private async void OnEnable()
        {
            try
            {
                await PlatformScale();
            }
            catch (OperationCanceledException) { }
        }

        private async UniTask PlatformScale()
        {
            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                transform.localScale = new Vector3(1, _currentSize, 1);
                SetSize(_platformMovement.IsMoving() ? MaxSize : Size);
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }

        private void SetSize(float sizeTo) => _currentSize = Mathf.Lerp(_currentSize, sizeTo, Time.deltaTime * 4f);

        [Inject] private void Construct(PlatformMovement platformMovement) => _platformMovement = platformMovement;
    }
}