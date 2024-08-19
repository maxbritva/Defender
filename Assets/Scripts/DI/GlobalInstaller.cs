using MainMenu.Shop;
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
            Container.BindInstance(new Wallet(100));
        }
    }
}