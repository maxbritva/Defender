using System;
using Game.Bonus;
using Game.GameCore.GameProgression;
using Game.GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Game.GameCore.GameStates
{
    public class GameManager : MonoBehaviour
    {
        public Action OnGameStarted;
        public Action OnGameEnded;
        private PauseHandler _pauseHandler;
        private GameTimer _gameTimer;
        private LevelSystem _levelSystem;
        private BonusSpawner _bonusSpawner;
        private void Start() => _pauseHandler.SetPause(true);

        private void OnEnable()
        {
            OnGameStarted += StartGameplay;
            OnGameEnded += EndGame;
        }

        private void OnDisable()
        {
            OnGameStarted -= StartGameplay;
            OnGameEnded -= EndGame;
        }

        [Inject] private void Construct(PauseHandler pauseHandler, GameTimer gameTimer, LevelSystem levelSystem, BonusSpawner bonusSpawner)
        {
            _bonusSpawner = bonusSpawner;
            _levelSystem = levelSystem;
            _gameTimer = gameTimer;
            _pauseHandler = pauseHandler;
        }

        private void StartGameplay()
        {
            _pauseHandler.SetPause(false);
            _gameTimer.Activate();
            _levelSystem.Activate();
            _bonusSpawner.Activate();
        }

        private void EndGame() => _pauseHandler.SetPause(true);
        
    }
}