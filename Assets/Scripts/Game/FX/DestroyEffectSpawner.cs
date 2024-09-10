using Game.GameCore.GameStates;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class DestroyEffectSpawner
    {
        private GameObjectPool _gameObjectPool;
        private GameObject _prefab;
        private GameManager _gameManager;

        public DestroyEffectSpawner() => _prefab = Resources.Load<GameObject>("Prefabs/FX/Explosion");

        public void Spawn(Transform target) {
            GameObject effect = _gameObjectPool.GetFromPool(_prefab, _gameManager.transform);
            effect.transform.position = target.position;
        }
        
        [Inject] private void Construct(GameObjectPool gameObjectPool, GameManager gameManager)
        {
            _gameObjectPool = gameObjectPool;
            _gameManager = gameManager;
        }
    }
}