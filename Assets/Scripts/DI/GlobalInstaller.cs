using SceneLoader;
using Zenject;

namespace DI
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLoader();
        }

        private void BindLoader()
        {
            Container.Bind<ZenjectSceneLoaderWrapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader.SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoadMediator>().AsSingle();
            
        }
    }
}