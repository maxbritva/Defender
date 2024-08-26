using System;
using Newtonsoft.Json;

namespace Player
{
    [Serializable]
    public class PlayerData
    {
        public event Action<int> BalanceChanged;
        public int Balance { get; private set; }
        
        public int TopScore { get; private set; }
        public int PlatformGunLevel { get; private set; }
        public int LivesCountLevel { get; private set; }
        public int ShieldTimerLevel { get; private set; }
        public int ShootRateLevel { get; private set; }
        public int DamageLevel { get; private set; }
        public int CritLevel { get; private set; }
        
        public bool ShowTips { get; private set; }
        
        public PlayerData() { }
        
        [JsonConstructor] public PlayerData(int balance, int platformGunLevel, int livesCountLevel, int shieldTimerLevel, int shootRateLevel, int damageLevel, int critLevel, bool showTips, int topScore)
        {
            Balance = balance;
            TopScore = topScore;
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
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            Balance = value;
            BalanceChanged?.Invoke(Balance);
        }
        
        public void SetTopScore(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            TopScore = value;
        }

        public void AddBalance(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            Balance += value;
            BalanceChanged?.Invoke(Balance);
        }

        public void SpendBalance(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            Balance -= value;
            BalanceChanged?.Invoke(Balance);
        }
        public bool IsEnough(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins)); 
            return  Balance >= coins;
        }

        public void SetPlatformGunLevel(int value)
        {
            if (CheckValidValue(value))
                PlatformGunLevel = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public void SetLivesCountLevel(int value)
        {
            if (CheckValidValue(value))
                LivesCountLevel = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }
        public void SetShieldTimerLevel(int value)
        {
            if (CheckValidValue(value))
                ShieldTimerLevel = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public void SetShootRateLevel(int value)
        {
            if (CheckValidValue(value))
                ShootRateLevel = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }
        public void SetDamageLevelLevel(int value)
        {
            if (CheckValidValue(value))
                DamageLevel = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public void SetCritLevel(int value)
        {
            if (CheckValidValue(value))
                CritLevel = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

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
        
        private bool CheckValidValue(int value) => value >= 1 && value <= 5;

        public bool SetShowTips(bool value) => ShowTips = value;
    }
}