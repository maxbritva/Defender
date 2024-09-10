using UnityEngine;

namespace Game.Enemy.Boss
{
    public class BossShield : MonoBehaviour
    {
        [SerializeField] private SphereCollider _bossShieldCollider;
        [SerializeField] private GameObject _particleSystemGameObject;

        public void SetCollider(bool enable) => _bossShieldCollider.enabled = enable;

        public void SetShield(bool enable)
        {
            if (enable)
            {
                _bossShieldCollider.enabled = true;
                _particleSystemGameObject.SetActive(true);
            }
            else
            {
                _bossShieldCollider.enabled = false;
                _particleSystemGameObject.SetActive(false);
            }
        }
    }
}