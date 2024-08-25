using System.Collections.Generic;
using MainMenu.Shop;
using UnityEngine;
using Zenject;

namespace Player
{
    public class UpgradesHandler
    {
        private List<ShopItem> _platformGunUpgrades = new List<ShopItem>();
        private List<ShopItem> _livesCountLevelUpgrades = new List<ShopItem>();
        private List<ShopItem> _shieldTimerLevelUpgrades = new List<ShopItem>();
        private List<ShopItem> _shootRateLevelUpgrades = new List<ShopItem>();
        private List<ShopItem> _damageLevelUpgrades = new List<ShopItem>();
        private List<ShopItem> _critLevelUpgrades = new List<ShopItem>();
       
       [Inject] private PlayerData _playerData;
       
       public ShopItem PlatformCurrentLevel { get; private set; }
       public ShopItem LivesCurrentLevel { get; private set; }
       public ShopItem ShieldCurrentLevel { get; private set; }
       public ShopItem ShootRateCurrentLevel { get; private set; }
       public ShopItem DamageCurrentLevel { get; private set; }
       public ShopItem CritCurrentLevel { get; private set; }
       

       public void UpdateCurrentUpgrades()
       {
           PlatformCurrentLevel = _platformGunUpgrades[_playerData.PlatformGunLevel - 1];
           LivesCurrentLevel = _livesCountLevelUpgrades[_playerData.LivesCountLevel - 1];
           ShieldCurrentLevel = _shieldTimerLevelUpgrades[_playerData.ShieldTimerLevel - 1];
           ShootRateCurrentLevel = _shootRateLevelUpgrades[_playerData.ShootRateLevel - 1];
           DamageCurrentLevel = _damageLevelUpgrades[_playerData.DamageLevel - 1];
           CritCurrentLevel = _critLevelUpgrades[_playerData.CritLevel - 1];
       }

       public void LoadUpgrades()
       {
           for (int i = 0; i < 5; i++)
           {
               _platformGunUpgrades.Add(Resources.Load<ShopItem>($"Shop/PlatformGun/{i}"));
               _livesCountLevelUpgrades.Add(Resources.Load<ShopItem>($"Shop/Lives/{i}"));
               _shieldTimerLevelUpgrades.Add(Resources.Load<ShopItem>($"Shop/Shield/{i}"));
               _shootRateLevelUpgrades.Add(Resources.Load<ShopItem>($"Shop/ShootRate/{i}"));
               _damageLevelUpgrades.Add(Resources.Load<ShopItem>($"Shop/Damage/{i}"));
               _critLevelUpgrades.Add(Resources.Load<ShopItem>($"Shop/Crit/{i}"));
           }
       }
       [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}