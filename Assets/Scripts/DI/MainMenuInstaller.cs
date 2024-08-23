using MainMenu.Shop;
using MainMenu.UI;
using Player;
using Save;
using UnityEngine;
using Zenject;

namespace DI
{
    public class MainMenuInstaller: MonoInstaller
    {
        [SerializeField] private ShopItemsHandler _shopItemsHandler;
        [SerializeField] private Shop _shop;
        private PlayerData _playerData;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DataProvider>().FromNew().AsSingle().NonLazy();
            Container.Bind<UpgradesHandler>().FromNew().AsSingle().NonLazy();
            Container.BindInstance(new Wallet(_playerData.Balance));
            Container.Bind<Shop>().FromInstance(_shop);
            Container.Bind<ShopItemsHandler>().FromInstance(_shopItemsHandler);
        }

        [Inject] private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}