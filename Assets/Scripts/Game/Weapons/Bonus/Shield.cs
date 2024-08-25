using System.Collections;
using Game.Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Game.Weapons.Bonus
{
    public class Shield : MonoBehaviour, IEnemyDestroyable
    {
        [SerializeField] private SphereCollider _shieldCollider;
        [SerializeField] private Renderer _renderer;
        private WaitForSeconds _shieldTimer;
        private UpgradesHandler _upgradesHandler;

        private void Start() => _shieldTimer = new WaitForSeconds(_upgradesHandler.ShieldCurrentLevel.Value);

        public void ActivateShield() {
            StartCoroutine(DisolveShield(0));
            _shieldCollider.enabled = true;
            StartCoroutine(ShieldTimer());
        }

        private IEnumerator ShieldTimer()
        {
            yield return _shieldTimer;
            StartCoroutine(DisolveShield(1));
            _shieldCollider.enabled = false;
        }

        private  IEnumerator DisolveShield(float target) {
            float start = _renderer.material.GetFloat("_Disolve");
            float lerp = 0;
            while (lerp < 1) {
                _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start,target,lerp));
                lerp += Time.deltaTime;
                yield return null;
            }
        }

        [Inject] private void Construct(UpgradesHandler upgradesHandler) => _upgradesHandler = upgradesHandler;
    }
}