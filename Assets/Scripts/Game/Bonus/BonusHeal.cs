using Game.Health;
using Zenject;

namespace Game.Bonus
{
    public class BonusHealth : BonusBase
    {
        private PlayerHealth _playerHealth;
        protected override void ActivateBonus()
        {
            _playerHealth.TakeHeal(1);
            _playerHealth.OnPlayerHeal?.Invoke();
        }
        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;
    }
}