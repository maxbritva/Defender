using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.FX
{
    public class RotateObject: MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;

        private async void OnEnable()
        {
            try
            {
                await Rotate();
            }
            catch (OperationCanceledException) { }
        }

        private async UniTask Rotate()
        {
            while (gameObject.activeInHierarchy)
            {
                transform.Rotate(_rotation * Time.deltaTime);
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }
    }
}