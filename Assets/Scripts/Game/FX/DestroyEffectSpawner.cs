using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.ObjectPool;
using UnityEngine;

namespace Game.FX
{
    public class DestroyEffectSpawner : MonoBehaviour
    {
        [SerializeField] private Pool _destroyEffectPool;
        private CancellationTokenSource _cancellationToken;

        private void Start() => _cancellationToken = new CancellationTokenSource();

        private async UniTask Hide(GameObject targetToHide)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2.2f), _cancellationToken.IsCancellationRequested);
            targetToHide.SetActive(false);
            _cancellationToken?.Cancel();
        }

        private void OnDestroy() => _cancellationToken?.Cancel();

        public async UniTask Spawn(Transform target) {
            GameObject effect = _destroyEffectPool.GetFromPool();
            effect.transform.position = target.position;
            await Hide(effect);
        }
    }
}