namespace Player
{
    public interface IPlayerData
    {
        int GetBalance();
        void SetPlatformGunLevel(int value); 
        void SetLivesCountLevel(int value); 
        void SetShieldTimerLevel(int value); 
        void SetShootRateLevel(int value); 
        void SetDamageLevelLevel(int value); 
        void SetCritLevel(int value); 
        void SetBalance(int value);
        bool SetShowTips(bool value);
        void SetTopScore(int value);
    }
}