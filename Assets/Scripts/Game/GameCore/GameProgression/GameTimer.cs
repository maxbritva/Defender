using System;
using System.Collections;
using Game.GameCore.Pause;
using Game.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.GameCore.GameProgression
{
    public class GameTimer : MonoBehaviour, IActivatable, IPause
    {
        public Action OnProgressionChanged;
        [SerializeField] private TMP_Text _timerText;
        private LevelSystem _levelSystem;
        private PauseHandler _pauseHandler;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerCoroutine;
        private int _progression = 0;
        private int _level = 0;
        private bool _isPaused;
        public int Level => _level;
        public int Progression => _progression;

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        public void Activate() => _timerCoroutine = StartCoroutine(TimerInGame());

        public void LevelUp()
        {
            _progression = 0;
            _level++;
            OnProgressionChanged?.Invoke();
            _levelSystem.OnLevelChanged?.Invoke();
        }

        public void Deactivate()
        {
          if(_timerCoroutine != null)
              StopCoroutine(_timerCoroutine);
        }
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private IEnumerator TimerInGame()
        {
            while (true)
            {
                if (_isPaused == false)
                {
                    _progression++;
                    OnProgressionChanged?.Invoke();
                    if (_progression >= 30) 
                        LevelUp();
                }
                yield return _tick;
            }
        }

        [Inject] private void Construct(LevelSystem levelSystem, PauseHandler pauseHandler)
        {
            _levelSystem = levelSystem;
            _pauseHandler = pauseHandler;
        }
    }
}