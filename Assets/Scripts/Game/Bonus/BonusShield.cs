using Game.Weapons.Bonus;
using UnityEngine;
using Zenject;

namespace Game.Bonus
{
    public class BonusShield : BonusBase
    {
        private Shield _shield;

        protected override void ActivateBonus(GameObject playerProjectile)
        {
            base.ActivateBonus(playerProjectile);
            _shield.ActivateShield();
        }

       [Inject] private void Construct(Shield shield) => _shield = shield;
    }
}