using Zenject;

namespace SceneLoader
{
    public class SceneLoadMediator: ISceneLoadMediator
    {
        private ISceneLoader _sceneLoader;

        public void GoToMainMenu() => _sceneLoader.Load(SceneID.MainMenu);
        
        public void EndGame(GameScoreData data) => _sceneLoader.Load(data);
        
        public void StartGame() => _sceneLoader.Load(SceneID.Game);
        
        [Inject] private void Construct(ISceneLoader sceneLoader) => _sceneLoader = sceneLoader;
    }
}