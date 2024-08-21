using System.Collections;
using Game.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class DestroyEffectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Pool _destroyEffectPool;
        private readonly WaitForSeconds _wait = new WaitForSeconds(2.2f);

        private IEnumerator Hide(GameObject targetToHide) {
			yield return _wait;
			targetToHide.SetActive(false);
		}

        public void Spawn(Transform target) {
            GameObject effect = _destroyEffectPool.GetFromPool();
            effect.transform.position = target.position;
            StartCoroutine(Hide(effect));
        }
    }
}