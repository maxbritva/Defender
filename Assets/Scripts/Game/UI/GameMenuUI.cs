using Game.GameCore.GameStates;
using Game.GameCore.Pause;
using SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class GameMenuUI: MonoBehaviour
    {
        [SerializeField] private Button _showMenuButton;
        [SerializeField] private Button _endGameButton;
        [SerializeField] private Button _resumeGameButton;
        [SerializeField] private GameObject _UIPanel;
        private PauseHandler _pauseHandler;
        private GameManager _gameManager;

        private void OnEnable()
        {
            _endGameButton.onClick.AddListener(EndGameClick);
            _resumeGameButton.onClick.AddListener(ResumeGameClick);
            _showMenuButton.onClick.AddListener(ShowMenuClick);
        }

        private void OnDisable()
        {
            _endGameButton.onClick.RemoveListener(EndGameClick);
            _resumeGameButton.onClick.RemoveListener(ResumeGameClick);
            _showMenuButton.onClick.RemoveListener(ShowMenuClick);
        }

        private void ResumeGameClick()
        {
            _UIPanel.SetActive(false);
            _pauseHandler.SetPause(false);
        }

        private void ShowMenuClick()
        {
            _UIPanel.SetActive(true);
            _pauseHandler.SetPause(true);
        }

        private void EndGameClick()
        {
            _UIPanel.SetActive(false);
            _gameManager.OnGameEnded?.Invoke();
        }

        [Inject] private void Construct(PauseHandler pauseHandler, GameManager gameManager)
        {
            _pauseHandler = pauseHandler;
            _gameManager = gameManager;
        }
    }
}