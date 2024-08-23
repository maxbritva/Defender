using MainMenu.Shop;
using MainMenu.UI;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace DI
{
    public class MainMenuInstaller: MonoInstaller
    {
        [FormerlySerializedAs("_availableButtonsChecker")] [SerializeField] private ShopItemsHandler shopItemsHandler;
        private PlayerData _playerData;
        public override void InstallBindings()
        {
            Container.BindInstance(new Wallet(_playerData.Balance));
            Container.Bind<ShopItemsHandler>().FromInstance(shopItemsHandler);
        }

        [Inject] private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}