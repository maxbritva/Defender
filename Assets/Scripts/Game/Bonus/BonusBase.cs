using Game.Weapons;
using UnityEngine;

namespace Game.Bonus
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class BonusBase : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out PlayerProjectile playerProjectile) == false) return;
            ActivateBonus(playerProjectile.gameObject);
            gameObject.SetActive(false);
        }
        protected virtual void ActivateBonus(GameObject playerProjectile) { }
        
    }
}