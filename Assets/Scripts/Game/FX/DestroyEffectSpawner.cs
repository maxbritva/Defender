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

        public void Spawn(Transform target) {
            GameObject effect = _destroyEffectPool.GetFromPool();
            effect.transform.position = target.position;
           
        }
    }
}