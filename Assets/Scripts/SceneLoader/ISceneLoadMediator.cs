namespace SceneLoader
{
    public interface ISceneLoadMediator
    {
        void GoToMainMenu();
        void StartGame();
        void EndGame(GameScoreData data);
    }
}