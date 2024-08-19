using System.Collections.Generic;
using Game.Interfaces;
using Game.Weapons;
using Player;
using Player.Platform;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private PlatformMovement _platformMovement;
        [SerializeField] private List<GameObject> _prefabPlatforms;
        [SerializeField] private Transform _placerPlatforms;
        [SerializeField] private PlatformConfiguration _platformConfiguration;
        private PlatformCreator _platformCreator;
        private PlayerData _playerData;
        private GunMultiply _gunMultiply;
        public override void InstallBindings()
        {
            Container.Bind<PlatformMovement>().FromInstance(_platformMovement).AsSingle().NonLazy();
            //Container.Bind<PlatformConfiguration>().FromInstance(_platformConfiguration).AsSingle().NonLazy();
            CreatePlatforms(_playerData.PlatformGunLevel);
        }

        private void CreatePlatforms(int level)
        {
           // Container.Bind<PlatformCreator>().FromNew().AsSingle().NonLazy();
           // _gunMultiply = _platformCreator.Platform;
          //  Container.Bind<IWeapon>().FromInstance(_gunMultiply);
            GunMultiply platform = Container.InstantiatePrefabForComponent<GunMultiply>(_prefabPlatforms[level - 1], 
                _placerPlatforms.position, _placerPlatforms.rotation, _placerPlatforms);
            Container.Bind<IWeapon>().FromInstance(platform).AsSingle();
         
           //  Container.Bind<IWeapon>().FromNewComponentOnNewPrefab(_prefabPlatforms[level - 1]); // префаб принимает и все
        }

        [Inject] private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
           
        }
    }
}