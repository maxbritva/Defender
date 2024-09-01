using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.FX
{
    public class RandomRotate : MonoBehaviour
    {
        private Vector3 _newRandomRotation;
        
        private async void OnEnable() 
        {
            try
            {
                _newRandomRotation = new Vector3(Random.Range(-80f, 80f), 
                    Random.Range(-80f, 80f), Random.Range(-80f, 80f));
                await Rotate();
            }
            catch (OperationCanceledException) { }
        }

        private async UniTask Rotate()
        {
            while (gameObject.activeInHierarchy)
            {
                transform.Rotate(_newRandomRotation * Time.deltaTime);
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }
    }
}