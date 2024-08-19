using SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        private ISceneLoadMediator _sceneLoader;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(StartGameClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(StartGameClick);
        }
        
        private void StartGameClick() => _sceneLoader.StartGame();
        

        [Inject] private void Construct(ISceneLoadMediator loadMediator) => _sceneLoader = loadMediator;
    }
}