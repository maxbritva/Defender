using System;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.GameCore.GameProgression
{
    public class LevelSystem : MonoBehaviour, IActivatable
    {
        public Action OnLevelChanged;
        private LevelsHandler _levelsHandler;
        private GameTimer _gameTimer;

        private void OnEnable() => OnLevelChanged += LevelChange;
        private void OnDisable() => OnLevelChanged -= LevelChange;

        private void Start() => Activate();

        public void Activate() => _levelsHandler.Activate(_gameTimer.Level);

        public void Deactivate() => _levelsHandler.Deactivate();

        private void LevelChange()
        {
            Deactivate();
            Activate();
        }

       [Inject] private void Construct(GameTimer gameTimer, LevelsHandler levelsHandler)
       {
           _levelsHandler = levelsHandler;
           _gameTimer = gameTimer;
       }
    }
}