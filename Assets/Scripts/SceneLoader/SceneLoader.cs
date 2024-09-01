
namespace SceneLoader
{
    public class SceneLoader: ISceneLoader
    {
        private ZenjectSceneLoaderWrapper _zenjectSceneLoader;
        
        public SceneLoader(ZenjectSceneLoaderWrapper zenjectSceneLoader) => 
            _zenjectSceneLoader = zenjectSceneLoader;

        public void Load(SceneID sceneID) => _zenjectSceneLoader.Load(null, (int)sceneID);
    }
}