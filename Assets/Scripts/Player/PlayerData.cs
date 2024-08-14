namespace Player
{
    public class PlayerData
    {
        private int _platformGunLevel = 1;
        public int PlatformGunLevel => _platformGunLevel;
        
        public void SetPlatformGunLevel(int value) => _platformGunLevel = value;
    }
}