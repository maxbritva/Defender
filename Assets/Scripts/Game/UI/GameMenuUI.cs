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
        private ISceneLoadMediator _sceneLoader;

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

        private void ResumeGameClick() => _UIPanel.SetActive(false);
        private void ShowMenuClick() => _UIPanel.SetActive(true);

        private void EndGameClick() => _sceneLoader.GoToMainMenu();

        [Inject] private void Construct(ISceneLoadMediator loadMediator) => _sceneLoader = loadMediator;
    }
}