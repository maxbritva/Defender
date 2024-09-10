using Game.Weapons.Bonus;
using Zenject;

namespace Game.Bonus
{
    public class BonusBomb : BonusBase
    {
        private Bomb _bomb;
       
        protected override void ActivateBonus() => _bomb.gameObject.SetActive(true);

        [Inject] private void Construct(Bomb bomb) => _bomb = bomb;
    }
}