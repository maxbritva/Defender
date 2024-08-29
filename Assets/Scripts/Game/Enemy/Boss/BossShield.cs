using System;
using UnityEngine;

namespace Game.Enemy.Boss
{
    public class BossShield : MonoBehaviour
    {
        [SerializeField] private SphereCollider _bossShieldCollider;
        [SerializeField] private ParticleSystem _particleSystemShield;

        private void Start() => SetShield(false);

        public void SetShield(bool enable)
        {
            if (enable)
            {
                _bossShieldCollider.enabled = false;
                _particleSystemShield.Play();
            }
            else
            {
                _bossShieldCollider.enabled = true;
                _particleSystemShield.Stop();
            }
        }
    }
}