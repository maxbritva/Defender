using Game.Weapons.Bonus;
using UnityEngine;
using Zenject;

namespace Game.Bonus
{
    public class BonusBomb : BonusBase
    {
        private Bomb _bomb;
       
        protected override void ActivateBonus(GameObject playerProjectile)
        {
            _bomb.gameObject.SetActive(true);
            //_bomb.ActivateBomb();
        }

        [Inject] private void Construct(Bomb bomb) => _bomb = bomb;
    }
}