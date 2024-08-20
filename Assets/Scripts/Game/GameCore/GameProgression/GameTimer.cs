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
        [SerializeField] private TMP_Text _timerText;
        private LevelSystem _levelSystem;
        private PauseHandler _pauseHandler;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerCoroutine;
        private int _seconds, _minutes;
        private bool _isPaused;
        public int Minutes => _minutes;

        private void Start()
        {
            Activate();
        }

        private void OnEnable() => _pauseHandler.Add(this);

        private void OnDisable() => _pauseHandler.Remove(this);

        public void Activate() => _timerCoroutine = StartCoroutine(TimerInGame());

        public void Deactivate()
        {
          if(_timerCoroutine != null)
              StopCoroutine(TimerInGame());
        }
        public void SetPause(bool isPaused) => _isPaused = isPaused;

        private IEnumerator TimerInGame()
        {
            while (true)
            {
                if (_isPaused == false)
                {
                    _seconds++;
                    if (_seconds >= 60)
                    {
                        _seconds = 0;
                        _minutes++;
                        _levelSystem.OnLevelChanged?.Invoke();
                        TimeFormat();
                    }
                }
                yield return _tick;
            }
        }

        private void TimeFormat()
        {
            _timerText.text = $"{_minutes}:{_seconds}";
            if(_seconds < 10 && _minutes <10)
                _timerText.text = $"0{_minutes}:0{_seconds}";
            else if(_seconds < 10) 
                _timerText.text = $"{_minutes}:0{_seconds}";
            else if(_minutes < 10) 
                _timerText.text = $"0{_minutes}:{_seconds}";
        }

        [Inject] private void Construct(LevelSystem levelSystem, PauseHandler pauseHandler)
        {
            _levelSystem = levelSystem;
            _pauseHandler = pauseHandler;
        }
    }
}