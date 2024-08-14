namespace Player
{
    public class PlayerData
    {
        private int _platformGunLevel = 1;
        private float _shieldTimer = 5f;
        public float ShieldTimer => _shieldTimer;

        public int PlatformGunLevel => _platformGunLevel;
        
        public void SetPlatformGunLevel(int value) => _platformGunLevel = value;
        public void SetShieldTimer(float value) => _shieldTimer = value;
    }
}