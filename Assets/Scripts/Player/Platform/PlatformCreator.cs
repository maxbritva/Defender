using Game.Weapons;
using UnityEngine;
using Zenject;

namespace Player.Platform
{
    public class PlatformCreator: IFactory<GunMultiply>
    {
        private UpgradesHandler _upgradesHandler;
        private DiContainer _container;
        public GunMultiply _platform;

        public PlatformCreator() => _platform = Initialize();
     
        public GunMultiply Platform => _platform;
        private PlatformConfiguration _platformConfiguration;
        
        
        [Inject] private void Construct(UpgradesHandler upgradesHandler, DiContainer container, PlatformConfiguration platformConfiguration)
        {
            _upgradesHandler = upgradesHandler;
            _container = container;
            _platformConfiguration = platformConfiguration;
        }
      

        private GunMultiply Initialize()
        {
            return _platform = _container.InstantiatePrefabForComponent<GunMultiply>(_platformConfiguration.PrefabPlatforms[(int)_upgradesHandler.PlatformCurrentLevel.Value - 1], 
                _platformConfiguration.PlacerPlatform.position,
                _platformConfiguration.PlacerPlatform.rotation,
                _platformConfiguration.PlacerPlatform);
        }

        public GunMultiply Create()
        {
            return _container.InstantiatePrefabForComponent<GunMultiply>(_platformConfiguration.PrefabPlatforms
                    [(int)_upgradesHandler.PlatformCurrentLevel.Value - 1],
                _platformConfiguration.PlacerPlatform.position,
                _platformConfiguration.PlacerPlatform.rotation,
                _platformConfiguration.PlacerPlatform);
        }
    }
}