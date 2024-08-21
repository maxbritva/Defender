using Game.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ScoreUIUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        private ScoreCollector _scoreCollector;

        private void OnEnable() => _scoreCollector.OnScoreChanged += UpdateScoreText;
        private void OnDisable() => _scoreCollector.OnScoreChanged -= UpdateScoreText;
        private void Start() => UpdateScoreText();
        private void UpdateScoreText() => _scoreText.text = $"ОЧКИ: {_scoreCollector.CurrentScore}";

        [Inject] private void Construct(ScoreCollector scoreCollector) => _scoreCollector = scoreCollector;
    }
}