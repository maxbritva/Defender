namespace SceneLoader
{
    public interface ISceneLoader
    {
        void Load(SceneID sceneID);
        void Load(GameScoreData gameScoreData);
    }
}