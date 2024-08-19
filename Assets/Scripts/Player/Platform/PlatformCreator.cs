using Game.Weapons;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformCreator: IFactory<GunMultiply>
    {
        private PlayerData _playerData;
        private DiContainer _container;
        public GunMultiply _platform;

        public PlatformCreator() => _platform = Initialize();
     
        public GunMultiply Platform => _platform;
        private PlatformConfiguration _platformConfiguration;
        
        
        [Inject] private void Construct(PlayerData playerData, DiContainer container, PlatformConfiguration platformConfiguration)
        {
            _playerData = playerData;
            _container = container;
            _platformConfiguration = platformConfiguration;
        }
      

        private GunMultiply Initialize()
        {
            return _platform = _container.InstantiatePrefabForComponent<GunMultiply>(_platformConfiguration.PrefabPlatforms[_playerData.PlatformGunLevel - 1], 
                _platformConfiguration.PlacerPlatform.position,
                _platformConfiguration.PlacerPlatform.rotation,
                _platformConfiguration.PlacerPlatform);
        }

        public GunMultiply Create()
        {
            return _container.InstantiatePrefabForComponent<GunMultiply>(_platformConfiguration.PrefabPlatforms
                    [_playerData.PlatformGunLevel - 1],
                _platformConfiguration.PlacerPlatform.position,
                _platformConfiguration.PlacerPlatform.rotation,
                _platformConfiguration.PlacerPlatform);
        }
    }
}