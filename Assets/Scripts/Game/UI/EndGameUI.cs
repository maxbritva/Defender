using Audio;
using Game.FX;
using Game.GameCore.GameStates;
using Game.Score;
using SceneLoader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _gameplayPanel;
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private GameObject _topScoreImage;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private TMP_Text _scoreText;
        private EndGameAnimation _endGameAnimation;
        private ScoreCollector _scoreCollector;
        private GameManager _gameManager;
        private EndGame _endGame;
        private SceneLoadMediator _sceneLoadMediator;
        private AudioManager _audioManager;
        
        private void OnEnable()
        {
            _gameManager.OnGameEnded += ActivateEndGamePanel;
            _endGameAnimation.OnAnimationEnd += ActivateButtons;
            _exitButton.onClick.AddListener(ExitButtonClick);
            _playAgainButton.onClick.AddListener(PlayAgainButtonClick);
        }

        private void OnDisable()
        {
            _gameManager.OnGameEnded -= ActivateEndGamePanel;
            _endGameAnimation.OnAnimationEnd -= ActivateButtons;
            _exitButton.onClick.RemoveListener(ExitButtonClick);
            _playAgainButton.onClick.RemoveListener(PlayAgainButtonClick);
        }

        public void ShowTopScore() => _topScoreImage.SetActive(true);
        private void ActivateEndGamePanel()
        {
            _gameplayPanel.SetActive(false);
            _endGamePanel.SetActive(true);
            _endGame.Initialize();
            UpdateScoreText();
            _endGameAnimation.StartAnimation();
        }

        private void ActivateButtons()
        {
            _exitButton.interactable = true;
            _playAgainButton.interactable = true;
        }

        private void ExitButtonClick()
        {
            _sceneLoadMediator.GoToMainMenu();
            _audioManager.PlayMenuMusic();
        }

        private void PlayAgainButtonClick() => _sceneLoadMediator.StartGame();
        private void UpdateScoreText() => _scoreText.text = $"ОЧКИ ЗА ИГРУ: {_scoreCollector.CurrentScore}";

        [Inject] private void Construct(GameManager gameManager, EndGame endGame, EndGameAnimation endGameAnimation, 
            ScoreCollector scoreCollector, SceneLoadMediator sceneLoadMediator, AudioManager audioManager)
        {
            _audioManager = audioManager;
            _endGameAnimation = endGameAnimation;
            _gameManager = gameManager;
            _scoreCollector = scoreCollector;
            _endGame = endGame;
            _sceneLoadMediator = sceneLoadMediator;
        }
    }
}