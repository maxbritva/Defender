using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.FX
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool.ObjectPool _damageTextPool;
        private readonly WaitForSeconds _animationTick = new WaitForSeconds(0.05f);

        public void SpawnDamageText(Transform target, int damage)
        {
            var damageText = _damageTextPool.GetFromPool();
            damageText.transform.SetParent(transform);
            damageText.transform.position = target.position + GetRandomPosition();
            if (!damageText.TryGetComponent(out TextMeshPro damageMeshText)) return;
            damageMeshText.text = damage.ToString();
            float damageSize = damage;
            damageMeshText.fontSize = Mathf.Clamp(damageSize, 7f, 20f);
            StartCoroutine(TextAnimation(damageMeshText, damageText));
        }

        private IEnumerator TextAnimation(TextMeshPro text, GameObject target)
        {
            var color = text.color;
            color.a = 1f;
            for (float f = 1f; f >= -0.05; f-=0.05f)
            {
                text.color = color;
                color.a = f;
                yield return _animationTick;
            }
            target.SetActive(false);
        }
        
        private Vector3 GetRandomPosition() => new Vector3(Random.Range(-0.5f,0.5f),
            Random.Range(-0.5f,0.5f),0);
        
    }
}