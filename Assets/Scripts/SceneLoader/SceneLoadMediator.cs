﻿using Zenject;

namespace SceneLoader
{
    public class SceneLoadMediator: ISceneLoadMediator
    {
        private ISceneLoader _sceneLoader;
        public SceneLoadMediator(ISceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void GoToMainMenu() => _sceneLoader.Load(SceneID.MainMenu);

        public void StartGame() => _sceneLoader.Load(SceneID.Game);
        
    }
}