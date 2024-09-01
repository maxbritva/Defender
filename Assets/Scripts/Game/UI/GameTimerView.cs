using DG.Tweening;
using Game.GameCore.GameProgression;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class GameTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Image _progressBar;
        private LevelSystem _levelSystem;
        private GameTimer _gameTimer;
        private int _level;

        private void Start()
        {
            UpdateProgression();
            UpdateLevelText();
        }

        private void OnEnable()
        {
            _levelSystem.OnLevelChanged += UpdateLevelText;
            _gameTimer.OnProgressionChanged += UpdateProgression;
            _progressBar.transform.DOScaleX(_gameTimer.Progression / 30f, 0.5f);
        }

        private void OnDisable()
        {
            _levelSystem.OnLevelChanged -= UpdateLevelText;
            _gameTimer.OnProgressionChanged -= UpdateProgression;
        }

        private void UpdateLevelText()
        {
            _level = _gameTimer.Level + 1;
            _levelText.text = _level.ToString();
        }

        private void UpdateProgression() => 
            _progressBar.transform.DOScaleX(_gameTimer.Progression / 30f, 0.1f).SetEase(Ease.InOutSine);

        [Inject] private void Construct(LevelSystem levelSystem, GameTimer gameTimer)
        {
            _levelSystem = levelSystem;
            _gameTimer = gameTimer;
        }
    }
}