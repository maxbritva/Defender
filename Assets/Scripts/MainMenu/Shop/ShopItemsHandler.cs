using System.Collections.Generic;
using Player;
using UnityEngine;
using Zenject;

namespace MainMenu.Shop
{
    public class ShopItemsHandler : MonoBehaviour
    {
        [SerializeField] private List<ShopItemView> _buttons;
        
        private UpgradesHandler _upgradeHandler;
        private PlayerData _playerData;
        private Shop _shop;

        private void OnEnable()
        {
            UpdateShopItemsInfo();
            _shop.OnBuyUpgrade += UpdateShopItemsInfo;
        }

        private void OnDisable() => _shop.OnBuyUpgrade -= UpdateShopItemsInfo;

        private void UpdateShopItemsInfo()
        {
            foreach (var button in _buttons)
            {
                button.UpdatePrice(_upgradeHandler.Upgrades[(button.Tag, _playerData.GetUpgradeLevel(button.Tag))].Cost);
                button.UpdateLevel(_upgradeHandler.Upgrades[(button.Tag, _playerData.GetUpgradeLevel(button.Tag))].Level);
                button.IsLockCheck(_playerData.Balance >= _upgradeHandler.Upgrades[(button.Tag, 
                    _playerData.GetUpgradeLevel(button.Tag))].Cost && _playerData.PlatformGunLevel < 5);
            }
        }
        [Inject] private void Construct(PlayerData playerData, UpgradesHandler upgradesHandler, MainMenu.Shop.Shop shop)
        {
            _playerData = playerData;
            _upgradeHandler = upgradesHandler;
            _shop = shop;
        }
    }
}