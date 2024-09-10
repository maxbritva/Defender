using DG.Tweening;
using Game.ObjectPool;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.FX
{
    public class DamageTextSpawner
    {
        private GameObject _prefab;
        private GameObjectPool _gameObjectPool;

        public DamageTextSpawner() => _prefab = Resources.Load<GameObject>("Prefabs/FX/DamageText");

        public void SpawnDamageText(Transform target, int damage)
        {
            var damageText = _gameObjectPool.GetFromPool(_prefab);
            damageText.transform.position = target.position + GetRandomPosition();
            if (!damageText.TryGetComponent(out TextMeshPro damageMeshText)) return;
            damageMeshText.color = new Color(damageMeshText.color.r,damageMeshText.color.g,damageMeshText.color.b,1f);
            damageMeshText.text = damage.ToString();
            float damageSize = damage;
            damageMeshText.fontSize = Mathf.Clamp(damageSize, 7f, 20f);
            damageMeshText.DOFade(0, 1.5f);
        }

        private Vector3 GetRandomPosition() => new Vector3(Random.Range(-1,1f),
            Random.Range(-1f,1),5f);
        
        [Inject] private void Construct(GameObjectPool gameObjectPool) => _gameObjectPool = gameObjectPool;
    }
}