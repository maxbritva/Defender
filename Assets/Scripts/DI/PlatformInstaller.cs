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
        private PlayerData _playerData;
        public override void InstallBindings()
        {
            Container.Bind<PlatformMovement>().FromInstance(_platformMovement).AsSingle().NonLazy();
            CreatePlatforms(_playerData.PlatformGunLevel);
        }
        private void CreatePlatforms(int level)
        {
            GunMultiply platform = Container.InstantiatePrefabForComponent<GunMultiply>(_prefabPlatforms[level - 1], 
                _placerPlatforms.position, _placerPlatforms.rotation, _placerPlatforms);
            Container.Bind<GunMultiply>().FromInstance(platform).AsSingle();
        }
        [Inject] private void Construct(PlayerData playerData) => _playerData = playerData;
    }
}