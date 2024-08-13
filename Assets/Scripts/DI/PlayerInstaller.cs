using Game.Health;
using Player;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHealth _playerHealth;
        public override void InstallBindings()
        {
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth);
        }
    }
}