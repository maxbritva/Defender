using System;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.GameCore.GameProgression
{
    public class LevelSystem : MonoBehaviour, IActivatable
    {
        public Action OnLevelChanged;
        
        [SerializeField] private List<GameLevel> _levels = new List<GameLevel>();
        private GameTimer _gameTimer;

        private void OnEnable() => OnLevelChanged += LevelChange;
        private void OnDisable() => OnLevelChanged -= LevelChange;

        private void Start() => Activate();

        public void Activate() => _levels[_gameTimer.Minutes].Activate();

        public void Deactivate() => _levels[_gameTimer.Minutes].Deactivate();

        private void LevelChange()
        {
            _levels[_gameTimer.Minutes-1].Deactivate();
            Activate();
        }

       [Inject] private void Construct(GameTimer gameTimer) => _gameTimer = gameTimer;
    }
}