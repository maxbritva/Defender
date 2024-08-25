using MainMenu.Shop;
using Player;
using Save;
using SceneLoader;
using Zenject;

namespace DI
{
    public class GlobalInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            BindLoader();
            BindPlayerData();
        }

        private void BindLoader()
        {
            Container.Bind<ZenjectSceneLoaderWrapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader.SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoadMediator>().AsSingle();
        }

        private void BindPlayerData()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DataProvider>().FromNew().AsSingle().NonLazy();
            Container.Bind<UpgradesHandler>().FromNew().AsSingle().NonLazy();
        }
        
    }
}