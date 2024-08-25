using MainMenu;
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
        public override void InstallBindings()
        {
            
            Container.BindInterfacesAndSelfTo<MainMenuManager>().FromNew().AsSingle().NonLazy();
          
            Container.Bind<Shop>().FromInstance(_shop);
            Container.Bind<Wallet>().FromNew().AsSingle().NonLazy();
            Container.Bind<ShopItemsHandler>().FromInstance(_shopItemsHandler);
        }
        
    }
}