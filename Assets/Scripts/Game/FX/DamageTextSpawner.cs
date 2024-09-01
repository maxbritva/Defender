using DG.Tweening;
using Game.ObjectPool;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.FX
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private Pool _damageTextPool;

        public void SpawnDamageText(Transform target, int damage)
        {
            var damageText = _damageTextPool.GetFromPool();
            damageText.transform.SetParent(transform);
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
        
    }
}