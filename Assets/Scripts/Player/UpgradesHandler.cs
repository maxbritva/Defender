using System.Collections.Generic;
using MainMenu.Shop;
using UnityEngine;
using Zenject;

namespace Player
{
    public class UpgradesHandler
    {
     public Dictionary<(string, int), ShopItem> Upgrades { get;}
        public UpgradesHandler()
        {
            Upgrades = new Dictionary<(string, int), ShopItem>();
            LoadUpgrades();
        }

        private PlayerData _playerData;

        public float GetCurrentUpgradeValue(string tag) => Upgrades[(tag, _playerData.GetUpgradeLevel(tag))].Value;

        private void LoadUpgrades()
       {
           for (int i = 0; i < 5; i++)
           {
               var shopItem = Resources.Load<ShopItem>($"Shop/PlatformGun/{i}");
               
               Upgrades.Add((shopItem.Tag, shopItem.Level),shopItem);
               
                shopItem = Resources.Load<ShopItem>($"Shop/Lives/{i}");
                Upgrades.Add((shopItem.Tag, shopItem.Level),shopItem);
               
                shopItem = Resources.Load<ShopItem>($"Shop/Shield/{i}");
                Upgrades.Add((shopItem.Tag, shopItem.Level),shopItem);
               
                shopItem = Resources.Load<ShopItem>($"Shop/ShootRate/{i}");
                Upgrades.Add((shopItem.Tag, shopItem.Level),shopItem);
               
                shopItem = Resources.Load<ShopItem>($"Shop/Damage/{i}");
                Upgrades.Add((shopItem.Tag, shopItem.Level),shopItem);
               
                shopItem = Resources.Load<ShopItem>($"Shop/Crit/{i}");
                Upgrades.Add((shopItem.Tag, shopItem.Level),shopItem);
           }
       }
        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}