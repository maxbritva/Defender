using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class DestroyEffectSpawner
    {
        private GameObjectPool _gameObjectPool;
        private GameObject _prefab;
        public DestroyEffectSpawner() => _prefab = Resources.Load<GameObject>("Prefabs/FX/Explosion");

        public void Spawn(Transform target) {
            GameObject effect = _gameObjectPool.GetFromPool(_prefab);
            effect.transform.position = target.position;
        }
        [Inject] private void Construct(GameObjectPool gameObjectPool) => _gameObjectPool = gameObjectPool;
    }
}