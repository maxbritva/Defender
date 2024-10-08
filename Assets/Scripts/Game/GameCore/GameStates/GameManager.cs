﻿using System;
using Audio;
using Game.Bonus;
using Game.Enemy.Boss;
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

        public Action OnBossLevelStarted;
        public Action OnBossLevelEnded;
        private PauseHandler _pauseHandler;
        private GameTimer _gameTimer;
        private LevelSystem _levelSystem;
        private BonusSpawner _bonusSpawner;
        private AudioManager _audioManager;
        private BossSpawner _bossSpawner;
        private void Start() => _pauseHandler.SetPause(true);

        private void OnEnable()
        {
            OnGameStarted += StartGameplay;
            _audioManager.PlayGameMusic();
            OnGameEnded += EndGame;
            OnBossLevelStarted += BossLevelStart;
            OnBossLevelEnded += BossLevelEnd;
        }

        private void OnDisable()
        {
            OnGameStarted -= StartGameplay;
            OnGameEnded -= EndGame;
            OnBossLevelStarted -= BossLevelStart;
            OnBossLevelEnded -= BossLevelEnd;
        }

        private void StartGameplay()
        {
            _pauseHandler.SetPause(false);
            _gameTimer.Activate();
            _levelSystem.Activate();
            _bonusSpawner.Activate();
        }

        private void BossLevelStart()
        {
            _gameTimer.Deactivate();
            _bonusSpawner.Deactivate();
            _bonusSpawner.HideAllBonuses();
        }
        
        private void BossLevelEnd()
        {
            _bossSpawner.Deactivate();
            _bossSpawner.StopSpawnMinions();
            _gameTimer.Activate();
            _bonusSpawner.Activate();
            _gameTimer.LevelUp();
        }

        private void EndGame()
        {
            _gameTimer.Deactivate();
            _levelSystem.Deactivate();
            _bonusSpawner.Deactivate();
            _pauseHandler.SetPause(true);
        }
        
        [Inject] private void Construct(PauseHandler pauseHandler, GameTimer gameTimer, LevelSystem levelSystem, BonusSpawner bonusSpawner, 
            AudioManager audioManager, BossSpawner bossSpawner)
        {
            _audioManager = audioManager;
            _bonusSpawner = bonusSpawner;
            _levelSystem = levelSystem;
            _gameTimer = gameTimer;
            _pauseHandler = pauseHandler;
            _bossSpawner = bossSpawner;
        }
    }
}