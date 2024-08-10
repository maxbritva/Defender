namespace Player
{
    public class PlayerData
    {
        private int _platformGunLevel = 3;
        public int PlatformGunLevel => _platformGunLevel;
        
        public void SetPlatformGunLevel(int value) => _platformGunLevel = value;
    }
}