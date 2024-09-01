using SceneLoader;
using Zenject;

namespace MainMenu.Menu
{
    public class MainMenuMediator
    {
        private ISceneLoadMediator _sceneLoader;
        public void StartGame() => _sceneLoader.StartGame();

        [Inject] private void Construct(ISceneLoadMediator loadMediator) => _sceneLoader = loadMediator;
    }
}