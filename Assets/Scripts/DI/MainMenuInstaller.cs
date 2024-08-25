using MainMenu;
using MainMenu.Shop;
using MainMenu.UI;
using Player;
using Save;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace DI
{
    public class MainMenuInstaller: MonoInstaller
    {
        [SerializeField] private ShopItemsHandler _shopItemsHandler;
        [SerializeField] private Shop _shop;
        [SerializeField] private BalanceView balanceView;
        public override void InstallBindings()
        {
            Container.Bind<Shop>().FromInstance(_shop);
            Container.Bind<BalanceView>().FromInstance(balanceView).AsSingle().NonLazy();
            Container.Bind<ShopItemsHandler>().FromInstance(_shopItemsHandler);
        }
        
    }
}