using System;
using Newtonsoft.Json;

namespace Player
{
    
    public class PlayerData
    {
        public int Balance { get; private set; }
        public int PlatformGunLevel { get; private set; }
        public int LivesCountLevel { get; private set; }
        public int ShieldTimerLevel { get; private set; }
        public int ShootRateLevel { get; private set; }
        public int DamageLevel { get; private set; }
        public int CritLevel { get; private set; }
        
        public bool ShowTips { get; private set; }
        
        public PlayerData()
        {
            Balance = 10000;
            PlatformGunLevel = 1;
            LivesCountLevel = 1;
            ShieldTimerLevel = 1;
            ShootRateLevel = 1;
            DamageLevel = 1;
            CritLevel = 1;
            ShowTips = true;
        }
        
        [JsonConstructor] 
        public PlayerData(int balance, int platformGunLevel, int livesCountLevel, int shieldTimerLevel, int shootRateLevel, int damageLevel, int critLevel, bool showTips)
        {
            Balance = balance;
            PlatformGunLevel = platformGunLevel;
            LivesCountLevel = livesCountLevel;
            ShieldTimerLevel = shieldTimerLevel;
            ShootRateLevel = shootRateLevel;
            DamageLevel = damageLevel;
            CritLevel = critLevel;
            ShowTips = showTips;
        }

        public void SetBalance(int value)
        {
            if(value >= 0)
                Balance = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }
        public void SetPlatformGunLevel(int value) => CheckInvalidValue(value, PlatformGunLevel);
        public void SetLivesCountLevel(int value) => CheckInvalidValue(value, LivesCountLevel);
        public void SetShieldTimerLevel(int value) => CheckInvalidValue(value, ShieldTimerLevel);
        public void SetShootRateLevel(int value) => CheckInvalidValue(value, ShootRateLevel);
        public void SetDamageLevelLevel(int value) => CheckInvalidValue(value, DamageLevel);
        public void SetCritLevel(int value) => CheckInvalidValue(value, CritLevel);

        public void ResetData()
        {
            Balance = 0;
            PlatformGunLevel = 1;
            LivesCountLevel = 1;
            ShieldTimerLevel = 1;
            ShootRateLevel = 1;
            DamageLevel = 1;
            CritLevel = 1;
            ShowTips = true;
        }
        
        private void CheckInvalidValue(int value, int targetToUpgrade)
        {
            if(value >= 1 && value <= 5)
                targetToUpgrade = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public bool SetShowTips(bool value) => ShowTips = value;
    }
}