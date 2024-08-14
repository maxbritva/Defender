using Game.Health;
using UnityEngine;
using Zenject;

namespace Game.Bonus
{
    public class BonusHealth : BonusBase
    {
        private PlayerHealth _playerHealth;
        protected override void ActivateBonus(GameObject playerProjectile)
        {
            _playerHealth.TakeHeal(1);
            _playerHealth.OnPlayerHeal?.Invoke();
        }

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}