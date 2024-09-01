using MainMenu.Menu;
using MainMenu.Settings;
using MainMenu.Shop;
using MainMenu.UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class MainMenuInstaller: MonoInstaller
    {
        [SerializeField] private ShopItemsHandler _shopItemsHandler;
        [SerializeField] private Shop _shop;
        [SerializeField] private BalanceView balanceView;
        [SerializeField] private MainMenuUI _mainMenuUI;
        public override void InstallBindings()
        {
            Container.Bind<Shop>().FromInstance(_shop);
            Container.Bind<BalanceView>().FromInstance(balanceView).AsSingle().NonLazy();
            Container.Bind<ShopItemsHandler>().FromInstance(_shopItemsHandler);
            Container.Bind<MainMenuUI>().FromInstance(_mainMenuUI);
            Container.Bind<MainMenuMediator>().FromNew().AsSingle().NonLazy();
            Container.Bind<SettingsMediator>().FromNew().AsSingle().NonLazy();
        }
        
    }
}