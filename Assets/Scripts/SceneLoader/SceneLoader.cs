using System;

namespace SceneLoader
{
    public class SceneLoader: ISceneLoader
    {
        private ZenjectSceneLoaderWrapper _zenjectSceneLoader;
        
        public SceneLoader(ZenjectSceneLoaderWrapper zenjectSceneLoader) => 
            _zenjectSceneLoader = zenjectSceneLoader;

        public void Load(SceneID sceneID)
        {
         if(sceneID == SceneID.EndGame)
             throw new ArgumentException($"{SceneID.EndGame} can not be started, need to configuration");
         _zenjectSceneLoader.Load(null, (int)sceneID);
        }

        public void Load(GameScoreData gameScoreData) => 
            _zenjectSceneLoader.Load(container => 
                container.BindInstance(gameScoreData), (int)SceneID.EndGame);
    }
}