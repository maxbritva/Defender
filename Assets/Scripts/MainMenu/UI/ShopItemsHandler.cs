using Player;
using UnityEngine;
using Zenject;

namespace MainMenu.UI
{
    public class ShopItemsHandler : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private ShopItemView _platformView;
        [SerializeField] private ShopItemView _livesView;
        [SerializeField] private ShopItemView _shieldView;
        [SerializeField] private ShopItemView _shootRateView;
        [SerializeField] private ShopItemView _damageView;
        [SerializeField] private ShopItemView _critView;
        
        private UpgradesHandler _upgradeHandler;
        private PlayerData _playerData;

        private void OnEnable()
        {
            CheckAvailableButtons();
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            _platformView.UpdatePrice(_upgradeHandler.PlatformCurrentLevel.Cost);
            _livesView.UpdatePrice(_upgradeHandler.LivesCurrentLevel.Cost);
            _shieldView.UpdatePrice(_upgradeHandler.ShieldCurrentLevel.Cost);
            _shootRateView.UpdatePrice(_upgradeHandler.ShootRateCurrentLevel.Cost);
            _damageView.UpdatePrice(_upgradeHandler.DamageCurrentLevel.Cost);
            _critView.UpdatePrice(_upgradeHandler.CritCurrentLevel.Cost);
        }
        
        private void CheckAvailableButtons() 
        {
            _platformView.IsLockCheck(_playerData.Balance >= _upgradeHandler.PlatformCurrentLevel.Cost && _playerData.PlatformGunLevel < 5);
            _livesView.IsLockCheck(_playerData.Balance >= _upgradeHandler.LivesCurrentLevel.Cost && _playerData.LivesCountLevel < 5);
            _shieldView.IsLockCheck(_playerData.Balance >= _upgradeHandler.ShieldCurrentLevel.Cost && _playerData.ShieldTimerLevel < 5);
            _shootRateView.IsLockCheck(_playerData.Balance >= _upgradeHandler.ShootRateCurrentLevel.Cost && _playerData.ShootRateLevel < 5);
            _damageView.IsLockCheck(_playerData.Balance >= _upgradeHandler.DamageCurrentLevel.Cost && _playerData.DamageLevel < 5);
            _critView.IsLockCheck(_playerData.Balance >= _upgradeHandler.CritCurrentLevel.Cost && _playerData.CritLevel < 5);
        }

        [Inject] private void Construct(PlayerData playerData, UpgradesHandler upgradesHandler)
        {
            _playerData = playerData;
            _upgradeHandler = upgradesHandler;
        }
    }
}