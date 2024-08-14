using System.Collections;
using Game.Health;
using UnityEngine;
using Zenject;

namespace Game.FX
{
    public class DestroyEffectSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool.ObjectPool _destroyEffectPool;
        private readonly WaitForSeconds _wait = new WaitForSeconds(2.2f);
        private PlayerHealth _playerHealth;
        
        private IEnumerator Hide(GameObject targetToHide) {
			yield return _wait;
			targetToHide.SetActive(false);
		}

        public void Spawn(Transform target) {
            GameObject effect = _destroyEffectPool.GetFromPool();
            effect.transform.position = target.position;
            StartCoroutine(Hide(effect));
        }

       [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}